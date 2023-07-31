// 文件：C2A_LoginAccountHandler.cs
// 作者：xj
// 描述：
// 日期：2023/07/30 7:49

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ET
{
    [FriendClassAttribute(typeof(Account))]
    public class C2A_LoginAccountHandler : AMRpcHandler<C2A_LoginAccount, A2c_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2c_LoginAccount response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求Scene错误,当前Scene为:{session.DomainScene().SceneType} ,非 SceneType.Account");
                session.Dispose();
                return;
            }

            //移除验证超时组件
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            //校验客户端传来的帐号和密码是否为空
            if (string.IsNullOrEmpty(request.AccountName.Trim()) || string.IsNullOrEmpty(request.Password.Trim()))
            {
                response.Error = ErrorCode.Err_LoginInfoError;
                reply();
                session.Dispose();
                return;
            }

            //校验客户端传来的帐号格式
            if (Regex.IsMatch(request.AccountName.Trim(), @"^[a-zA-Z0-9]{6,15}$"))
            {
                response.Error = ErrorCode.Err_LoginInfoError;
                reply();
                session.Dispose();
                return;
            }

            //校验客户端传来的密码格式
            if (Regex.IsMatch(request.Password.Trim(), @"^[a-zA-Z0-9]{6,15}$"))
            {
                response.Error = ErrorCode.Err_LoginInfoError;
                reply();
                session.Dispose();
                return;
            }

            List<Account> accountInfoList = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                    .Query<Account>(d => d.AccountName.Equals(request.AccountName.Trim()));

            Account account = null;
            if (accountInfoList.Count>0)
            {
                account = accountInfoList[0];
                //检验帐号黑名单和密码
                if (account.AccountType ==(int)AccountType.BlackList)
                {
                    response.Error = ErrorCode.Err_LoginInfoError;
                    reply();
                    session.Dispose();
                    return;
                }

                if (!account.Password.Equals(request.Password.Trim()))
                {
                    response.Error = ErrorCode.Err_LoginInfoError;
                    reply();
                    session.Dispose();
                    return;
                }

                session.AddChild(account);
            }
            else
            {
                //数据库没信息则表示第一次登陆,创建新帐号
                account = session.AddChild<Account>();
                account.AccountName = request.AccountName.Trim();
                //todo 加密
                account.Password = request.Password.Trim();
                account.CreateTime = TimeHelper.ServerNow();
                account.AccountType =(int) AccountType.General;
                await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                        .Save<Account>(account);

            }
            
            //设置token,返回
            string token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue);
            session.DomainScene().GetComponent<TokenComponent>().Remove(account.Id);
            session.DomainScene().GetComponent<TokenComponent>().Add(account.Id,token);

            response.AccountId = account.Id;
            response.Token = token;

            reply();
            

        }
    }
}