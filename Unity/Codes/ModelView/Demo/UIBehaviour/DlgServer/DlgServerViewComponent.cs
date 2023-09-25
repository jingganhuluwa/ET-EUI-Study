
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgServerViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Text ELabel_EnterTextText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_EnterTextText == null )
     			{
		    		this.m_ELabel_EnterTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Sprite_BackGround/EButton_Enter/ELabel_EnterText");
     			}
     			return this.m_ELabel_EnterTextText;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_ServerInfoLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ServerInfoLoopVerticalScrollRect == null )
     			{
		    		this.m_E_ServerInfoLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"Sprite_BackGround/E_ServerInfo");
     			}
     			return this.m_E_ServerInfoLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_EnterButton = null;
			this.m_EButton_EnterImage = null;
			this.m_ELabel_EnterTextText = null;
			this.m_E_ServerInfoLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButton_EnterButton = null;
		private UnityEngine.UI.Image m_EButton_EnterImage = null;
		private UnityEngine.UI.Text m_ELabel_EnterTextText = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_ServerInfoLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
