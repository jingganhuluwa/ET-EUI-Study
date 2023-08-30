// 文件：ServerInfoManagerComponentSystem.cs
// 作者：xj
// 描述：
// 日期：2023/08/27 9:33

using System.Collections.Generic;

namespace ET
{
    public class ServerInfoManagerComponentAwakeSystem: AwakeSystem<ServerInfoManagerComponent>
    {
        public override void Awake(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }

    public class ServerInfoManagerComponentDestroySystem: DestroySystem<ServerInfoManagerComponent>
    {
        public override void Destroy(ServerInfoManagerComponent self)
        {
            foreach (ServerInfo serverInfo in self.ServerInfoList)
            {
                serverInfo?.Dispose();
            }

            self.ServerInfoList.Clear();
        }
    }

    public class ServerInfoManagerComponentLoadSystem: LoadSystem<ServerInfoManagerComponent>
    {
        public override void Load(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }

    [FriendClass(typeof (ServerInfoManagerComponent))]
    [FriendClass(typeof (ServerInfo))]
    public static class ServerInfoManagerComponentSystem
    {
        public static async ETTask Awake(this ServerInfoManagerComponent self)
        {
            self.ServerInfoList.Clear();

            List<ServerInfo> serverInfoList = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<ServerInfo>(d => true);
            if (serverInfoList == null || serverInfoList.Count <= 0)
            {
                Log.Error("ServerInfo Count is Zero");
                Dictionary<int, ServerInfoConfig> serverInfoConfigs = ServerInfoConfigCategory.Instance.GetAll();
                foreach (ServerInfoConfig info in serverInfoConfigs.Values)
                {
                    ServerInfo newServerInfo = self.AddChildWithId<ServerInfo>(info.Id);
                    newServerInfo.ServerName = info.ServerName;
                    newServerInfo.Status = (int) ServerStatus.Normal;
                    self.ServerInfoList.Add(newServerInfo);
                    await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(newServerInfo);
                }

                return;
            }

            foreach (ServerInfo serverInfo in serverInfoList)
            {
                self.AddChild(serverInfo);
                self.ServerInfoList.Add(serverInfo);
            }
        }
    }
}