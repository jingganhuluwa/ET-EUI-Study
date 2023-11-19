
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_EntryDestroySystem : DestroySystem<Scroll_Item_Entry> 
	{
		public override void Destroy( Scroll_Item_Entry self )
		{
			self.DestroyWidget();
		}
	}
}
