// 文件：Other2UnitCache_AddOrUpdateUnitHandler.cs
// 作者：xj
// 描述：
// 日期：2023/11/10 3:47

using System;
using System.Collections.Generic;

namespace ET
{
    public class Other2UnitCache_AddOrUpdateUnitHandler: AMActorRpcHandler<Scene, Other2UnitCache_AddOrUpdateUnit, UnitCache2Other_AddOrUpdateUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_AddOrUpdateUnit request, UnitCache2Other_AddOrUpdateUnit response,
        Action reply)
        {
            UpdateUnitCacheAsync(scene, request, response).Coroutine();
            reply();
            await ETTask.CompletedTask;
        }

        private async ETTask UpdateUnitCacheAsync(Scene scene, Other2UnitCache_AddOrUpdateUnit request, UnitCache2Other_AddOrUpdateUnit response)
        {
            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            using (ListComponent<Entity> entityList=ListComponent<Entity>.Create())
            {
                for (var index = 0; index < request.EntityTypes.Count; index++)
                {
                    Type type = Game.EventSystem.GetType(request.EntityTypes[index]);
                    Entity entity = MongoHelper.FromBson(type,request.EntityBytes[index]) as Entity;
                    entityList.Add(entity);
                }

                await unitCacheComponent.AddOrUpdate(request.UnitId, entityList);
            }
        }
    }
}