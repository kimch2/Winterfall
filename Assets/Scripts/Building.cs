using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{

    [System.Serializable]
    public class BuildingsClass
    {
        public string name;
        public GameObject prefab;
        public Transform spawn;
        public Renderer rend;
        public bool key;
        public bool building;
    }

    public BuildingsClass[] buildings;

    public LayerMask myLayerMask;

    private GameObject player;
    private Inventory inv;

    public int maxBuildDistance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inv = player.GetComponent<Inventory>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2), 0)), out hit, maxBuildDistance, myLayerMask))
        {
            /**********************************************************/

            /*FOUNDATION*/
            if (buildings[0].building == false)
            {
                buildings[0].spawn.gameObject.SetActive(false);
            }
            if (buildings[0].building == false && inv.items[18].item >= 1 && buildings[0].key)
            {
                buildings[0].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[0].building = true;
            }
            else if (buildings[0].building == true && inv.items[18].item == 0)
            {
                buildings[0].spawn.gameObject.SetActive(false);
                buildings[0].building = false;
            }
            else if (buildings[0].building == true && buildings[0].key == false)
            {
                buildings[0].spawn.gameObject.SetActive(false);
                buildings[0].building = false;
            }
            /********************************************************/

            /*PILLAR*/
            if (buildings[1].building == false)
            {
                buildings[1].spawn.gameObject.SetActive(false);
            }
            if (buildings[1].building == false && inv.items[19].item >= 1 && buildings[1].key)
            {
                buildings[1].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[1].building = true;
            }
            else if (buildings[1].building == true && inv.items[19].item == 0)
            {
                buildings[1].spawn.gameObject.SetActive(false);
                buildings[1].building = false;
            }
            else if (buildings[1].building == true && buildings[1].key == false)
            {
                buildings[1].spawn.gameObject.SetActive(false);
                buildings[1].building = false;
            }
            /************************************************************/

            /*WALL*/
            if (buildings[2].building == false)
            {
                buildings[2].spawn.gameObject.SetActive(false);
            }
            if (buildings[2].building == false && inv.items[20].item >= 1 && buildings[2].key)
            {
                buildings[2].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[2].building = true;
            }
            else if (buildings[2].building == true && inv.items[20].item == 0)
            {
                buildings[2].spawn.gameObject.SetActive(false);
                buildings[2].building = false;
            }
            else if (buildings[2].building == true && buildings[2].key == false)
            {
                buildings[2].spawn.gameObject.SetActive(false);
                buildings[2].building = false;
            }
            /************************************************************/

            /*DOORWAY*/
            if (buildings[3].building == false)
            {
                buildings[3].spawn.gameObject.SetActive(false);
            }
            if (buildings[3].building == false && inv.items[21].item >= 1 && buildings[3].key)
            {
                buildings[3].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[3].building = true;
            }
            else if (buildings[3].building == true && inv.items[21].item == 0)
            {
                buildings[3].spawn.gameObject.SetActive(false);
                buildings[3].building = false;
            }
            else if (buildings[3].building == true && buildings[3].key == false)
            {
                buildings[3].spawn.gameObject.SetActive(false);
                buildings[3].building = false;
            }
            /************************************************************/

            /*DOOR*/
            if (buildings[4].building == false)
            {
                buildings[4].spawn.gameObject.SetActive(false);
            }
            if (buildings[4].building == false && inv.items[22].item >= 1 && buildings[4].key)
            {
                buildings[4].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[4].building = true;
            }
            else if (buildings[4].building == true && inv.items[22].item == 0)
            {
                buildings[4].spawn.gameObject.SetActive(false);
                buildings[4].building = false;
            }
            else if (buildings[4].building == true && buildings[4].key == false)
            {
                buildings[4].spawn.gameObject.SetActive(false);
                buildings[4].building = false;
            }
            /************************************************************/

            /*STAIRS*/
            if (buildings[5].building == false)
            {
                buildings[5].spawn.gameObject.SetActive(false);
            }
            if (buildings[5].building == false && inv.items[23].item >= 1 && buildings[5].key)
            {
                buildings[5].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[5].building = true;
            }
            else if (buildings[5].building == true && inv.items[23].item == 0)
            {
                buildings[5].spawn.gameObject.SetActive(false);
                buildings[5].building = false;
            }
            else if (buildings[5].building == true && buildings[5].key == false)
            {
                buildings[5].spawn.gameObject.SetActive(false);
                buildings[5].building = false;
            }

            if (Input.GetKeyDown("r"))
            {
                transform.Rotate(0, 90, 0); //All buildings
            }
            /************************************************************/

            /*CEILING*/
            if (buildings[6].building == false)
            {
                buildings[6].spawn.gameObject.SetActive(false);
            }
            if (buildings[6].building == false && inv.items[24].item >= 1 && buildings[6].key)
            {
                buildings[6].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[6].building = true;
            }
            else if (buildings[6].building == true && inv.items[24].item == 0)
            {
                buildings[6].spawn.gameObject.SetActive(false);
                buildings[6].building = false;
            }
            else if (buildings[6].building == true && buildings[6].key == false)
            {
                buildings[6].spawn.gameObject.SetActive(false);
                buildings[6].building = false;
            }
            /************************************************************/

            /*WINDOW*/
            if (buildings[7].building == false)
            {
                buildings[7].spawn.gameObject.SetActive(false);
            }
            if (buildings[7].building == false && inv.items[25].item >= 1 && buildings[7].key)
            {
                buildings[7].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[7].building = true;
            }
            else if (buildings[7].building == true && inv.items[25].item == 0)
            {
                buildings[7].spawn.gameObject.SetActive(false);
                buildings[7].building = false;
            }
            else if (buildings[7].building == true && buildings[7].key == false)
            {
                buildings[7].spawn.gameObject.SetActive(false);
                buildings[7].building = false;
            }
            /************************************************************/

            /*CRAFTING TABLE*/
            if (buildings[8].building == false)
            {
                buildings[8].spawn.gameObject.SetActive(false);
            }
            if (buildings[8].building == false && inv.items[26].item >= 1 && buildings[8].key)
            {
                buildings[8].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[8].building = true;
            }
            else if (buildings[8].building == true && inv.items[26].item == 0)
            {
                buildings[8].spawn.gameObject.SetActive(false);
                buildings[8].building = false;
            }
            else if (buildings[8].building == true && buildings[8].key == false)
            {
                buildings[8].spawn.gameObject.SetActive(false);
                buildings[8].building = false;
            }
            /************************************************************/

            /*CAMPFIRE*/
            if (buildings[9].building == false)
            {
                buildings[9].spawn.gameObject.SetActive(false);
            }
            if (buildings[9].building == false && inv.items[27].item >= 1 && buildings[9].key)
            {
                buildings[9].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[9].building = true;
            }
            else if (buildings[9].building == true && inv.items[27].item == 0)
            {
                buildings[9].spawn.gameObject.SetActive(false);
                buildings[9].building = false;
            }
            else if (buildings[9].building == true && buildings[9].key == false)
            {
                buildings[9].spawn.gameObject.SetActive(false);
                buildings[9].building = false;
            }
            /************************************************************/

            /*BED*/
            if (buildings[10].building == false)
            {
                buildings[10].spawn.gameObject.SetActive(false);
            }
            if (buildings[10].building == false && inv.items[28].item >= 1 && buildings[10].key)
            {
                buildings[10].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[10].building = true;
            }
            else if (buildings[10].building == true && inv.items[28].item == 0)
            {
                buildings[10].spawn.gameObject.SetActive(false);
                buildings[10].building = false;
            }
            else if (buildings[10].building == true && buildings[10].key == false)
            {
                buildings[10].spawn.gameObject.SetActive(false);
                buildings[10].building = false;
            }
            /************************************************************/

            /*ROOF*/
            if (buildings[11].building == false)
            {
                buildings[11].spawn.gameObject.SetActive(false);
            }
            if (buildings[11].building == false && inv.items[29].item >= 1 && buildings[11].key)
            {
                buildings[11].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[11].building = true;
            }
            else if (buildings[11].building == true && inv.items[29].item == 0)
            {
                buildings[11].spawn.gameObject.SetActive(false);
                buildings[11].building = false;
            }
            else if (buildings[11].building == true && buildings[11].key == false)
            {
                buildings[11].spawn.gameObject.SetActive(false);
                buildings[11].building = false;
            }
            /************************************************************/

            /*SAPLING*/
            if (buildings[12].building == false)
            {
                buildings[12].spawn.gameObject.SetActive(false);
            }
            if (buildings[12].building == false && inv.items[30].item >= 1 && buildings[12].key)
            {
                buildings[12].spawn.gameObject.SetActive(true);
                foreach (BuildingsClass building in buildings)
                {
                    building.building = false;
                }
                buildings[12].building = true;
            }
            else if (buildings[12].building == true && inv.items[30].item == 0)
            {
                buildings[12].spawn.gameObject.SetActive(false);
                buildings[12].building = false;
            }
            else if (buildings[12].building == true && buildings[12].key == false)
            {
                buildings[12].spawn.gameObject.SetActive(false);
                buildings[12].building = false;
            }
            /************************************************************/

            //* HIT POINTS *//

            //Foundation
            if (buildings[0].building == true)
            {
                buildings[0].spawn.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            //Pillar
            if (buildings[1].building == true)
            {
                buildings[1].spawn.transform.position = new Vector3(hit.point.x, hit.point.y + 2, hit.point.z);
            }

            //Wall
            if (buildings[2].building == true)
            {
                buildings[2].spawn.transform.position = new Vector3(hit.point.x, hit.point.y + 2, hit.point.z);
            }

            //Doorway
            if (buildings[3].building == true)
            {
                buildings[3].spawn.transform.position = new Vector3(hit.point.x, hit.point.y + 2, hit.point.z);
            }

            //Door
            if (buildings[4].building == true)
            {
                buildings[4].spawn.transform.position = new Vector3(hit.point.x, hit.point.y + 2, hit.point.z);
            }

            //Stairs
            if (buildings[5].building == true)
            {
                buildings[5].spawn.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            //Ceiling
            if (buildings[6].building == true)
            {
                buildings[6].spawn.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            //Window
            if (buildings[7].building == true)
            {
                buildings[7].spawn.transform.position = new Vector3(hit.point.x, hit.point.y + 2, hit.point.z);
            }

            //Crafting Table
            if (buildings[8].building == true)
            {
                buildings[8].spawn.transform.position = new Vector3(hit.point.x, hit.point.y + .5f, hit.point.z);
            }

            //Campfire
            if (buildings[9].building == true)
            {
                buildings[9].spawn.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            //Bed
            if (buildings[10].building == true)
            {
                buildings[10].spawn.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            //Roof
            if (buildings[11].building == true)
            {
                buildings[11].spawn.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            //Sapling
            if (buildings[12].building == true)
            {
                buildings[12].spawn.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            //* What? *//

            if (hit.transform.tag == "RoofPos")
            {
                buildings[11].spawn.transform.position = hit.transform.position;
            }

            if (hit.transform.tag == "SueloPos")
            {
                buildings[0].spawn.transform.position = hit.transform.position;
            }

            //* ??? *//

            //Foundation
            if (hit.transform.tag == "Floor" || hit.transform.tag == "SueloPos")
            {
                buildings[0].rend.material.color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                buildings[0].rend.material.color = new Color(1, 0, 0, 0.5f);
            }

            //Crafting Table
            if (hit.transform.tag == "Floor" || hit.transform.tag == "SueloMadera")
            {
                buildings[8].rend.material.color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                buildings[8].rend.material.color = new Color(1, 0, 0, 0.5f);
            }

            //Campfire
            if (hit.transform.tag == "Floor" || hit.transform.tag == "SueloMadera")
            {
                buildings[9].rend.material.color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                buildings[9].rend.material.color = new Color(1, 0, 0, 0.5f);
            }

            //Bed
            if (hit.transform.tag == "Floor" || hit.transform.tag == "SueloMadera")
            {
                buildings[10].rend.material.color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                buildings[10].rend.material.color = new Color(1, 0, 0, 0.5f);
            }

            //Roof
            if (hit.transform.tag == "RoofPos")
            {
                buildings[11].rend.material.color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                buildings[11].rend.material.color = new Color(1, 0, 0, 0.5f);
            }

            //Sapling
            if (hit.transform.tag == "Floor" || hit.transform.tag == "SueloMadera")
            {
                buildings[12].rend.material.color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                buildings[12].rend.material.color = new Color(1, 0, 0, 0.5f);
            }

            if (hit.transform.tag == "SueloMadera" || hit.transform.tag == "EscaleraPos" || hit.transform.tag == "Techo" || hit.transform.tag == "TechoPos")
            {
                buildings[5].spawn.transform.position = hit.transform.position + hit.normal.normalized * 0.4f;
                buildings[5].rend.material.color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                buildings[5].rend.material.color = new Color(1, 0, 0, 0.5f);
            }

            if (hit.transform.tag == "Pilar")
            {
                buildings[1].spawn.transform.position = hit.transform.position;
                buildings[1].rend.material.color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                buildings[1].rend.material.color = new Color(1, 0, 0, 0.5f);
            }

            if (hit.transform.tag == "Pared")
            {
                buildings[2].spawn.transform.position = hit.transform.position;
                buildings[2].spawn.transform.rotation = hit.transform.rotation;
                buildings[2].rend.material.color = new Color(0, 1, 0, 0.3f);

                buildings[3].spawn.transform.position = hit.transform.position;
                buildings[3].spawn.transform.rotation = hit.transform.rotation;
                buildings[3].rend.material.color = new Color(0, 1, 0, 0.3f);

                buildings[7].spawn.transform.position = hit.transform.position;
                buildings[7].spawn.transform.rotation = hit.transform.rotation;
                buildings[7].rend.material.color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                buildings[2].rend.material.color = new Color(1, 0, 0, 0.5f);

                buildings[3].rend.material.color = new Color(1, 0, 0, 0.5f);

                buildings[7].rend.material.color = new Color(1, 0, 0, 0.5f);
            }

            if (hit.transform.tag == "HuecoPuertaPos")
            {
                buildings[4].spawn.transform.position = hit.transform.position;
                buildings[4].spawn.transform.rotation = hit.transform.rotation;
                buildings[4].rend.material.color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                buildings[4].rend.material.color = new Color(1, 0, 0, 0.5f);
            }

            if (hit.transform.tag == "TechoPos")
            {
                buildings[6].spawn.transform.position = hit.transform.position;
                buildings[6].rend.material.color = new Color(0, 1, 0, 0.3f);
            }
            else
            {
                buildings[6].rend.material.color = new Color(1, 0, 0, 0.5f);
            }

            if (hit.transform.tag == "EscaleraPos")
            {
                buildings[5].spawn.transform.position = hit.transform.position;
                buildings[5].spawn.transform.rotation = hit.transform.rotation;
            }

            //* PLACE OBJECT *//

            //Foundation
            if (buildings[0].building == true && Input.GetMouseButtonDown(0))
                if (hit.transform.tag == "Floor" || hit.transform.tag == "SueloPos")
                {
                    Instantiate(buildings[0].prefab, buildings[0].spawn.transform.position, buildings[0].spawn.transform.rotation);
                    inv.items[18].item -= 1;
                }

            //Pillar
            if (buildings[1].building == true && hit.transform.tag == "Pilar" && Input.GetMouseButtonDown(0))
            {
                Instantiate(buildings[1].prefab, buildings[1].spawn.transform.position, buildings[1].spawn.transform.rotation);
                inv.items[19].item -= 1;
            }

            if (buildings[2].building == true && hit.transform.tag == "Pared" && Input.GetMouseButtonDown(0))
            {
                Instantiate(buildings[2].prefab, buildings[2].spawn.transform.position, buildings[2].spawn.transform.rotation);
                inv.items[20].item -= 1;
            }

            if (buildings[3].building == true && hit.transform.tag == "Pared" && Input.GetMouseButtonDown(0))
            {
                Instantiate(buildings[3].prefab, buildings[3].spawn.transform.position, buildings[3].spawn.transform.rotation);
                inv.items[21].item -= 1;
            }

            if (buildings[4].building == true && hit.transform.tag == "HuecoPuertaPos" && Input.GetMouseButtonDown(0))
            {
                Instantiate(buildings[4].prefab, buildings[4].spawn.transform.position, buildings[4].spawn.transform.rotation);
                inv.items[22].item -= 1;
            }

            if (buildings[5].building == true && Input.GetMouseButtonDown(0))
                if (hit.transform.tag == "SueloMadera" || hit.transform.tag == "TechoPos" || hit.transform.tag == "Techo" || hit.transform.tag == "EscaleraPos")
                {
                    Instantiate(buildings[5].prefab, buildings[5].spawn.transform.position, buildings[5].spawn.transform.rotation);
                    inv.items[23].item -= 1;
                }

            if (buildings[6].building == true && hit.transform.tag == "TechoPos" && Input.GetMouseButtonDown(0))
            {
                Instantiate(buildings[6].prefab, buildings[6].spawn.transform.position, buildings[6].spawn.transform.rotation);
                inv.items[24].item -= 1;
            }

            if (buildings[7].building == true && hit.transform.tag == "Pared" && Input.GetMouseButtonDown(0))
            {
                Instantiate(buildings[7].prefab, buildings[7].spawn.transform.position, buildings[7].spawn.transform.rotation);
                inv.items[25].item -= 1;
            }
            
            //Crafting Table
            if (buildings[8].building == true && Input.GetMouseButtonDown(0))
                if (hit.transform.tag == "Floor" || hit.transform.tag == "SueloMadera")
                {
                    Instantiate(buildings[8].prefab, buildings[8].spawn.transform.position, buildings[8].spawn.transform.rotation);
                    inv.items[26].item -= 1;
                }

            //Campfire
            if (buildings[9].building == true && Input.GetMouseButtonDown(0))
                if (hit.transform.tag == "Floor" || hit.transform.tag == "SueloMadera")
                {
                    Instantiate(buildings[9].prefab, buildings[9].spawn.transform.position, buildings[9].spawn.transform.rotation);
                    inv.items[27].item -= 1;
                }

            //Bed
            if (buildings[10].building == true && Input.GetMouseButtonDown(0))
                if (hit.transform.tag == "Floor" || hit.transform.tag == "SueloMadera")
                {
                    Instantiate(buildings[10].prefab, buildings[10].spawn.transform.position, buildings[10].spawn.transform.rotation);
                    inv.items[28].item -= 1;
                }

            //Roof
            if (buildings[11].building == true && hit.transform.tag == "RoofPos" && Input.GetMouseButtonDown(0))
            {
                Instantiate(buildings[11].prefab, buildings[11].spawn.transform.position, buildings[11].spawn.transform.rotation);
                inv.items[29].item -= 1;
            }

            //Sapling
            if (buildings[12].building == true && hit.transform.tag == "Floor" && Input.GetMouseButtonDown(0))
            {
                Instantiate(buildings[12].prefab, buildings[12].spawn.transform.position, buildings[12].spawn.transform.rotation);
                inv.items[30].item -= 1;
            }

            //Destroy Object
            if (Input.GetKeyDown("f10"))
            {
                if (hit.transform.tag == "ParedMadera" || hit.transform.tag == "ParedMaderaPuerta" || hit.transform.tag == "PilarMadera" || hit.transform.tag == "SueloMadera" || hit.transform.tag == "Door" || hit.transform.tag == "Techo")
                    Destroy(hit.transform.gameObject);
            }
        }
    }
}
