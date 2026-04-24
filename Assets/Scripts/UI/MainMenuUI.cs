using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneLoader.Instance.LoadScene(MyScenes.MainLevel);
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
