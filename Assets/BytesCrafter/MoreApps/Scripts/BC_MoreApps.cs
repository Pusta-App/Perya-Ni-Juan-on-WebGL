using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BC_MoreApps : MonoBehaviour
{
    public Image test;
    void Start() {
        StartCoroutine(GetTexture());
    }
 
    IEnumerator GetTexture() {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://firebasestorage.googleapis.com/v0/b/peryahan-ni-juan---color-g-494.appspot.com/o/images%2Flogo.jpg?alt=media&token=883a9cbf-ba1e-4bc4-b612-acaaa4dd6bfc");
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Texture2D t2d = ((DownloadHandlerTexture)www.downloadHandler).texture;
            test.sprite = Sprite.Create(t2d, new Rect(0f, 0f, 512f, 512f), new Vector2(0.5f, 0.5f));
        }
    }
}
