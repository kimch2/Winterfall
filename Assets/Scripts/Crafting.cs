using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{

    public Inventory inv;
    public GameObject icon;
    private bool crafting = false;
    public Button[] craftingButtons;
    public Button[] lvl2craftingButtons;

    //Tier 1 Crafting

    //Planks
    public void CraftPlanks()
    {
        if (inv.items[0].item >= 1 && crafting == false)
        {
            inv.items[0].item -= 1;
            StartCoroutine(Time(3, "CraftedPlanks"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedPlanks()
    {
        inv.items[5].item++;
        inv.xp += 5;
        StartCoroutine(Notifications.Call("+2 Planks!"));
    }

    public void CraftHatchet()
    {
        if (inv.items[5].item >= 5 && inv.items[3].item >= 10 && inv.items[4].item >= 1 && crafting == false)
        {
            inv.items[5].item -= 5;
            inv.items[3].item -= 10;
            inv.items[4].item--;
            StartCoroutine(Time(3, "CraftedHatchet"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedHatchet()
    {
        inv.items[15].item += 105;
        inv.xp += 15;
        StartCoroutine(Notifications.Call("+105 Hatchet Durability!"));
    }

    public void CraftPickaxe()
    {
        if (inv.items[5].item >= 5 && inv.items[3].item >= 10 && inv.items[4].item >= 1 && crafting == false)
        {
            inv.items[5].item -= 5;
            inv.items[3].item -= 10;
            inv.items[4].item--;
            StartCoroutine(Time(3, "CraftedPickaxe"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedPickaxe()
    {
        inv.items[16].item += 70;
        inv.xp += 15;
        StartCoroutine(Notifications.Call(" +70 Pickaxe Durability!"));
    }

    public void CraftFlashlight()
    {
        if (inv.items[3].item >= 20 && inv.items[4].item >= 1 && crafting == false)
        {
            inv.items[3].item -= 20;
            inv.items[4].item--;
            StartCoroutine(Time(3, "CraftedFlashlight"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedFlashlight()
    {
        inv.items[17].item += 80;
        inv.xp += 25;
        StartCoroutine(Notifications.Call("+80 Flashlight Energy!"));
    }

    public void CraftFoundation()
    {
        if (inv.items[5].item >= 10 && inv.items[4].item >= 1 && inv.xp >= 100 && crafting == false)
        {
            inv.items[5].item -= 10;
            inv.items[4].item--;
            StartCoroutine(Time(3, "CraftedFoundation"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedFoundation()
    {
        inv.items[18].item++;
        inv.xp += 25;
        StartCoroutine(Notifications.Call("+1 Foundation!"));
    }

    public void CraftPillar()
    {
        if (inv.items[5].item >= 5 && inv.items[4].item >= 1 && inv.xp >= 100 && crafting == false)
        {
            inv.items[5].item -= 5;
            inv.items[4].item--;
            StartCoroutine(Time(3, "CraftedPillar"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedPillar()
    {
        inv.items[19].item += 2;
        inv.xp += 20;
        StartCoroutine(Notifications.Call("+2 Pillars!"));
    }

    public void CraftWall()
    {
        if (inv.items[5].item >= 10 && inv.items[4].item >= 1 && inv.xp >= 100 && crafting == false)
        {
            inv.items[5].item -= 10;
            inv.items[4].item--;
            StartCoroutine(Time(3, "CraftedWall"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedWall()
    {
        inv.items[20].item++;
        inv.xp += 20;
        StartCoroutine(Notifications.Call("+1 Wall!"));
    }

    //Doorway
    public void CraftDoorway()
    {
        if (inv.items[5].item >= 7 && inv.items[4].item >= 1 && inv.xp >= 250 && crafting == false)
        {
            inv.items[5].item -= 7;
            inv.items[4].item--;
            StartCoroutine(Time(3, "CraftedDoorway"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedDoorway()
    {
        inv.items[21].item++;
        inv.xp += 20;
        StartCoroutine(Notifications.Call("+1 Doorway!"));
    }

    //Door
    public void CraftDoor()
    {
        if (inv.items[5].item >= 5 && inv.items[4].item >= 1 && inv.xp >= 250 && crafting == false)
        {
            inv.items[5].item -= 5;
            inv.items[4].item--;
            StartCoroutine(Time(3, "CraftedDoor"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedDoor()
    {
        inv.items[22].item++;
        inv.xp += 20;
        StartCoroutine(Notifications.Call("+1 Door!"));
    }

    public void CraftStairs()
    {
        if (inv.items[5].item >= 15 && inv.items[4].item >= 1 && inv.xp >= 500 && crafting == false)
        {
            inv.items[5].item -= 15;
            inv.items[4].item--;
            StartCoroutine(Time(3, "CraftedStairs"));
        }
        else if (inv.xp <= 300)
        {
            StartCoroutine(Notifications.Call("Not Enough XP!"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedStairs()
    {
        inv.items[23].item++;
        inv.xp += 20;
        StartCoroutine(Notifications.Call("+1 Stairs!"));
    }

    //Ceiling
    public void CraftCeiling()
    {
        if (inv.items[5].item >= 12 && inv.items[4].item >= 1 && inv.xp >= 500 && crafting == false)
        {
            inv.items[5].item -= 12;
            inv.items[4].item--;
            StartCoroutine(Time(3, "CraftedCeiling"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedCeiling()
    {
        inv.items[24].item++;
        inv.xp += 20;
        StartCoroutine(Notifications.Call("+1 Ceiling!"));
    }


    //Window
    public void CraftWindow()
    {
        if (inv.items[5].item >= 7 && inv.items[4].item >= 1 && inv.items[3].item >= 1 && inv.xp >= 250 && crafting == false)
        {
            inv.items[5].item -= 3;
            inv.items[4].item--;
            inv.items[3].item--;
            StartCoroutine(Time(3, "CraftedWindow"));
        }
        else if (inv.xp <= 500)
        {
            StartCoroutine(Notifications.Call("Not Enough XP!"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedWindow()
    {
        inv.items[25].item++;
        inv.xp += 20;
        StartCoroutine(Notifications.Call("+1 Window!"));
    }

    //Crafting Table
    public void CraftCraftingTable()
    {
        if (inv.items[0].item >= 20 && inv.items[4].item >= 1 && crafting == false)
        {
            inv.items[0].item -= 20;
            inv.items[4].item--;
            StartCoroutine(Time(3, "CraftedCraftingTable"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedCraftingTable()
    {
        inv.items[27].item++;
        inv.xp += 35;
        StartCoroutine(Notifications.Call("+1 Crafting Table!"));
    }

    //Campfire
    public void CraftCampfire()
    {
        if (inv.items[0].item >= 10 && inv.items[2].item >= 1 && inv.items[1].item >= 10 && inv.xp >= 250 && crafting == false)
        {
            inv.items[0].item -= 10;
            inv.items[2].item -= 10;
            inv.items[1].item--;
            StartCoroutine(Time(3, "CraftedCampfire"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedCampfire()
    {
        inv.items[26].item++;
        inv.xp += 20;
        StartCoroutine(Notifications.Call("+1 Campfire!"));
    }

    //Bed
    public void CraftBed()
    {
        if (inv.items[5].item >= 30 && inv.xp >= 250 && crafting == false)
        {
            inv.items[5].item -= 30;
            StartCoroutine(Time(5, "CraftedBed"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedBed()
    {
        inv.items[28].item++;
        inv.xp += 25;
        StartCoroutine(Notifications.Call("+1 Bed!"));
    }

    //Roof
    public void CraftRoof()
    {
        if (inv.items[5].item >= 10 && inv.items[4].item >= 1 && inv.xp >= 250 && crafting == false)
        {
            inv.items[5].item -= 10;
            inv.items[4].item--;
            StartCoroutine(Time(5, "CraftedRoof"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void CraftedRoof()
    {
        inv.items[28].item++;
        inv.xp += 25;
        StartCoroutine(Notifications.Call("+1 Bed!"));
    }

    //Refill Canteen
    public void RefillCanteen()
    {
        if (transform.position.y <= 15 && inv.items[7].item >= 1)
        {
            inv.items[7].item--;
            StartCoroutine(Time(8, "RefilledCanteen"));
        }
    }

    void RefilledCanteen()
    {
        inv.items[12].item++;
        inv.xp += 15;
        StartCoroutine(Notifications.Call("+1 Canteen (Water)!"));
    }

    //Smelting\\

    //Iron
    public void SmeltIron()
    {
        if (inv.items[0].item >= 2 && inv.items[8].item >= 1 && crafting == false)
        {
            inv.items[0].item -= 2;
            inv.items[8].item--;
            StartCoroutine(Time(5, "SmeltedIron"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void SmeltedIron()
    {
        inv.items[3].item += 5;
        inv.xp += 15;
        StartCoroutine(Notifications.Call("+5 Metal!"));
    }

    //Berries
    public void RoastBerries()
    {
        if (inv.items[0].item >= 1 && inv.items[13].item >= 1 && crafting == false)
        {
            inv.items[0].item--;
            inv.items[13].item--;
            StartCoroutine(Time(1, "RoastedBerries"));
        }
        else if (crafting)
        {
            StartCoroutine(Notifications.Call("Crafting queue is full!"));
        }
        else
        {
            StartCoroutine(Notifications.Call("Not enough materials!"));
        }
    }

    void RoastedBerries()
    {
        inv.items[14].item++;
        inv.xp += 3;
        StartCoroutine(Notifications.Call("Roasted the Berries!"));
    }

    IEnumerator Time(int time, string function)
    {
        icon.SetActive(true);
        crafting = true;
        yield return new WaitForSeconds(time);
        icon.SetActive(false);
        Invoke(function, 0);
        crafting = false;
    }
}
