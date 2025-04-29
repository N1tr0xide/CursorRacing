using System;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private CheckPointController _finishLine;
    [SerializeField] private List<CheckPointController> _checkPoints;
    [SerializeField] private GameUIManager _uiManager;
    private int _nextCheckPoint = 0, _lap = 1;
    private float _lapTime, _trackTime;
    private bool _runTimer = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _finishLine.OnPlayerPass += OnFinishLinePass;
        foreach (CheckPointController cpc in _checkPoints)
        {
            cpc.OnPlayerPass += OnCheckpointPass;
        }
    }

    private void Update()
    {
        if (!_runTimer) return;
        _lapTime += Time.deltaTime;
        _uiManager.UpdateCurrentLapTime(_lapTime);
    }

    private void OnFinishLinePass(CheckPointController cpc)
    {
        if (AllCheckPointsPassed())
        {
            _trackTime += _lapTime;
            _uiManager.SetPastLapTime(_lapTime, _lap);
            _lap++;

            if (_lap >= 4) //End of race
            {
                Player player = FindFirstObjectByType<Player>();
                player.DisableInput();
                _uiManager.SetGameOverPanel(true, _trackTime);
                _runTimer = false;
                return;
            }
            
            _uiManager.UpdateLapCounter(_lap); //End of lap, start next lap.
            _nextCheckPoint = 0;
            _lapTime = 0;
            foreach (CheckPointController pc in _checkPoints)
            {
                pc.SetPassed(false);
            }
            return;
        }

        _runTimer = true;
    }
    
    private void OnCheckpointPass(CheckPointController cpc)
    {
        if (_checkPoints.IndexOf(cpc) != _nextCheckPoint) return;
        cpc.SetPassed(true);
        _nextCheckPoint++;
    }

    /// <returns>If all checkpoints are marked as passed</returns>
    private bool AllCheckPointsPassed()
    {
        foreach (CheckPointController cpc in _checkPoints)
        {
            if(cpc.Passed) continue;
            return false;
        }

        return true;
    }
}


