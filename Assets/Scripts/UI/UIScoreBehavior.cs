using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIScoreBehavior : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private float StartTime = 60;

    private void FixedUpdate()
    {
        score.text = "Score: " + (GameManager.Instance.movePoints + GameManager.Instance.bonusPoints);
        timer.text = "Time: " + (int)StartTime;
    }
    private void Update()
    {
        if (StartTime <= 0)
            SceneLoader.Instance.LoadScene(MyScenes.ScoreScene);

        StartTime -= Time.deltaTime;
    }
}
