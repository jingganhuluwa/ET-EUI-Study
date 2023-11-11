// 文件：C2A_DeleteRoleHandler.cs
// 作者：xj
// 描述：
// 日期：2023/09/25 20:29

using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    public class C2A_DeleteRoleHandler : AMRpcHandler<C2A_DeleteRole, A2C_DeleteRole>
    {
        protected override async ETTask Run(Session session, C2A_DeleteRole request, A2C_DeleteRole response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求Scene错误,当前Scene为:{session.DomainScene().SceneType} ,非 SceneType.Account");
                session.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRole, request.AccountId))
                {
                    List<RoleInfo> roleInfos = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                            .Query<RoleInfo>(d => d.Id == request.RoleInfoId && d.ServerId == request.ServerId);

                    if (roleInfos==null || roleInfos.Count==0)
                    {
                        response.Error = ErrorCode.ERR_RoleNoExist;
                        reply();
                        return;
                    }

                    RoleInfo roleInfo = roleInfos[0];
                    session.AddChild(roleInfo);
                    //冻结
                    roleInfo.State = (int) RoleInfoState.Freeze;

                    await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save(roleInfo);
                    response.DeleteRoleInfoId = roleInfo.Id;
                    roleInfo.Dispose();

                    reply();
                }
            }
        }
    }
}