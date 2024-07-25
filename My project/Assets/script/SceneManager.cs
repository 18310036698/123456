using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景转换管理脚本
/// </summary>
public class ScenesManager : Singleton<ScenesManager>
{

    public CanvasGroup fadePanel; // 淡出画布

    private bool isFade; // 判断是否淡出
    private float duration = 0.5f; // 淡出持续时间

    /// <summary>
    /// 场景转换
    /// </summary>
    /// <param name="currentScene"></param>
    /// <param name="targetScene"></param>
    public void Transition(SceneEnums currentScene, SceneEnums targetScene)
    {
        // 没有淡出时
        if (!isFade)
        {
            StartCoroutine(TransitionToScene(currentScene, targetScene));
        }
    }

    /// <summary>
    /// 场景转换协程
    /// </summary>
    /// <param name="currentScene"></param>
    /// <param name="targetScene"></param>
    /// <returns></returns>

    private IEnumerator TransitionToScene(SceneEnums currentScene, SceneEnums targetScene)
    {
        // 开始淡出
        yield return FadeAction(1);
        // 卸载当前场景 当前场景此时有persistent H1 0 1
        yield return SceneManager.UnloadSceneAsync(currentScene.ToString());
        // 加载新场景 场景:persistent 0
        yield return SceneManager.LoadSceneAsync(targetScene.ToString(), LoadSceneMode.Additive);
        // 获取新场景 场景: persitent H2 索引 0 1 
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        // 激活场景
        SceneManager.SetActiveScene(newScene);
        // 结束淡出
        yield return FadeAction(0);
    }

    /// <summary>
    /// 淡出动画
    /// </summary>
    /// <param name="alpha"></param>
    /// <returns></returns>
    private IEnumerator FadeAction(float alpha)
    {
        isFade = true; // 正在淡出
        fadePanel.blocksRaycasts = true; // 隐藏鼠标点击
        // 淡出速度
        float speed = Mathf.Abs(fadePanel.alpha - alpha) / duration;
        // 当非常接近时
        while (!Mathf.Approximately(fadePanel.alpha, alpha))
        {
            // alpha值过渡
            fadePanel.alpha = Mathf.MoveTowards(fadePanel.alpha, alpha, speed * Time.deltaTime);
            yield return null;
        }
        fadePanel.blocksRaycasts = false;
        isFade = false;
    }
}
