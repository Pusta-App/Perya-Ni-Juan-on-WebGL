using System;
using UnityEngine;

public class BC_UTILS_VoidCount : MonoBehaviour
{
    public PNJ_GameState gameState = PNJ_GameState.MainMenu;
    [Header("OTHER CONFIGS")]
    public string countPrefs;
    public BC_Void_Calls OnCountDone;
    private int curCount = 0;

    void OnEnable()
    {
        if( PlayerPrefs.HasKey(countPrefs) )
        {
            curCount = PlayerPrefs.GetInt(countPrefs);
            curCount += 1;
            PlayerPrefs.SetInt(countPrefs, curCount);
        } else {
            PlayerPrefs.SetInt(countPrefs, curCount);
        }

        if( curCount >= getTargetTolerance )
        {
            curCount = 0;
            PlayerPrefs.SetInt(countPrefs, curCount);
            OnCountDone.Invoke();
        }
    }

    public int getTargetTolerance {
        get {
            int currentValue = 0;
            switch ( gameState )
            {
                 case PNJ_GameState.MainMenu:
                    currentValue = CustomReference.Access.settings.defaultConfigs.adsPatience.mainMenu - 1;
                    break;
                case PNJ_GameState.ColorGame:
                    currentValue = CustomReference.Access.settings.defaultConfigs.adsPatience.colorGame - 1;
                    break;
                case PNJ_GameState.DropBall:
                    currentValue = CustomReference.Access.settings.defaultConfigs.adsPatience.ballDrop - 1;
                    break;
                case PNJ_GameState.SabongWheel:
                    currentValue = CustomReference.Access.settings.defaultConfigs.adsPatience.manokRoleta - 1;
                    break;
                default:
                    break;
            }
            return currentValue;
        }
    }
}