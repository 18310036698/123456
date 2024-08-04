using UnityEngine.SceneManagement;
using UnityEngine;
/// <summary>
/// ��������
/// ѡ��ת������
/// </summary>
public enum SceneEnums
{
    Area1,Area2,Area3,Area4,WorldMap,MainMenu
}
public class SceneTransition : MonoBehaviour
{
    public SceneEnums currentScene; // ��ǰ����
    public SceneEnums targetScene; // Ŀ�곡��

    public void MoveScene()
    {
        // ��ȡ����������
        ScenesManager.Instance.Transition(currentScene, targetScene);
    }

}
