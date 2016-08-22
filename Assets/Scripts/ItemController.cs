using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour
{
    private Inventory inv;

    public int id;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void PickUp()
    {
        inv.items[id].item++;

        //Custom Functions
        if (id == 3) //ID 3 = Metal
        {
            inv.items[3].item += Random.Range(1, 3);
        }   

        Destroy(gameObject);
    }
}
