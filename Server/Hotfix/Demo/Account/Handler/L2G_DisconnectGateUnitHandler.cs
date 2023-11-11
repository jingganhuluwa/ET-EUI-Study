// 文件：L2G_DisconnectGateUnitHandler.cs
// 作者：xj
// 描述：
// 日期：2023/08/14 16:19

using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.SessionPlayerComponent))]
    public class L2G_DisconnectGateUnitHandler : AMActorRpcHandler<Scene, L2G_DisconnectGateUnit, G2L_DisconnectGateUnit>
    {
        protected override async ETTask Run(Scene scene, L2G_DisconnectGateUnit request, G2L_DisconnectGateUnit response, Action reply)
        {
            long accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, accountId.GetHashCode()))
            {
                PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
                Player player = playerComponent.Get(accountId);

                if (player == null)
                {
                    reply();
                    return;
                }

                scene.GetComponent<GateSessionKeyComponent>().Remove(accountId);
                Session gateSession = Game.EventSystem.Get(player.ClientSession.InstanceId) as Session;
                if (gateSession != null && !gateSession.IsDisposed)
                {
                    if (gateSession.GetComponent<SessionPlayerComponent>() != null)
                    {
                        gateSession.GetComponent<SessionPlayerComponent>().IsLoginAgain = true;
                    }
                    gateSession.Send(new A2C_Disconnect() { Error = ErrorCode.ERR_OtherAccountLogin });
                    gateSession?.Disconnect().Coroutine();
                }

                player.ClientSession = null;
                player.AddComponent<PlayerOfflineOutTimeComponent>();

            }

            reply();
        }
    }
}