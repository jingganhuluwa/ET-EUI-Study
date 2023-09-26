// 文件：C2A_GetRelamKeyHandler.cs
// 作者：xj
// 描述：
// 日期：2023/09/26 22:17

using System;

namespace ET
{
    public class C2A_GetRealmKeyHandler:AMRpcHandler<C2A_GetRealmKey,A2C_GetRealmKey>
    {
        protected override async ETTask Run(Session session, C2A_GetRealmKey request, A2C_GetRealmKey response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求Scene错误,当前Scene为:{session.DomainScene().SceneType} ,非 SceneType.Account");
                session.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.Err_RequestRepeatedly;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.Err_TokenError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.AccountId))
                {
                    StartSceneConfig realmStartSceneConfig =RealmGateAddressHelper.GetRealm(request.ServerId);
                    R2A_GetRealmKey result = null;
                    try
                    {
                        result = (R2A_GetRealmKey)await MessageHelper.CallActor(realmStartSceneConfig.InstanceId, new A2R_GetRealmKey()
                        {
                            AccountId = request.AccountId
                        });
                    }
                    catch (Exception e)
                    {
                        response.Error = ErrorCode.Err_NetWorkError;
                        reply();
                        session.Disconnect().Coroutine();
                        Log.Error(e);
                        return ;
                    }

                    if (result.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = result.Error;
                        reply();
                        session.Disconnect().Coroutine();
                        return ;
                    }


                    response.RealmKey = result.RealmKey;
                    response.RealmAddress = realmStartSceneConfig.OuterIPPort.ToString();
                    reply();
                    session.Disconnect().Coroutine();

                }
            }
        }
    }
}