using UnityEngine;

/// <summary>
///  泛型单例
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance; // 唯一实例

    // 向外提供
    public static T Instance
    {
        get { return instance; }
    }

    // 是否创建
    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        // 要是不为空，表示已经存在一个单例 销毁
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            // 强转单例
            instance = (T)this;
        }
    }



    protected virtual void OnDestroy()
    {
        // 销毁当前单例
        if (instance == this)
        {
            instance = null;
        }
    }
}