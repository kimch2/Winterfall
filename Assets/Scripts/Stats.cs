using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    public TOD_Sky sky;
    public GameObject deathPanel;
    public GameObject damagePanel;

    public float air;

    [Header("Health")]
    public Slider healthSlider;
    public float health;

    [Header("Energy")]
    public Slider energySlider;
    public float energy;
    public bool sleeping;

    [Header("Hunger")]
    public Slider hungerSlider;
    public float hunger;
    public float hungerSaturation;

    [Header("Thrist")]
    public Slider thirstSlider;
    public float thirst;
    public float thirstSaturation;

    [Header("Temperature")]
    public Text temperatureText;
    public float temperature;

    [Header("Money")]
    public Text moneyText;
    public int maxMoney;
    public float money;

    [Header("Coordinates")]
    public Text coordinatesText;

    [Header("Time")]
    public Text dateText;

    void Start()
    {
        InvokeRepeating("Refresh", 0, 3);
        //InvokeRepeating("Temperature", 2, 1000);
    }

    void Refresh()
    {
        //int coordinatesX = (int)gameObject.transform.position.x;
        //int coordinatesZ = (int)gameObject.transform.position.z;

        float hour = sky.Cycle.Hour;

        //coordinatesText.text = "X: " + coordinatesX + " | Z: " + coordinatesZ;
        dateText.text = hour.ToString("F2");
    }

    /*
    void Temperature()
    {
        // TEMPERATURE CONTROL SECTION
        if (sky.Cycle.Hour >= 7)
        {
            temperature = Random.Range(-5, 5);
        }
    }
    */

    void Update() {

        healthSlider.value = health;
        energySlider.value = energy;
        hungerSlider.value = hunger;
        thirstSlider.value = thirst;
        moneyText.text = (int)money + "€";

        /* MONEY CONTROL SECTION */

        if (money >= maxMoney)
        {
            money = maxMoney;
            StartCoroutine(Notifications.Call("You can't have more money!"));
        }

        /* ENERGY CONTROL SECTION */

        if (sleeping == false)
        {
            if (energy > 0)
            {
                energy -= Time.deltaTime / 10;
            }

            if (energy >= 100)
            {
                energy = 100;
            }
        }
        else
        {
            sky.Cycle.Hour += Time.deltaTime / 4;
            if (energy >= 100)
            {
                StartCoroutine(Notifications.Call("Fully Rested!"));
            }
            else
            {
                energy += Time.deltaTime;
                health += Time.deltaTime / 4;
            }
        }

        /* THIRST CONTROL SECTION*/

        if (thirst > 0 && thirstSaturation <= 0)
        {
            thirst -= Time.deltaTime / 7;
        }

        if (thirst >= 100)
        {
            thirst = 100;
        }

        /* AIR CONTROL SECTION*/

        if (transform.position.y <= 15)
        {
            if (air > 0)
            {
                air -= Time.deltaTime;
            }
        }
        else
        {
            if(air > 0)
            {
                air = 70;
            }
        }

        /* SATURATION CONTROL SECTION */
        if (hungerSaturation > 0)
        {
            hungerSaturation -= Time.deltaTime;
        }

        if (thirstSaturation > 0)
        {
            thirstSaturation -= Time.deltaTime;
        }

        /* HUNGER CONTROL SECTION */
        if (hunger > 0 && hungerSaturation <= 0)
        {
            hunger -= Time.deltaTime / 9;
        }

        if (hunger >= 100)
        {
            hunger = 100;
        }

        /* DAMAGE CONTROL SECTION*/
        if (health >= 100)
        {
            health = 100;
        }

        if (damagePanel.activeSelf)
        {
            damagePanel.SetActive(false);
        }

        if (hunger <= 0 && (thirst <= 0))
        {
            health -= Time.deltaTime * 3;
            if (damagePanel.activeSelf == false)
            {
                damagePanel.SetActive(true);
            }
        }

        else
        {
            if (hunger <= 0 || thirst <= 0 || energy <= 0)
            {
                health -= Time.deltaTime * 1.5f;
                if (damagePanel.activeSelf == false)
                {
                    damagePanel.SetActive(true);
                }
            }
        }

        if(air <= 0)
        {
            health -= Time.deltaTime * 2;
        }

        /* DEATH CONTROL SECTION*/
        if (health <= 0)
        {
            if (deathPanel.activeSelf == false)
            {
                deathPanel.SetActive(true);
                CursorControll.UnlockCursor();
            }
        }
    }

    public void Respawn()
    {
        SceneManager.LoadScene(1);
    }
}
