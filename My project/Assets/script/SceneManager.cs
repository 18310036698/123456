using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����ת������ű�
/// </summary>
public class ScenesManager : Singleton<ScenesManager>
{

    public CanvasGroup fadePanel; // ��������

    private bool isFade; // �ж��Ƿ񵭳�
    private float duration = 0.5f; // ��������ʱ��

    /// <summary>
    /// ����ת��
    /// </summary>
    /// <param name="currentScene"></param>
    /// <param name="targetScene"></param>
    public void Transition(SceneEnums currentScene, SceneEnums targetScene)
    {
        // û�е���ʱ
        if (!isFade)
        {
            StartCoroutine(TransitionToScene(currentScene, targetScene));
        }
    }

    /// <summary>
    /// ����ת��Э��
    /// </summary>
    /// <param name="currentScene"></param>
    /// <param name="targetScene"></param>
    /// <returns></returns>

    private IEnumerator TransitionToScene(SceneEnums currentScene, SceneEnums targetScene)
    {
        // ��ʼ����
        yield return FadeAction(1);
        // ж�ص�ǰ���� ��ǰ������ʱ��persistent H1 0 1
        yield return SceneManager.UnloadSceneAsync(currentScene.ToString());
        // �����³��� ����:persistent 0
        yield return SceneManager.LoadSceneAsync(targetScene.ToString(), LoadSceneMode.Additive);
        // ��ȡ�³��� ����: persitent H2 ���� 0 1 
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        // �����
        SceneManager.SetActiveScene(newScene);
        // ��������
        yield return FadeAction(0);
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    private IEnumerator FadeAction(float alpha)
    {
        isFade = true; // ���ڵ���
        fadePanel.blocksRaycasts = true; // ���������
        // �����ٶ�
        float speed = Mathf.Abs(fadePanel.alpha - alpha) / duration;
        // ���ǳ��ӽ�ʱ
        while (!Mathf.Approximately(fadePanel.alpha, alpha))
        {
            // alphaֵ����
            fadePanel.alpha = Mathf.MoveTowards(fadePanel.alpha, alpha, speed * Time.deltaTime);
            yield return null;
        }
        fadePanel.blocksRaycasts = false;
        isFade = false;
    }
}
