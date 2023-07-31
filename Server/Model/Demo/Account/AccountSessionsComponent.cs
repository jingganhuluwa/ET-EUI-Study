// 文件：AccountSessionsComponent.cs
// 作者：xj
// 描述：
// 日期：2023/07/31 20:19

using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class AccountSessionsComponent:Entity,IAwake,IDestroy
    {
        public readonly Dictionary<long, long> AccountSessionDict = new Dictionary<long, long>();
    }
}