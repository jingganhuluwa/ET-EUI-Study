// 文件：LoginInfoRecordComponentSystem.cs
// 作者：xj
// 描述：
// 日期：2023/08/01 8:29

namespace ET
{
    public class LoginInfoRecordComponentDestroySystem: DestroySystem<LoginInfoRecordComponent>
    {
        public override void Destroy(LoginInfoRecordComponent self)
        {
            self.AccountLoginInfoDict.Clear();
        }
    }

    [FriendClassAttribute(typeof (ET.LoginInfoRecordComponent))]
    public static class LoginInfoRecordComponentSystem
    {
        public static void Add(this LoginInfoRecordComponent self, long key, int value)
        {
            if (self.AccountLoginInfoDict.ContainsKey(key))
            {
                self.AccountLoginInfoDict[key] = value;
                return;
            }

            self.AccountLoginInfoDict.Add(key, value);
        }

        public static void Remove(this LoginInfoRecordComponent self, long key)
        {
            if (self.AccountLoginInfoDict.ContainsKey(key))
            {
                self.AccountLoginInfoDict.Remove(key);
            }
        }
        
        public static int Get(this LoginInfoRecordComponent self, long key)
        {
            if (self.AccountLoginInfoDict.TryGetValue(key,out int value))
            {
                return value;
            }

            return -1;
        }

        public static bool IsExist(this LoginInfoRecordComponent self,long key)
        {
            return self.AccountLoginInfoDict.ContainsKey(key);
        }
    }
}