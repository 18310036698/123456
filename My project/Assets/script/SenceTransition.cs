using UnityEngine.SceneManagement;
using UnityEngine;
public enum SceneEnums
{
    Area1,WorldMap
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
