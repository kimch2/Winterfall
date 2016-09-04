using UnityEngine;

public class Underwater : MonoBehaviour
{

    public float waterLevel;
    private bool isUnderwater;

    void Update()
    {
        if ((transform.position.y < waterLevel) != isUnderwater)
        {
            isUnderwater = transform.position.y < waterLevel;
            if (isUnderwater) SetUnderwater();
            if (!isUnderwater) SetNormal();
        }
    }

    void SetNormal()
    {
        RenderSettings.fogDensity = 0.003f;
    }

    void SetUnderwater()
    {
        RenderSettings.fogDensity = 0.05f;
    }
}
