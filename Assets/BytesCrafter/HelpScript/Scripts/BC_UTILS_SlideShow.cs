
using UnityEngine;
using UnityEngine.UI;

public class BC_UTILS_SlideShow : MonoBehaviour
{
    public Image display;

    public Sprite[] sprites;

    public float timePerFrame = 3f;

    private int curFrame;
    private float curTimer;
    // Update is called once per frame
    void Update()
    {
        curTimer += Time.deltaTime;

        if( curTimer >= timePerFrame)
        {
            curFrame += 1;
            if( curFrame >= (sprites.Length - 1) )
            {
                curFrame = 0;
            }
            display.sprite = sprites[curFrame];
            curTimer = 0f;
        }
    }
}
