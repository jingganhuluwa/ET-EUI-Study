// 文件：ServerInfoManagerComponent.cs
// 作者：xj
// 描述：
// 日期：2023/08/27 9:31

using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(ServerInfo))]
    public class ServerInfoManagerComponent:Entity,IAwake,IDestroy,ILoad
    {
        public List<ServerInfo> ServerInfoList = new List<ServerInfo>();
    }
}