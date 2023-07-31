// 文件：AccountCheckOutTimeComponent.cs
// 作者：xj
// 描述：
// 日期：2023/07/31 20:56

namespace ET
{
    [ComponentOf(typeof(Session))]
    public class AccountCheckOutTimeComponent:Entity,IAwake<long>,IDestroy
    {
        public long Timer = 0;
        public long AccountId = 0;
        
    }
}