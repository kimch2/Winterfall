using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour {

    public int health;

    public GameObject prefab1;
    public GameObject prefab2;
    Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (health <= 0)
        {
            rb.isKinematic = false;
            rb.AddForce(transform.forward * 8);
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);

        int outcome = Random.Range(0, 3);

        if (outcome == 0)
        {
            GameObject log1 = (GameObject)Instantiate(prefab1, gameObject.transform.position + new Vector3(0, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Quaternion.identity);
            GameObject log2 = (GameObject)Instantiate(prefab1, gameObject.transform.position + new Vector3(1, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Quaternion.identity);
            GameObject apple = (GameObject)Instantiate(prefab2, gameObject.transform.position + new Vector3(2, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Quaternion.identity);

            log1.transform.rotation = Random.rotation;
            log2.transform.rotation = Random.rotation;
            apple.transform.rotation = Random.rotation;
            log1.name = log2.name = "Log";
            apple.name = "Apple";

            Debug.Log("1");
        }
        if (outcome == 1)
        {
            GameObject newViking1 = (GameObject)Instantiate(prefab1, gameObject.transform.position + new Vector3(0, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Quaternion.identity);
            GameObject newViking2 = (GameObject)Instantiate(prefab1, gameObject.transform.position + new Vector3(1, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Quaternion.identity);

            newViking1.transform.rotation = Random.rotation;
            newViking2.transform.rotation = Random.rotation;
            newViking1.name = newViking2.name = "Log";
            Debug.Log("2");
        }
        if (outcome == 2)
        {
            GameObject newViking1 = (GameObject)Instantiate(prefab1, gameObject.transform.position + new Vector3(0, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Quaternion.identity);
            GameObject newViking3 = (GameObject)Instantiate(prefab2, gameObject.transform.position + new Vector3(2, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Quaternion.identity);

            newViking1.transform.rotation = Random.rotation;
            newViking3.transform.rotation = Random.rotation;
            newViking1.name = "Log";
            newViking3.name = "Apple";
            Debug.Log("3");
        }
    }
}
