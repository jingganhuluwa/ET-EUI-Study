﻿
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

		public void DestroyWidget()
		{
			this.m_EXPImage = null;
			this.m_E_ExpTextTextMeshProUGUI = null;
			this.m_E_GoldTextTextMeshProUGUI = null;
			this.m_E_LevelTextTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_EXPImage = null;
		private TMPro.TextMeshProUGUI m_E_ExpTextTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_GoldTextTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_LevelTextTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
