using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PNJ_ConfigString
{
    public bool useDefault = true;
    public string currentValue;
    public string defaultValue;

    public string GetRequired
    {
        get 
        {
           if( useDefault )
            {
                return currentValue;
            }

            else
            {
                return defaultValue;
            } 
        }
    }

    public PNJ_ConfigString()
    {
        currentValue = string.Empty;
        defaultValue = string.Empty;
    }

    public PNJ_ConfigString(string newCurrent, string newDefault)
    {
        currentValue = newCurrent;
        defaultValue = newDefault;
    }
}

[System.Serializable]
public class PNJ_ConfigInt
{
    public bool useDefault = true;
    public int currentValue;
    public int defaultValue;

    public int GetRequired
    {
        get 
        {
           if( useDefault )
            {
                return currentValue;
            }

            else
            {
                return defaultValue;
            } 
        }
    }

     public PNJ_ConfigInt()
    {
        currentValue = 0;
        defaultValue = 0;
    }

    public PNJ_ConfigInt(int newCurrent, int newDefault)
    {
        currentValue = newCurrent;
        defaultValue = newDefault;
    }
}

[System.Serializable]
public class PNJ_ConfigFloat
{
    public bool useDefault = true;
    public float currentValue;
    public float defaultValue;

    public float GetRequired
    {
        get 
        {
           if( useDefault )
            {
                return currentValue;
            }

            else
            {
                return defaultValue;
            } 
        }
    }

    public PNJ_ConfigFloat()
    {
        currentValue = 0f;
        defaultValue = 0f;
    }

    public PNJ_ConfigFloat(float newCurrent, float newDefault)
    {
        currentValue = newCurrent;
        defaultValue = newDefault;
    }
}

[System.Serializable]
public class PNJ_ConfigBool
{
    public bool useDefault = true;
    public bool currentValue;
    public bool defaultValue;

    public bool GetRequired
    {
        get 
        {
           if( useDefault )
            {
                return currentValue;
            }

            else
            {
                return defaultValue;
            } 
        }
    }

    public PNJ_ConfigBool()
    {
        currentValue = false;
        defaultValue = false;
    }

    public PNJ_ConfigBool(bool newCurrent, bool newDefault)
    {
        currentValue = newCurrent;
        defaultValue = newDefault;
    }
}

public enum PNJ_ConfigAdEnum
{
    AdmobInterstitial, AdmobRewardAds, UnitySkippedAds, UnityRewardAds
}

[System.Serializable]
public class PNJ_ConfigAdType
{
     public bool useDefault = true;
    public PNJ_ConfigAdEnum currentValue = PNJ_ConfigAdEnum.AdmobInterstitial;
    public PNJ_ConfigAdEnum defaultValue = PNJ_ConfigAdEnum.AdmobInterstitial;

    public PNJ_ConfigAdEnum GetRequired
    {
        get 
        {
           if( useDefault )
            {
                return currentValue;
            }

            else
            {
                return defaultValue;
            } 
        }
    }

    public PNJ_ConfigAdType()
    {
        currentValue = PNJ_ConfigAdEnum.AdmobInterstitial;
        defaultValue = PNJ_ConfigAdEnum.AdmobInterstitial;
    }

    public PNJ_ConfigAdType(PNJ_ConfigAdEnum newCurrent, PNJ_ConfigAdEnum newDefault)
    {
        currentValue = newCurrent;
        defaultValue = newDefault;
    }
}

public class PNJ_RemoteConfigs : MonoBehaviour
{
    public PNJ_ConfigInt attendantPatience = new PNJ_ConfigInt(3, 3);
    public PNJ_ConfigAdType attendantAnnoyAds = new PNJ_ConfigAdType();


    public void Orient_Portrait()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void Orient_LandscapeL()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}