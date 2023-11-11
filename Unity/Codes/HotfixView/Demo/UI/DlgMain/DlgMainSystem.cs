using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof (DlgMain))]
    public static class DlgMainSystem
    {
        public static void RegisterUIEvent(this DlgMain self)
        {
            self.View.E_TestButtonButton.AddListenerAsync(() => { return self.OnTestButtonClick();} );
        }

        public static void ShowWindow(this DlgMain self, Entity contextData = null)
        {
            self.Refresh().Coroutine();
        }

        public static async ETTask Refresh(this DlgMain self)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            
            self.View.E_ExpTextTextMeshProUGUI.SetText(numericComponent.GetAsInt((int)NumericType.Exp).ToString());
            self.View.E_GoldTextTextMeshProUGUI.SetText(numericComponent.GetAsInt((int)NumericType.Gold).ToString());
            self.View.E_LevelTextTextMeshProUGUI.SetText(numericComponent.GetAsInt((int)NumericType.Level).ToString());
        }

        public static async ETTask OnTestButtonClick(this DlgMain self)
        {
            try
            {
                int error = await NumericHelper.TestUpdaterNumeric(self.ZoneScene());
                if (error!=ErrorCode.ERR_Success)
                {
                    return;
                }
                Log.Debug("发送更新属性测试信息成功");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}