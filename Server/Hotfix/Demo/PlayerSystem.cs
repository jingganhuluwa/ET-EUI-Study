﻿namespace ET
{
    [FriendClass(typeof(Player))]
    public static class PlayerSystem
    {
        [ObjectSystem]
        public class PlayerAwakeSystem : AwakeSystem<Player, long,long>
        {
            public override void Awake(Player self, long account,long roleId)
            {
                self.Account = account;
                self.UnitId = roleId;
            }
        }
        
        
        [ObjectSystem]
        public class PlayerDestroySystem : DestroySystem<Player>
        {
            public override void Destroy(Player self)
            {
                self.Account = 0;
                self.UnitId    = 0;
                self.PlayerState = PlayerState.Disconnect;
            }
        }
    }
}