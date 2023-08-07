// 文件：LoginInfoRecordComponent.cs
// 作者：xj
// 描述：
// 日期：2023/08/01 8:27

using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class LoginInfoRecordComponent:Entity,IAwake,IDestroy
    {
        public readonly Dictionary<long, int> AccountLoginInfoDict = new Dictionary<long, int>();
    }
}