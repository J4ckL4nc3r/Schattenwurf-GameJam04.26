using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FishyBossBosssuperking : MonoBehaviour
{
    [SerializeField] int scorePoints = 100;

    [SerializeField] private Material material;

    private float colorValue = 0;

    private MeshRenderer mR;
    private void Start()
    {
        mR = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.score += scorePoints;
    }

    private void FixedUpdate()
    {


        if (material == null)
            return;
        if (colorValue >= 255)
            colorValue = 0;

        List<Material> mat = new List<Material>()
        mat.Add(material);
        mR.SetMaterials(mat);
    }
}
