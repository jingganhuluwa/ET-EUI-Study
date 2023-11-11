// 文件：NumericNoticeComponentSystem.cs
// 作者：xj
// 描述：
// 日期：2023/11/11 16:42

using ET.EventType;

namespace ET
{
    [FriendClassAttribute(typeof(ET.NumericNoticeComponent))]
    public static class NumericNoticeComponentSystem
    {
        public static void NoticeImmediately(this NumericNoticeComponent self, NumbericChange args)
        {
            Unit unit = self.GetParent<Unit>();
            self.NoticeUnitNumericMessgae.UnitId = unit.Id;
            self.NoticeUnitNumericMessgae.NumericType = args.NumericType;
            self.NoticeUnitNumericMessgae.NewValue = args.New;
            MessageHelper.SendToClient(unit, self.NoticeUnitNumericMessgae);
        }
    }
}