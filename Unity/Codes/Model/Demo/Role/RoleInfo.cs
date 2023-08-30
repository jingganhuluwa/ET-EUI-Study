// 文件：RoleInfo.cs
// 作者：xj
// 描述：
// 日期：2023/08/30 8:50

namespace ET
{
    public enum RoleInfoState
    {
        Normal = 0,
        Freeze = 1
    }

    public class RoleInfo: Entity, IAwake
    {
        public string Name;
        public int ServerId;
        public int State;
        public long AccountId;
        public long LastLoginTime;
        public long CreateTime;
    }
}