// 文件：C2A_GetRolesHandler.cs
// 作者：xj
// 描述：
// 日期：2023/09/24 23:57

using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof (RoleInfo))]
    public class C2A_GetRolesHandler: AMRpcHandler<C2A_GetRoles, A2C_GetRoles>
    {
        protected override async ETTask Run(Session session, C2A_GetRoles request, A2C_GetRoles response, Action reply)
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
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRole, request.AccountId))
                {
                    List<RoleInfo> roleInfos = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                            .Query<RoleInfo>(d => d.AccountId == request.AccountId && d.ServerId == request.ServerId && d.State == (int) RoleInfoState.Normal);

                    if (roleInfos == null || roleInfos.Count == 0)
                    {
                        reply();
                        return;
                    }

                    foreach (RoleInfo roleInfo in roleInfos)
                    {
                        response.RoleInfoList.Add(roleInfo.ToMessage());
                        roleInfo?.Dispose();
                    }

                    roleInfos.Clear();

                    reply();
                }
            }
        }
    }
}