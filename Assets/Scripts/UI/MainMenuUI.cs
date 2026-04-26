using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneLoader.Instance.LoadScene(MyScenes.MainLevel);
    }
    public void OnCreditsButtonClicked()
    {
        SceneLoader.Instance.LoadScene(MyScenes.Credits);
    }
    public void OnMainMenuButtonClicked()
    {
        SceneLoader.Instance.LoadScene(MyScenes.MainMenu);
    }

    public void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
