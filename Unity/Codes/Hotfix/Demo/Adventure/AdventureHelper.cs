// 文件：AdventureHelper.cs
// 作者：xj
// 描述：
// 日期：2023/11/12 23:46

using System;

namespace ET
{
    public static class AdventureHelper
    {
        public static async ETTask<int> RequestStartGameLevel(Scene zoneScene, int levelId)
        {
            M2C_StartGameLevel m2CStartGameLvel = null;
            try
            {
                m2CStartGameLvel  =  (M2C_StartGameLevel) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_StartGameLevel() {LevelId = levelId});
            }
            catch (Exception e)
            {
                Log.Error(e);
                return ErrorCode.ERR_NetWorkError;
            }

            if (m2CStartGameLvel.Error != ErrorCode.ERR_Success)
            {
                Log.Error(m2CStartGameLvel.Error.ToString());
                return m2CStartGameLvel.Error;
            }
            return ErrorCode.ERR_Success;
        }
    }
}