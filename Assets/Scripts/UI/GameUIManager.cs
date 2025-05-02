using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    private bool _newLapRecord = false;
    [SerializeField] private Player _player;
    [Header("Lap related UI")]
    [SerializeField] private TMP_Text _lapCounter;
    [SerializeField] private TMP_Text _currentLapTime;
    [SerializeField] private TMP_Text[] _pastLapTimes;
    [SerializeField] private TMP_Text _lapTimeRecordText;
    [SerializeField] private TMP_Text _trackTimeText;
    [Header("Button related UI")]
    [SerializeField] private Animator _gearAnimator;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameOverPanel;

    public void Start()
    {
        string anim = _player.ReverseEnabled ? "GearReverse" : "GearDrive";
        _gearAnimator.Play(anim);
        Pause(false);
        SetGameOverPanel(false);
        _player.Input.Player.GearSwitch.performed += GearSwitchPerformed;
    }

    private void GearSwitchPerformed(InputAction.CallbackContext obj)
    {
        SwitchGears();
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
        
        if (LapTimingUtils.CompareSingleLapTimeRecord(time, SceneManager.GetActiveScene().name))
        {
            _newLapRecord = true;
        }
    }

    public void SetGameOverPanel(bool activeState, float time = 0)
    {
        _gameOverPanel.SetActive(activeState);
        if(!activeState) return;
        string trackText = "";
        
        //track time check
        if (LapTimingUtils.CompareTrackTimeRecord(time, SceneManager.GetActiveScene().name))
        {
            trackText += "New Track Record!!\n";
            _trackTimeText.color = Color.yellow;
        }
        else
        {
            trackText += "Track time:\n";
        }
        _trackTimeText.text = trackText + LapTimingUtils.FloatToStopWatchTime(time);

        //lap record check
        if (_newLapRecord)
        {
            _lapTimeRecordText.text = "New Lap record!!\n" + LapTimingUtils.GetLapRecordTime(SceneManager.GetActiveScene().name);
            _lapTimeRecordText.color = Color.yellow;
            _lapTimeRecordText.enabled = true;
            return;
        }

        _lapTimeRecordText.enabled = false;
    }

    public void SwitchGears()
    {
        _player.ReverseEnabled = !_player.ReverseEnabled;
        string anim = _player.ReverseEnabled ? "GearReverse" : "GearDrive";
        _gearAnimator.Play(anim);
    }

    public void Pause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
        _pausePanel.SetActive(pause);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
