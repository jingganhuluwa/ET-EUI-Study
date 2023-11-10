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
        }

        public static void ShowWindow(this DlgMain self, Entity contextData = null)
        {
        }

        public static async ETTask Refresh(this DlgMain self)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            
            self.View.E_ExpTextTextMeshProUGUI.SetText(numericComponent.GetAsInt((int)NumericType.Exp).ToString());
            self.View.E_GoldTextTextMeshProUGUI.SetText(numericComponent.GetAsInt((int)NumericType.Gold).ToString());
            self.View.E_LevelTextTextMeshProUGUI.SetText(numericComponent.GetAsInt((int)NumericType.Level).ToString());
        }
    }
}