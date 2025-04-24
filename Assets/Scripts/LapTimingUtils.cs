using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LapTimingUtils
{
    /// Transform amount of seconds into a string formated, minutes : seconds : miliseconds.
    /// <param name="time">time in seconds</param>
    /// <returns>the formated string</returns>
    public static string FloatToStopWatchTime(float time)
    {
        TimeSpan timer = TimeSpan.FromSeconds(time);
        return $"{timer.Minutes}:{timer.Seconds}:{timer.Milliseconds}";
    }

    /// Save best single lap time.
    /// <param name="time"></param>
    /// <param name="levelName">Name of scene</param>
    public static void SaveLapRecord(float time, string levelName)
    {
        if(PlayerPrefs.HasKey(levelName + "_lapRecord"))
        {
            if (PlayerPrefs.GetFloat(levelName + "_lapRecord") < time) return;
        }

        PlayerPrefs.SetFloat(levelName + "_lapRecord", time);
    }

    /// Get best single lap time in level
    /// <param name="levelName">Name of scene</param>
    /// <returns>the lap time in seconds</returns>
    public static float GetLapRecordTime(string levelName) 
    {  
        if(PlayerPrefs.HasKey(levelName + "_lapRecord"))
        {
            return PlayerPrefs.GetFloat(levelName + "_lapRecord");
        }

        Debug.LogWarning("Lap record key not found!!");
        return 3599.999f;
    }

    /// Deletes best single lap time in level
    /// <param name="levelName">Name of scene</param>
    public static void DeleteLapRecord(string levelName)
    {
        if (PlayerPrefs.HasKey(levelName + "_lapRecord"))
        {
            PlayerPrefs.DeleteKey(levelName + "_lapRecord");
        }
    }

    public static void SaveTrackRecord(float time, string levelName)
    {
        if (PlayerPrefs.HasKey(levelName + "_lapRecord"))
        {
            if (PlayerPrefs.GetFloat(levelName + "_lapRecord") < time) return;
        }

        PlayerPrefs.SetFloat(levelName + "_lapRecord", time);
    }

    public static float GetTrackRecordTime(string levelName)
    {
        if (PlayerPrefs.HasKey(levelName + "_lapRecord"))
        {
            return PlayerPrefs.GetFloat(levelName + "_lapRecord");
        }

        Debug.LogWarning("Lap record key not found!!");
        return 3599.999f;
    }

    public static void DeleteTrackRecord(string levelName)
    {
        if (PlayerPrefs.HasKey(levelName + "_lapRecord"))
        {
            PlayerPrefs.DeleteKey(levelName + "_lapRecord");
        }
    }
}
