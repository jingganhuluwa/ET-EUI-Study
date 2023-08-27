// 文件：ServerInfosComponent.cs
// 作者：xj
// 描述：
// 日期：2023/08/27 9:16

using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(ServerInfo))]
    public class ServerInfosComponent:Entity,IAwake,IDestroy
    {
        public List<ServerInfo> ServerInfoList = new List<ServerInfo>();
    }
}