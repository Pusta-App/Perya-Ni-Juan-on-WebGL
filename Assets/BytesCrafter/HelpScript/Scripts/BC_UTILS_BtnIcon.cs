
using UnityEngine;
using UnityEngine.UI;

public class BC_UTILS_BtnIcon : MonoBehaviour
{
    public Image icon;
    public Button button;

    public Color disable;

    // Update is called once per frame
    void Update()
    {   
        if(button != null && icon != null)
        {
            if(button.interactable)
            {
                icon.color = Color.white;
            }

            else 
            {
                icon.color = disable;
            }
        }
        
    }
}
