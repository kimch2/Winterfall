
using UnityEditor;
using UnityEngine;
public class EmeraldAIMenu : MonoBehaviour {



	[MenuItem ("Window/Emerald AI/Create AI/Create new AI (on selected object)", false, 1)]
    static void AssignEmeraldAIComponents () 
	{
		foreach (GameObject obj in Selection.gameObjects) 
		{
			obj.AddComponent<Emerald_Animal_AI>();
		}

		if (Selection.gameObjects.Length == 0)
		{
			Debug.Log("In order for the New AI button to work, you must have an active GameObject selected.");
		}
    }	

	[MenuItem ("Window/Emerald AI/Create Player/Combat Demo Player", false, 0)]
	static void SpawnPlayerCombat () 
	{
		Selection.activeObject = SceneView.currentDrawingSceneView;
		GameObject PlayerObject = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Emerald AI/Prefabs/Player/Emerald Combat Demo Player.prefab", typeof(GameObject))) as GameObject;
		GameObject PlayerUI = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Emerald AI/Prefabs/Player/Player UI.prefab", typeof(GameObject))) as GameObject;
		PlayerObject.transform.position = new Vector3 (0, 0, 0);
		PlayerObject.gameObject.name = "Emerald Combat Demo Player";
		PlayerUI.gameObject.name = "Player UI";
	}

	[MenuItem ("Window/Emerald AI/Create Player/Breeding Demo Player", false, 0)]
	static void SpawnPlayerBreeding () 
	{
		Selection.activeObject = SceneView.currentDrawingSceneView;
		GameObject PlayerObject = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Emerald AI/Prefabs/Player/Emerald Breeding Demo Player.prefab", typeof(GameObject))) as GameObject;
		GameObject PlayerUI = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Emerald AI/Prefabs/Player/Player UI.prefab", typeof(GameObject))) as GameObject;
		PlayerObject.transform.position = new Vector3 (0, 0, 0);
		PlayerObject.gameObject.name = "Emerald Breeding Demo Player";
		PlayerUI.gameObject.name = "Player UI";
	}

	[MenuItem ("Window/Emerald AI/Create Player/Player Inventory", false, 0)]
	static void PlayerInventory () 
	{
		Selection.activeObject = SceneView.currentDrawingSceneView;
		GameObject PlayerObject = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Emerald AI/Prefabs/Player/Player Inventory.prefab", typeof(GameObject))) as GameObject;
		GameObject PlayerUI = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Emerald AI/Prefabs/Player/Player Inventory UI.prefab", typeof(GameObject))) as GameObject;
		PlayerObject.transform.position = new Vector3 (0, 0, 0);
		PlayerObject.gameObject.name = "Player Inventory";
		PlayerUI.gameObject.name = "Player Inventory UI";
	}

	[MenuItem ("Window/Emerald AI/Create Example AI/Breedable AI", false, 0)]
	static void SpawnPassiveBreedingAI () 
	{
		Selection.activeObject = SceneView.currentDrawingSceneView;
		GameObject PlayerObject = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Emerald AI/Prefabs/Animals - NPCs/AI Examples/Breedable Animal Example.prefab", typeof(GameObject))) as GameObject;
		PlayerObject.gameObject.name = "Breeding Animal Example";
		PlayerObject.transform.position = new Vector3 (0, 0, 0);
	}

	[MenuItem ("Window/Emerald AI/Create Example AI/Passive AI", false, 0)]
	static void SpawnPassiveAI () 
	{
		Selection.activeObject = SceneView.currentDrawingSceneView;
		GameObject PlayerObject = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Emerald AI/Prefabs/Animals - NPCs/AI Examples/Passive AI Example.prefab", typeof(GameObject))) as GameObject;
		PlayerObject.gameObject.name = "Passive AI Example";
		PlayerObject.transform.position = new Vector3 (0, 0, 0);
	}

