
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BC_UTILS_SceneLoader : MonoBehaviour
{
    public Slider loadingSlider;
    public string targetScene = string.Empty;
    public float startDelay = 0f;

    void Start()
    {
        StartCoroutine(LoadAsynchronsely());
    }

    IEnumerator LoadAsynchronsely()
    {
        yield return new WaitForSeconds(startDelay);

        AsyncOperation asyncOps = SceneManager.LoadSceneAsync(targetScene);
        while ( !asyncOps.isDone )
        {
            float progress = Mathf.Clamp01(asyncOps.progress / 0.9f);
            if(loadingSlider != null) { loadingSlider.value = progress; }
            CustomReference.Access.Debugs("Progress: " + progress);
            yield return null;
        }
    }
}
