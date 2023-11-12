
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class Scroll_Item_BattleLevel : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_BattleLevel BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public TMPro.TextMeshProUGUI E_LevelNameTextMeshProUGUI
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
     				if( this.m_E_LevelNameTextMeshProUGUI == null )
     				{
		    			this.m_E_LevelNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_LevelName");
     				}
     				return this.m_E_LevelNameTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_LevelName");
     			}
     		}
     	}

		public TMPro.TextMeshProUGUI E_LevelNotEnoughTextMeshProUGUI
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
     				if( this.m_E_LevelNotEnoughTextMeshProUGUI == null )
     				{
		    			this.m_E_LevelNotEnoughTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_LevelNotEnough");
     				}
     				return this.m_E_LevelNotEnoughTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_LevelNotEnough");
     			}
     		}
     	}

		public TMPro.TextMeshProUGUI E_InAdventureTipsTextMeshProUGUI
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
     				if( this.m_E_InAdventureTipsTextMeshProUGUI == null )
     				{
		    			this.m_E_InAdventureTipsTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_InAdventureTips");
     				}
     				return this.m_E_InAdventureTipsTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_InAdventureTips");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_GoButton
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
     				if( this.m_E_GoButton == null )
     				{
		    			this.m_E_GoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Go");
     				}
     				return this.m_E_GoButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Go");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_GoImage
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
     				if( this.m_E_GoImage == null )
     				{
		    			this.m_E_GoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Go");
     				}
     				return this.m_E_GoImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Go");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_LevelNameTextMeshProUGUI = null;
			this.m_E_LevelNotEnoughTextMeshProUGUI = null;
			this.m_E_InAdventureTipsTextMeshProUGUI = null;
			this.m_E_GoButton = null;
			this.m_E_GoImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private TMPro.TextMeshProUGUI m_E_LevelNameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_LevelNotEnoughTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_InAdventureTipsTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_GoButton = null;
		private UnityEngine.UI.Image m_E_GoImage = null;
		public Transform uiTransform = null;
	}
}
