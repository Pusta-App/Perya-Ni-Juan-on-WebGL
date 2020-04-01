using UnityEngine;
using UnityEngine.UI;

public enum PNJ_ScreenSleep
{
    NeverSleep, DeviceSettings
}

[System.Serializable]
public class US_AdsTolerance
{
    public int mainMenu = 0;
    public int colorGame = 0;
    public int ballDrop = 0;
    public int manokRoleta = 0;
}

[System.Serializable]
public class US_LoyalRewards
{
    public int thirtyMinutes = 25;
    public int oneHour = 50;
    public int sevenHours = 75;
    public int twelveHours = 100;
    public int oneDay = 125;
}

[System.Serializable]
public class US_RewardsValue
{
    public int storeWatch = 25;
}

[System.Serializable]
public class US_DefaultConfig 
{
    public US_AdsTolerance adsPatience = new US_AdsTolerance();
    public US_LoyalRewards loyalRewards = new US_LoyalRewards();
    public US_RewardsValue rewardsValue = new US_RewardsValue();
}

public class PNJ_UserSettings : MonoBehaviour
{
    [Header("SERIALIZABLES")]
    public US_DefaultConfig defaultConfigs = new US_DefaultConfig();

    [Header("OTHER CONFIGS")]
    public PNJ_ScreenSleep sleepScreen = PNJ_ScreenSleep.DeviceSettings; 
    public Slider audioSlider;
    private string audioVolume = "UserSettiongs_AudioVolume";
    private string camBackground = "UserSettiongs_CameraBackground";

    public int colorIndex = 0;
    public ToggleGroup toggleGroup;

    void Awake()
    {
        CustomReference.Access.Initialized();
        Screen.sleepTimeout = sleepScreen == PNJ_ScreenSleep.DeviceSettings ? SleepTimeout.SystemSetting : SleepTimeout.NeverSleep;
    }

    public void Load()
    {
        float audioLevel = PlayerPrefs.HasKey(audioVolume) ? PlayerPrefs.GetFloat(audioVolume) : 1f;
        AudioListener.volume = audioLevel;
        audioSlider.value = audioLevel * 100f;

        colorIndex = PlayerPrefs.HasKey(camBackground) ? PlayerPrefs.GetInt(camBackground) : 0;
        for(int i = 0; i < toggleGroup.transform.childCount; i++)
        {
            if( i == colorIndex)
            {
                toggleGroup.transform.GetChild(i).GetComponent<Toggle>().isOn = true;
            }
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(audioVolume, AudioListener.volume);
        PlayerPrefs.SetInt(camBackground, colorIndex);
    }

    public void OnUpdateMusicVolume()
    {
       AudioListener.volume = audioSlider.value/100f;
    }

    public void OnUpdateBackground(int index)
    {
        colorIndex = index;
        if( CustomReference.Access.cameraControl != null)
        {
            CustomReference.Access.soundMix.Play("Pick");
            CustomReference.Access.cameraControl.SetBackgroundColor(index);
        }
    }
}
