// 文件：UnitCacheComponent.cs
// 作者：xj
// 描述：
// 日期：2023/11/10 0:10

using System.Collections.Generic;

namespace ET
{
    public class UnitCacheComponent:Entity,IAwake,IDestroy
    {
        public Dictionary<string, UnitCache> UnitCacheDict = new Dictionary<string, UnitCache>();
        public List<string> UnitCacheKeyList = new List<string>();
    }
}