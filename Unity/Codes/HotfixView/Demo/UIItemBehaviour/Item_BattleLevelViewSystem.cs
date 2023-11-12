
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_BattleLevelDestroySystem : DestroySystem<Scroll_Item_BattleLevel> 
	{
		public override void Destroy( Scroll_Item_BattleLevel self )
		{
			self.DestroyWidget();
		}
	}
}
