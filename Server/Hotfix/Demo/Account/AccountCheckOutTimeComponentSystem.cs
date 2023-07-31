// 文件：AccountCheckOutTimeComponentSystem.cs
// 作者：xj
// 描述：超时断连
// 日期：2023/07/31 20:58

using System;

namespace ET
{
    [Timer(TimerType.AccountSessionCheckOutTime)]
    public class AccountCheckOutTimer: ATimer<AccountCheckOutTimeComponent>
    {
        public override void Run(AccountCheckOutTimeComponent self)
        {
            try
            {
                self.DeleteSession();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }

    public class AccountCheckOutTimeComponentAwakeSystem: AwakeSystem<AccountCheckOutTimeComponent, long>
    {
        public override void Awake(AccountCheckOutTimeComponent self, long accountId)
        {
            self.AccountId = accountId;
            TimerComponent.Instance.Remove(ref self.Timer);
            self.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 10 * TimeHelper.Minute, TimerType.AccountSessionCheckOutTime,
                self);
        }
    }
    
    public class AccountCheckOutTimeComponentDestroySystem:DestroySystem<AccountCheckOutTimeComponent>
    {
        public override void Destroy(AccountCheckOutTimeComponent self)
        {
            self.AccountId = 0;
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }

    [FriendClassAttribute(typeof (ET.AccountCheckOutTimeComponent))]
    public static class AccountCheckOutTimeComponentSystem
    {
        public static void DeleteSession(this AccountCheckOutTimeComponent self)
        {
            Session session = self.GetParent<Session>();
            long sessionInstanceId = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(self.AccountId);
            if (session?.InstanceId == sessionInstanceId)
            {
                session.DomainScene().GetComponent<AccountSessionsComponent>().Remove(self.AccountId);
            }

            session?.Send(new A2C_Disconnect() { Error = 1 });
            session?.Disconnect().Coroutine();
        }
    }
}