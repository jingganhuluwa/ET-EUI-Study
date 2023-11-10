// 文件：UnitCacheHelper.cs
// 作者：xj
// 描述：
// 日期：2023/11/10 3:15

using System;

namespace ET
{
    public static class UnitCacheHelper
    {
        /// <summary>
        /// 保存或更新玩家缓存
        /// </summary>
        public static async ETTask AddOrUpdateUnitCache<T>(this T self) where T : Entity, IUnitCache
        {
            Other2UnitCache_AddOrUpdateUnit message = new Other2UnitCache_AddOrUpdateUnit() { UnitId = self.Id };
            message.EntityType.Add(typeof (T).Name);
            message.EntityBytes.Add(MongoHelper.ToBson(self));
            long instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(self.Id).InstanceId;
            await MessageHelper.CallActor(instanceId, message);
        }

        public static async ETTask<Unit> GetUnitCache(Scene scene, long unitId)
        {
            long instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId;
            Other2UnitCache_GetUnit message = new Other2UnitCache_GetUnit() { UnitId = unitId };
            UnitCache2Other_GetUnit result = (UnitCache2Other_GetUnit)await MessageHelper.CallActor(instanceId, message);
            if (result.Error!=ErrorCode.ERR_Success || result.EntityList.Count<=0)
            {
                return null;
            }

            int indexOf = result.ComponentNameList.IndexOf(nameof (Unit));
            Unit unit = result.EntityList[indexOf] as Unit;
            if (unit == null)
            {
                return null;
            }

            
            scene.GetComponent<UnitComponent>().AddChild(unit);
            foreach (Entity entity in result.EntityList)
            {
                if (entity==null || entity is Unit)
                {
                    continue;
                }

                unit.AddComponent(entity);
            }

            return unit;
        }
        
        
        /// <summary>
        /// 获取玩家组件缓存
        /// </summary>
        public static async ETTask<T> GetUnitComponentCache<T>(long unitId) where T : Entity, IUnitCache
        {
            Other2UnitCache_GetUnit message = new Other2UnitCache_GetUnit() { UnitId = unitId };
            message.ComponentNameList.Add(typeof (T).Name);
            long instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId;
            UnitCache2Other_GetUnit result = (UnitCache2Other_GetUnit) await MessageHelper.CallActor(instanceId, message);
            if (result.Error == ErrorCode.ERR_Success && result.EntityList.Count > 0)
            {
                return result.EntityList[0] as T;
            }

            return null;
        }

        /// <summary>
        /// 删除玩家缓存
        /// </summary>
        /// <param name="unitId"></param>
        public static async ETTask DeleteUnitCache(long unitId)
        {
            Other2UnitCache_DeleteUnit message = new Other2UnitCache_DeleteUnit() { UnitId = unitId };
            long instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId;
            await MessageHelper.CallActor(instanceId, message);
        }

        public static void AddOrUpdateUnitAllCache(Unit unit)
        {
            Other2UnitCache_AddOrUpdateUnit message = new Other2UnitCache_AddOrUpdateUnit(){UnitId = unit.Id};
            long instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unit.Id).InstanceId;
            message.EntityType.Add(unit.GetType().FullName);
            message.EntityBytes.Add(MongoHelper.ToBson(unit));
            
            foreach ((Type key, Entity entity) in unit.Components)
            {
                if (!typeof(IUnitCache).IsAssignableFrom(key))
                {
                    continue;
                }
                message.EntityType.Add(key.FullName);
                message.EntityBytes.Add(MongoHelper.ToBson(entity));
            }
            
            MessageHelper.CallActor(instanceId,message).Coroutine();
        }
    }
}