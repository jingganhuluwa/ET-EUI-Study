// 文件：R2G_GetLoginGateKeyHandler.cs
// 作者：xj
// 描述：
// 日期：2023/11/07 20:04

using System;

namespace ET
{
    public class R2G_GetLoginGateKeyHandler:AMActorRpcHandler<Scene,R2G_GetLoginGateKey,G2R_GetLoginGateKey>
    {
        protected override async ETTask Run(Scene scene, R2G_GetLoginGateKey request, G2R_GetLoginGateKey response, Action reply)
        {
            if (scene.SceneType != SceneType.Gate)
            {
                Log.Error($"请求Scene错误,当前Scene为:{scene.SceneType} ,非 SceneType.Gate");
                response.Error = ErrorCode.Err_RequestSceneTypeError;
                return;
            }

            string key = RandomHelper.RandInt64().ToString() + TimeHelper.ServerNow();
            
            scene.GetComponent<GateSessionKeyComponent>().Remove(request.AccountId);
            scene.GetComponent<GateSessionKeyComponent>().Add(request.AccountId,key);
            response.GateSessionKey = key;
            reply();

            await ETTask.CompletedTask;
        }
    }
}