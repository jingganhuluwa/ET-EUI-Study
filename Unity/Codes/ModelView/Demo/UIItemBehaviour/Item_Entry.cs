
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class Scroll_Item_Entry : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Entry BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public TMPro.TextMeshProUGUI E_EntryNameTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_EntryNameTextMeshProUGUI == null )
     				{
		    			this.m_E_EntryNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_EntryName");
     				}
     				return this.m_E_EntryNameTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_EntryName");
     			}
     		}
     	}

		public TMPro.TextMeshProUGUI E_EntryValueTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_EntryValueTextMeshProUGUI == null )
     				{
		    			this.m_E_EntryValueTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_EntryValue");
     				}
     				return this.m_E_EntryValueTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_EntryValue");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_EntryNameTextMeshProUGUI = null;
			this.m_E_EntryValueTextMeshProUGUI = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private TMPro.TextMeshProUGUI m_E_EntryNameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_EntryValueTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
