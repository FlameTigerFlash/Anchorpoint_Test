using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string _mainMenuSceneName = "MainMenu";
    [SerializeField] private string _mainLevelSceneName = "MainScene";
    [SerializeField] private string _defeatSceneName = "DefeatScreen";
    [SerializeField] private string _victorySceneName = "VictoryScreen";

    public void OnMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void OnMainLevel()
    {
        SceneManager.LoadScene(_mainLevelSceneName);
    }

    public void OnDefeatScreen()
    {
        SceneManager.LoadScene(_defeatSceneName);
    }

    public void OnVictoryScreen()
    {
        SceneManager.LoadScene(_victorySceneName);
    }

    public void OnQuitApplication()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
