using UnityEngine;
using System.Collections;

public class BushController : MonoBehaviour {

    public int time;
    Inventory inv;
    public Texture[] textures;
    Renderer rend;

    void Start ()
    {
        time = Random.Range(0, 1000);
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        rend = GetComponent<Renderer>();
    }

	public void Pick () {
	    if(time <= Time.time)
        {
            time = (int)Time.time + Random.Range(50, 2600);
            inv.items[13].item += 1 + Random.Range(0, 5);
            rend.material.mainTexture = textures[1];
        }
        else
        {
            StartCoroutine(Notifications.Call("No Berries! Regrowth in: " + (time - (int)Time.time) + "sec"));
        }
	}

    void Update ()
    {
        if(time < Time.time)
        {
            rend.material.mainTexture = textures[0];
        }
    }
}
