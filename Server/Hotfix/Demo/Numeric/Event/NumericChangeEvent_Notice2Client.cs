// 文件：NumericChangeEvent_Notice2Client.cs
// 作者：xj
// 描述：
// 日期：2023/11/11 16:27

using ET.EventType;

namespace ET
{
    public class NumericChangeEvent_Notice2Client: AEventClass<EventType.NumbericChange>
    {

        protected override void Run(object a)
        {
            EventType.NumbericChange args = a as EventType.NumbericChange;
            if (!(args.Parent is Unit unit))
            {
                return;
            }

            //只允许通知玩家Unit
            if (unit.Type != UnitType.Player)
            {
                return;
            }
            unit.GetComponent<NumericNoticeComponent>()?.NoticeImmediately(args);
        }
    }
}