using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LapTimingUtils
{
    /// Transform amount of seconds into a string formated, minutes : seconds : miliseconds.
    /// <param name="time">time in seconds</param>
    /// <returns>the formatted string</returns>
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
        if (PlayerPrefs.HasKey(levelName + "_trackRecord"))
        {
            if (PlayerPrefs.GetFloat(levelName + "_trackRecord") < time) return;
        }

        PlayerPrefs.SetFloat(levelName + "_trackRecord", time);
    }

    public static float GetTrackRecordTime(string levelName)
    {
        if (PlayerPrefs.HasKey(levelName + "_trackRecord"))
        {
            return PlayerPrefs.GetFloat(levelName + "_trackRecord");
        }

        Debug.LogWarning("Lap record key not found!!");
        return 3599.999f;
    }

    public static void DeleteTrackRecord(string levelName)
    {
        if (PlayerPrefs.HasKey(levelName + "_trackRecord"))
        {
            PlayerPrefs.DeleteKey(levelName + "_trackRecord");
        }
    }
    
    /// Compares if time is greater than the best saved single lap time record of level.
    /// If it its the new time is saved as best.
    /// <param name="time">the lap time to compare</param>
    /// <param name="levelName">Name of scene</param>
    /// <returns>True if new lap record is set, False if not.</returns>
    public static bool CompareSingleLapTimeRecord(float time, string levelName)
    {
        if (time > GetLapRecordTime(levelName)) return false;
        SaveLapRecord(time, levelName);
        return true;
    }
    
    /// Compares if time is greater than the best saved track time record of level.
    /// If it its the new time is saved as best.
    /// <param name="time">the track time to compare</param>
    /// <param name="levelName">Name of scene</param>
    /// <returns>True if new track record is set, False if not.</returns>
    public static bool CompareTrackTimeRecord(float time, string levelName)
    {
        if (time > GetTrackRecordTime(levelName)) return false;
        SaveTrackRecord(time, levelName);
        return true;
    }
}
