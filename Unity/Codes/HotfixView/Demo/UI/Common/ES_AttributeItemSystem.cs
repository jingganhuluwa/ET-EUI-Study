// 文件：ES_AttributeItemSystem.cs
// 作者：xj
// 描述：
// 日期：2023/11/12 0:19

using System;

namespace ET
{
    public static class ES_AttributeItemSystem
    {
        public static void Refresh(this ES_AttributeItem self, int numbericType)
        {
            self.EAttributeValueTextMeshProUGUI.text = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene())
                    .GetComponent<NumericComponent>().GetAsLong(numbericType).ToString();
        }
        
        public static void RegisterEvent(this ES_AttributeItem self, int numbericType)
        {
            self.E_AddButton.AddListenerAsync(() => { return self.RequestAddAttribute(numbericType);});
        }
        
        public static async ETTask RequestAddAttribute(this ES_AttributeItem self, int numericType)
        {
            NumericComponent numericComponent        = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene());
            int attributePoint = numericComponent.GetAsInt(NumericType.AttributePoint);
            if (attributePoint<=0)
            {
                return;
            }
            try
            {
                int errorCode = await NumericHelper.ReqeustAddAttributePoint(self.ZoneScene(), numericType);
                if (errorCode != ErrorCode.ERR_Success)
                {
                    return;
                }
                Log.Debug("加点成功");
                self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgRoleInfo>()?.Refresh();
            }
            catch(Exception e)
            {
                Log.Error(e.ToString());
            }
            
            await ETTask.CompletedTask;
        }

    }
}