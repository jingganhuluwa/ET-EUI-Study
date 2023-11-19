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
			self.View.E_LevelUpButton.AddListenerAsync(self.OnLevelUpBUttonHandler);
			
			RedDotHelper.AddRedDotNodeView(self.ZoneScene(), "UpLevelButton", self.View.E_LevelUpButton.gameObject, Vector3.one, new Vector3(115f,10f,0));
			RedDotHelper.AddRedDotNodeView(self.ZoneScene(), "AddAttribute", self.View.E_AttributePointTextMeshProUGUI.gameObject, new Vector3(0.5f,0.5f,1), new Vector3(-17,10f,0));
		}

		public static void OnUnLoadWindow(this DlgRoleInfo self)
		{
			RedDotMonoView redDotMonoView = self.View.E_LevelUpButton.gameObject.GetComponent<RedDotMonoView>();
			RedDotHelper.RemoveRedDotView(self.ZoneScene(),"UpLevelButton",out redDotMonoView);
			
			redDotMonoView =  self.View.E_AttributePointTextMeshProUGUI.gameObject.GetComponent<RedDotMonoView>();
			RedDotHelper.RemoveRedDotView(self.ZoneScene(),"AddAttribute",out redDotMonoView);
		}
		
		
		public static void ShowWindow(this DlgRoleInfo self, Entity contextData = null)
		{
			self.Refresh();
			
		}

		public static void RefreshEquipShowItems(this DlgRoleInfo self)
		{
			// self.View.ES_EquipItem_Head.RefreshShowItem(EquipPosition.Head);
			// self.View.ES_EquipItem_Clothes.RefreshShowItem(EquipPosition.Clothes);
			// self.View.ES_EquipItem_Shoes.RefreshShowItem(EquipPosition.Shoes);
			// self.View.ES_EquipItem_Ring.RefreshShowItem(EquipPosition.Ring);
			// self.View.ES_EquipItem_Weapon.RefreshShowItem(EquipPosition.Weapon);
			// self.View.ES_EquipItem_Shield.RefreshShowItem(EquipPosition.Shield);
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
		
		public static async ETTask OnLevelUpBUttonHandler(this DlgRoleInfo self)
		{
			try
			{
				int errorCode = await NumericHelper.ReqeustUpRoleLevel(self.ZoneScene());

				if (errorCode != ErrorCode.ERR_Success)
				{
					return;
				}
				self.Refresh();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

	}
}
