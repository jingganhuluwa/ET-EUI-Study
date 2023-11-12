// 文件：UnitDBSaveComponent.cs
// 作者：xj
// 描述：
// 日期：2023/11/12 7:14

using System;
using System.Collections.Generic;

namespace ET
{
    public class UnitDBSaveComponent: Entity, IAwake, IDestroy
    {
        public HashSet<Type> EntityChangeTypeSet { get; } = new HashSet<Type>();
        public long Timer;
    }
}