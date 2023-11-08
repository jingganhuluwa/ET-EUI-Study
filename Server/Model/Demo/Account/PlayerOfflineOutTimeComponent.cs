// 文件：PlayerOfflineOutTimeComponent.cs
// 作者：xj
// 描述：
// 日期：2023/11/09 2:14

namespace ET
{
    [ComponentOf(typeof(Player))]
    public class PlayerOfflineOutTimeComponent:Entity,IAwake,IDestroy
    {
        public long Timer;
    }
}