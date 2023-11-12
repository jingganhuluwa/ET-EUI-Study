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
        public const int ERR_NetWorkError = 200002;

        /// <summary>
        /// 帐号密码存在空
        /// </summary>
        public const int ERR_LoginInfoIsNull = 200003;

        /// <summary>
        /// 账号格式错误
        /// </summary>
        public const int ERR_AccountNameFormError = 200004;

        /// <summary>
        /// 密码格式错误
        /// </summary>
        public const int ERR_PasswordFormError = 200005;

        /// <summary>
        /// 帐号处于黑名单
        /// </summary>
        public const int ERR_AccountInBlackList = 200006;

        /// <summary>
        /// 登陆密码错误
        /// </summary>
        public const int ERR_LoginPasswordError = 200007;

        /// <summary>
        /// 请求重复
        /// </summary>
        public const int ERR_RequestRepeatedly = 200008;
        
        /// <summary>
        /// 令牌Token错误
        /// </summary>
        public const int ERR_TokenError = 200009;
        
        /// <summary>
        /// 角色名为空
        /// </summary>
        public const int ERR_RoleNameIsNull = 200010;
        
        /// <summary>
        /// 角色名已存在
        /// </summary>
        public const int ERR_RoleNameIsExist = 200011;
        
        /// <summary>
        /// 角色不存在
        /// </summary>
        public const int ERR_RoleNoExist = 200012;
        
        /// <summary>
        /// 请求Scene错误
        /// </summary>
        public const int ERR_RequestSceneTypeError = 200013; 
        
        /// <summary>
        /// Gate Key验证失败
        /// </summary>
        public const int ERR_ConnectGateKeyError = 200014;
        
        /// <summary>
        /// 帐号在其他地方登陆
        /// </summary>
        public const int ERR_OtherAccountLogin = 200015;
        
        /// <summary>
        /// SessionPlayer错误
        /// </summary>
        public const int ERR_SessionPlayerError = 200016;
        
        /// <summary>
        /// 无Player错误
        /// </summary>
        public const int ERR_NonePlayerError = 200017;
        
        /// <summary>
        /// PlayerSession错误
        /// </summary>
        public const int ERR_PlayerSessionError = 200018;
        
        /// <summary>
        /// Session状态错误
        /// </summary>
        public const int ERR_SessionStateError = 200019;
        
        /// <summary>
        /// 重进错误
        /// </summary>
        public const int ERR_ReEnterGameError = 200020;
        
        /// <summary>
        /// 二次登陆错误
        /// </summary>
        public const int ERR_ReEnterGameError2 = 200021;
        
        /// <summary>
        /// 角色进入逻辑服错误
        /// </summary>
        public const int ERR_EnterGameError = 200022;
        
        /// <summary>
        /// 数值类型不存在
        /// </summary>
        public const int ERR_NumericTypeNotExist = 200023;
        
        /// <summary>
        /// 数值类型不可加点
        /// </summary>
        public const int ERR_NumericTypeNotAddPoint = 200024; 
        /// <summary>
        /// 属性点不足
        /// </summary>
        public const int ERR_AddPointNotEnough = 200025;
        
        
        public const int ERR_AlreadyAdventureState = 200027;
        
        public const int ERR_AdventureInDying = 200028;
        
        public const int ERR_AdventureErrorLevel = 200029;
        
        public const int ERR_AdventureLevelNotEnough = 200030;
    }
}