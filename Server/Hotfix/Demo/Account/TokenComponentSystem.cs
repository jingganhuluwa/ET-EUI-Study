// 文件：TokenComponentSystem.cs
// 作者：xj
// 描述：
// 日期：2023/07/31 10:35

namespace ET
{
    [FriendClassAttribute(typeof (TokenComponent))]
    public static class TokenComponentSystem
    {
        public static void Add(this TokenComponent self, long key, string token)
        {
            self.TokenDict.Add(key, token);
            self.TimeOutRemoveKey(key, token).Coroutine();
        }

        public static string Get(this TokenComponent self, long key)
        {
            string token = null;
            self.TokenDict.TryGetValue(key, out token);
            return token;
        }

        public static void Remove(this TokenComponent self, long key)
        {
            if (self.TokenDict.ContainsKey(key))
            {
                self.TokenDict.Remove(key);
            }
        }

        private static async ETTask TimeOutRemoveKey(this TokenComponent self, long key, string token)
        {
            //等待10分钟
            await TimerComponent.Instance.WaitAsync(1000 * 60 * 10);
            string onlineToken = self.Get(key);
            if (string.IsNullOrEmpty(onlineToken) && onlineToken == token)
            {
                self.Remove(key);
            }
        }
    }
}