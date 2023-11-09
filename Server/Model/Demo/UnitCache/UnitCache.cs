// 文件：UnitCache.cs
// 作者：xj
// 描述：
// 日期：2023/11/10 0:09

using System.Collections.Generic;

namespace ET
{

    public interface IUnitCache
    {
        
    }
    
    public class UnitCache:Entity,IAwake,IDestroy
    {
        public string Key;
        public Dictionary<long, Entity> CacheComponentDict = new Dictionary<long, Entity>();
    }
}