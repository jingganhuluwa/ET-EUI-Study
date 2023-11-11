using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgRoleInfo))]
	public static  class DlgRoleInfoSystem
	{

		public static void RegisterUIEvent(this DlgRoleInfo self)
		{
			self.RegisterCloseEvent<DlgRoleInfo>(self.View.E_CloseButton);
			self.View.ES_AttributeItem.RegisterEvent(NumericType.Power);
			self.View.ES_AttributeItem1.RegisterEvent(NumericType.PhysicalStrength);
			self.View.ES_AttributeItem2.RegisterEvent(NumericType.Agile);
			self.View.ES_AttributeItem3.RegisterEvent(NumericType.Spirit);
			self.View.ELoopScrollList_AttributeLoopVerticalScrollRect.AddItemRefreshListener(( transform,  index) => { self.OnAttributeItemRefreshHandler(transform,index); });
		}

		public static void ShowWindow(this DlgRoleInfo self, Entity contextData = null)
		{
			self.Refresh();
			
		}

		public static void Refresh(this DlgRoleInfo self)
		{
			self.View.ES_AttributeItem.Refresh(NumericType.Power);
			self.View.ES_AttributeItem1.Refresh(NumericType.PhysicalStrength);
			self.View.ES_AttributeItem2.Refresh(NumericType.Agile);
			self.View.ES_AttributeItem3.Refresh(NumericType.Spirit);

			NumericComponent numericComponent        = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene());
			self.View.E_CombatEffectivenessTextMeshProUGUI.text = "战力值:" + numericComponent.GetAsLong(NumericType.CombatEffectiveness).ToString();
			self.View.E_AttributePointTextMeshProUGUI.text      = numericComponent.GetAsInt(NumericType.AttributePoint).ToString();
			
			int count = PlayerNumericConfigCategory.Instance.GetShowConfigCount();
			self.AddUIScrollItems(ref self.ScrollItemAttributeDict,count);
			self.View.ELoopScrollList_AttributeLoopVerticalScrollRect.SetVisible(true,count);
		}
		
		public static void OnAttributeItemRefreshHandler(this DlgRoleInfo self, Transform transform, int index)
		{
			Scroll_Item_Attribute scrollItemAttribute     = self.ScrollItemAttributeDict[index].BindTrans(transform);
			PlayerNumericConfig config                    = PlayerNumericConfigCategory.Instance.GetConfigByIndex(index);
			NumericComponent numericComponent = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene());
			
			scrollItemAttribute.EAttributeNameTextMeshProUGUI.text  = config.Name + ":";
			scrollItemAttribute.EAttributeValueTextMeshProUGUI.text = config.isPrecent == 0? 
					numericComponent.GetAsLong(config.Id).ToString():
					$"{numericComponent.GetAsFloat(config.Id):0.00}%";
		}

	}
}
