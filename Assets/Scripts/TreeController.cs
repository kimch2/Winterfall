using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour
{
    public int health;

    public GameObject log;
    public GameObject apple;
    public GameObject sapling;
    private Rigidbody rb;

    void Start()
    {
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

        switch (Random.Range(0, 4))
        {
            case 0: // 2 Logs, 1 Apple
                GameObject log1_0 = Instantiate(log, gameObject.transform.position + new Vector3(0, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Random.rotation);
                GameObject log2_0 = Instantiate(log, gameObject.transform.position + new Vector3(1, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Random.rotation);
                GameObject apple1_0 = Instantiate(apple, gameObject.transform.position + new Vector3(2, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Random.rotation);

                log1_0.name = log2_0.name = "Log";
                apple1_0.name = "Apple";
                break;

            case 1: // 2 Logs
                GameObject log1_1 = Instantiate(log, gameObject.transform.position + new Vector3(0, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Random.rotation);
                GameObject log2_1 = Instantiate(log, gameObject.transform.position + new Vector3(1, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Random.rotation);

                log1_1.name = log2_1.name = "Log";
                break;

            case 2: // 1 Log, 1 Apple
                GameObject log1_2 = Instantiate(log, gameObject.transform.position + new Vector3(0, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Random.rotation);
                GameObject apple1_2 = Instantiate(apple, gameObject.transform.position + new Vector3(2, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Random.rotation);

                log1_2.name = "Log";
                apple1_2.name = "Apple";
                break;

            case 3: // 1 Log, 1 Sapling
                GameObject log1_3 = Instantiate(log, gameObject.transform.position + new Vector3(0, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Random.rotation);
                GameObject sapling1_3 = Instantiate(sapling, gameObject.transform.position + new Vector3(2, 0, 0) + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), Random.rotation);

                log1_3.name = "Log";
                sapling1_3.name = "Sapling";
                break;
        }
    }
}
