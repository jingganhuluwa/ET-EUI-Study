// 文件：C2A_GetServerInfosHandler.cs
// 作者：xj
// 描述：
// 日期：2023/08/27 9:46

using System;

namespace ET
{
    [FriendClass(typeof(ServerInfoManagerComponent))]
    public class C2A_GetServerInfosHandler : AMRpcHandler<C2A_GetServerInfos, A2C_GetServerInfos>
    {
        protected override async ETTask Run(Session session, C2A_GetServerInfos request, A2C_GetServerInfos response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求Scene错误,当前Scene为:{session.DomainScene().SceneType} ,非 SceneType.Account");
                session.Dispose();
                return;
            }

            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session?.Disconnect().Coroutine();
                return;
            }

            foreach (ServerInfo serverInfo in session.DomainScene().GetComponent<ServerInfoManagerComponent>().ServerInfoList)
            {
                 response.ServerInfoList.Add(serverInfo.ToMessage());
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}