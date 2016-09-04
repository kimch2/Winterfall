using UnityEngine;

public class SaplingController : MonoBehaviour
{

    public float timeLeft;
    public GameObject[] tree;

    void Start()
    {
        timeLeft = Random.Range(300, 7200);
    }

    void Update()
    {
        if (timeLeft <= 0)
        {
            Instantiate(tree[Random.Range(0, tree.Length + 1)], gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
    }
}
