
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class ES_AttributeItem : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy 
	{
		public TMPro.TextMeshProUGUI EAttributeNameTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EAttributeNameTextMeshProUGUI == null )
     			{
		    		this.m_EAttributeNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EAttributeName");
     			}
     			return this.m_EAttributeNameTextMeshProUGUI;
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
     			if( this.m_EAttributeValueTextMeshProUGUI == null )
     			{
		    		this.m_EAttributeValueTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EAttributeValue");
     			}
     			return this.m_EAttributeValueTextMeshProUGUI;
     		}
     	}

		public UnityEngine.UI.Button E_AddButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddButton == null )
     			{
		    		this.m_E_AddButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Add");
     			}
     			return this.m_E_AddButton;
     		}
     	}

		public UnityEngine.UI.Image E_AddImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddImage == null )
     			{
		    		this.m_E_AddImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Add");
     			}
     			return this.m_E_AddImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EAttributeNameTextMeshProUGUI = null;
			this.m_EAttributeValueTextMeshProUGUI = null;
			this.m_E_AddButton = null;
			this.m_E_AddImage = null;
			this.uiTransform = null;
		}

		private TMPro.TextMeshProUGUI m_EAttributeNameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_EAttributeValueTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_AddButton = null;
		private UnityEngine.UI.Image m_E_AddImage = null;
		public Transform uiTransform = null;
	}
}
