using System;

namespace ET
{
    [FriendClass(typeof (AccountInfoComponent))]
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2C_LoginAccount a2CLoginAccount = null;
            Session accountSession = null;
            try
            {
                accountSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                //MD5加密密码
                password = MD5Helper.StringMD5(password);
                a2CLoginAccount = (A2C_LoginAccount) await accountSession.Call(new C2A_LoginAccount() { AccountName = account, Password = password });
            }
            catch (Exception e)
            {
                accountSession?.Dispose();
                Log.Error(e.ToString());
                return ErrorCode.Err_NetWorkError;
            }

            if (a2CLoginAccount.Error != ErrorCode.ERR_Success)
            {
                accountSession?.Dispose();
                return a2CLoginAccount.Error;
            }

            //成功,保存连接session
            zoneScene.AddComponent<SessionComponent>().Session = accountSession;
            //添加PingComponent组件,心跳检测机制,隔段时间发送消息到服务器,防止被下线
            zoneScene.GetComponent<SessionComponent>().Session.AddComponent<PingComponent>();

            zoneScene.GetComponent<AccountInfoComponent>().Token = a2CLoginAccount.Token;
            zoneScene.GetComponent<AccountInfoComponent>().AccountId = a2CLoginAccount.AccountId;

            return ErrorCode.ERR_Success;
        }
    }
}