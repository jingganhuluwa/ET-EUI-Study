
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgBagViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_CloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseButton == null )
     			{
		    		this.m_E_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Close");
     			}
     			return this.m_E_CloseButton;
     		}
     	}

		public UnityEngine.UI.Image E_CloseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseImage == null )
     			{
		    		this.m_E_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Close");
     			}
     			return this.m_E_CloseImage;
     		}
     	}

		public UnityEngine.UI.ToggleGroup E_TopButtonToggleGroup
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TopButtonToggleGroup == null )
     			{
		    		this.m_E_TopButtonToggleGroup = UIFindHelper.FindDeepChild<UnityEngine.UI.ToggleGroup>(this.uiTransform.gameObject,"E_TopButton");
     			}
     			return this.m_E_TopButtonToggleGroup;
     		}
     	}

		public UnityEngine.UI.Toggle E_WeaponToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponToggle == null )
     			{
		    		this.m_E_WeaponToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_TopButton/E_Weapon");
     			}
     			return this.m_E_WeaponToggle;
     		}
     	}

		public UnityEngine.UI.Image E_WeaponImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponImage == null )
     			{
		    		this.m_E_WeaponImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TopButton/E_Weapon");
     			}
     			return this.m_E_WeaponImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_ArmorToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ArmorToggle == null )
     			{
		    		this.m_E_ArmorToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_TopButton/E_Armor");
     			}
     			return this.m_E_ArmorToggle;
     		}
     	}

		public UnityEngine.UI.Image E_ArmorImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ArmorImage == null )
     			{
		    		this.m_E_ArmorImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TopButton/E_Armor");
     			}
     			return this.m_E_ArmorImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_RingToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RingToggle == null )
     			{
		    		this.m_E_RingToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_TopButton/E_Ring");
     			}
     			return this.m_E_RingToggle;
     		}
     	}

		public UnityEngine.UI.Image E_RingImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RingImage == null )
     			{
		    		this.m_E_RingImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TopButton/E_Ring");
     			}
     			return this.m_E_RingImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_PropToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PropToggle == null )
     			{
		    		this.m_E_PropToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_TopButton/E_Prop");
     			}
     			return this.m_E_PropToggle;
     		}
     	}

		public UnityEngine.UI.Image E_PropImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PropImage == null )
     			{
		    		this.m_E_PropImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TopButton/E_Prop");
     			}
     			return this.m_E_PropImage;
     		}
     	}

		public UnityEngine.UI.Image E_BagItemListImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagItemListImage == null )
     			{
		    		this.m_E_BagItemListImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BagItemList");
     			}
     			return this.m_E_BagItemListImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_BagItemListLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagItemListLoopVerticalScrollRect == null )
     			{
		    		this.m_E_BagItemListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"E_BagItemList");
     			}
     			return this.m_E_BagItemListLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Button E_PreviousButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PreviousButton == null )
     			{
		    		this.m_E_PreviousButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bottom/E_Previous");
     			}
     			return this.m_E_PreviousButton;
     		}
     	}

		public UnityEngine.UI.Image E_PreviousImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PreviousImage == null )
     			{
		    		this.m_E_PreviousImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bottom/E_Previous");
     			}
     			return this.m_E_PreviousImage;
     		}
     	}

		public TMPro.TextMeshProUGUI E_PageTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PageTextMeshProUGUI == null )
     			{
		    		this.m_E_PageTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"Bottom/E_Page");
     			}
     			return this.m_E_PageTextMeshProUGUI;
     		}
     	}

		public UnityEngine.UI.Button E_NextButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextButton == null )
     			{
		    		this.m_E_NextButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bottom/E_Next");
     			}
     			return this.m_E_NextButton;
     		}
     	}

		public UnityEngine.UI.Image E_NextImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextImage == null )
     			{
		    		this.m_E_NextImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bottom/E_Next");
     			}
     			return this.m_E_NextImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.m_E_TopButtonToggleGroup = null;
			this.m_E_WeaponToggle = null;
			this.m_E_WeaponImage = null;
			this.m_E_ArmorToggle = null;
			this.m_E_ArmorImage = null;
			this.m_E_RingToggle = null;
			this.m_E_RingImage = null;
			this.m_E_PropToggle = null;
			this.m_E_PropImage = null;
			this.m_E_BagItemListImage = null;
			this.m_E_BagItemListLoopVerticalScrollRect = null;
			this.m_E_PreviousButton = null;
			this.m_E_PreviousImage = null;
			this.m_E_PageTextMeshProUGUI = null;
			this.m_E_NextButton = null;
			this.m_E_NextImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		private UnityEngine.UI.ToggleGroup m_E_TopButtonToggleGroup = null;
		private UnityEngine.UI.Toggle m_E_WeaponToggle = null;
		private UnityEngine.UI.Image m_E_WeaponImage = null;
		private UnityEngine.UI.Toggle m_E_ArmorToggle = null;
		private UnityEngine.UI.Image m_E_ArmorImage = null;
		private UnityEngine.UI.Toggle m_E_RingToggle = null;
		private UnityEngine.UI.Image m_E_RingImage = null;
		private UnityEngine.UI.Toggle m_E_PropToggle = null;
		private UnityEngine.UI.Image m_E_PropImage = null;
		private UnityEngine.UI.Image m_E_BagItemListImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_BagItemListLoopVerticalScrollRect = null;
		private UnityEngine.UI.Button m_E_PreviousButton = null;
		private UnityEngine.UI.Image m_E_PreviousImage = null;
		private TMPro.TextMeshProUGUI m_E_PageTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_NextButton = null;
		private UnityEngine.UI.Image m_E_NextImage = null;
		public Transform uiTransform = null;
	}
}
