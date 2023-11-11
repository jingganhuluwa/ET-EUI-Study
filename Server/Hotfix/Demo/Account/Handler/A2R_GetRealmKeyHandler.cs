// 文件：A2R_GetRealmKeyHandler.cs
// 作者：xj
// 描述：
// 日期：2023/11/07 18:59

using System;

namespace ET
{
    public class A2R_GetRealmKeyHandler:AMActorRpcHandler<Scene,A2R_GetRealmKey,R2A_GetRealmKey>
    {
        protected override async ETTask Run(Scene session, A2R_GetRealmKey request, R2A_GetRealmKey response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Realm)
            {
                Log.Error($"请求Scene错误,当前Scene为:{session.DomainScene().SceneType} ,非 SceneType.Realm");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                return;
            }

            string key = TimeHelper.ServerNow().ToString() + RandomHelper.RandInt64().ToString();
            session.GetComponent<TokenComponent>().Remove(request.AccountId);
            session.GetComponent<TokenComponent>().Add(request.AccountId,key);
            response.RealmKey = key;
            reply();
            await ETTask.CompletedTask;
        }
    }
}