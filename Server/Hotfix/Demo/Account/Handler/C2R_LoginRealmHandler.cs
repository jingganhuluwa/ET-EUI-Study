// 文件：C2RLoginRealmHandler.cs
// 作者：xj
// 描述：
// 日期：2023/11/07 19:33

using System;

namespace ET
{
    public class C2R_LoginRealmHandler: AMRpcHandler<C2R_LoginRealm, R2C_LoginRealm>
    {
        protected override async ETTask Run(Session session, C2R_LoginRealm request, R2C_LoginRealm response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Realm)
            {
                Log.Error($"请求Scene错误,当前Scene为:{session.DomainScene().SceneType} ,非 SceneType.Realm");
                session.Dispose();
                return;
            }

            Scene domainScene = session.DomainScene();

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || token != request.RealmTokenKey)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            domainScene.GetComponent<TokenComponent>().Remove(request.AccountId);

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginRealm, request.AccountId))
                {
                    //获取固定分配的一个Gate
                    StartSceneConfig config = RealmGateAddressHelper.GetGate(domainScene.Zone, request.AccountId);

                    //向Gate请求一个Key,客户端用Key连接进Gate
                    G2R_GetLoginGateKey result = (G2R_GetLoginGateKey) await MessageHelper.CallActor(config.InstanceId,
                        new R2G_GetLoginGateKey() { AccountId = request.AccountId });
                    

                    if (result.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = result.Error;
                        reply();
                        return;
                    }

                    response.GateSessionKey = result.GateSessionKey;
                    response.GateAddress = config.OuterIPPort.ToString();
                    reply();
                    
                    session.Disconnect().Coroutine();
                }
            }
        }
    }
}