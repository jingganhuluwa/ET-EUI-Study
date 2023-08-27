// 文件：ServerInfoSystem.cs
// 作者：xj
// 描述：
// 日期：2023/08/27 9:02

namespace ET
{
    [FriendClassAttribute(typeof(ET.ServerInfo))]
    public static class ServerInfoSystem
    {
        public static void FromMessage(this ServerInfo self, ServerInfoProto serverInfoProto)
        {
            self.Id = serverInfoProto.Id;
            self.Status = serverInfoProto.Status;
            self.ServerName = serverInfoProto.ServerInfoName;
        }

        public static ServerInfoProto ToMessage(this ServerInfo self)
        {
            return new ServerInfoProto() { Id = (int) self.Id, ServerInfoName = self.ServerName, Status = self.Status };
        }
    }
}