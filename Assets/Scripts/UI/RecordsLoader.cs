using TMPro;
using UnityEngine;

public class RecordsLoader : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string levelName;

    void OnEnable()
    {
        float trackTime = LapTimingUtils.GetTrackRecordTime(levelName);
        string trackString = LapTimingUtils.FloatToStopWatchTime(trackTime);
        float lapTime = LapTimingUtils.GetLapRecordTime(levelName);
        string lapString = LapTimingUtils.FloatToStopWatchTime(lapTime);

        string records = "Best Track Time:\n";
        records += trackString + "\n";
        records += "Best Lap Time:\n";
        records += lapString + "\n";

        text.text = records;
    }
}
