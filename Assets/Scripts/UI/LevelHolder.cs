using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject recordsGm;
    [SerializeField] private TMP_Text recordsText;
    private bool displayingRecords;

    void OnEnable()
    {
        recordsGm.SetActive(false);
        displayingRecords = false;

        float trackTime = LapTimingUtils.GetTrackRecordTime(levelName);
        string trackString = LapTimingUtils.FloatToStopWatchTime(trackTime);
        float lapTime = LapTimingUtils.GetLapRecordTime(levelName);
        string lapString = LapTimingUtils.FloatToStopWatchTime(lapTime);

        string records = "Best Track Time:\n";
        records += trackString + "\n";
        records += "Best Lap Time:\n";
        records += lapString + "\n";

        recordsText.text = records;
    }

    public void DisplayRecords()
    {
        displayingRecords = !displayingRecords;
        recordsGm.SetActive(displayingRecords);
    }

    public void StartRace()
    {
        SceneManager.LoadScene(levelName);
    }
}