	[MenuItem ("Window/Emerald AI/Create Example AI/Fleeing AI", false, 0)]
	static void SpawnFleeingAI () 
	{
		Selection.activeObject = SceneView.currentDrawingSceneView;
		GameObject PlayerObject = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Emerald AI/Prefabs/Animals - NPCs/AI Examples/Fleeing AI Example.prefab", typeof(GameObject))) as GameObject;
		PlayerObject.gameObject.name = "Fleeing AI Example";
		PlayerObject.transform.position = new Vector3 (0, 0, 0);
	}

	[MenuItem ("Window/Emerald AI/Create Example AI/Defensive AI", false, 0)]
	static void SpawnDefensiveAI () 
	{
		Selection.activeObject = SceneView.currentDrawingSceneView;
		GameObject PlayerObject = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Emerald AI/Prefabs/Animals - NPCs/AI Examples/Defensive AI Example.prefab", typeof(GameObject))) as GameObject;
		PlayerObject.gameObject.name = "Defensive AI Example";
		PlayerObject.transform.position = new Vector3 (0, 0, 0);
	}

	[MenuItem ("Window/Emerald AI/Create Example AI/Hostile AI", false, 0)]
	static void SpawnHostileAI () 
	{
		Selection.activeObject = SceneView.currentDrawingSceneView;
		GameObject PlayerObject = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Emerald AI/Prefabs/Animals - NPCs/AI Examples/Hostile AI Example.prefab", typeof(GameObject))) as GameObject;
		PlayerObject.gameObject.name = "Hostile AI Example";
		PlayerObject.transform.position = new Vector3 (0, 0, 0);
	}

	[MenuItem ("Window/Emerald AI/Documentation/Home", false, 1000)]
	static void Home ()
	{
		Application.OpenURL("http://emerald-animal-ai.wikia.com/wiki/Emerald_Animal_AI_Wikia");
	}
	
	[MenuItem ("Window/Emerald AI/Documentation/Documentation", false, 1000)]
	static void Introduction ()
	{
		Application.OpenURL("http://emerald-animal-ai.wikia.com/wiki/Documentation");
	}
	
	[MenuItem ("Window/Emerald AI/Documentation/Tutorials/Video Tutorials", false, 100)]
	static void VideoTutorials ()
	{
		Application.OpenURL("https://www.youtube.com/playlist?list=PLlyiPBj7FznYWfVe4x7ffbdbF1eBjBE9x");
	}
	
	[MenuItem ("Window/Emerald AI/Documentation/Tutorials/Text Tutorials", false, 100)]
	static void Tutorials ()
	{
		Application.OpenURL("http://emerald-animal-ai.wikia.com/wiki/Tutorials");
	}
	
	[MenuItem ("Window/Emerald AI/Documentation/Code References", false, 1000)]
	static void CodeReferences ()
	{
		Application.OpenURL("http://emerald-animal-ai.wikia.com/wiki/Code_References");
	}
	
	[MenuItem ("Window/Emerald AI/Documentation/Example Scripts", false, 1000)]
	static void ExampleScripts ()
	{
		Application.OpenURL("http://emerald-animal-ai.wikia.com/wiki/Example_Scripts");
	}
	
	[MenuItem ("Window/Emerald AI/Documentation/Solutions to Possible Issues", false, 1000)]
	static void Solutions ()
	{
		Application.OpenURL("http://emerald-animal-ai.wikia.com/wiki/Solutions_to_Possible_Issues");
	}
	
	[MenuItem ("Window/Emerald AI/Documentation/Forums", false, 1000)]
	static void Forums ()
	{
		Application.OpenURL("http://forum.unity3d.com/threads/released-emerald-animal-ai-v1-1-dynamic-wildlife-predators-prey-packs-herds-npcs-more.336521/");
	}
	
	[MenuItem ("Window/Emerald AI/Customer Service", false, 10000000)]
	static void CustomerService ()
	{
		Application.OpenURL("http://www.blackhorizonstudios.com/contact/");
	}
}