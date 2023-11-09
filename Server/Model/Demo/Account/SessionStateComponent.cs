// 文件：SessionStateComponent.cs
// 作者：xj
// 描述：
// 日期：2023/11/09 22:05

namespace ET
{
    public enum SessionState
    {
        Normal,
        Game
    }
    
    [ComponentOf(typeof(Session))]
    public class SessionStateComponent : Entity,IAwake
    {
        public SessionState State;
    }
}