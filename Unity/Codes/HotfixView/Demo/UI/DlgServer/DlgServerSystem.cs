using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof (DlgServer))]
    [FriendClass(typeof (ServerInfosComponent))]
    [FriendClass(typeof (ServerInfo))]
    public static class DlgServerSystem
    {
        public static void RegisterUIEvent(this DlgServer self)
        {
            self.View.EButton_EnterButton.AddListenerAsync(() => { return self.OnEnterClickHandler(); });
            self.View.E_ServerInfoLoopVerticalScrollRect.AddItemRefreshListener((transform, index) =>
            {
                self.OnScrollItemRefreshHandler(transform, index);
            });
        }

        public static void ShowWindow(this DlgServer self, Entity contextData = null)
        {
            int count = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfoList.Count;
            self.AddUIScrollItems(ref self.ServerInfoDict, count);
            self.View.E_ServerInfoLoopVerticalScrollRect.SetVisible(true, count);
        }

        public static void HideWindow(this DlgServer self)
        {
            self.RemoveUIScrollItems(ref self.ServerInfoDict);
        }

        public static void OnScrollItemRefreshHandler(this DlgServer self, Transform transform, int index)
        {
            Scroll_Item_ServerInfo serverInfo = self.ServerInfoDict[index].BindTrans(transform);
            ServerInfo info = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfoList[index];
            serverInfo.EButton_SelectImage.color =
                    info.Id == self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId? Color.red : Color.cyan;
            serverInfo.ELabel_ContentText.SetText(info.ServerName);
            serverInfo.EButton_SelectButton.AddListener(() => self.OnSelectServerItemHandler(info.Id));
        }

        public static void OnSelectServerItemHandler(this DlgServer self, long serverId)
        {
            self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId = (int) serverId;
            Log.Debug($"当前选择的服务器Id是:{serverId}");
            self.View.E_ServerInfoLoopVerticalScrollRect.RefillCells();
        }

        public static async ETTask OnEnterClickHandler(this DlgServer self)
        {
            bool isSelect = self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId != 0;
            if (!isSelect)
            {
                Log.Error("请先选择区服");
                
                return;
            }

            try
            {
                int errorcode=await LoginHelper.GetRoles(self.ZoneScene());
                if (errorcode!=ErrorCode.ERR_Success)
                {
                    Log.Error(errorcode.ToString());
                    return;
                }
                self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Role);
                self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Server);

            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}