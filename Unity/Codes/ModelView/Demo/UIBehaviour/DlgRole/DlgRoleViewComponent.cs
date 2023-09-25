
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgRoleViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button EButton_EnterButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_EnterButton == null )
     			{
		    		this.m_EButton_EnterButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/EButton_Enter");
     			}
     			return this.m_EButton_EnterButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_EnterImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_EnterImage == null )
     			{
		    		this.m_EButton_EnterImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/EButton_Enter");
     			}
     			return this.m_EButton_EnterImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_CreateButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CreateButton == null )
     			{
		    		this.m_EButton_CreateButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/EButton_Create");
     			}
     			return this.m_EButton_CreateButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_CreateImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CreateImage == null )
     			{
		    		this.m_EButton_CreateImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/EButton_Create");
     			}
     			return this.m_EButton_CreateImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_DeleteButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_DeleteButton == null )
     			{
		    		this.m_EButton_DeleteButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/EButton_Delete");
     			}
     			return this.m_EButton_DeleteButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_DeleteImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_DeleteImage == null )
     			{
		    		this.m_EButton_DeleteImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/EButton_Delete");
     			}
     			return this.m_EButton_DeleteImage;
     		}
     	}

		public TMPro.TMP_InputField E_RoleNameTMP_InputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleNameTMP_InputField == null )
     			{
		    		this.m_E_RoleNameTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject,"Sprite_BackGround/E_RoleName");
     			}
     			return this.m_E_RoleNameTMP_InputField;
     		}
     	}

		public UnityEngine.UI.Image E_RoleNameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleNameImage == null )
     			{
		    		this.m_E_RoleNameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/E_RoleName");
     			}
     			return this.m_E_RoleNameImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect E_RoleInfoListLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleInfoListLoopHorizontalScrollRect == null )
     			{
		    		this.m_E_RoleInfoListLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"Sprite_BackGround/E_RoleInfoList");
     			}
     			return this.m_E_RoleInfoListLoopHorizontalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_EnterButton = null;
			this.m_EButton_EnterImage = null;
			this.m_EButton_CreateButton = null;
			this.m_EButton_CreateImage = null;
			this.m_EButton_DeleteButton = null;
			this.m_EButton_DeleteImage = null;
			this.m_E_RoleNameTMP_InputField = null;
			this.m_E_RoleNameImage = null;
			this.m_E_RoleInfoListLoopHorizontalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButton_EnterButton = null;
		private UnityEngine.UI.Image m_EButton_EnterImage = null;
		private UnityEngine.UI.Button m_EButton_CreateButton = null;
		private UnityEngine.UI.Image m_EButton_CreateImage = null;
		private UnityEngine.UI.Button m_EButton_DeleteButton = null;
		private UnityEngine.UI.Image m_EButton_DeleteImage = null;
		private TMPro.TMP_InputField m_E_RoleNameTMP_InputField = null;
		private UnityEngine.UI.Image m_E_RoleNameImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_E_RoleInfoListLoopHorizontalScrollRect = null;
		public Transform uiTransform = null;
	}
}
