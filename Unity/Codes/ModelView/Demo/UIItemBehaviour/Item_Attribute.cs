
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class Scroll_Item_Attribute : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Attribute BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public TMPro.TextMeshProUGUI EAttributeNameTextMeshProUGUI
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
     				if( this.m_EAttributeNameTextMeshProUGUI == null )
     				{
		    			this.m_EAttributeNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EAttributeName");
     				}
     				return this.m_EAttributeNameTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EAttributeName");
     			}
     		}
     	}

		public TMPro.TextMeshProUGUI EAttributeValueTextMeshProUGUI
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
     				if( this.m_EAttributeValueTextMeshProUGUI == null )
     				{
		    			this.m_EAttributeValueTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EAttributeValue");
     				}
     				return this.m_EAttributeValueTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EAttributeValue");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EAttributeNameTextMeshProUGUI = null;
			this.m_EAttributeValueTextMeshProUGUI = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private TMPro.TextMeshProUGUI m_EAttributeNameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_EAttributeValueTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
