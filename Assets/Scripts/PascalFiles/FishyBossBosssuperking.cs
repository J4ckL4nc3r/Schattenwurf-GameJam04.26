using System.Collections.Generic;
using UnityEngine;

public class FishyBossBosssuperking : MonoBehaviour
{
    [SerializeField] int scorePoints = 100;
    [SerializeField] private float changeValue = 0.01f;

    public float colorValue = 0;

    private MeshRenderer[] mRs;
    private Material runtimeMaterial;

    private void Start()
    {
        mRs = GetComponentsInChildren<MeshRenderer>();

        runtimeMaterial = new Material(Shader.Find("Standard"));

        foreach (MeshRenderer mR in mRs)
        {
            mR.material = runtimeMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.score += scorePoints;
        Destroy(gameObject);
    }

    private void Update()
    {
        if (colorValue >= 1)
            colorValue = 0;

        runtimeMaterial.color = Color.HSVToRGB(colorValue, 1, 1);

        colorValue += changeValue;
    }
}