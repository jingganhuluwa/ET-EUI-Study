// 文件：C2M_AddAttributePointHandler.cs
// 作者：xj
// 描述：
// 日期：2023/11/12 2:18

using System;

namespace ET
{
    public class C2M_AddAttributePointHandler:AMActorLocationRpcHandler<Unit,C2M_AddAttributePoint,M2C_AddAttributePoint>
    {
        protected override async ETTask Run(Unit unit, C2M_AddAttributePoint request, M2C_AddAttributePoint response, Action reply)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int targetNumericType = request.NumericType;

            if (!PlayerNumericConfigCategory.Instance.Contain(targetNumericType))
            {
                //数值类型不存在
                response.Error = ErrorCode.ERR_NumericTypeNotExist;
                reply();
                return;
            }

            PlayerNumericConfig config = PlayerNumericConfigCategory.Instance.Get(targetNumericType);
            if (config.isAddPoint!=1)
            {
                //不是可加点的属性
                response.Error = ErrorCode.ERR_NumericTypeNotAddPoint;
                reply();
                return;
            }

            int attributePointCount = numericComponent.GetAsInt(NumericType.AttributePoint);
            if (attributePointCount<=0)
            {
                //可加点数不足
                response.Error = ErrorCode.ERR_AddPointNotEnough;
                reply();
                return;
            }

            //可加点数减少
            attributePointCount--;
            numericComponent.Set(NumericType.AttributePoint,attributePointCount);

            //目标点数增加
            int targetAttribute = numericComponent.GetAsInt(targetNumericType)+1;
            numericComponent.Set(targetNumericType,targetAttribute);

            //todo 待优化,定时写入
            //await numericComponent.AddOrUpdateUnitCache(); //存储
            reply();
            await ETTask.CompletedTask;
        }
    }
}