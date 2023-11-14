using UnityEngine;

namespace ET
{
    [FriendClass(typeof (GlobalComponent))]
    [FriendClass(typeof (GameObjectComponent))]
    public class AfterUnitCreate_CreateUnitView: AEventAsync<EventType.AfterUnitCreate>
    {
        protected override async ETTask Run(EventType.AfterUnitCreate args)
        {
            // Unit View层
            // 这里可以改成异步加载，demo就不搞了
            Debug.Log($"{args.Unit.Config.PrefabName}.unity3d");
            //await ResourcesComponent.Instance.LoadBundleAsync($"{args.Unit.Config.PrefabName}.unity3d");
            await ResourcesComponent.Instance.LoadBundleAsync("battleman.unity3d");
            //GameObject bundleGameObject = (GameObject) ResourcesComponent.Instance.GetAsset($"{args.Unit.Config.PrefabName}.unity3d", args.Unit.Config.PrefabName);
            GameObject bundleGameObject = (GameObject) ResourcesComponent.Instance.GetAsset("battleman.unity3d", args.Unit.Config.PrefabName);
            GameObject go = UnityEngine.Object.Instantiate(bundleGameObject);
            go.transform.SetParent(GlobalComponent.Instance.Unit, true);

            args.Unit.AddComponent<GameObjectComponent>().GameObject = go;
            args.Unit.GetComponent<GameObjectComponent>().SpriteRenderers = go.GetComponentsInChildren<SpriteRenderer>();
            
            args.Unit.AddComponent<AnimatorComponent>();

            args.Unit.Position = args.Unit.Type == UnitType.Player? new Vector3(-4f, 0, 0) : new Vector3(1.5f, RandomHelper.RandomNumber(-1, 1), 0);

            await ETTask.CompletedTask;
        }
    }
}