using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Raycast : MonoBehaviour {

    public Text infoText;
    public int range;

    public GameObject sleepPanel;

    public LayerMask myLayerMask;

    float timeStamp;

    public Inventory inv;
    public Stats statsScript;

    public AudioSource audioSrc;
    public AudioClip[] otherClip;

    public Animation axeAnim;
    public Animation pickaxeAnim;

    public FirstPersonController fpc;

    //public GameObject effect;

    public void WakeUp()
    {
        statsScript.sleeping = false;
        sleepPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        fpc.enabled = true;
    }

    void Update ()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, range, myLayerMask))
        {
            //TREE RAYCAST
            if (hit.collider.gameObject.tag == "Tree")
            {
                GameObject tree = (hit.collider.gameObject);

                if (Input.GetButtonDown("Fire1") && inv.items[15].item >= 1 && inv.hatchetGO.activeSelf && timeStamp <= Time.time)
                {
                    inv.items[15].item--;
                    timeStamp = Time.time + 1f;
                    audioSrc.clip = otherClip[0];
                    audioSrc.Play();
                    axeAnim.Play();
                    tree.GetComponent<TreeController>().health--;
                    inv.xp++;
                }
            }

            //STONE RAYCAST
            if (hit.collider.gameObject.tag == "Stone")
            {
                GameObject stone = (hit.collider.gameObject);

                if (Input.GetButtonDown("Fire1") && inv.items[16].item >= 1 && inv.pickaxeGO.activeSelf && timeStamp <= Time.time)
                {
                    //Object eff = Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal));
                    //Destroy(eff, 0.25f);
                    inv.items[16].item--;
                    timeStamp = Time.time + 1f;
                    audioSrc.clip = otherClip[0];
                    audioSrc.Play();
                    pickaxeAnim.Play();
                    stone.GetComponent<StoneController>().health--;
                    inv.xp++;
                }
            }

            /*
            //BUILDING RAYCAST
            else if (hit.collider.gameObject.tag == "Building")
            {

                if (Input.GetButtonDown("Fire1") && currentMiningLevel >= 1)
                {
                    //building.GetComponent<BuildingController>().health--;
                }
            }
            */

            //CAMPFIRE RAYCAST
            else if (hit.collider.gameObject.tag == "Campfire")
            {
                infoText.gameObject.SetActive(true);
                infoText.text = "Open/Close Campfire";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inv.OpenCampfire();
                }
            }

            //SLEEP RAYCAST
            else if (hit.collider.gameObject.tag == "Sleep")
            {
                infoText.gameObject.SetActive(true);
                infoText.text = "Sleep/Wake up";

                if (Input.GetButtonDown("Open"))
                {
                    if (statsScript.sleeping == false)
                    {
                        sleepPanel.SetActive(true);
                        statsScript.sleeping = true;
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        fpc.enabled = false;
                    }
                    else
                    {
                        sleepPanel.SetActive(false);
                        statsScript.sleeping = false;
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        fpc.enabled = true;
                    }
                }
            }

            //CRAFTING TABLE RAYCAST
            else if (hit.collider.gameObject.tag == "CraftingTable")
            {
                infoText.gameObject.SetActive(true);
                infoText.text = "Open/Close Crafting Table";

                if (Input.GetButtonDown("Open"))
                {
                    inv.OpenCraftingTable();
                }
            }

            //BUSH RAYCAST
            else if (hit.collider.gameObject.tag == "Berry")
            {
                GameObject bush = (hit.collider.gameObject);
                infoText.gameObject.SetActive(true);
                infoText.text = "Pick Berries";

                if (Input.GetButtonDown("Open"))
                {
                    BushController bushScript = bush.GetComponent<BushController>();
                    bushScript.Pick();
                }
            }

            //ITEM RAYCAST
            else if (hit.collider.gameObject.tag == "Item")
            {
                GameObject item = (hit.collider.gameObject);
                infoText.gameObject.SetActive(true);
                infoText.text = "Pick Up <i>" + item.name + "</i>";

                if (Input.GetButtonDown("Pickup"))
                {
                    ItemController itemScript = item.GetComponent<ItemController>();
                    itemScript.PickUp();
                    StartCoroutine(Notifications.Call("Picked Up <i>" + item.name + "</i>!"));
                    audioSrc.clip = otherClip[1];
                    audioSrc.Play();
                }
            }

            /*
            //CHEST RAYCAST
            else if (hit.collider.gameObject.tag == "Chest")
            {
                GameObject item = (hit.collider.gameObject);
                infoText.gameObject.SetActive(true);
                infoText.text = "Open/Close Chest";

                if (Input.GetButtonDown("Open"))
                {
                    //inv.OpenChest();
                }
            }
            */

            else
            {
                infoText.gameObject.SetActive(false);
            }
        }
        else
        {
            infoText.gameObject.SetActive(false);
        }
    }
}
