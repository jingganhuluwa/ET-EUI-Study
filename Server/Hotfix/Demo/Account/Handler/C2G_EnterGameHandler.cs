// 文件：C2G_EnterGameHandler.cs
// 作者：xj
// 描述：
// 日期：2023/11/09 18:21

using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.SessionPlayerComponent))]
    [FriendClassAttribute(typeof(ET.SessionStateComponent))]
    [FriendClassAttribute(typeof(ET.GateMapComponent))]
    public class C2G_EnterGameHandler : AMRpcHandler<C2G_EnterGame, G2C_EnterGame>
    {
        protected override async ETTask Run(Session session, C2G_EnterGame request, G2C_EnterGame response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Gate)
            {
                Log.Error($"请求Scene错误,当前Scene为:{session.DomainScene().SceneType} ,非 SceneType.Gate");
                session.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.Err_RequestRepeatedly;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            SessionPlayerComponent sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
            if (sessionPlayerComponent == null)
            {
                response.Error = ErrorCode.Err_SessionPlayerError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            Player player = Game.EventSystem.Get(sessionPlayerComponent.PlayerInstanceId) as Player;
            if (player == null || player.IsDisposed)
            {
                response.Error = ErrorCode.Err_NonePlayerError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            long instanceId = session.InstanceId;
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, player.AccountId.GetHashCode()))
                {
                    if (instanceId != session.InstanceId || player.IsDisposed)
                    {
                        response.Error = ErrorCode.Err_PlayerSessionError;
                        reply();
                        session.Disconnect().Coroutine();
                        return;
                    }

                    if (session.GetComponent<SessionStateComponent>() != null && session.GetComponent<SessionStateComponent>().State == SessionState.Game)
                    {
                        response.Error = ErrorCode.ERR_SessionStateError;
                        session.Disconnect().Coroutine();
                        reply();
                        return;
                    }


                    if (player.PlayerState == PlayerState.Game)
                    {
                        try
                        {
                            IActorResponse reqEnter = await MessageHelper.CallLocationActor(player.UnitId, new G2M_RequestEnterGameState());
                            if (reqEnter.Error == ErrorCode.ERR_Success)
                            {
                                reply();
                                return;
                            }
                            Log.Error("二次登录失败  " + reqEnter.Error + " | " + reqEnter.Message);
                            response.Error = ErrorCode.ERR_ReEnterGameError;
                            await DisconnectHelper.KickPlayer(player, true);
                            reply();
                            session?.Disconnect().Coroutine();
                        }
                        catch (Exception e)
                        {
                            Log.Error("二次登录失败  " + e.ToString());
                            response.Error = ErrorCode.ERR_ReEnterGameError2;
                            await DisconnectHelper.KickPlayer(player, true);
                            reply();
                            session?.Disconnect().Coroutine();
                            throw;
                        }
                        return;
                    }

                    try
                    {

                        // GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
                        // gateMapComponent.Scene = await SceneFactory.Create(gateMapComponent, "GateMap", SceneType.Map);

                        //从数据库或缓存中加载出Unit实体及其相关组件
                        (bool isNewPlayer,Unit unit)=await UnitHelper.LoadUnit(player);
                        unit.AddComponent<UnitGateComponent, long>(player.InstanceId);
                        
                        //玩家Unit上线后初始化操作
                        await UnitHelper.InitUnit(unit, isNewPlayer);
                        
                        //Unit unit = UnitFactory.Create(gateMapComponent.Scene, player.Id, UnitType.Player);
                        //unit.AddComponent<UnitGateComponent, long>(session.InstanceId);
                        long unitId = unit.Id;


                        StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Game");
                        await TransferHelper.Transfer(unit, startSceneConfig.InstanceId, startSceneConfig.Name);


                        player.UnitId = unitId;
                        response.MyId = unitId;

                        reply();

                        SessionStateComponent SessionStateComponent = session.GetComponent<SessionStateComponent>();
                        if (SessionStateComponent == null)
                        {
                            SessionStateComponent = session.AddComponent<SessionStateComponent>();
                        }
                        SessionStateComponent.State = SessionState.Game;

                        player.PlayerState = PlayerState.Game;
                    }
                    catch (Exception e)
                    {

                        Log.Error($"角色进入游戏逻辑服出现问题 账号Id: {player.AccountId}  角色Id: {player.Id}   异常信息： {e.ToString()}");
                        response.Error = ErrorCode.ERR_EnterGameError;
                        reply();
                        await DisconnectHelper.KickPlayer(player, true);
                        session.Disconnect().Coroutine();

                    }

                }
            }
        }
    }
}