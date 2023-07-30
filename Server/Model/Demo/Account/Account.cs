// 文件：Account.cs
// 作者：xj
// 描述：
// 日期：2023/07/30 7:25

namespace ET
{
    public enum AccountType
    {
        /// <summary>
        /// 正常
        /// </summary>
        General = 0,

        /// <summary>
        /// 黑名单
        /// </summary>
        BlackList = 1
    }

    [ComponentOf(typeof (Scene))]
    public class Account: Entity, IAwake
    {
        /// <summary>
        /// 帐号名称
        /// </summary>
        public string AccountName;

        /// <summary>
        /// 帐号密码
        /// </summary>
        public string Password;

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime;

        /// <summary>
        /// 帐号类型
        /// </summary>
        public int AccountType;
    }
}