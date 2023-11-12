
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgMainViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image EXPImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EXPImage == null )
     			{
		    		this.m_EXPImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"HeaderBar/EXP");
     			}
     			return this.m_EXPImage;
     		}
     	}

		public TMPro.TextMeshProUGUI E_ExpTextTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExpTextTextMeshProUGUI == null )
     			{
		    		this.m_E_ExpTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"HeaderBar/EXP/E_ExpText");
     			}
     			return this.m_E_ExpTextTextMeshProUGUI;
     		}
     	}

		public TMPro.TextMeshProUGUI E_GoldTextTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GoldTextTextMeshProUGUI == null )
     			{
		    		this.m_E_GoldTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"HeaderBar/Gold/E_GoldText");
     			}
     			return this.m_E_GoldTextTextMeshProUGUI;
     		}
     	}

		public TMPro.TextMeshProUGUI E_LevelTextTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LevelTextTextMeshProUGUI == null )
     			{
		    		this.m_E_LevelTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"HeaderBar/Player/E_LevelText");
     			}
     			return this.m_E_LevelTextTextMeshProUGUI;
     		}
     	}

		public UnityEngine.UI.Button E_TestButtonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TestButtonButton == null )
     			{
		    		this.m_E_TestButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomBar/E_TestButton");
     			}
     			return this.m_E_TestButtonButton;
     		}
     	}

		public UnityEngine.UI.Image E_TestButtonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TestButtonImage == null )
     			{
		    		this.m_E_TestButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomBar/E_TestButton");
     			}
     			return this.m_E_TestButtonImage;
     		}
     	}

		public UnityEngine.UI.Button E_RoleInfoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleInfoButton == null )
     			{
		    		this.m_E_RoleInfoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomBar/E_RoleInfo");
     			}
     			return this.m_E_RoleInfoButton;
     		}
     	}

		public UnityEngine.UI.Image E_RoleInfoImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleInfoImage == null )
     			{
		    		this.m_E_RoleInfoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomBar/E_RoleInfo");
     			}
     			return this.m_E_RoleInfoImage;
     		}
     	}

		public UnityEngine.UI.Button E_AdventureButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AdventureButton == null )
     			{
		    		this.m_E_AdventureButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BottomBar/E_Adventure");
     			}
     			return this.m_E_AdventureButton;
     		}
     	}

		public UnityEngine.UI.Image E_AdventureImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AdventureImage == null )
     			{
		    		this.m_E_AdventureImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BottomBar/E_Adventure");
     			}
     			return this.m_E_AdventureImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EXPImage = null;
			this.m_E_ExpTextTextMeshProUGUI = null;
			this.m_E_GoldTextTextMeshProUGUI = null;
			this.m_E_LevelTextTextMeshProUGUI = null;
			this.m_E_TestButtonButton = null;
			this.m_E_TestButtonImage = null;
			this.m_E_RoleInfoButton = null;
			this.m_E_RoleInfoImage = null;
			this.m_E_AdventureButton = null;
			this.m_E_AdventureImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_EXPImage = null;
		private TMPro.TextMeshProUGUI m_E_ExpTextTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_GoldTextTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_LevelTextTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_TestButtonButton = null;
		private UnityEngine.UI.Image m_E_TestButtonImage = null;
		private UnityEngine.UI.Button m_E_RoleInfoButton = null;
		private UnityEngine.UI.Image m_E_RoleInfoImage = null;
		private UnityEngine.UI.Button m_E_AdventureButton = null;
		private UnityEngine.UI.Image m_E_AdventureImage = null;
		public Transform uiTransform = null;
	}
}
