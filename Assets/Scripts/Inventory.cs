using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    public GameObject inventoryWindow;
    public GameObject smeltingWindow;
    public GameObject tier2CraftingWindow;
    public GameObject craftingWindow;
    public Stats stats;
    public Building buildingScript;

    [System.Serializable]
    public class ItemsClass
    {
        public string name;
        public int item;
        public Text text;
    }

    public ItemsClass[] items;

    [Header("Leveling")]
    public int xp;
    public Text levelText;

    [Header("Tools")]
    public GameObject hatchetGO;
    public GameObject pickaxeGO;
    public GameObject flashlightGO;

    public Transform invWindow;

    public List<Item> itemss = new List<Item>();

    [System.Serializable]
    public class Item
    {
        public int id;
        public int count;
        public string name;
        public string description;
        public Texture2D icon;
        public ItemType type;

        public enum ItemType
        {
            Standard,
            Equipable,
            Consumable
        }

        public Item(string nam, int ide, string desc, ItemType typ)
        {
            name = nam;
            id = ide;
            description = desc;
            type = typ;
            icon = Resources.Load<Texture2D>("Item Icons/" + nam);
        }

        public Item()
        {

        }
    }

    void Start()
    {
        itemss.Add(new Item("Log", 0, "Wooden Log.", Item.ItemType.Standard));
        itemss.Add(new Item("Stone", 1, "Stone.", Item.ItemType.Standard));
    }

    public void Equip(int itemID)
    {
        if (itemID == 0) //Hatchet
        {
            if (items[15].item >= 1 && hatchetGO.activeSelf == false)
            {
                if (pickaxeGO.activeSelf)
                {
                    pickaxeGO.SetActive(false);
                }
                hatchetGO.SetActive(true);
            }
        }

        else if (itemID == 1) //Pickaxe
        {
            if (items[16].item >= 1 && pickaxeGO.activeSelf == false)
            {
                if (hatchetGO.activeSelf)
                {
                    hatchetGO.SetActive(false);
                }
                pickaxeGO.SetActive(true);
            }
        }

        else if (itemID == 2) //Flashlight
        {
            if (items[17].item >= 1 && flashlightGO.activeSelf == false)
            {
                flashlightGO.SetActive(true);
            }
        }
    }

    public void Use(int itemID)
    {
        if (itemID == 0) //Apple
        {
            if (items[9].item >= 1)
            {
                items[9].item--;
                stats.hunger += 15;
                stats.hungerSaturation += 20;
            }
        }
        else if (itemID == 1) //Pills
        {
            if (items[10].item >= 1)
            {
                items[10].item--;
                stats.health += 10;
            }
        }
        else if (itemID == 2) //Canned Beans
        {
            if (items[11].item >= 1)
            {
                items[11].item--;
                items[6].item++;
                stats.hunger += 100;
                stats.hungerSaturation += 120;
            }
        }
        else if (itemID == 3) //Canteen Water
        {
            if (items[12].item >= 1)
            {
                items[12].item--;
                items[7].item++;
                stats.thirst += 25;
                stats.thirstSaturation += 30;
            }
        }
        else if (itemID == 4) //Foundation
        {
            if (buildingScript.buildings[0].key)
            {
                buildingScript.buildings[0].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[0].key = true;
            }
        }
        else if (itemID == 5) //Pillar
        {
            if (buildingScript.buildings[1].key)
            {
                buildingScript.buildings[1].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[1].key = true;
                /*
                buildingScript.buildings[0].key = false;
                buildingScript.buildings[1].key = true;
                buildingScript.buildings[2].key = false;
                buildingScript.buildings[3].key = false;
                buildingScript.buildings[4].key = false;
                buildingScript.buildings[5].key = false;
                buildingScript.buildings[6].key = false;
                buildingScript.buildings[7].key = false;
                */
            }
        }
        else if (itemID == 6) //Wall
        {
            if (buildingScript.buildings[2].key)
            {
                buildingScript.buildings[2].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[2].key = true;
            }
        }
        else if (itemID == 7) //Doorway
        {
            if (buildingScript.buildings[3].key)
            {
                buildingScript.buildings[3].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[3].key = true;
            }
        }
        else if (itemID == 8) //Door
        {
            if (buildingScript.buildings[4].key)
            {
                buildingScript.buildings[4].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[4].key = true;
            }
        }
        else if (itemID == 9) //Stairs
        {
            if (buildingScript.buildings[5].key)
            {
                buildingScript.buildings[5].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[5].key = true;
            }
        }
        else if (itemID == 10) //Ceiling
        {
            if (buildingScript.buildings[6].key)
            {
                buildingScript.buildings[6].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[6].key = true;
            }
        }
        else if (itemID == 11) //Window
        {
            if (buildingScript.buildings[7].key)
            {
                buildingScript.buildings[7].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[7].key = true;
            }
        }
        else if (itemID == 12) //Berries
        {
            if (items[13].item >= 1)
            {
                items[13].item--;
                stats.hunger += 1;
                stats.hungerSaturation += 5;
            }
        }
        else if (itemID == 13) //Crafting Table
        {
            if (buildingScript.buildings[8].key)
            {
                buildingScript.buildings[8].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[8].key = true;
            }
        }
        else if (itemID == 14) //Campfire
        {
            if (buildingScript.buildings[9].key)
            {
                buildingScript.buildings[9].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[9].key = true;
            }
        }
        else if (itemID == 15) //Roasted Berries
        {
            if (items[14].item >= 1)
            {
                items[14].item--;
                stats.hunger += 5;
                stats.hungerSaturation += 20;
            }
        }
        else if (itemID == 17) //Bed
        {
            if (buildingScript.buildings[10].key)
            {
                buildingScript.buildings[10].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[10].key = true;
            }
        }
        else if (itemID == 18) //Roof
        {
            if (buildingScript.buildings[11].key)
            {
                buildingScript.buildings[11].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[11].key = true;
            }
        }
    }

    public GameObject canvas;
    public GameObject emptyItem;

    void Update()
    {
        levelText.text = "XP: " + xp;

        if (flashlightGO.activeSelf)
        {
            items[17].item -= (int)Time.deltaTime / 2;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (flashlightGO.activeSelf) flashlightGO.SetActive(false);
            else if (hatchetGO.activeSelf) hatchetGO.SetActive(false);
            else if (pickaxeGO.activeSelf) pickaxeGO.SetActive(false);
        }

        if (Input.GetButtonDown("Inventory"))
        {
            if (inventoryWindow.activeSelf) CloseAllWindows();
            else
            {
                inventoryWindow.SetActive(true);
                craftingWindow.SetActive(true);
                foreach (Building.BuildingsClass build in buildingScript.buildings) build.key = false;
                CursorControll.UnlockCursor();

                foreach (Item itm in itemss)
                {
                    GameObject itemGO = Instantiate(emptyItem, new Vector2(0, 0), Quaternion.identity) as GameObject;
                    itemGO.transform.SetParent(canvas.transform, false);
                    if(itemGO != null) itemGO.GetComponent<Text>().text = itm.count.ToString();
                }
            }
        }
    }

    public void OpenCampfire()
    {
        if (smeltingWindow.activeSelf) CloseAllWindows();
        else
        {
            craftingWindow.SetActive(false);
            smeltingWindow.SetActive(true);
            tier2CraftingWindow.SetActive(false);
            inventoryWindow.SetActive(true);
            CursorControll.UnlockCursor();
        }
    }

    public void CloseAllWindows()
    {
        var children = new List<GameObject>();
        foreach (Transform child in invWindow) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        smeltingWindow.SetActive(false);
        tier2CraftingWindow.SetActive(false);
        inventoryWindow.SetActive(false);
        CursorControll.LockCursor();
    }

    public void OpenCraftingTable()
    {
        if (tier2CraftingWindow.activeSelf) CloseAllWindows();
        else
        {
            craftingWindow.SetActive(false);
            smeltingWindow.SetActive(false);
            tier2CraftingWindow.SetActive(true);
            inventoryWindow.SetActive(true);
            CursorControll.UnlockCursor();
        }
    }
}
