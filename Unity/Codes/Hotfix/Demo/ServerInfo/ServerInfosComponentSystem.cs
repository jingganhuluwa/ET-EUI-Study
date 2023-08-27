// 文件：ServerInfosComponentSystem.cs
// 作者：xj
// 描述：
// 日期：2023/08/27 9:17

namespace ET
{
    public class ServerInfosComponentDestroySystem:DestroySystem<ServerInfosComponent>
    {
        public override void Destroy(ServerInfosComponent self)
        {
            foreach (ServerInfo serverInfo in self.ServerInfoList)
            {
                serverInfo?.Dispose();
            }
            self.ServerInfoList.Clear();
        }
    }
    [FriendClass(typeof(ServerInfosComponent))]
    public static class ServerInfosComponentSystem
    {
        public static void Add(this ServerInfosComponent self, ServerInfo serverInfo)
        {
            self.ServerInfoList.Add(serverInfo);
        }
    }
}