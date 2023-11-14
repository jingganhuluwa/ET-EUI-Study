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
            self?.PlayOneBattleRound().Coroutine();
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
        
        
         public static async ETTask  PlayOneBattleRound(this AdventureComponent self)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
            if ( self.Round % 2 == 0 )
            {
                //玩家回合
                Unit monsterUnit = self.GetTargetMonsterUnit();
                Game.EventSystem.PublishAsync(new EventType.AdventureBattleRoundView()
                {
                    ZoneScene = self.ZoneScene(), AttackUnit = unit, TargetUnit = monsterUnit
                }).Coroutine();
                
                await Game.EventSystem.PublishAsync(new EventType.AdventureBattleRound()
                {
                    ZoneScene = self.ZoneScene(), AttackUnit = unit, TargetUnit = monsterUnit
                });
                
                await TimerComponent.Instance.WaitAsync(1000);
            }
            else
            {
                //敌人回合
                for ( int i = 0; i < self.EnemyIdList.Count; i++ )
                {
                    if (!unit.IsAlive())
                    {
                        break;
                    }
                    
                    Unit monsterUnit = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);

                    if (!monsterUnit.IsAlive())
                    {
                        continue;
                    }
                    
                    Game.EventSystem.PublishAsync(new EventType.AdventureBattleRoundView()
                    {
                        ZoneScene = self.ZoneScene(), AttackUnit = monsterUnit, TargetUnit = unit
                    }).Coroutine();
                    
                    await Game.EventSystem.PublishAsync(new EventType.AdventureBattleRound()
                    {
                        ZoneScene = self.ZoneScene(), AttackUnit = monsterUnit, TargetUnit = unit
                    });
                    
                    await TimerComponent.Instance.WaitAsync(1000);
                }
            }
            
            self.BattleRoundOver();
        }
         
          public static  void  BattleRoundOver(this AdventureComponent self)
        {
            ++self.Round;
            BattleRoundResult battleRoundResult = self.GetBattleRoundResult();
            Log.Debug("当前回合结果:" + battleRoundResult);
            switch ( battleRoundResult )
            {
                case BattleRoundResult.KeepBattle:
                {
                    self.BattleTimer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 500, TimerType.BattleRound, self);
                }
                    break;
                case BattleRoundResult.WinBattle:
                {
                    Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
                    Game.EventSystem.PublishAsync(new EventType.AdventureBattleOver() { ZoneScene = self.ZoneScene(), WinUnit = unit }).Coroutine();
                }
                    break;
                case BattleRoundResult.LoseBattle:
                {
                    for (int i = 0; i < self.EnemyIdList.Count; i++)
                    {
                        Unit monsterUnit = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);
                        if (!monsterUnit.IsAlive())
                        {
                            continue;
                        }
                        Game.EventSystem.PublishAsync(new EventType.AdventureBattleOver() { ZoneScene = self.ZoneScene(), WinUnit = monsterUnit }).Coroutine();
                    }
                }
                    break;
            }
            
            Game.EventSystem.PublishAsync(new EventType.AdventureBattleReport()
            {
                Round = self.Round, BattleRoundResult = battleRoundResult, ZoneScene = self.ZoneScene()
            }).Coroutine();
        }
        
        public static Unit GetTargetMonsterUnit(this AdventureComponent self)
        {
           self.AliveEnemyIdList.Clear();
           for ( int i = 0; i < self.EnemyIdList.Count; i++ )
           {
               Unit monsterUnit = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);
               if ( monsterUnit.IsAlive() )
               {
                   self.AliveEnemyIdList.Add(monsterUnit.Id);
               }
           }

           if ( self.AliveEnemyIdList.Count <= 0 )
           {
               return null;
           }
           return self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.AliveEnemyIdList[0]);
        }
        
        public static BattleRoundResult GetBattleRoundResult(this AdventureComponent self)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
            if ( !unit.IsAlive() )
            {
                return BattleRoundResult.LoseBattle;
            }
            
            Unit monsterUnit = self.GetTargetMonsterUnit();
            if ( monsterUnit == null )
            {
                return BattleRoundResult.WinBattle;
            }
            
            return BattleRoundResult.KeepBattle;
        }
    }
}