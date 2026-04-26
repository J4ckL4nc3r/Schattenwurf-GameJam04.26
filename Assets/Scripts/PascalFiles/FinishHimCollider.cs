using UnityEngine;

public class FinishHimCollider : MonoBehaviour
{
    [SerializeField] private MyScenes sceneToLoad = MyScenes.DeathScene;
    private void OnTriggerEnter(Collider other)
    {
        SceneLoader.Instance.LoadScene(sceneToLoad);
    }
}
