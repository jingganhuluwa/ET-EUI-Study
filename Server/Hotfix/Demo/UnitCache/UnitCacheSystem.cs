// 文件：UnitCacheSystem.cs
// 作者：xj
// 描述：
// 日期：2023/11/10 4:07

using System.Collections.Generic;

namespace ET
{
    public class UnitCacheDestroySystem:DestroySystem<UnitCache>
    {
        public override void Destroy(UnitCache self)
        {
            foreach (Entity entity in self.CacheComponentDict.Values)
            {
                entity?.Dispose();
            }
            self.CacheComponentDict.Clear();
            self.Key = null;
        }
    }

    public static class UnitCacheSystem
    {
        public static void AddOrUpdate(this UnitCache self, Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (self.CacheComponentDict.TryGetValue(entity.Id, out Entity oldEntity))
            {
                if (entity != oldEntity)
                {
                    oldEntity.Dispose();
                }

                self.CacheComponentDict.Remove(entity.Id);
            }
            
            self.CacheComponentDict.Add(entity.Id,entity);
        }

        public static async ETTask<Entity> Get(this UnitCache self, long unitId)
        {
            Entity entity = null;
            if (!self.CacheComponentDict.TryGetValue(unitId,out entity))
            {
                entity = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                        .Query<Entity>(unitId, self.Key);
                if (entity!=null)
                {
                    self.AddOrUpdate(entity);
                }
            }

            return entity;
        }

        public static void Delete(this UnitCache self, long unitId)
        {
            if (self.CacheComponentDict.TryGetValue(unitId,out Entity entity))
            {
                entity.Dispose();
                self.CacheComponentDict.Remove(unitId);
            }
        }
    }
}