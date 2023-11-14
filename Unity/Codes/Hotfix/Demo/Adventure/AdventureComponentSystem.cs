// 文件：AdventureComponentSystem.cs
// 作者：xj
// 描述：
// 日期：2023/11/13 23:36

using UnityEngine;

namespace ET
{
    [Timer(TimerType.BattleRound)]
    public class AdventureBattleRoundTimer : ATimer<AdventureComponent>
    {
        public override void Run(AdventureComponent self)
        {
            //self?.PlayOneBattleRound().Coroutine();
        }
    }
    
    
    public class AdventureComponentDestroySystem : DestroySystem<AdventureComponent>
    {
        public override void Destroy(AdventureComponent self)
        {
            TimerComponent.Instance?.Remove(ref self.BattleTimer);
            self.BattleTimer = 0;
            self.Round = 0;
            self.EnemyIdList.Clear();
            self.AliveEnemyIdList.Clear();
            //self.Random = null;
        }
    }
    
    
    [FriendClassAttribute(typeof(ET.AdventureComponent))]
    public static class AdventureComponentSystem
    {
        public static void ResetAdventure(this AdventureComponent self)
        {
            foreach (long enemyId in self.EnemyIdList)
            {
                self.ZoneScene().CurrentScene().GetComponent<UnitComponent>()?.Remove(enemyId);
            }

            TimerComponent.Instance.Remove(ref self.BattleTimer);
            self.BattleTimer = 0;
            self.Round = 0;
            self.EnemyIdList.Clear();
            self.AliveEnemyIdList.Clear();

            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int maxHP = numericComponent.GetAsInt(NumericType.MaxHp);
            numericComponent.Set(NumericType.Hp,maxHP);
            numericComponent.Set(NumericType.IsAlive,1);
            
            Game.EventSystem.PublishAsync(new EventType.AdventureRoundReset() {ZoneScene = self.ZoneScene()}).Coroutine();
        }

        public static async ETTask StartAdventure(this AdventureComponent self)
        {
            self.ResetAdventure();
            await self.CreateAdventureEnemy();
            //self.ShowAdventureHpBarInfo(true);
            self.BattleTimer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 500, TimerType.BattleRound, self);
        }
        
        public static async ETTask CreateAdventureEnemy(this AdventureComponent self)
        {
            //根据关卡ID创建出怪物
            Unit unit   = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
            int levelId = unit.GetComponent<NumericComponent>().GetAsInt(NumericType.AdventureState);

            BattleLevelConfig battleLevelConfig = BattleLevelConfigCategory.Instance.Get(levelId);
            for ( int i = 0; i < battleLevelConfig.MonsterIds.Length; i++ )
            {
                Unit monsterUnit     = await UnitFactory.CreateMonster(self.ZoneScene().CurrentScene(), battleLevelConfig.MonsterIds[i]);
                monsterUnit.Position = new Vector3(4f, -2+i*2, 0);
                self.EnemyIdList.Add(monsterUnit.Id);
            }
        }
        
    }
}