using UnityEngine;
using System.Collections;

public class StoneController : MonoBehaviour {

    public int health;

    public GameObject prefab1;
    public GameObject prefab2;

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);

            GameObject newViking1 = Instantiate(prefab1, gameObject.transform.position + new Vector3(0, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Quaternion.identity);
            GameObject newViking2 = Instantiate(prefab1, gameObject.transform.position + new Vector3(1, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Quaternion.identity);
            GameObject newViking3 = Instantiate(prefab2, gameObject.transform.position + new Vector3(2, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Quaternion.identity);

            newViking1.transform.rotation = Random.rotation;
            newViking2.transform.rotation = Random.rotation;
            newViking3.transform.rotation = Random.rotation;
            newViking1.name = newViking2.name = "Rock";
            newViking3.name = "Iron Ore";
        }
    }
}
