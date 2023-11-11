// 文件：NumericWatcher_RefreshMainUI.cs
// 作者：xj
// 描述：
// 日期：2023/11/11 16:55

using ET.EventType;

namespace ET
{
    [NumericWatcher(NumericType.Level)]
    [NumericWatcher(NumericType.Gold)]
    [NumericWatcher(NumericType.Exp)]
    public class NumericWatcher_RefreshMainUI:INumericWatcher
    {
        public void Run(NumbericChange args)
        {
            args.Parent.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgMain>()?.Refresh();
        }
    }
}