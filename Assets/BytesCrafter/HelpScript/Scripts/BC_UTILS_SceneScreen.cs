
using UnityEngine;
using UnityEngine.SceneManagement;

public class BC_UTILS_SceneScreen : MonoBehaviour
{
    public ScreenOrientation sceneOrientation = ScreenOrientation.Portrait;

    void Update()
    {
        if(Screen.orientation != sceneOrientation)
        {
            Screen.orientation = sceneOrientation;
        }
    }

    public void LoadScene(string scene)
    {
        CustomReference.Access.soundMix.Play("Enter");
        SceneManager.LoadScene(scene);
    }
}