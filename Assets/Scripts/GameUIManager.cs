using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [Header("Lap related UI")]
    [SerializeField] private TMP_Text _lapCounter;
    [SerializeField] private TMP_Text _currentLapTime;
    [SerializeField] private TMP_Text[] _pastLapTimes;
    [Header("Button related UI")]
    [SerializeField] private Animator gearAnimator;
    [SerializeField] private GameObject pausePanel;

    public void Start()
    {
        string anim = player._reverseEnabled ? "GearReverse" : "GearDrive";
        gearAnimator.Play(anim);
        Pause(false);
    }

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

    public void SwitchGears()
    {
        player._reverseEnabled = !player._reverseEnabled;
        string anim = player._reverseEnabled ? "GearReverse" : "GearDrive";
        gearAnimator.Play(anim);
    }

    public void Pause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
        pausePanel.SetActive(pause);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
