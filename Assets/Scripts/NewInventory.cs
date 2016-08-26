using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class NewInventory : MonoBehaviour
{

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

    public GameObject canvas;
    public GameObject emptyItem;

    /*
    void Update()
    {
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
                    if (itemGO != null) itemGO.GetComponent<Text>().text = itm.count.ToString();
                }

            }
        }
    }
    */
}
