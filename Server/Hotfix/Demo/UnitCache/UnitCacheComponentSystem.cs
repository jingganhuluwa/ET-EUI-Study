// 文件：UnitCacheComponentSystem.cs
// 作者：xj
// 描述：
// 日期：2023/11/10 3:59

using System;
using System.Collections.Generic;

namespace ET
{
    [ObjectSystem]
    public class UnitCacheComponentAwakeSystem:AwakeSystem<UnitCacheComponent>
    {
        public override void Awake(UnitCacheComponent self)
        {
            self.UnitCacheKeyList.Clear();
            foreach (Type type in Game.EventSystem.GetTypes().Values)
            {
                if (type!=typeof(IUnitCache) && typeof(IUnitCache).IsAssignableFrom(type))
                {
                    self.UnitCacheKeyList.Add(type.Name);
                }
            }
            
            foreach (string key in self.UnitCacheKeyList)
            {
                UnitCache unitCache = self.AddChild<UnitCache>();
                unitCache.Key = key;
                self.UnitCacheDict.Add(key,unitCache);
            }
        }
    }
    
    [ObjectSystem]
    public class UnitCacheComponentDestroySystem:DestroySystem<UnitCacheComponent>
    {
        public override void Destroy(UnitCacheComponent self)
        {
            foreach (UnitCache unitCache in self.UnitCacheDict.Values)
            {
                unitCache?.Dispose();
            }
            
            self.UnitCacheDict.Clear();
        }
    }

    public static class UnitCacheComponentSystem
    {
        public static async ETTask AddOrUpdate(this UnitCacheComponent self,long id, ListComponent<Entity> entityList)
        {
            using (ListComponent<Entity> list=ListComponent<Entity>.Create())
            {
                foreach (Entity entity in entityList)
                {
                    string key = entity.GetType().Name;
                    if (!self.UnitCacheDict.TryGetValue(key,out UnitCache unitCache))
                    {
                        unitCache = self.AddChild<UnitCache>();
                        unitCache.Key = key;
                        self.UnitCacheDict.Add(key,unitCache);
                    }

                    unitCache.AddOrUpdate(entity);
                    list.Add(entity);
                }

                if (list.Count>0)
                {
                    await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(id, list);
                }
            }
        }

        public static async ETTask<Entity> Get(this UnitCacheComponent self, long unitId, string key)
        {
            if (!self.UnitCacheDict.TryGetValue(key,out UnitCache unitCache))
            {
                unitCache = self.AddChild<UnitCache>();
                unitCache.Key = key;
                self.UnitCacheDict.Add(key,unitCache);
            }

            return await unitCache.Get(unitId);
        }

        public static void Delete(this UnitCacheComponent self, long unitId)
        {
            foreach (UnitCache cache in self.UnitCacheDict.Values)
            {
                cache.Delete(unitId);
            }
        }
    }
}