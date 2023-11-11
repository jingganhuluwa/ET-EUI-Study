// 文件：NumericHelper.cs
// 作者：xj
// 描述：
// 日期：2023/11/11 14:03

using System;

namespace ET
{
    public static class NumericHelper
    {
        public static async ETTask<int> TestUpdaterNumeric(Scene zoneScene)
        {
            M2C_TestUnitNumeric result;

            try
            {
                result = (M2C_TestUnitNumeric) await zoneScene.GetComponent<SessionComponent>().Session
                        .Call(new C2M_TestUnitNumeric());
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (result.Error != ErrorCode.ERR_Success)
            {
                Log.Error(result.Error.ToString());
                return result.Error;
            }

            return ErrorCode.ERR_Success;
        }
        
        /// <summary>
        /// 加点
        /// </summary>
        public static async ETTask<int> ReqeustAddAttributePoint(Scene zoneScene,int numericType)
        {
            M2C_AddAttributePoint m2CAddAttributePoint = null;
            try
            {
                m2CAddAttributePoint  =  (M2C_AddAttributePoint) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_AddAttributePoint() { NumericType = numericType});
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
                
            }

            if (m2CAddAttributePoint.Error != ErrorCode.ERR_Success)
            {
                Log.Error(m2CAddAttributePoint.Error.ToString());
                return m2CAddAttributePoint.Error;
            }
            return ErrorCode.ERR_Success;
        }
        
        

    }
}