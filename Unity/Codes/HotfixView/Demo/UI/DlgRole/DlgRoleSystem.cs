using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace ET
{
    [FriendClass(typeof(DlgRole))]
    [FriendClassAttribute(typeof(ET.RoleInfosComponent))]
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    public static class DlgRoleSystem
    {

        public static void RegisterUIEvent(this DlgRole self)
        {
            self.View.EButton_CreateButton.AddListenerAsync(() => { return self.OnCreateClickHandler(); });
            self.View.EButton_EnterButton.AddListenerAsync(() => { return self.OnEnterClickHandler(); });
            self.View.EButton_DeleteButton.AddListenerAsync(() => { return self.OnDeleteClickHandler(); });
            
            self.View.E_RoleInfoListLoopHorizontalScrollRect.AddItemRefreshListener((transform, index) =>
            {
                self.OnScrollItemRefreshHandler(transform, index);
            });
        }

        public static void ShowWindow(this DlgRole self, Entity contextData = null)
        {
            self.RefreshRoleItem();
        }

        public static void HideWindow(this DlgRole self)
        {
            self.RemoveUIScrollItems(ref self.RoleInfoDict);
        }

        public static void RefreshRoleItem(this DlgRole self)
        {
            int count = self.ZoneScene().GetComponent<RoleInfosComponent>().RoleInfoList.Count;
            self.AddUIScrollItems(ref self.RoleInfoDict, count);
            self.View.E_RoleInfoListLoopHorizontalScrollRect.SetVisible(true, count);
        }
        
        public static void OnScrollItemRefreshHandler(this DlgRole self, Transform transform, int index)
        {
            var roleInfo = self.RoleInfoDict[index].BindTrans(transform);
            var info = self.ZoneScene().GetComponent<RoleInfosComponent>().RoleInfoList[index];
            roleInfo.EButton_SelectImage.color =
                    info.Id == self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId ? Color.red : Color.cyan;
            roleInfo.ELabel_ContentText.SetText(info.Name);
            roleInfo.EButton_SelectButton.AddListener(() => self.OnSelectServerItemHandler(info.Id));
        }

        public static void OnSelectServerItemHandler(this DlgRole self, long roleId)
        {
            self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId = roleId;
            Log.Debug($"当前选择的角色Id是:{roleId}");
            self.View.E_RoleInfoListLoopHorizontalScrollRect.RefillCells();
        }


        public static async ETTask OnEnterClickHandler(this DlgRole self)
        {
            bool isSelect = self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId != 0;
            if (!isSelect)
            {
                Log.Error("请先选择角色");
                return;
            }

            try
            {
                int errorCode=await LoginHelper.GetRelamKey(self.ZoneScene());
                if (errorCode!=ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                
                
                errorCode=await LoginHelper.EnterGame(self.ZoneScene());
                if (errorCode!=ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                
                self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Main);
                self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Role);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
            
        }

        public static async ETTask OnCreateClickHandler(this DlgRole self)
        {
            
            string name=self.View.E_RoleNameTMP_InputField.text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                Log.Error("名字为空");
                return;
            }
            try
            {
                int errorcode = await LoginHelper.CreateRole(self.ZoneScene(), name);
                if (errorcode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorcode.ToString());
                    return;
                }

                self.RefreshRoleItem();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        public static async ETTask OnDeleteClickHandler(this DlgRole self)
        {
            bool isSelect = self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId != 0;
            if (!isSelect)
            {
                Log.Error("请先选择角色");
                return;
            }

            try
            {
                int errorcode=await LoginHelper.DeleteRole(self.ZoneScene());
                if (errorcode!=ErrorCode.ERR_Success)
                {
                    Log.Error(errorcode.ToString());
                    return;
                }


                self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentRoleId = 0;
                self.RefreshRoleItem();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}
