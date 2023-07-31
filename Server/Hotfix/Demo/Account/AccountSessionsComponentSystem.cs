// 文件：AccountSessionsComponentSystem.cs
// 作者：xj
// 描述：
// 日期：2023/07/31 20:21

namespace ET
{
    public class AccountSessionsComponentDestroySystem: DestroySystem<AccountSessionsComponent>
    {
        public override void Destroy(AccountSessionsComponent self)
        {
            self.AccountSessionDict.Clear();
        }
    }

    [FriendClassAttribute(typeof (AccountSessionsComponent))]
    public static class AccountSessionsComponentSystem
    {
        public static long Get(this AccountSessionsComponent self, long accountId)
        {
            if (self.AccountSessionDict.TryGetValue(accountId, out long instanceId))
            {
                return instanceId;
            }

            return 0;
        }

        public static void Add(this AccountSessionsComponent self, long accountId, long sessionInstanceId)
        {
            if (self.AccountSessionDict.ContainsKey(accountId))
            {
                self.AccountSessionDict[accountId] = sessionInstanceId;
                return;
            }

            self.AccountSessionDict.Add(accountId, sessionInstanceId);
        }

        public static void Remove(this AccountSessionsComponent self, long accountId)
        {
            if (self.AccountSessionDict.ContainsKey(accountId))
            {
                self.AccountSessionDict.Remove(accountId);
            }
        }
    }
}