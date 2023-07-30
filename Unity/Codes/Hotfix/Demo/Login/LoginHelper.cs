using System;

namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2c_LoginAccount a2CLoginAccount = null;
            Session accountSession = null;
            try
            {
                accountSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                a2CLoginAccount = (A2c_LoginAccount) await accountSession.Call(new C2A_LoginAccount() { AccountName = account, Password = password });
            }
            catch (Exception e)
            {
                accountSession?.Dispose();
                Log.Error(e.ToString());
                return ErrorCode.Err_NetWorkError;
            }

            if (a2CLoginAccount.Error!=ErrorCode.ERR_Success)
            {
                return a2CLoginAccount.Error;
            }

            //成功,保存连接session
            zoneScene.AddComponent<SessionComponent>().Session = accountSession;

            return ErrorCode.ERR_Success;
        }
    }
}