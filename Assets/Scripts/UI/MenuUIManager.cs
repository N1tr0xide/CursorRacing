using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject trackSelectPanel, howToPanel;

    private void Start()
    {
        trackSelectPanel.SetActive(false);
        howToPanel.SetActive(false);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void SetHowToPanel(bool activeState)
    {
        howToPanel.SetActive(true);
    }

    public void SetTrackSelectPanel(bool activeState)
    {
        trackSelectPanel.SetActive(activeState);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
