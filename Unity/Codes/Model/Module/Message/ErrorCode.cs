namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误

        // 110000以下的错误请看ErrorCore.cs

        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常

        /// <summary>
        /// 网络错误
        /// </summary>
        public const int Err_NetWorkError = 200002;

        /// <summary>
        /// 帐号密码存在空
        /// </summary>
        public const int Err_LoginInfoIsNull = 200003;

        /// <summary>
        /// 账号格式错误
        /// </summary>
        public const int Err_AccountNameFormError = 200004;

        /// <summary>
        /// 密码格式错误
        /// </summary>
        public const int Err_PasswordFormError = 200005;

        /// <summary>
        /// 帐号处于黑名单
        /// </summary>
        public const int Err_AccountInBlackList = 200006;

        /// <summary>
        /// 登陆密码错误
        /// </summary>
        public const int Err_LoginPasswordError = 200007;

        /// <summary>
        /// 请求重复
        /// </summary>
        public const int Err_RequestRepeatedly = 200008;
        
        /// <summary>
        /// 令牌Token错误
        /// </summary>
        public const int Err_TokenError = 200009;
    }
}