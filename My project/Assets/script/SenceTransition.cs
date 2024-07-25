using UnityEngine.SceneManagement;
using UnityEngine;
public enum SceneEnums
{
    Area1,WorldMap
}
public class SceneTransition : MonoBehaviour
{
    public SceneEnums currentScene; // 当前场景
    public SceneEnums targetScene; // 目标场景

    public void MoveScene()
    {
        // 获取场景管理器
        ScenesManager.Instance.Transition(currentScene, targetScene);
    }

}
