// 文件：ServerInfo.cs
// 作者：xj
// 描述：
// 日期：2023/08/27 8:39

namespace ET
{
    public enum ServerStatus
    {
        Normal = 0,
        Stop = 2
    }

    public class ServerInfo: Entity, IAwake
    {
        public int Status;
        public string ServerName;
    }
}