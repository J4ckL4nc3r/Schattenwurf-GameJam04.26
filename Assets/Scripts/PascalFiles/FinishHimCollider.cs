using UnityEngine;

public class FinishHimCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneLoader.Instance.LoadScene(MyScenes.MainMenu);
    }
}
