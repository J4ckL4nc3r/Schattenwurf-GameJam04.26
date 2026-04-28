using Unity.VisualScripting;
using UnityEngine;

public class Toggle_World: MonoBehaviour
{
    [Header("Welten Zuweisung")]
    public GameObject Light_World;
    public GameObject Shadow_World;

    public GameObject Light_Background;
    public GameObject Shadow_Background;

    [SerializeField] AudioClip[] darkAthmo;
    [SerializeField] AudioClip normAthmo;

    private AudioSource aS;

    private void Start()
    {
        aS = transform.AddComponent<AudioSource>();
        aS.playOnAwake = false;
        aS.clip = normAthmo;
        aS.Play();
    }

    public bool isWorldActive = true;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ToggleWorld();
        }
    }
    public void ToggleWorld()
    {
        isWorldActive = !isWorldActive;

        Light_World.SetActive(isWorldActive);
        Shadow_World.SetActive(!isWorldActive);

        Light_Background.SetActive(isWorldActive);
        Shadow_Background.SetActive(!isWorldActive);

        if (aS.isPlaying)
        {
            aS.Stop();
        }
        if (isWorldActive)
        {
            aS.clip = normAthmo;
        }
        else
        {
            int rnd = Random.Range(0, 1);
            aS.clip = darkAthmo[rnd];
        }
        aS.Play();

        Debug.Log("Aktive Welt : " + isWorldActive);
    }
}
