// 文件：NumericNoticeComponent.cs
// 作者：xj
// 描述：
// 日期：2023/11/11 16:37

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class NumericNoticeComponent:Entity,IAwake
    {
        public M2C_NoticeUnitNumeric NoticeUnitNumericMessgae = new M2C_NoticeUnitNumeric();
    }
}