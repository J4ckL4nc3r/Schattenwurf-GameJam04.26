using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI score;
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

    private void Start()
    {
        if (score != null) score.text = "Dein Score: " + GameManager.Instance.score;
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
