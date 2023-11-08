// 文件：C2G_LoginGameGateHandler.cs
// 作者：xj
// 描述：
// 日期：2023/11/08 17:47

using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.SessionPlayerComponent))]
    public class C2G_LoginGameGateHandler : AMRpcHandler<C2G_LoginGameGate, G2C_LoginGameGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response, Action reply)
        {
            Scene scene = session.DomainScene();
            if (scene.SceneType != SceneType.Gate)
            {
                Log.Error($"请求Scene错误,当前Scene为:{session.DomainScene().SceneType} ,非 SceneType.Gate");
                session.Dispose();
                return;
            }

            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.Err_RequestRepeatedly;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            string token = scene.GetComponent<GateSessionKeyComponent>().Get(request.AccountId);
            if (token == null || token != request.Key)
            {
                response.Error = ErrorCode.Err_ConnectGateKeyError;
                response.Message = "Gate Key验证失败";
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            scene.GetComponent<GateSessionKeyComponent>().Remove(request.AccountId);

            long sessionId = session.InstanceId;
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, request.AccountId.GetHashCode()))
                {
                    //多个客户端异步操作可能不等于
                    if (sessionId != session.InstanceId)
                    {
                        return;
                    }

                    StartSceneConfig loginCenterConfig = StartSceneConfigCategory.Instance.LoginCenterConfig;
                    L2G_AddLoginRecord result = (L2G_AddLoginRecord)await MessageHelper.CallActor(loginCenterConfig.InstanceId, new G2L_AddLoginRecord()
                    {
                        AccountId = request.AccountId,
                        ServerId = session.DomainScene().Zone
                    });



                    if (result.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = result.Error;
                        reply();
                        session.Disconnect().Coroutine();
                        return;
                    }

                    Player player = scene.GetComponent<PlayerComponent>().Get(request.RoleId);

                    if (player == null)
                    {
                        //添加一个新的GateUnit
                        player = session.DomainScene().GetComponent<PlayerComponent>()
                                .AddChildWithId<Player, long, long>(request.RoleId, request.AccountId, request.RoleId);
                        player.PlayerState = PlayerState.Gate;
                        scene.GetComponent<PlayerComponent>().Add(player);
                        session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
                    }
                    else
                    {
                        //player.RemoveComponent<>();
                    }

                    session.AddComponent<SessionPlayerComponent>().PlayerId = player.Id;
                    session.GetComponent<SessionPlayerComponent>().PlayerInstanceId = player.InstanceId;
                    player.SessionInstanceId = session.InstanceId;
                }
            }

            reply();
        }
    }
}