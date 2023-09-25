// 文件：RoleInfosComponentSystem.cs
// 作者：xj
// 描述：
// 日期：2023/09/04 7:14

namespace ET
{
    public class RoleInfosComponentDestroySystem: DestroySystem<RoleInfosComponent>
    {
        public override void Destroy(RoleInfosComponent self)
        {
            foreach (RoleInfo roleInfo in self.RoleInfoList)
            {
                roleInfo?.Dispose();
            }

            self.RoleInfoList.Clear();
            self.CurrentRoleId = 0;
        }
    }

    public static class RoleInfosComponentSystem
    {
    }
}