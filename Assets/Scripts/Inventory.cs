using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Panels")]
    public GameObject inventoryPanel;
    public GameObject smeltingPanel;
    public GameObject craftingPanel;

    public int craftingTier;

    public Stats stats;
    public Building buildingScript;

    [System.Serializable]
    public class ItemsClass
    {
        public string name;
        public int item;
        public Text text;

        public ItemsClass(string name, int item, Text text)
        {
            this.name = name;
            this.item = item;
            this.text = text;
        }
    }

    public List<ItemsClass> items = new List<ItemsClass>();
    //public Dictionary<string, ItemsClass> itemDictionary = new Dictionary<string, ItemsClass>();

    [Header("Leveling")]
    public int xp;
    public Text levelText;

    [Header("Tools")]
    public GameObject[] equippedObjects;
    public GameObject hatchetGO;
    public GameObject pickaxeGO;
    public GameObject flashlightGO;

    public Transform invWindow;

    void Start()
    {
        items.Clear();

        items.Add(new ItemsClass("Log", 0, null)); // 0
        items.Add(new ItemsClass("Stone", 0, null)); // 1
        items.Add(new ItemsClass("Flint", 0, null)); // 2
        items.Add(new ItemsClass("Metal", 0, null)); // 3
        items.Add(new ItemsClass("Duct_Tape", 0, null)); // 4
        items.Add(new ItemsClass("Plank", 0, null)); // 5
        items.Add(new ItemsClass("Can", 0, null)); // 6
        items.Add(new ItemsClass("Canteen", 0, null)); // 7
        items.Add(new ItemsClass("Iron_Ore", 0, null)); // 8
        items.Add(new ItemsClass("Apple", 0, null)); // 9
        items.Add(new ItemsClass("Pills", 0, null)); // 10
        items.Add(new ItemsClass("Canned_Beans", 0, null)); // 11
        items.Add(new ItemsClass("Canteen_Water", 0, null)); // 12

        for (int i = 0; i < items.Count; i++)
        {
            GameObject inventoryGameObject = GameObject.FindGameObjectWithTag("InventoryItem");
            Text itemText = inventoryGameObject.transform.GetChild(i).GetChild(1).gameObject.GetComponent<Text>();
            items[i].text = itemText;
        }
    }

    #region Equip
    public void Equip(int itemID)
    {
        if (itemID == 0) //Hatchet
        {
            if (items[15].item >= 1 && equippedObjects[itemID].activeSelf == false)
            {
                UnequipAll();
                equippedObjects[itemID].SetActive(true);
            }
        }

        else if (itemID == 1) //Pickaxe
        {
            if (items[16].item >= 1 && equippedObjects[itemID].activeSelf == false)
            {
                UnequipAll();
                equippedObjects[itemID].SetActive(true);
            }
        }

        else if (itemID == 2) //Flashlight
        {
            if (items[17].item >= 1 && equippedObjects[itemID].activeSelf == false)
            {
                UnequipAll();
                equippedObjects[itemID].SetActive(true);
            }
        }

        else if (itemID == 3) //Torch
        {
            if (items[31].item >= 1 && equippedObjects[itemID].activeSelf == false)
            {
                UnequipAll();
                equippedObjects[itemID].SetActive(true);
            }
        }
    }
    #endregion

    #region Use 
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
        else if (itemID == 19) //Sapling
        {
            if (buildingScript.buildings[12].key)
            {
                buildingScript.buildings[12].key = false;
            }
            else
            {
                foreach (Building.BuildingsClass build in buildingScript.buildings)
                {
                    build.key = false;
                }
                buildingScript.buildings[12].key = true;
            }
        }
    }
    #endregion

    void Update()
    {
        levelText.text = "XP: " + xp;

        if (flashlightGO.activeSelf)
        {
            items[17].item -= (int)Time.deltaTime / 2;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UnequipAll();
        }

        if (Input.GetButtonDown("Inventory"))
        {
            OpenInventory();
        }
    }

    void OpenInventory()
    {
        if (inventoryPanel.activeSelf) CloseAllWindows();
        else
        {
            inventoryPanel.SetActive(true);
            craftingPanel.SetActive(true);
            foreach (Building.BuildingsClass build in buildingScript.buildings) build.key = false;
            CursorControll.UnlockCursor();
            foreach (ItemsClass itm in items) itm.text.text = itm.item.ToString();
            craftingTier = 0;
        }
    }

    void UnequipAll()
    {
        foreach (GameObject objct in equippedObjects)
        {
            objct.SetActive(false);
        }
    }

    #region Window Manager
    public void OpenCampfire()
    {
        if (smeltingPanel.activeSelf) CloseAllWindows();
        else
        {
            CursorControll.UnlockCursor();
            CloseAllWindows();
            smeltingPanel.SetActive(true);
            inventoryPanel.SetActive(true);
        }
    }

    public void CloseAllWindows()
    {
        smeltingPanel.SetActive(false);
        craftingPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        CursorControll.LockCursor();
    }

    public void OpenCraftingTable()
    {
        if (craftingPanel.activeSelf) CloseAllWindows();
        else
        {
            CursorControll.UnlockCursor();
            CloseAllWindows();
            craftingPanel.SetActive(true);
            inventoryPanel.SetActive(true);
            craftingTier = 1;
        }
    }
    #endregion          
}
