using UnityEngine.SceneManagement;
using UnityEngine;
/// <summary>
/// 举例场景
/// 选择转换场景
/// </summary>
public enum SceneEnums
{
    Area1,Area2,Area3,Area4,WorldMap,MainMenu
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
