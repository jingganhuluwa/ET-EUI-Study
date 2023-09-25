// 文件：RoleInfosComponent.cs
// 作者：xj
// 描述：
// 日期：2023/09/04 7:12

using System.Collections.Generic;

namespace ET
{
    [ChildType(typeof(RoleInfo))]
    [ComponentOf(typeof(Scene))]
    public class RoleInfosComponent:Entity,IAwake,IDestroy
    {
        public List<RoleInfo> RoleInfoList = new List<RoleInfo>();
        public long CurrentRoleId = 0;
    }
}