// 文件：C2M_TestUnitNumericHandler.cs
// 作者：xj
// 描述：
// 日期：2023/11/11 14:14

using System;

namespace ET
{
    public class C2M_TestUnitNumericHandler: AMActorLocationRpcHandler<Unit, C2M_TestUnitNumeric, M2C_TestUnitNumeric>
    {
        protected override async ETTask Run(Unit unit, C2M_TestUnitNumeric request, M2C_TestUnitNumeric response, Action reply)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int newGold = numericComponent.GetAsInt(NumericType.Gold) + 100;
            int newExp = numericComponent.GetAsInt(NumericType.Exp) + 100;
            int newLevel = numericComponent.GetAsInt(NumericType.Level) + 1;

            numericComponent.Set(NumericType.Gold, newGold);
            numericComponent.Set(NumericType.Exp, newExp);
            numericComponent.Set(NumericType.Level, newLevel);

            reply();

            await ETTask.CompletedTask;
        }
    }
}