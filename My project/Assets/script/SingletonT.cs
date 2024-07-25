using UnityEngine;

/// <summary>
///  ���͵���
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance; // Ψһʵ��

    // �����ṩ
    public static T Instance
    {
        get { return instance; }
    }

    // �Ƿ񴴽�
    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        // Ҫ�ǲ�Ϊ�գ���ʾ�Ѿ�����һ������ ����
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            // ǿת����
            instance = (T)this;
        }
    }



    protected virtual void OnDestroy()
    {
        // ���ٵ�ǰ����
        if (instance == this)
        {
            instance = null;
        }
    }
}