// 文件：AdventureRoundResetEvent_ResetAnimation.cs
// 作者：xj
// 描述：
// 日期：2023/11/14 5:44

using ET.EventType;

namespace ET
{
    public class AdventureRoundResetEvent_ResetAnimation:AEventAsync<EventType.AdventureRoundReset>
    {
        protected override async ETTask Run(AdventureRoundReset args)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(args.ZoneScene.CurrentScene());
            unit.GetComponent<AnimatorComponent>()?.Play(MotionType.Idle);

            await ETTask.CompletedTask;
        }
    }
}