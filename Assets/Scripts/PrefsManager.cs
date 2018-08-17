using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefsManager
{


    private const string CURRENT_SCORE = "current_score";
    private const string HIGH_SCORE = "highscore";
    private const string LAST_WORLD = "last_world";
    private const string LAST_LEVEL = "level_index";
    private const string AUDIO = "audio";
    private const string COIN = "coin";
    private const string QUEST = "quest_";
    private const string TIME = "time";
    private const string DAYTIME = "dayTime";
    private const string DAILYREWARD = "dailyReward";
    private const string DAY = "day";
    private const string REWARDED_TODAY = "rewarded";
    private const string REVIVE_SCORE = "reviveScore";
    private const string REVIVE_SPEED = "reviveSpeed";
    private const string IS_REVIVED = "isRevived";
    private const string TUTORIAL = "tutorial";
    private const string DEATH_COUNTER = "deathcounter";
    private const string MAIN_SKIN_INDEX = "main_skin_index";
    private const string BACKGROUND_SKIN_INDEX = "background_skin_index";

    public static int MainSkinIndex
    {
        get
        {
            return PlayerPrefs.GetInt(MAIN_SKIN_INDEX);
        }
        set
        {
            PlayerPrefs.SetInt(MAIN_SKIN_INDEX, value);
        }
    }
    
    public static int BackgroundSkinIndex
    {
        get
        {
            return PlayerPrefs.GetInt(BACKGROUND_SKIN_INDEX);
        }
        set
        {
            PlayerPrefs.SetInt(BACKGROUND_SKIN_INDEX, value);
        }
    }

    public static int DeathCounter
    {
        get
        {
            return PlayerPrefs.GetInt(DEATH_COUNTER);
        }
        set
        {
            PlayerPrefs.SetInt(DEATH_COUNTER, value);
        }
    }

    public static int CurrentScore
    {
        get
        {
            return PlayerPrefs.GetInt(CURRENT_SCORE);
        }
        set
        {
            PlayerPrefs.SetInt(CURRENT_SCORE, value);
        }
    }


    public static int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt(HIGH_SCORE);
        }
        set
        {
            PlayerPrefs.SetInt(HIGH_SCORE, value);
        }
    }

    public static int LastWorld
    {
        get
        {
            return PlayerPrefs.GetInt(LAST_WORLD);
        }
        set
        {
            PlayerPrefs.SetInt(LAST_WORLD, value);
        }
    }

    public static int LastLevel
    {
        get
        {
            return PlayerPrefs.GetInt(LAST_LEVEL);
        }
        set
        {
            PlayerPrefs.SetInt(LAST_LEVEL, value);
        }
    }

    public static bool Audio
    { // 1 - true, 0 - false
        get
        {
            return PlayerPrefs.GetInt(AUDIO, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(AUDIO, value ? 1 : 0);
        }
    }

    public static int Coins
    {
        get
        {
            return PlayerPrefs.GetInt(COIN);
        }
        set
        {
            PlayerPrefs.SetInt(COIN, value);
        }

    }
    public static int Day
    {
        get
        {
            return PlayerPrefs.GetInt(DAY);
        }
        set
        {
            PlayerPrefs.SetInt(DAY, value);
        }
    }

    public static int LastTime
    {
        get
        {
            return PlayerPrefs.GetInt(TIME);

        }
        set
        {
            PlayerPrefs.SetInt(TIME, value);
        }
    }

    public static int LastTimeForDailyReward
    {
        get
        {
            return PlayerPrefs.GetInt(DAYTIME);

        }
        set
        {
            PlayerPrefs.SetInt(DAYTIME, value);
        }
    }

    public static string GetQuest(int index)
    {
        return PlayerPrefs.GetString(QUEST + index);
    }
    public static void SetQuest(int index, string value)
    {
        PlayerPrefs.SetString(QUEST + index, value);
    }

    public static string DailyReward
    {
        get
        {
            return PlayerPrefs.GetString(DAILYREWARD, "1");

        }
        set
        {
            PlayerPrefs.SetString(DAILYREWARD, value);
        }
    }

    public static bool RewardedToday
    {
        get
        {
            return PlayerPrefs.GetInt(REWARDED_TODAY) == 1;

        }
        set
        {
            PlayerPrefs.SetInt(REWARDED_TODAY, value ? 1 : 0);
        }
    }

    public static int ReviveSpeed
    {
        get
        {
            return PlayerPrefs.GetInt(REVIVE_SPEED);

        }
        set
        {
            PlayerPrefs.SetInt(REVIVE_SPEED, value);
        }
    }

    public static int ReviveScore
    {
        get
        {
            return PlayerPrefs.GetInt(REVIVE_SCORE);

        }
        set
        {
            PlayerPrefs.SetInt(REVIVE_SCORE, value);
        }
    }

    public static bool IsRevived
    {
        get
        {
            return PlayerPrefs.GetInt(IS_REVIVED) == 1;

        }
        set
        {
            PlayerPrefs.SetInt(IS_REVIVED, value ? 1 : 0);
        }
    }
    public static bool Tutorial
    {
        get
        {
            return PlayerPrefs.GetInt(TUTORIAL) == 0;

        }
        set
        {
            PlayerPrefs.SetInt(TUTORIAL, value ? 0 : 1);
        }
    }

#if UNITY_EDITOR

    [UnityEditor.MenuItem("Prefs/Clear")]
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
