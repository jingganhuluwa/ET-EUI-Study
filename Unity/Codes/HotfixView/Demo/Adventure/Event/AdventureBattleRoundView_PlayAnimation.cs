﻿using UnityEngine;


namespace ET
{
    [FriendClass(typeof(GameObjectComponent))]
    public class AdventureBattleRoundView_PlayAnimation: AEventAsync<EventType.AdventureBattleRoundView>
    {
        protected override async ETTask Run(EventType.AdventureBattleRoundView args)
        {

            if (!args.AttackUnit.IsAlive() || !args.TargetUnit.IsAlive())
            {
                return;
            }
            
            args.AttackUnit?.GetComponent<AnimatorComponent>().Play(MotionType.Attack);
            args.TargetUnit?.GetComponent<AnimatorComponent>().Play(MotionType.Hurt);

            long instanceId = args.TargetUnit.InstanceId;
            
            foreach (SpriteRenderer spriteRenderer in args.TargetUnit.GetComponent<GameObjectComponent>().SpriteRenderers)
            {
                spriteRenderer.color = Color.red;
            }
            
            await TimerComponent.Instance.WaitAsync(300);

            if (instanceId != args.TargetUnit.InstanceId)
            {
                return;
            }
            
            foreach (SpriteRenderer spriteRenderer in args.TargetUnit.GetComponent<GameObjectComponent>().SpriteRenderers)
            {
                spriteRenderer.color = Color.white;
            }
            
            await ETTask.CompletedTask;
        }
    }
}