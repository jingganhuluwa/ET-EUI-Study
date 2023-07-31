// 文件：TokenComponent.cs
// 作者：xj
// 描述：
// 日期：2023/07/31 10:33

using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class TokenComponent:Entity,IAwake
    {
        public readonly Dictionary<long, string> TokenDict = new Dictionary<long, string>();
    }
}