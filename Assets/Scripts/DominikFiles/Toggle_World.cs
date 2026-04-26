using UnityEngine;

public class Toggle_World: MonoBehaviour
{
    [Header("Welten Zuweisung")]
    public GameObject Light_World;
    public GameObject Shadow_World;

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

        Debug.Log("Aktive Welt : " + isWorldActive);
    }
}
