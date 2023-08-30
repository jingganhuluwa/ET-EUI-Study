// 文件：C2A_CreateRoleHandler.cs
// 作者：xj
// 描述：
// 日期：2023/08/30 9:19

using System;
using System.Collections.Generic;

namespace ET
{
    public class C2A_CreateRoleHandler: AMRpcHandler<C2A_CreateRole, A2C_CreateRole>
    {
        protected override async ETTask Run(Session session, C2A_CreateRole request, A2C_CreateRole response, Action reply)
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
                session?.Disconnect().Coroutine();
                return;
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.Err_RoleNameIsNull;
                reply();
                return;
            }
            //todo 角色名长度判定和敏感词以及特殊符号过滤

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRole, request.AccountId))
                {
                    List<RoleInfo> roleInfos =
                            await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Query<RoleInfo>(d => d.Name == request.Name);
                    if (roleInfos != null && roleInfos.Count > 0)
                    {
                        //同名
                        response.Error = ErrorCode.Err_RoleNameIsExist;
                        reply();
                        return;
                    }

                    RoleInfo newRoleInfo = session.AddChildWithId<RoleInfo>(IdGenerater.Instance.GenerateUnitId(request.ServerId));
                    newRoleInfo.Name = request.Name;
                    newRoleInfo.AccountId = request.AccountId;
                    newRoleInfo.State = (int) RoleInfoState.Normal;
                    newRoleInfo.ServerId = request.ServerId;
                    newRoleInfo.CreateTime = TimeHelper.ServerNow();
                    newRoleInfo.LastLoginTime = 0;

                    await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save(newRoleInfo);

                    response.RoleInfo = newRoleInfo.ToMessage();
                    reply();

                    newRoleInfo?.Dispose();
                }
            }

            await ETTask.CompletedTask;
        }
    }
}