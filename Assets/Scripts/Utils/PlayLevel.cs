using UnityEngine;
using UnityEngine.SceneManagement;
using Save;
using TMPro;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;

    private void Start()
    {
        SaveManager.Instance.FileLoaded += OnFileLoaded;

        OnFileLoaded(SaveManager.Instance.GetSaveSetup());
    }

    private void OnDestroy()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.FileLoaded -= OnFileLoaded;
        }
    }

    private void OnFileLoaded(SaveSetup setup)
    {
        uiTextName.text = "Play " + (setup.lastLevel + 1);
    }

    public void LoadNextLevel()
    {
        int nextLevel = SaveManager.Instance.lastLevel + 1;

        SceneManager.LoadScene(nextLevel);
    }
}