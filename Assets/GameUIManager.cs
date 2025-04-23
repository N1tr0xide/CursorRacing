using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _lapCounter, _currentLapTime;
    [SerializeField] private TMP_Text[] _pastLapTimes;

    public void UpdateLapCounter(int lap)
    {
        _lapCounter.text = $"Lap {lap} / 3";
    }
    
    public void UpdateCurrentLapTime(float time)
    {
        _currentLapTime.text = "Time: " + LapTimingUtils.FloatToStopWatchTime(time);
    }

    public void SetPastLapTime(float time, int lap)
    {
        if(lap > _pastLapTimes.Length || lap == 0) return;
        _pastLapTimes[lap - 1].text = $"Lap {lap}: " + LapTimingUtils.FloatToStopWatchTime(time);
    }
}
