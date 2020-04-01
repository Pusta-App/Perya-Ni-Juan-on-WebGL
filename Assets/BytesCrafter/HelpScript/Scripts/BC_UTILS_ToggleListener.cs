
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class BC_UTILS_ToggleListener : MonoBehaviour
{
    public Toggle target;
    public BC_Void_Calls OnToggleOn;
    public BC_Void_Calls OnToggleOff;
    void OnValidate()
    {
        target = GetComponent<Toggle>();
    }

    void OnEnable()
    {
        target.onValueChanged.AddListener ( (value) => {   
                OnToggleChange(value);
            }
       );
    }

    void OnToggleChange(bool isOn)
    {
        if(isOn)
        {
            OnToggleOn.Invoke();
        }

        else
        {
            OnToggleOff.Invoke();
        }
    }
}
