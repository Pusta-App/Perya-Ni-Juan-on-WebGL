
using UnityEngine;

public class PNJ_CameraControl : MonoBehaviour
{
    public BC_UTILS_SceneScreen sceneScreen;
    public int colorIndex;
    public Color[] colorList;
    public Camera cameraTarget;
    void OnEnable()
    {
        CustomReference.Access.cameraControl = this;
        if(CustomReference.Access.settings != null)
        {
            SetBackgroundColor(CustomReference.Access.settings.colorIndex);
        }
    }

    public void SetBackgroundColor(int index)
    {
        colorIndex = index;
        cameraTarget.backgroundColor = colorList[index];
    }
}