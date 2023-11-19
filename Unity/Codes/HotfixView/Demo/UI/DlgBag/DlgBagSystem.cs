using System.Collections;
using System.Collections.Generic;
using System;
using ILRuntime.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(DlgBag))]
    [FriendClassAttribute(typeof(ET.BagComponent))]
    public static class DlgBagSystem
    {
        private const int _pageSize = 25;
        
        public static void RegisterUIEvent(this DlgBag self)
        {
            self.RegisterCloseEvent<DlgBag>(self.View.E_CloseButton);
            self.View.E_TopButtonToggleGroup.AddListener(self.OnTopToggleSelectedHandler);
            self.View.E_PreviousButton.AddListener(self.OnPreviousPageHandler);
            self.View.E_NextButton.AddListener(self.OnNextPageHandler);
            self.View.E_BagItemListLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopItemRefreshHandler);
        }

        public static void ShowWindow(this DlgBag self, Entity contextData = null)
        {
            self.View.E_WeaponToggle.IsSelected(true);
        }

        public static void HideWindow(this DlgBag self)
        {
            self.RemoveUIScrollItems(ref self.ScrollItemBagItemDict);
        }

        public static void Refresh(this DlgBag self)
        {
            self.RefreshItems();
            self.RefeshPageIndexInfo();
        }

        public static void RefreshItems(this DlgBag self)
        {
            self.ZoneScene().GetComponent<BagComponent>().ItemsMap.TryGetValue((int)self.CurrentItemType, out List<Item> itemList);

            int showCount = itemList == null ? 0 : itemList.Count - (self.CurrentPageIndex * _pageSize);
            showCount = showCount > _pageSize ? _pageSize : showCount;
            self.AddUIScrollItems(ref self.ScrollItemBagItemDict, showCount);
            self.View.E_BagItemListLoopVerticalScrollRect.SetVisible(true, showCount);
        }

        public static void RefeshPageIndexInfo(this DlgBag self)
        {
            int itemCount = self.ZoneScene().GetComponent<BagComponent>().GetItemCountByItemType(self.CurrentItemType);
            int maxShowCount = (self.CurrentPageIndex * _pageSize) + _pageSize;

            self.View.E_PreviousButton.interactable = self.CurrentPageIndex != 0;
            self.View.E_NextButton.interactable = itemCount > maxShowCount;

            int maxPageIndex = Mathf.CeilToInt(itemCount / _pageSize.ToFloat());
            self.View.E_PageTextMeshProUGUI.text = $"{self.CurrentPageIndex + 1} / {maxPageIndex}";
        }

        public static void OnTopToggleSelectedHandler(this DlgBag self, int index)
        {
            self.CurrentItemType = (ItemType)index;
            self.CurrentPageIndex = 0;
            self.Refresh();
        }

        public static void OnLoopItemRefreshHandler(this DlgBag self, Transform transform, int index)
        {
            self.ZoneScene().GetComponent<BagComponent>().ItemsMap.TryGetValue((int)self.CurrentItemType, out List<Item> itemList);
            Scroll_Item_BagItem scrollItemBagItem = self.ScrollItemBagItemDict[index].BindTrans(transform);

            index = (self.CurrentPageIndex * _pageSize) + index;
            scrollItemBagItem.Refresh(itemList[index].Id);
        }

        public static void OnNextPageHandler(this DlgBag self)
        {
            ++self.CurrentPageIndex;
            self.Refresh();
        }

        public static void OnPreviousPageHandler(this DlgBag self)
        {
            --self.CurrentPageIndex;
            self.Refresh();
        }
    }
}