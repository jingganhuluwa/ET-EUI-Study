using System.Collections.Generic;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgBag :Entity,IAwake,IUILogic
	{

		public DlgBagViewComponent View { get => this.Parent.GetComponent<DlgBagViewComponent>();} 

		public Dictionary<int, Scroll_Item_BagItem> ScrollItemBagItemDict;
		
		public ItemType CurrentItemType;

		public int CurrentPageIndex = 0;

	}
}
