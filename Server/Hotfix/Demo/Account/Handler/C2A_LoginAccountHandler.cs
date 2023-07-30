// 文件：C2A_LoginAccountHandler.cs
// 作者：xj
// 描述：
// 日期：2023/07/30 7:49

using System;

namespace ET
{
    public class C2A_LoginAccountHandler:AMRpcHandler<C2A_LoginAccount,A2c_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2c_LoginAccount response, Action reply)
        {
            
            
            
            await ETTask.CompletedTask;
        }
    }
}