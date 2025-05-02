using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _trackSelectPanel, _howToPanel, _creditsPanel;

    private void Start()
    {
        _trackSelectPanel.SetActive(false);
        _howToPanel.SetActive(false);
        _creditsPanel.SetActive(false);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void SetHowToPanel(bool activeState)
    {
        _howToPanel.SetActive(activeState);
    }

    public void SetTrackSelectPanel(bool activeState)
    {
        _trackSelectPanel.SetActive(activeState);
    }
    
    public void SetCreditsPanel(bool activeState)
    {
        _creditsPanel.SetActive(activeState);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
