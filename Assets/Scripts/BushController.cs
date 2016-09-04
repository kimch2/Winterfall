using UnityEngine;

public class BushController : MonoBehaviour
{

    private float timeLeft;
    Inventory inv;
    public Texture[] textures;
    Renderer rend;
    private bool isMature;

    void Start()
    {
        timeLeft = Random.Range(0, 1000);
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        rend = GetComponent<Renderer>();
    }

    public void Pick()
    {
        if (isMature)
        {
            timeLeft = Random.Range(80, 2600);
            isMature = false;
            inv.items[13].item += 1 + Random.Range(0, 5);
            rend.material.mainTexture = textures[1];
        }
        //else
        //{
        //    StartCoroutine(Notifications.Call("No Berries! Regrowth in: " + (time - (int)Time.time) + "sec"));
        //}
    }

    void Update()
    {
        if (timeLeft <= 0 && !isMature)
        {
            isMature = true;
            rend.material.mainTexture = textures[0];
        }
        else if (!isMature)
        {
            timeLeft -= Time.deltaTime;
        }
    }
}
