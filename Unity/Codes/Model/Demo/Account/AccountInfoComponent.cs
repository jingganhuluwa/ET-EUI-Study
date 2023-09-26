// 文件：AccountInfoComponent.cs
// 作者：xj
// 描述：
// 日期：2023/07/31 9:34

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class AccountInfoComponent:Entity,IAwake,IDestroy
    {
        public string Token;
        public long AccountId;
        public string RealmKey;
        public string RealmAdress;
    }
}