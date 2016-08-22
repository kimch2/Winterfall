//Emerald - Animal AI by: Black Horizon Studios
//Version 1.1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Emerald_Animal_AI))] 
//[CanEditMultipleObjects]
[System.Serializable]
public class Emerald_Animal_AI_Editor : Editor 
{

	bool showHelpOptions = true;

	//Animal Only
	enum AgressionDropDown
	{
		Cowardly = 1,
		Passive = 2,
		Hostile = 3,
		Defensive = 4
	}

	enum AgressionNPCDropDown
	{
		Cowardly = 0,
		Passive = 2,
		Hostile = 3,
		Defensive = 4
	}

	enum PreyOrPredator
	{
		Prey = 1,
		Predator = 2,
		None = 3
	}

	/*
	enum PreyOrPredatorNPC
	{
		Cowardly = 1,
		Defensive = 2,
		Passive = 3
	}
	*/

	enum PreySize
	{
		Small = 1,
		Medium = 2,
		Large = 3
	}

	enum PredatorSize
	{
		Small = 1,
		Medium = 2,
		Large = 3
	}

	enum Alpha
	{
		Yes = 1,
		No = 2
	}

	enum AnimalOrNPC
	{
		Animal = 0,
		NPC = 1
		//Companion = 2
	}

	enum FleeType
	{
		Distance = 0,
		_Time = 1
		//Companion = 2
	}

	enum UseBreeding
	{
		Yes = 1,
		No = 2
	}
	

	AgressionNPCDropDown editorAgressionNPC = AgressionNPCDropDown.Hostile;
	AgressionDropDown editorAgression = AgressionDropDown.Cowardly;

	PreyOrPredator editorPreyOrPredator = PreyOrPredator.Prey;
	//PreyOrPredatorNPC editorPreyOrPredatorNPC = PreyOrPredatorNPC.Defensive;

	PreySize editorPreySize = PreySize.Medium;
	PredatorSize editorPredatorSize = PredatorSize.Medium;
	Alpha editorAlpha = Alpha.No;
	FleeType editorFleeType = FleeType._Time;

	AnimalOrNPC editorAnimalOrNPC = AnimalOrNPC.Animal;
	UseBreeding editorUseBreeding = UseBreeding.No;

	public string tagStr = "";

	public string[] TabString = new string[] {"Behavior Options", "Herd Options", "Attack Options", "Health Options", "Sound Options", "Pathfinding Options", "Range Options", "Animation Options", "Movement Options", "Tag Options", "Effects Options", "Breeding Options", "Show All Options"};
	//public string[] TabAllString = new string[] {"Show All Options"};


	SerializedObject temp;
	SerializedObject tabTemp;

	//Int Serialized Properties
	SerializedProperty TabNumberProp;
	SerializedProperty TabNumberAllProp;
	SerializedProperty AggressionProp;
	SerializedProperty AITypeProp;
	SerializedProperty CurrentHealthProp;
	SerializedProperty AttackDamageMinProp;
	SerializedProperty AttackDamageMaxProp;
	SerializedProperty PreyOrPredatorProp;
	SerializedProperty PreySizeProp;
	SerializedProperty PredatorSizeProp;
	SerializedProperty IsAlphaOrNotProp;
	SerializedProperty MaxPackSizeProp;
	SerializedProperty MaxDistanceFromHerdProp;
	SerializedProperty StartingHealthProp;
	SerializedProperty TotalAttackSoundsProp;
	SerializedProperty TotalHitSoundsProp;
	SerializedProperty TotalAnimalSoundsProp;
	SerializedProperty ExtraFleeSecondsProp;
	SerializedProperty MaxFleeDistanceProp;
	SerializedProperty FleeTypeProp;
	SerializedProperty FleeRadiusProp;
	SerializedProperty ChaseSecondsProp;
	SerializedProperty HuntRadiusProp;
	SerializedProperty WanderRangeProp;
	SerializedProperty GrazeLengthMinProp;
	SerializedProperty GrazeLengthMaxProp;
	SerializedProperty TurnSpeedProp;
	SerializedProperty TotalGrazeAnimationsProp;
	SerializedProperty TotalAttackAnimationsProp;
	SerializedProperty UseBreedingProp;
	SerializedProperty CommonOddsProp;
	SerializedProperty UncommonOddsProp;
	SerializedProperty RareOddsProp;
	SerializedProperty SuperRareOddsProp;
	SerializedProperty MaximumWalkingVelocityProp;

	//String Serialized Properties
	SerializedProperty NPCNameProp;
	SerializedProperty AnimalTypeProp;
	SerializedProperty PreyTagNameProp;
	SerializedProperty PlayerTagNameProp;
	SerializedProperty PredatorTagNameProp;
	SerializedProperty EnemyTagNameProp;
	SerializedProperty FollowTagNameProp;

	//Float Serialized Properties
	SerializedProperty OffSetHerdXMinProp;
	SerializedProperty OffSetHerdXMaxProp;
	SerializedProperty OffSetHerdZMinProp;
	SerializedProperty OffSetHerdZMaxProp;
	SerializedProperty AttackTimeMinProp;
	SerializedProperty AttackTimeMaxProp;
	SerializedProperty AttackDelaySecondsProp;
	SerializedProperty FootStepSecondsWalkProp;
	SerializedProperty FootStepSecondsProp;
	SerializedProperty MinSoundPitchProp;
	SerializedProperty MaxSoundPitchProp;
	SerializedProperty PathWidthProp;
	SerializedProperty LineYOffSetProp;
	SerializedProperty UpdateSpeedProp;
	SerializedProperty StoppingDistanceProp;
	SerializedProperty CoolDownSecondsProp;
	SerializedProperty MaxChaseDistanceProp;
	SerializedProperty FreezeSecondsMinProp;
	SerializedProperty FreezeSecondsMaxProp;
	SerializedProperty WalkSpeedProp;
	SerializedProperty RunSpeedProp;
	SerializedProperty WalkAnimationSpeedProp;
	SerializedProperty RunAnimationSpeedProp;
	SerializedProperty BreedSecondsProp;
	SerializedProperty BreedCoolDownSecondsProp;
	SerializedProperty BabySecondsProp;
	SerializedProperty BaseOffsetNavProp;
	
	//Bool Serialized Properties
	SerializedProperty HerdFullProp;
	SerializedProperty AutoGenerateAlphaProp;
	SerializedProperty AlphaWaitForHerdProp;
	SerializedProperty AutoCalculateDelaySecondsProp;
	SerializedProperty UseDeadReplacementProp;
	SerializedProperty UseWeaponSoundProp;
	SerializedProperty UseAttackSoundProp;
	SerializedProperty UseHitSoundProp;
	SerializedProperty UseAnimalSoundProp;
	SerializedProperty UseDustEffectProp;
	SerializedProperty UseDieSoundProp;
	SerializedProperty PlaySoundOnFleeProp;
	SerializedProperty UseWalkSoundProp;
	SerializedProperty UseRunSoundProp;
	SerializedProperty DrawWaypointsProp;
	SerializedProperty DrawPathsProp;
	SerializedProperty AlignAIProp;
	SerializedProperty UseVisualRadiusProp;
	SerializedProperty ReturnsToStartProp;
	SerializedProperty ReturnBackToStartingPointProtectionProp;
	SerializedProperty UseAnimationsProp;
	SerializedProperty UseTurnAnimationProp;
	SerializedProperty UseRunAttackAnimationsProp;
	SerializedProperty UseBloodProp;
	SerializedProperty IsBabyProp;
	SerializedProperty UseBreedEffectProp;
	SerializedProperty UseHitAnimationProp;
	
	//Object Serialized Properties
	SerializedProperty DeadObjectProp;
	SerializedProperty WeaponSoundProp;
	SerializedProperty AttackSound1Prop;
	SerializedProperty AttackSound2Prop;
	SerializedProperty AttackSound3Prop;
	SerializedProperty AttackSound4Prop;
	SerializedProperty AttackSound5Prop;
	SerializedProperty HitSound1Prop;
	SerializedProperty HitSound2Prop;
	SerializedProperty HitSound3Prop;
	SerializedProperty HitSound4Prop;
	SerializedProperty HitSound5Prop;
	SerializedProperty AnimalSound1Prop;
	SerializedProperty AnimalSound2Prop;
	SerializedProperty AnimalSound3Prop;
	SerializedProperty DieSoundProp;
	SerializedProperty FleeSoundProp;
	SerializedProperty RunSoundProp;
	SerializedProperty PathMaterialProp;
	SerializedProperty WalkSoundProp;
	SerializedProperty IdleAnimationProp;
	SerializedProperty IdleBattleAnimationProp;
	SerializedProperty Graze1AnimationProp;
	SerializedProperty Graze2AnimationProp;
	SerializedProperty Graze3AnimationProp;
	SerializedProperty WalkAnimationProp;
	SerializedProperty RunAnimationProp;
	SerializedProperty TurnAnimationProp;
	SerializedProperty HitAnimationProp;
	SerializedProperty AttackAnimation1Prop;
	SerializedProperty AttackAnimation2Prop;
	SerializedProperty AttackAnimation3Prop;
	SerializedProperty AttackAnimation4Prop;
	SerializedProperty AttackAnimation5Prop;
	SerializedProperty AttackAnimation6Prop;
	SerializedProperty RunAttackAnimationProp;
	SerializedProperty DeathAnimationProp;
	SerializedProperty BloodEffectProp;
	SerializedProperty DustEffectProp;
	SerializedProperty FullGrownPrefabProp;
	SerializedProperty BreedEffectProp;
	SerializedProperty BabyPrefabCommonProp;
	SerializedProperty BabyPrefabUncommonProp;
	SerializedProperty BabyPrefabRareProp;
	SerializedProperty BabyPrefabSuperRareProp;

	
	//List
	SerializedProperty AttackSoundProp;

	//Color
	SerializedProperty PathColorProp;
	SerializedProperty FleeRadiusColorProp;
	SerializedProperty HuntRadiusColorProp;
	SerializedProperty WanderRangeColorProp;
	SerializedProperty StoppingRangeColorProp;

	SerializedProperty serializedProperty;

	//Vector3
	SerializedProperty BreedEffectOffSetProp;

	//SerializedObject _Object;
	

	void OnEnable () 
	{
		//Emerald_Animal_AI self = (Emerald_Animal_AI)target;

		temp = new SerializedObject(targets);
		tabTemp = new SerializedObject(target);

		//Int Serialized Properties
		TabNumberProp = tabTemp.FindProperty ("TabNumber");
		//TabNumberAllProp = temp.FindProperty ("TabNumberAll");
		AggressionProp = temp.FindProperty ("aggression");
		AITypeProp = temp.FindProperty ("AIType");
		CurrentHealthProp = temp.FindProperty ("currentHealth");
		AttackDamageMinProp = temp.FindProperty ("attackDamageMin");
		AttackDamageMaxProp = temp.FindProperty ("attackDamageMax");
		PreyOrPredatorProp = temp.FindProperty ("preyOrPredator");
		PreySizeProp = temp.FindProperty ("preySize");
		PredatorSizeProp = temp.FindProperty ("predatorSize");
		IsAlphaOrNotProp = temp.FindProperty ("isAlphaOrNot");
		MaxPackSizeProp = temp.FindProperty ("maxPackSize");
		MaxDistanceFromHerdProp = temp.FindProperty ("maxDistanceFromHerd");
		StartingHealthProp = temp.FindProperty ("startingHealth");
		TotalAttackSoundsProp = temp.FindProperty ("totalAttackSounds");
		TotalHitSoundsProp = temp.FindProperty ("totalHitSounds");
		TotalAnimalSoundsProp = temp.FindProperty ("totalAnimalSounds");
		ExtraFleeSecondsProp = temp.FindProperty ("extraFleeSeconds");
		MaxFleeDistanceProp = temp.FindProperty ("maxFleeDistance");
		FleeTypeProp = temp.FindProperty ("fleeType");
		FleeRadiusProp = temp.FindProperty ("fleeRadius");
		ChaseSecondsProp = temp.FindProperty ("chaseSeconds");
		HuntRadiusProp = temp.FindProperty ("huntRadius");
		WanderRangeProp = temp.FindProperty ("wanderRange");
		GrazeLengthMinProp = temp.FindProperty ("grazeLengthMin");
		GrazeLengthMaxProp = temp.FindProperty ("grazeLengthMax");
		TurnSpeedProp = temp.FindProperty ("turnSpeed");
		TotalGrazeAnimationsProp = temp.FindProperty ("totalGrazeAnimations");
		TotalAttackAnimationsProp = temp.FindProperty ("totalAttackAnimations");
		UseBreedingProp = temp.FindProperty ("UseBreeding");
		CommonOddsProp = temp.FindProperty ("commonOdds");
		UncommonOddsProp = temp.FindProperty ("uncommonOdds");
		RareOddsProp = temp.FindProperty ("rareOdds");
		SuperRareOddsProp = temp.FindProperty ("superRareOdds");
		MaximumWalkingVelocityProp = temp.FindProperty ("maximumWalkingVelocity");

		//Float Serialized Properties
		OffSetHerdXMinProp = temp.FindProperty ("offSetHerdXMin");
		OffSetHerdXMaxProp = temp.FindProperty ("offSetHerdXMax");
		OffSetHerdZMinProp = temp.FindProperty ("offSetHerdZMin");
		OffSetHerdZMaxProp = temp.FindProperty ("offSetHerdZMax");
		AttackTimeMinProp = temp.FindProperty ("attackTimeMin");
		AttackTimeMaxProp = temp.FindProperty ("attackTimeMax");
		AttackDelaySecondsProp = temp.FindProperty ("attackDelaySeconds");
		FootStepSecondsWalkProp = temp.FindProperty ("footStepSecondsWalk");
		FootStepSecondsProp = temp.FindProperty ("footStepSeconds");
		MinSoundPitchProp = temp.FindProperty ("minSoundPitch");
		MaxSoundPitchProp = temp.FindProperty ("maxSoundPitch");
		PathWidthProp = temp.FindProperty ("pathWidth");
		LineYOffSetProp = temp.FindProperty ("lineYOffSet");
		UpdateSpeedProp = temp.FindProperty ("updateSpeed");
		StoppingDistanceProp = temp.FindProperty ("stoppingDistance");
		CoolDownSecondsProp = temp.FindProperty ("coolDownSeconds");
		MaxChaseDistanceProp = temp.FindProperty ("maxChaseDistance");
		FreezeSecondsMinProp = temp.FindProperty ("freezeSecondsMin");
		FreezeSecondsMaxProp = temp.FindProperty ("freezeSecondsMax");
		WalkSpeedProp = temp.FindProperty ("walkSpeed");
		RunSpeedProp = temp.FindProperty ("runSpeed");
		WalkAnimationSpeedProp = temp.FindProperty ("walkAnimationSpeed");
		RunAnimationSpeedProp = temp.FindProperty ("runAnimationSpeed");
		BreedSecondsProp = temp.FindProperty ("breedSeconds");
		BreedCoolDownSecondsProp = temp.FindProperty ("breedCoolDownSeconds");
		BabySecondsProp = temp.FindProperty ("babySeconds");
		BaseOffsetNavProp = temp.FindProperty ("baseOffsetNav");

		//String Serialized Properties
		NPCNameProp = temp.FindProperty ("NPCName");
		AnimalTypeProp = temp.FindProperty ("animalNameType");
		PreyTagNameProp = temp.FindProperty ("preyTagName");
		PlayerTagNameProp = temp.FindProperty ("playerTagName");
		PredatorTagNameProp = temp.FindProperty ("predatorTagName");
		EnemyTagNameProp = temp.FindProperty ("enemyTagName");
		FollowTagNameProp = temp.FindProperty ("followTagName");

		//Bool Serialized Properties
		HerdFullProp = temp.FindProperty ("herdFull");
		AutoGenerateAlphaProp = temp.FindProperty ("autoGenerateAlpha");
		AlphaWaitForHerdProp = temp.FindProperty ("alphaWaitForHerd");
		AutoCalculateDelaySecondsProp = temp.FindProperty ("autoCalculateDelaySeconds");
		UseDeadReplacementProp = temp.FindProperty ("useDeadReplacement");
		UseWeaponSoundProp = temp.FindProperty ("useWeaponSound");
		UseAttackSoundProp = temp.FindProperty ("useAttackSound");
		UseAnimalSoundProp = temp.FindProperty ("useAnimalSounds");
		UseHitSoundProp = temp.FindProperty ("useHitSound");
		UseDustEffectProp = temp.FindProperty ("useDustEffect");
		UseDieSoundProp = temp.FindProperty ("useDieSound");
		PlaySoundOnFleeProp = temp.FindProperty ("playSoundOnFlee");
		UseWalkSoundProp = temp.FindProperty ("useWalkSound");
		UseRunSoundProp = temp.FindProperty ("useRunSound");
		DrawWaypointsProp = temp.FindProperty ("drawWaypoints");
		DrawPathsProp = temp.FindProperty ("drawPaths");
		AlignAIProp = temp.FindProperty ("alignAI");
		UseVisualRadiusProp = temp.FindProperty ("useVisualRadius");
		ReturnsToStartProp = temp.FindProperty ("returnsToStart");
		ReturnBackToStartingPointProtectionProp = temp.FindProperty ("returnBackToStartingPointProtection");
		UseAnimationsProp = temp.FindProperty ("useAnimations");
		UseTurnAnimationProp = temp.FindProperty ("useTurnAnimation");
		UseRunAttackAnimationsProp = temp.FindProperty ("useRunAttackAnimations");
		UseBloodProp = temp.FindProperty ("useBlood");
		IsBabyProp = temp.FindProperty ("isBaby");
		UseBreedEffectProp = temp.FindProperty ("useBreedEffect");
		UseHitAnimationProp = temp.FindProperty ("useHitAnimation");


		//Object Serialized Properties
		DeadObjectProp = temp.FindProperty ("deadObject");
		WeaponSoundProp = temp.FindProperty ("weaponSound");
		AttackSound1Prop = temp.FindProperty ("AttackSound1");
		AttackSound2Prop = temp.FindProperty ("AttackSound2");
		AttackSound3Prop = temp.FindProperty ("AttackSound3");
		AttackSound4Prop = temp.FindProperty ("AttackSound4");
		AttackSound5Prop = temp.FindProperty ("AttackSound5");
		HitSound1Prop = temp.FindProperty ("HitSound1");
		HitSound2Prop = temp.FindProperty ("HitSound2");
		HitSound3Prop = temp.FindProperty ("HitSound3");
		HitSound4Prop = temp.FindProperty ("HitSound4");
		HitSound5Prop = temp.FindProperty ("HitSound5");
		AnimalSound1Prop = temp.FindProperty ("AnimalSound1");
		AnimalSound2Prop = temp.FindProperty ("AnimalSound2");
		AnimalSound3Prop = temp.FindProperty ("AnimalSound3");
		DieSoundProp = temp.FindProperty ("dieSound");
		FleeSoundProp = temp.FindProperty ("fleeSound");
		RunSoundProp = temp.FindProperty ("runSound");
		PathMaterialProp = temp.FindProperty ("pathMaterial");
		WalkSoundProp = temp.FindProperty ("walkSound");
		IdleAnimationProp = temp.FindProperty ("idleAnimation");
		IdleBattleAnimationProp = temp.FindProperty ("idleBattleAnimation");
		Graze1AnimationProp = temp.FindProperty ("graze1Animation");
		Graze2AnimationProp = temp.FindProperty ("graze2Animation");
		Graze3AnimationProp = temp.FindProperty ("graze3Animation");
		WalkAnimationProp = temp.FindProperty ("walkAnimation");
		RunAnimationProp = temp.FindProperty ("runAnimation");
		TurnAnimationProp = temp.FindProperty ("turnAnimation");
		HitAnimationProp = temp.FindProperty ("hitAnimation");
		AttackAnimation1Prop = temp.FindProperty ("attackAnimation1");
		AttackAnimation2Prop = temp.FindProperty ("attackAnimation2");
		AttackAnimation3Prop = temp.FindProperty ("attackAnimation3");
		AttackAnimation4Prop = temp.FindProperty ("attackAnimation4");
		AttackAnimation5Prop = temp.FindProperty ("attackAnimation5");
		AttackAnimation6Prop = temp.FindProperty ("attackAnimation6");
		RunAttackAnimationProp = temp.FindProperty ("runAttackAnimation");
		DeathAnimationProp = temp.FindProperty ("deathAnimation");
		BloodEffectProp = temp.FindProperty ("bloodEffect");
		DustEffectProp = temp.FindProperty ("dustEffect");
		FullGrownPrefabProp = temp.FindProperty ("fullGrownPrefab");
		BreedEffectProp = temp.FindProperty ("breedEffect");
		BabyPrefabCommonProp = temp.FindProperty ("babyPrefabCommon");
		BabyPrefabUncommonProp = temp.FindProperty ("babyPrefabUncommon");
		BabyPrefabRareProp = temp.FindProperty ("babyPrefabRare");
		BabyPrefabSuperRareProp = temp.FindProperty ("babyPrefabSuperRare");

		//Color Serialized Properties
		PathColorProp = temp.FindProperty ("pathColor");
		FleeRadiusColorProp = temp.FindProperty ("fleeRadiusColor");
		HuntRadiusColorProp = temp.FindProperty ("huntRadiusColor");
		WanderRangeColorProp = temp.FindProperty ("wanderRangeColor");
		StoppingRangeColorProp = temp.FindProperty ("stoppingRangeColor");

		//Vector3
		BreedEffectOffSetProp = temp.FindProperty ("breedEffectOffSet");
	}

	public override void OnInspectorGUI () 
	{
		temp.Update ();
		//Emerald_Animal_AI self = (Emerald_Animal_AI)target;


		float thirdOfScreen = Screen.width/3-10;
		int sizeOfHideButtons = 18;

		EditorGUILayout.LabelField("Emerald - Animal AI System Editor", EditorStyles.boldLabel);
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.LabelField("AI Info", EditorStyles.boldLabel);

		if (AITypeProp.intValue == 1)
		{
			EditorGUILayout.HelpBox("Name: " + NPCNameProp.stringValue, MessageType.None, true);
		}

		EditorGUILayout.HelpBox("Type: " + AnimalTypeProp.stringValue, MessageType.None, true);
		EditorGUILayout.HelpBox("Health: " + CurrentHealthProp.intValue + " points", MessageType.None, true);

		if (AggressionProp.intValue > 2)
		{
			EditorGUILayout.HelpBox("Damage: " + AttackDamageMinProp.intValue + " - " + AttackDamageMaxProp.intValue + " points", MessageType.None, true);
		}

		if (AggressionProp.intValue == 1 || AggressionProp.intValue == 0)
		{
			EditorGUILayout.HelpBox("Aggression: " + "Coward", MessageType.None, true);
		}
		
		if (AggressionProp.intValue == 2)
		{
			EditorGUILayout.HelpBox("Aggression: " + "Passive", MessageType.None, true);
		}
		
		if (AggressionProp.intValue == 3)
		{
			EditorGUILayout.HelpBox("Aggression: " + "Hotsile", MessageType.None, true);
		}
		
		if (AggressionProp.intValue == 4)
		{
			EditorGUILayout.HelpBox("Aggression: " + "Defensive", MessageType.None, true);
		}

		if (AITypeProp.intValue == 0)
		{
			if (PreyOrPredatorProp.intValue == 1)
			{
				EditorGUILayout.HelpBox("Prey or Predator: " + "Prey", MessageType.None, true);
			}

			if (PreyOrPredatorProp.intValue == 2)
			{
				EditorGUILayout.HelpBox("Prey or Predator: " + "Predator", MessageType.None, true);
			}

			if (PreyOrPredatorProp.intValue == 1)
			{
				if (PreySizeProp.intValue == 1)
				{
					EditorGUILayout.HelpBox("Size: " + "Small", MessageType.None, true);
				}

				if (PreySizeProp.intValue == 2)
				{
					EditorGUILayout.HelpBox("Size: " + "Medium", MessageType.None, true);
				}

				if (PreySizeProp.intValue == 3)
				{
					EditorGUILayout.HelpBox("Size: " + "Large", MessageType.None, true);
				}
			}

			if (PreyOrPredatorProp.intValue == 2)
			{
				if (PredatorSizeProp.intValue == 1)
				{
					EditorGUILayout.HelpBox("Size: " + "Small", MessageType.None, true);
				}
				
				if (PredatorSizeProp.intValue == 2)
				{
					EditorGUILayout.HelpBox("Size: " + "Medium", MessageType.None, true);
				}
				
				if (PredatorSizeProp.intValue == 3)
				{
					EditorGUILayout.HelpBox("Size: " + "Large", MessageType.None, true);
				}
			}

		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		//EditorGUILayout.Space();

		EditorGUILayout.LabelField("AI Option Tabs", EditorStyles.boldLabel);
		EditorGUILayout.Space();

		if (showHelpOptions == true)
		{
			EditorGUILayout.HelpBox("The AI Option Tabs allow you to individually view each option as one tab rather than having one huge list. However, the Show All Options tab will allow everything to be viewed as one list, if desired.", MessageType.None, true);
		}

		EditorGUILayout.Space();


		tabTemp.Update ();

		TabNumberProp.intValue = GUILayout.SelectionGrid (TabNumberProp.intValue, TabString, 2);

		tabTemp.ApplyModifiedProperties ();



		if (AggressionProp.intValue < 3 && TabNumberProp.intValue == 2)
		{
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.HelpBox("Passive and Coward animals cannot attack. If you'd like this AI to attack, make it either Defensive or Aggressive under the AI Setup Options tab.", MessageType.Warning, true);

			GUI.color = Color.red;
			//TabStrings[2] = "Attack";
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		//EditorGUILayout.Space();

		if (AITypeProp.intValue == 1)
		{
			if (AggressionProp.intValue == 0 || AggressionProp.intValue == 2)
			{
				PreyOrPredatorProp.intValue = 1; 
				PreySizeProp.intValue = 3;
				PredatorSizeProp.intValue = 3;
			}
			
			if (AggressionProp.intValue == 3 || AggressionProp.intValue == 4)
			{
				PreyOrPredatorProp.intValue = 2; 
				PreySizeProp.intValue = 3;
				PredatorSizeProp.intValue = 3;
			}
		}

		if (TabNumberProp.intValue == 0 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			EditorGUILayout.LabelField("AI Setup", EditorStyles.boldLabel);

			editorAnimalOrNPC = (AnimalOrNPC)AITypeProp.intValue;
			editorAnimalOrNPC = (AnimalOrNPC)EditorGUILayout.EnumPopup("AI Type", editorAnimalOrNPC);
			AITypeProp.intValue = (int)editorAnimalOrNPC;

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("AI Type determins whether your an Animal or NPC. For any wildlife AI, the Animal setting should be used. For any NPC AI, enemies, humanoid, creatures, etc. the NPC setting should be used.", MessageType.None, true);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			//Animal Behavior
			if (AITypeProp.intValue == 0)
			{
				editorAgression = (AgressionDropDown)AggressionProp.intValue;
				editorAgression = (AgressionDropDown)EditorGUILayout.EnumPopup("Animal Behavior Type", editorAgression);
				AggressionProp.intValue = (int)editorAgression;
			

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Behavior Type determins whether your Animal is Cowardly, Passive, Hostile, or Defensive.", MessageType.None, true);
				}
				
				if (showHelpOptions == true && AggressionProp.intValue == 1)
				{
					EditorGUILayout.HelpBox("A Cowardly Animal will flee when attacked by another Animal or player and will not fight back.", MessageType.None, true);
				}
				
				if (showHelpOptions == true && AggressionProp.intValue == 2)
				{
					EditorGUILayout.HelpBox("A Passive Animal will never attack. Even if hit, it will continue to wander around within its Wandering Range.", MessageType.None, true);
				}
				
				if (showHelpOptions == true && AggressionProp.intValue == 3)
				{
					EditorGUILayout.HelpBox("A Hostile Animal will automatically attack both Enemy and Player tags that enter its Attack Range.", MessageType.None, true);
				}
				
				if (showHelpOptions == true && AggressionProp.intValue == 4)
				{
					EditorGUILayout.HelpBox("A Defensive Animal will only attack its Enemy Tag if it's within range. If an NPC is attacked first, it will attempt to attack and kill the attacker.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				if (AggressionProp.intValue < 3)
				{
					EditorGUILayout.HelpBox("Passive and Coward animals are automatically marked as Prey because they cannot attack.", MessageType.Warning, true);
					PreyOrPredatorProp.intValue = 1;
				}
			}

			//NPC Behavior
			if (AITypeProp.intValue == 1)
			{
				editorAgressionNPC = (AgressionNPCDropDown)AggressionProp.intValue;
				editorAgressionNPC = (AgressionNPCDropDown)EditorGUILayout.EnumPopup("NPC Behavior Type", editorAgressionNPC);
				AggressionProp.intValue = (int)editorAgressionNPC;
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Behavior Type determins whether your NPC is Cowardly, Passive, Hostile, or Defensive.", MessageType.None, true);
				}
				
				if (showHelpOptions == true && AggressionProp.intValue == 1)
				{
					EditorGUILayout.HelpBox("A Cowardly NPC will flee when attacked by another NPC or player and will not fight back.", MessageType.None, true);
				}
				
				if (showHelpOptions == true && AggressionProp.intValue == 2)
				{
					EditorGUILayout.HelpBox("A Passive NPC will never attack. Even if hit, it will wander around within its Wandering Range.", MessageType.None, true);
				}
				
				if (showHelpOptions == true && AggressionProp.intValue == 3)
				{
					EditorGUILayout.HelpBox("An Hostile NPC will automatically attack both Enemy and Player tags that enter its Attack Range.", MessageType.None, true);
				}
				
				if (showHelpOptions == true && AggressionProp.intValue == 4)
				{
					EditorGUILayout.HelpBox("A Defensive NPC will only attack its Enemy Tag if it's within range. If an NPC is attacked first, it will attempt to attack and kill the attacker.", MessageType.None, true);
				}

			}

			EditorGUILayout.Space();

			if (AITypeProp.intValue == 0)
			{
				EditorGUILayout.LabelField("Animal Type Name", EditorStyles.miniLabel);
				AnimalTypeProp.stringValue = EditorGUILayout.TextField(AnimalTypeProp.stringValue, GUILayout.MaxHeight(75));

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Animal Type helps with Emerald dynamically forming herds and packs based on your Animal Name Type. So, only animals with the same Animal Name Type will create herds or packs.", MessageType.None, true);
				}
			}

			if (AITypeProp.intValue == 1)
			{
				EditorGUILayout.LabelField("NPC Name", EditorStyles.miniLabel);
				NPCNameProp.stringValue = EditorGUILayout.TextField(NPCNameProp.stringValue, GUILayout.MaxHeight(75));

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The name of your NPC can be useful for calling the name from an external script for things like quests, dialogue, UI, etc.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				EditorGUILayout.LabelField("NPC Type", EditorStyles.miniLabel);
				AnimalTypeProp.stringValue = EditorGUILayout.TextField(AnimalTypeProp.stringValue, GUILayout.MaxHeight(75));
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The NPC Type helps with Emerald dynamically forming groups based on your NPC Name Type. So, only NPCs with the same NPC Name Type will create groups.", MessageType.None, true);
				}
			}

			EditorGUILayout.Space();

			if (AITypeProp.intValue == 0)
			{
				if (AggressionProp.intValue >= 3)
				{
					editorPreyOrPredator = (PreyOrPredator)PreyOrPredatorProp.intValue;
					editorPreyOrPredator = (PreyOrPredator)EditorGUILayout.EnumPopup("Prey or Predator?", editorPreyOrPredator);
					PreyOrPredatorProp.intValue = (int)editorPreyOrPredator;
				}
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("This allows you to choose if your animal is either Prey or Predator. If an animal is marked as Prey, a Predator will be able to chase and kill them, if the Predator size is less than or equal to their size.", MessageType.None, true);
				}

				if (PreyOrPredatorProp.intValue == 2 && AggressionProp.intValue >= 3)
				{
					EditorGUILayout.Space();

					editorPredatorSize = (PredatorSize)PredatorSizeProp.intValue;
					editorPredatorSize = (PredatorSize)EditorGUILayout.EnumPopup("Predator Size", editorPredatorSize);
					PredatorSizeProp.intValue = (int)editorPredatorSize;
					
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("The Predator Size determins how big your Predator animal is. The Predator Size must be less than or equal to the Prey's Size in order for that predator to be able to hunt the prey.", MessageType.None, true);
					}
				}
			}
			
			EditorGUILayout.Space();

			if (AITypeProp.intValue == 0)
			{
				editorPreySize = (PreySize)PreySizeProp.intValue;
				editorPreySize = (PreySize)EditorGUILayout.EnumPopup("Prey Size", editorPreySize);
				PreySizeProp.intValue = (int)editorPreySize;

				if (PreyOrPredatorProp.intValue == 1)
				{
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("The Prey Size determins how big your Prey animal is. The Prey Size must match a Predator's Size in order for that predator to be able to hunt the prey.", MessageType.None, true);
					}
				}

				if (PreyOrPredatorProp.intValue == 2)
				{
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("The Prey Size determins the size that this Predator can hunt for. The Prey Size must match a Predator's Size in order for that predator to be able to hunt the prey.", MessageType.None, true);
					}
				}
			}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

		}

		if (TabNumberProp.intValue == 1 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			EditorGUILayout.LabelField("Herd Options", EditorStyles.boldLabel);

			EditorGUILayout.Space();

			if (AggressionProp.intValue != 2)
			{
				HerdFullProp.boolValue = EditorGUILayout.Toggle ("Disable Herds?",HerdFullProp.boolValue);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Disable Herds stops the AI from dynamically forming a herd, group, or pack with any other AI with the same type.", MessageType.None, true);
				}
			}

			if (AggressionProp.intValue == 2)
			{
				EditorGUILayout.HelpBox("Passive AI do not dynamically form herds as they tend to be used for farm animals and wandering NPCs.", MessageType.Warning, true);
			}

			EditorGUILayout.Space();

			if (!HerdFullProp.boolValue && AggressionProp.intValue != 2)
			{
				AutoGenerateAlphaProp.boolValue = EditorGUILayout.Toggle ("Auto Generate Alpha?",AutoGenerateAlphaProp.boolValue);

				EditorGUILayout.Space();

				if (!AutoGenerateAlphaProp.boolValue)
				{
					editorAlpha = (Alpha)IsAlphaOrNotProp.intValue;
					editorAlpha = (Alpha)EditorGUILayout.EnumPopup("Is this animal an Alpha?", editorAlpha);
					IsAlphaOrNotProp.intValue = (int)editorAlpha;
				}

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("If Auto Generate Alpha is enabled, alphas will be generated automatically. There is a 1 in 5 chance of an animal being an alpha. If this option is disabled, you can customize which animals are alphas manually.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				MaxPackSizeProp.intValue = EditorGUILayout.IntSlider ("Max Pack Size", MaxPackSizeProp.intValue, 1, 10);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Controls the max pack size for this animal, if they're generated to become an alpha.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				OffSetHerdXMinProp.floatValue = EditorGUILayout.Slider ("offset Herd X Min", (float)OffSetHerdXMinProp.floatValue, -20, -5);
				OffSetHerdXMaxProp.floatValue = EditorGUILayout.Slider ("offset Herd X Max", (float)OffSetHerdXMaxProp.floatValue, 5, 20);

				EditorGUILayout.Space();

				OffSetHerdZMinProp.floatValue = EditorGUILayout.Slider ("offset Herd Z Min", (float)OffSetHerdZMinProp.floatValue, -10, -5);
				OffSetHerdZMaxProp.floatValue = EditorGUILayout.Slider ("offset Herd Z Max", (float)OffSetHerdZMaxProp.floatValue, -5, -1);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("These control the Offset of how the aniaml's herds form. The bigger the distance, both positive and negative, the more separation.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				AlphaWaitForHerdProp.boolValue = EditorGUILayout.Toggle ("Alpha Waits for Herd?",AlphaWaitForHerdProp.boolValue);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("This toggles whether or not you want the alpha to wait for its herd if a member's distance from the alpha becomes too far away.", MessageType.None, true);
				}

				if (AlphaWaitForHerdProp.boolValue)
				{
					EditorGUILayout.Space();
	
					//EditorGUILayout.PropertyField(MaxDistanceFromHerdProp, true);

					MaxDistanceFromHerdProp.intValue = EditorGUILayout.IntSlider ("Max Distance From Herd", MaxDistanceFromHerdProp.intValue, 1, 100);

					/*
					if (MaxDistanceFromHerdProp.intValue > 100)
					{
						MaxDistanceFromHerdProp.intValue = 100;
					}
					*/


					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("This controls when the alpha will wait for its herd. This happens when this distance is met.", MessageType.None, true);
					}
				}
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

		}

		if (TabNumberProp.intValue == 2 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			if (AggressionProp.intValue > 2)
			{
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				
				EditorGUILayout.LabelField("Attack Options", EditorStyles.boldLabel);

				EditorGUILayout.Space();

				if (PreyOrPredatorProp.intValue == 2)
				{
					AttackDamageMinProp.intValue = EditorGUILayout.IntSlider ("Attack Damage Min", AttackDamageMinProp.intValue, 0, 25);
					
					AttackDamageMaxProp.intValue = EditorGUILayout.IntSlider ("Attack Damage Max", AttackDamageMaxProp.intValue, 0, 25);
					
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("Attack Damage calls the Damage function within Emerald. The amount set above is the damage the animal will do.", MessageType.None, true);
					}
				}
				
				EditorGUILayout.Space();
				
				AttackTimeMinProp.floatValue = EditorGUILayout.Slider ("Attack Speed Min", (float)AttackTimeMinProp.floatValue, 0.5f, 6.0f);
				AttackTimeMaxProp.floatValue = EditorGUILayout.Slider ("Attack Speed Max", (float)AttackTimeMaxProp.floatValue, 0.5f, 6.0f);
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Attack Speed controls how fast your AI can attack. Emerald calculates your AI's animations to match your attack speed. Note: The legnth of your AI's attack animation is applied to your attack speed.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				AutoCalculateDelaySecondsProp.boolValue = EditorGUILayout.Toggle ("Auto Calculate Delay Seconds?",AutoCalculateDelaySecondsProp.boolValue);
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Auto Calculate Delay Seconds will calcuate the optimum delay for your attack animations to hit your target.", MessageType.None, true);
				}

				if (!AutoCalculateDelaySecondsProp.boolValue)
				{
					EditorGUILayout.Space();
					
					AttackDelaySecondsProp.floatValue = EditorGUILayout.Slider ("Attack Delay", (float)AttackDelaySecondsProp.floatValue, 0f, 2.0f);
					
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("The Attack Delay (meassured in seconds) controls the delay that triggers a damage call. This is useful for if your animations need some time to reach the attacker.", MessageType.None, true);
					}
				}
				//EditorGUILayout.Space();
				//s.attackAnimationSpeedMultiplier = EditorGUILayout.Slider ("Attack Animation Speed", (float)s.attackAnimationSpeedMultiplier, 0.5f, 3.0f);

			}

				EditorGUILayout.Space();
				EditorGUILayout.Space();
		}


		if (TabNumberProp.intValue == 3 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			EditorGUILayout.LabelField("Health Options", EditorStyles.boldLabel);

			StartingHealthProp.intValue = EditorGUILayout.IntField("Starting Health", StartingHealthProp.intValue);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("The Starting Health is the health that your AI will start with.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			CurrentHealthProp.intValue = EditorGUILayout.IntField("Current Health", CurrentHealthProp.intValue);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("The Current Health is the current health your AI has. This is tracked in real time.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Here you can set your AI's health. When its health reaches 0, the animal will die and it will spawn a dead replacement.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			UseDeadReplacementProp.boolValue = EditorGUILayout.Toggle ("Use Dead Replacement?",UseDeadReplacementProp.boolValue);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Use Dead Replacement determines whether or not your AI will use a dead object replacement on death. Note: If set to true, this will disable the death animation from being used under the Animation Options.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			if (UseDeadReplacementProp.boolValue)
			{
				//Here
				//bool deadObject  = !EditorUtility.IsPersistent (self);
				DeadObjectProp.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField ("Dead Object", DeadObjectProp.objectReferenceValue, typeof(GameObject), true);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Here you can set your animal's dead GameObject replacement.", MessageType.None, true);
				}
			}

				EditorGUILayout.Space();
				EditorGUILayout.Space();
		}


		if (TabNumberProp.intValue == 4 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			EditorGUILayout.LabelField("Sound Options Options", EditorStyles.boldLabel);

			EditorGUILayout.Space();

			if (PreyOrPredatorProp.intValue == 2)
			{
				UseWeaponSoundProp.boolValue = EditorGUILayout.Toggle ("Use Weapon Sound?",UseWeaponSoundProp.boolValue);
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Weapon Sound is the sound that plays when your AI attacks. This is different than the attack sound. A weapon sound can be something like a swoosh or swing sound effect to simulate the sound of your AI's weapon making a noise. The pitch of these sounds are also varied based on your sound pitch randomness.", MessageType.None, true);
				}

				EditorGUILayout.Space();
				
				if (UseWeaponSoundProp.boolValue)
				{
					//bool weaponSound  = !EditorUtility.IsPersistent (self);
					WeaponSoundProp.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("Weapon Sound", WeaponSoundProp.objectReferenceValue, typeof(AudioClip), true);
					
					EditorGUILayout.Space();
				}

				EditorGUILayout.Space();

				UseAttackSoundProp.boolValue = EditorGUILayout.Toggle ("Use Attack Sound?",UseAttackSoundProp.boolValue);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Use Attacks Sounds determines whether or not your AI will make a sound when it attacks.", MessageType.None, true);
				}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				if (UseAttackSoundProp.boolValue)
				{
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("Below you can add an attack sound. For each sound you add, Emerald will randomly play it as one of the possible sounds the AI will make while it is attacking. This can be grunts, shouts, roars, growls, etc.", MessageType.None, true);
					}
					
					//TESTING
					EditorGUILayout.Space();	
					EditorGUILayout.Space();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					if (GUILayout.Button("Add Attack Sound", GUILayout.MinWidth(140), GUILayout.MaxWidth(140)))
					{
						if (TotalAttackSoundsProp.intValue < 5)
						{
							TotalAttackSoundsProp.intValue++;
						}
					}
					
					if (GUILayout.Button("Remove Attack Sound", GUILayout.MinWidth(140), GUILayout.MaxWidth(140)))
					{
						if (TotalAttackSoundsProp.intValue > 0)
						{
							TotalAttackSoundsProp.intValue--;
						}
					}
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					EditorGUILayout.Space();
					
					if (TotalAttackSoundsProp.intValue == 1)
					{
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound1Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
					}
					
					if (TotalAttackSoundsProp.intValue == 2)
					{
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound1Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound2Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound2Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
					}
					
					if (TotalAttackSoundsProp.intValue == 3)
					{
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound1Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound2Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound2Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound3Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound3Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
					}
					
					if (TotalAttackSoundsProp.intValue == 4)
					{
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound1Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound2Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound2Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound3Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound3Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound4Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound4Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
					}
					
					if (TotalAttackSoundsProp.intValue == 5)
					{
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound1Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound2Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound2Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound3Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound3Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound4Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound4Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AttackSound5Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AttackSound5Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
					}
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					EditorGUILayout.HelpBox("" + TotalAttackSoundsProp.intValue + "/5", MessageType.None, true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();


					if (TotalAttackSoundsProp.intValue == 0)
					{
						EditorGUILayout.HelpBox("You have no attack sounds assigned, even though Use Attack Sound is enabled. No sounds will play unless you have at least 1 attack sound assigned.", MessageType.Info, true);
					}
				}

				EditorGUILayout.Space();
				EditorGUILayout.Space();
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			UseHitSoundProp.boolValue = EditorGUILayout.Toggle ("Use Get Hit Sound?",UseHitSoundProp.boolValue);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Use Get Hit Sounds determine whether or not your AI will make a sound when it receives damage.", MessageType.None, true);
			}

			if (UseHitSoundProp.boolValue)
			{
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Below you can add a hit sound. For each sound you add, Emerald will randomly play it as one of the possible sounds the AI will make when getting hit. This can be grunts, shouts, roars, growls, etc.", MessageType.None, true);
				}

				EditorGUILayout.Space();	
				EditorGUILayout.Space();
				
				GUILayout.BeginVertical();
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				
				if (GUILayout.Button("Add Hit Sound", GUILayout.MinWidth(140), GUILayout.MaxWidth(140)))
				{
					if (TotalHitSoundsProp.intValue < 5)
					{
						TotalHitSoundsProp.intValue++;
					}
				}
				
				if (GUILayout.Button("Remove Hit Sound", GUILayout.MinWidth(140), GUILayout.MaxWidth(140)))
				{
					if (TotalHitSoundsProp.intValue > 0)
					{
						TotalHitSoundsProp.intValue--;
					}
				}

				
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.EndVertical();
				
				EditorGUILayout.Space();
				
				if (TotalHitSoundsProp.intValue == 1)
				{
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound1Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
				}
				
				if (TotalHitSoundsProp.intValue == 2)
				{
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound1Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound2Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound2Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
				}
				
				if (TotalHitSoundsProp.intValue == 3)
				{
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound1Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound2Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound2Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound3Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound3Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
				}
				
				if (TotalHitSoundsProp.intValue == 4)
				{
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound1Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound2Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound2Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound3Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound3Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound4Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound4Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
				}
				
				if (TotalHitSoundsProp.intValue == 5)
				{
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound1Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound2Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound2Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound3Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound3Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound4Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound4Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					HitSound5Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", HitSound5Prop.objectReferenceValue, typeof(AudioClip), true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
				}
				
				GUILayout.BeginVertical();
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				
				EditorGUILayout.HelpBox("" + TotalHitSoundsProp.intValue + "/5", MessageType.None, true);
				
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.EndVertical();

				if (TotalHitSoundsProp.intValue == 0)
				{
					EditorGUILayout.HelpBox("You have no hit sounds assigned, even though Use Hit Sound is enabled. No sounds will play unless you have at least 1 hit sound assigned.", MessageType.Info, true);
				}

				EditorGUILayout.Space();
				EditorGUILayout.Space();
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (AggressionProp.intValue == 2 && AITypeProp.intValue == 0)
			{
				UseAnimalSoundProp.boolValue = EditorGUILayout.Toggle ("Use Animal Sound?",UseAnimalSoundProp.boolValue);
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Use Animal Sounds determine whether or not your AI will make a sound as it wanders.", MessageType.None, true);
				}


				//Adding Animal Sounds
				if (UseAnimalSoundProp.boolValue)
				{
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("Below you can add an animal sound. For each sound you add, Emerald will randomly play it as one of the possible sounds the AI will make while it is wandering.", MessageType.None, true);
					}

					EditorGUILayout.Space();	
					EditorGUILayout.Space();
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					if (GUILayout.Button("Add Animal Sound", GUILayout.MinWidth(140), GUILayout.MaxWidth(140)))
					{
						if (TotalAnimalSoundsProp.intValue < 5)
						{
							TotalAnimalSoundsProp.intValue++;
						}
					}
					
					if (GUILayout.Button("Remove Animal Sound", GUILayout.MinWidth(140), GUILayout.MaxWidth(140)))
					{
						if (TotalAnimalSoundsProp.intValue > 0)
						{
							TotalAnimalSoundsProp.intValue--;
						}
					}
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					EditorGUILayout.Space();
					
					if (TotalAnimalSoundsProp.intValue == 1)
					{
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AnimalSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AnimalSound1Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
					}
					
					if (TotalAnimalSoundsProp.intValue == 2)
					{
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AnimalSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AnimalSound1Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AnimalSound2Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AnimalSound2Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
					}
					
					if (TotalAnimalSoundsProp.intValue == 3)
					{
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AnimalSound1Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AnimalSound1Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AnimalSound2Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AnimalSound2Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
						GUILayout.BeginVertical();
						GUILayout.BeginHorizontal();
						GUILayout.FlexibleSpace();
						
						AnimalSound3Prop.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("", AnimalSound3Prop.objectReferenceValue, typeof(AudioClip), true);
						
						GUILayout.FlexibleSpace();
						GUILayout.EndHorizontal();
						GUILayout.EndVertical();
						
					}
					
					GUILayout.BeginVertical();
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					
					EditorGUILayout.HelpBox("" + TotalAnimalSoundsProp.intValue + "/3", MessageType.None, true);
					
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
					
					
					if (TotalAnimalSoundsProp.intValue == 0)
					{
						EditorGUILayout.HelpBox("You have no animal sounds assigned, even though Use Animal Sound is enabled. No sounds will play unless you have at least 1 animal sound assigned.", MessageType.Info, true);
					}

						EditorGUILayout.Space();
						EditorGUILayout.Space();
				}
			}
			
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		



			UseDieSoundProp.boolValue = EditorGUILayout.Toggle ("Use Die Sound",UseDieSoundProp.boolValue);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Controls whether or not a sound will be played on death.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			if (UseDieSoundProp.boolValue)
			{
				DieSoundProp.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("Die Sound", DieSoundProp.objectReferenceValue, typeof(AudioClip), true);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The sound the animal uses when it dies. Note: This only works if your animal isn't using a dead replacement. If your animal is using a dead replacement, put your dead sound on the dead replacement.", MessageType.None, true);
				}

				EditorGUILayout.Space();
			}


			if (AggressionProp.intValue == 1)
			{
				PlaySoundOnFleeProp.boolValue = EditorGUILayout.Toggle ("Play Flee Sound?",PlaySoundOnFleeProp.boolValue);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Flee Sound is the sound the animal will use when it flees from a predator or player.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				if (PlaySoundOnFleeProp.boolValue)
				{
					//bool fleeSound  = !EditorUtility.IsPersistent (self);
					FleeSoundProp.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("Flee Sound", FleeSoundProp.objectReferenceValue, typeof(AudioClip), true);
				}
			}

			EditorGUILayout.Space();

			if (AITypeProp.intValue == 1 && AggressionProp.intValue != 3)
			{
				UseWalkSoundProp.boolValue = EditorGUILayout.Toggle ("Use Walk Sound?",UseWalkSoundProp.boolValue);
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The sound the AI uses when it walks.", MessageType.None, true);
				}
				
				
				EditorGUILayout.Space();
				
				if (UseWalkSoundProp.boolValue)
				{
					//bool walkSound  = !EditorUtility.IsPersistent (self);
					WalkSoundProp.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("Walk Sound", WalkSoundProp.objectReferenceValue, typeof(AudioClip), true);
					
					EditorGUILayout.Space();
					
					FootStepSecondsWalkProp.floatValue = EditorGUILayout.Slider ("Walk Footstep Seconds", (float)FootStepSecondsWalkProp.floatValue, 0.1f, 1.5f);
					
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("The Footstep Seconds controls the seconds in between each time the sound is playing while walking.", MessageType.None, true);
					}
				}
			}

			UseRunSoundProp.boolValue = EditorGUILayout.Toggle ("Use Run Sound?",UseRunSoundProp.boolValue);
			
			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("The sound the AI uses when it runs.", MessageType.None, true);
			}


			EditorGUILayout.Space();

			if (UseRunSoundProp.boolValue)
			{
				//bool runSound  = !EditorUtility.IsPersistent (self);
				RunSoundProp.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField ("Run Sound", RunSoundProp.objectReferenceValue, typeof(AudioClip), true);

				EditorGUILayout.Space();

				FootStepSecondsProp.floatValue = EditorGUILayout.Slider ("Footstep Seconds", (float)FootStepSecondsProp.floatValue, 0.1f, 2.0f);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Footstep Seconds controls the seconds in between each time the sound is playing.", MessageType.None, true);
				}
			}

			EditorGUILayout.Space();
			
			MinSoundPitchProp.floatValue = EditorGUILayout.Slider ("Min Sound Pitch", (float)MinSoundPitchProp.floatValue, 0.5f, 1.5f);
			MaxSoundPitchProp.floatValue = EditorGUILayout.Slider ("Max Sound Pitch", (float)MaxSoundPitchProp.floatValue, 0.5f, 1.5f);
			
			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("This controls the min and max sound pitch for the animal's AudioSource. This affects all sounds adding various pitches to each animal keeping them unique.", MessageType.None, true);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

		}


		if (TabNumberProp.intValue == 5 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			EditorGUILayout.LabelField("Pathfinding Options", EditorStyles.boldLabel);

			//Hold
			//selfmaxNumberOfActiveAnimals = EditorGUILayout.IntSlider ("Max Active Aniamls", selfmaxNumberOfActiveAnimals, 1, 100);
			
			DrawWaypointsProp.boolValue = EditorGUILayout.Toggle ("Draw Waypoints?",DrawWaypointsProp.boolValue);
			
			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Draw Waypoints determins if the AI will draw its current waypoint/destination. This can make it helpful for development/testing.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			DrawPathsProp.boolValue = EditorGUILayout.Toggle ("Draw Paths?",DrawPathsProp.boolValue);
			
			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Draw Paths determins if the AI will draw its current path to its current destination. This can make it helpful for development/testing.", MessageType.None, true);
			}

			if (DrawPathsProp.boolValue)
			{
				EditorGUILayout.Space();
				
				PathWidthProp.floatValue = EditorGUILayout.Slider ("Path Line Width", (float)PathWidthProp.floatValue, 1.0f, 100.0f);
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Choose how wide you would like your Path Lines drawn.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				LineYOffSetProp.floatValue = EditorGUILayout.Slider ("Path Line Y Offset", (float)LineYOffSetProp.floatValue, 0.0f, 5.0f);
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Path Line Y Offset will offset your path line on the Y axis based on the amount used on the slider above. This is useful if the Path Line is too high or too low.", MessageType.None, true);
				}
			}

			if (DrawPathsProp.boolValue)
			{
				EditorGUILayout.Space();

				PathColorProp.colorValue = EditorGUILayout.ColorField("Path Line Color", PathColorProp.colorValue);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Path Line Color allows you to customize what color you want the path lines to be.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				//bool pathMaterial  = !EditorUtility.IsPersistent (self);
				PathMaterialProp.objectReferenceValue = (Material)EditorGUILayout.ObjectField ("Path Line Material", PathMaterialProp.objectReferenceValue, typeof(Material), true);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Choose the material you want to be used for your Path Line. If no material is applied, a default one will be used. Note: The color of the default material is purple and can't be adjusted.", MessageType.None, true);
				}

			}

			EditorGUILayout.Space();

			/*
			selfenableDebugLogs = EditorGUILayout.Toggle ("Enable Debug Logs?",selfenableDebugLogs);
			
			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Enable Debug Logs can be useful to help balance your ecosystem. When an initial hunt or flee is triggered, it tells what's happening by the predator or prey's name.", MessageType.None, true);
			}
			*/

			EditorGUILayout.Space();

			AlignAIProp.boolValue = EditorGUILayout.Toggle ("Align AI?",AlignAIProp.boolValue);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("If Align AI is enabled, it will automatically, and smoothly, align AI to the slope of the terrain. This allows much more realistic results.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			UpdateSpeedProp.floatValue = EditorGUILayout.Slider ("Update Speed", UpdateSpeedProp.floatValue, 0.01f, 2.0f);
			
			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Update Speed determines how often culling is checked. If an animal is culled, it will be disabled to increase performance. If an animal is visible, it will enable all components on that animal. The less often this option is updated, the more it increases performance, but animals may not react the second a player looks at them. So, it's best to balance this option with performance and playing quality.", MessageType.None, true);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

		}


		if (TabNumberProp.intValue == 6 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			EditorGUILayout.LabelField("Range Options", EditorStyles.boldLabel);

			EditorGUILayout.Space();

			UseVisualRadiusProp.boolValue = EditorGUILayout.Toggle ("Use Visual Radiuses?",UseVisualRadiusProp.boolValue);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Using Visual Radiuses will visually render the radiuses in the scene view. This makes it easy to see where your AI's Ranges are.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			StoppingDistanceProp.floatValue = EditorGUILayout.Slider ("Stopping Distance", StoppingDistanceProp.floatValue, 0.1f, 10.0f);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("The Stopping Distance determins the distance in which your AI will stop for its waypoint/destination/target. If your AI get too close to a target or your AI slides before waypoints, increase its stopping distance.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			StoppingRangeColorProp.colorValue = EditorGUILayout.ColorField("Stopping Range Color", StoppingRangeColorProp.colorValue);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("The color of the Stopping Range Radius.", MessageType.None, true);
			}


			
			EditorGUILayout.Space();

			if (AggressionProp.intValue == 1)
			{
				FleeRadiusProp.intValue = EditorGUILayout.IntSlider ("Flee Trigger Radius", FleeRadiusProp.intValue, 1, 100);

				EditorGUILayout.Space();

				FleeRadiusColorProp.colorValue = EditorGUILayout.ColorField("Flee Radius Color", FleeRadiusColorProp.colorValue);
			
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Flee Trigger Radius is the radius in which the AI will be triggered to flee.", MessageType.None, true);
				}

				EditorGUILayout.Space();


				editorFleeType = (FleeType)FleeTypeProp.intValue;
				editorFleeType = (FleeType)EditorGUILayout.EnumPopup("Flee Type", editorFleeType);
				FleeTypeProp.intValue = (int)editorFleeType;

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Flee Type determines how your AI will flee from danger.", MessageType.None, true);
				}

				if (FleeTypeProp.intValue == 0)
				{
					EditorGUILayout.HelpBox("When using Distance, your AI will continue to flee until the appropriate distance is met before they stop. There is not cool down for the Distance Flee Type.", MessageType.None, true);
				}

				if (FleeTypeProp.intValue == 1)
				{
					EditorGUILayout.HelpBox("When using Time, your AI will flee for the amount of Flee Seconds set below. Once it is met, your AI will stop fleeing until their Cool Down Seconds have been met.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				if (FleeTypeProp.intValue == 1)
				{
					ExtraFleeSecondsProp.intValue = EditorGUILayout.IntSlider ("Flee Seconds", ExtraFleeSecondsProp.intValue, 1, 120);
				}

				if (FleeTypeProp.intValue == 0)
				{
					MaxFleeDistanceProp.intValue = EditorGUILayout.IntSlider ("Max Flee Distance", MaxFleeDistanceProp.intValue, FleeRadiusProp.intValue+25, 150);
				}

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Flee Seconds controls how many seconds an AI can flee before they are exhausted. Once exhausted, they will switch to cool down mode and will not be able to flee until their Cool Down Seconds below have been reached.", MessageType.None, true);
				}

				if (FleeTypeProp.intValue == 1)
				{
					EditorGUILayout.Space();

					CoolDownSecondsProp.floatValue = EditorGUILayout.Slider ("Cool Down Seconds", CoolDownSecondsProp.floatValue, 0, 25);
					
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("Cool Down Seconds controls how many seconds your animal will wait after they have reached their chase seconds. The animal will then return back to its statring position while in this mode.", MessageType.None, true);
					}
				}
			}

			if (AggressionProp.intValue == 3 || AggressionProp.intValue == 4)
			{

				EditorGUILayout.Space();
					
				ChaseSecondsProp.intValue = EditorGUILayout.IntSlider ("Chase Seconds", (int)ChaseSecondsProp.intValue, 1, 120);
					
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Chase Seconds determines how long an animal can flee before being exhausted and switching to cooldown mode.", MessageType.None, true);
				}

				if (AITypeProp.intValue == 0)
				{
					HuntRadiusProp.intValue = EditorGUILayout.IntSlider ("Hunt Trigger Radius", HuntRadiusProp.intValue, 1, 200);
				}

				if (AITypeProp.intValue == 1)
				{
					HuntRadiusProp.intValue = EditorGUILayout.IntSlider ("Attack Trigger Radius", HuntRadiusProp.intValue, 1, 200);
				}
				
				EditorGUILayout.Space();

				if (AITypeProp.intValue == 0)
				{
					HuntRadiusColorProp.colorValue = EditorGUILayout.ColorField("Hunt Radius Color", HuntRadiusColorProp.colorValue);

					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("The Hunt Trigger Radius is the radius in which the AI will be triggered to hunt. This process is pause if the animal is within attacking distance.", MessageType.None, true);
					}
				}

				if (AITypeProp.intValue == 1)
				{
					HuntRadiusColorProp.colorValue = EditorGUILayout.ColorField("Attack Radius Color", HuntRadiusColorProp.colorValue);

					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("The Attack Trigger Radius is the radius in which the AI will be triggered to attack. This process is pause if the NPC is within attacking distance.", MessageType.None, true);
					}
				}

				EditorGUILayout.Space();

				if (AITypeProp.intValue == 0)
				{
					//selfhuntSeconds = EditorGUILayout.Slider ("Hunt Seconds", selfhuntSeconds, 1, 50);
					MaxChaseDistanceProp.floatValue = EditorGUILayout.Slider ("Max Chase Distance", MaxChaseDistanceProp.floatValue, HuntRadiusProp.intValue, 200);
				}

				if (AITypeProp.intValue == 1)
				{
					//selfhuntSeconds = EditorGUILayout.Slider ("Attack Seconds", selfhuntSeconds, 1, 50);
					MaxChaseDistanceProp.floatValue = EditorGUILayout.Slider ("Max Chase Distance", MaxChaseDistanceProp.floatValue, HuntRadiusProp.intValue, 200);

					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("How far, distance wise, a target can be away before the AI gives up on the target.", MessageType.None, true);
					}

					EditorGUILayout.Space();

					ReturnsToStartProp.boolValue = EditorGUILayout.Toggle ("Returns to Start?",ReturnsToStartProp.boolValue);
					
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("Returns to Start controls whether or not your AI will return to its starting position once they have given up on a target.", MessageType.None, true);
					}

					if (ReturnsToStartProp.boolValue)
					{
						EditorGUILayout.Space();

						ReturnBackToStartingPointProtectionProp.boolValue = EditorGUILayout.Toggle ("Return Protection?",ReturnBackToStartingPointProtectionProp.boolValue);

						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("If an NPC is ReturnProtected, they cannot be injured until they have returned back to their starting position.", MessageType.None, true);
						}
					}
				}

				EditorGUILayout.Space();

				CoolDownSecondsProp.floatValue = EditorGUILayout.Slider ("Cool Down Seconds", CoolDownSecondsProp.floatValue, 0, 25);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Cool Down Seconds controls how many seconds your animal will wait after they have reached their max Hunt Seconds. During the Cool Down phase, the animal will return back to its statring position. The seconds for this are rest if an animal receives damage.", MessageType.None, true);
				}
			}

			EditorGUILayout.Space();

			WanderRangeProp.intValue = EditorGUILayout.IntSlider ("Wander Range", WanderRangeProp.intValue, 1, 500);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Wander Range controls the radius in which the animal will wander. It will not wander out of its Wander Range, unless it's fleeing.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			WanderRangeColorProp.colorValue = EditorGUILayout.ColorField("Wander Range Color", WanderRangeColorProp.colorValue);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("The Wander Range is a radius that the AI will wander in from its originally placed spot. It will not wander further than this radius.", MessageType.None, true);
			}

			if (AggressionProp.intValue == 1)
			{
				EditorGUILayout.Space();
				
				FreezeSecondsMinProp.floatValue = EditorGUILayout.Slider ("Min Freeze Seconds", (float)FreezeSecondsMinProp.floatValue, 0.25f, 3.0f);
				FreezeSecondsMaxProp.floatValue = EditorGUILayout.Slider ("Max Freeze Seconds", (float)FreezeSecondsMaxProp.floatValue, 0.5f, 8.0f);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("This controls the min and max seconds it takes for the animals to react to a predator or player triggering the animal to flee.", MessageType.None, true);
				}
			}

			EditorGUILayout.Space();

			if (AITypeProp.intValue == 0)
			{
				GrazeLengthMinProp.intValue = EditorGUILayout.IntSlider ("Graze Length Min", GrazeLengthMinProp.intValue, 1, 100);
				GrazeLengthMaxProp.intValue = EditorGUILayout.IntSlider ("Graze Length Max", GrazeLengthMaxProp.intValue, 1, 100);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Graze Lengths are generated with the min and max values entered above. This also plays a role in how often the waypoints are changed. If the AI is unable to reach its waypoint within its generated graze length time, a new waypoint will be generated.", MessageType.None, true);
				}
			}

			if (AITypeProp.intValue == 1)
			{
				GrazeLengthMinProp.intValue = EditorGUILayout.IntSlider ("Wait Length Min", GrazeLengthMinProp.intValue, 1, 100);
				GrazeLengthMaxProp.intValue = EditorGUILayout.IntSlider ("Wait Length Max", GrazeLengthMaxProp.intValue, 1, 100);
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Wait Lengths are generated with the min and max values entered above. This also plays a role in how often the waypoints are changed. If the AI is unable to reach its waypoint within its generated wait length time, a new waypoint will be generated.", MessageType.None, true);
				}
			}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

		}


		if (TabNumberProp.intValue == 8 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Movement Options", EditorStyles.boldLabel);

			EditorGUILayout.Space();

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Here you can adjust the speeds that your AI will use. This AI system uses a NavMeshAgent, which is applied automatically. These speeds will change the NavMeshAgent's speed when a AI goes into flee mode.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			WalkSpeedProp.floatValue = EditorGUILayout.Slider ("Walk Speed", (float)WalkSpeedProp.floatValue, 0.5f, 10.0f);
			RunSpeedProp.floatValue = EditorGUILayout.Slider ("Run Speed", (float)RunSpeedProp.floatValue, 3.0f, 15.0f);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("These control how fast the AI will walk and run.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			TurnSpeedProp.intValue = EditorGUILayout.IntSlider ("Turn Speed", TurnSpeedProp.intValue, 1, 2000);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("The Turn Speed determins how fast your AI will rotate to its waypoint/destination when a waypoint is generated.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			BaseOffsetNavProp.floatValue = EditorGUILayout.Slider ("Navmesh Base Offset", BaseOffsetNavProp.floatValue, -1f, 1f);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("The Navmesh Base Offset controls the gap distance between the ground and your AI. If you AI is hovering too far above the ground, setting a negative number will offset it so it'll be touching the ground.", MessageType.None, true);
			}

			if (AggressionProp.intValue == 1 || AggressionProp.intValue == 0)
			{
				EditorGUILayout.Space();

				MaximumWalkingVelocityProp.intValue = EditorGUILayout.IntSlider ("Maximum Walking Velocity", MaximumWalkingVelocityProp.intValue, 0, 75);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Maximum Walking Velocity controls when your AI will switch to the running animation. This only applies to fleeing AI.", MessageType.None, true);
				}
			}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

		}

		if (TabNumberProp.intValue == 7 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Animation Options", EditorStyles.boldLabel);

			if (showHelpOptions == true)
			{
					EditorGUILayout.HelpBox("Here you can setup your AI's animations. You simple drag and drop animations you'd like to use below and the system will use them for the selected animations.", MessageType.None, true);
			}

			EditorGUILayout.Space();

			UseAnimationsProp.boolValue = EditorGUILayout.Toggle ("Use Animations?", UseAnimationsProp.boolValue);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("If the AI does not use animations, animations can be disabled here. However, they will be disabled automatically if no animation component is found on the current model.", MessageType.None, true);
			}

			if (UseAnimationsProp.boolValue)
			{

				EditorGUILayout.Space();

				//bool idleAnimation  = !EditorUtility.IsPersistent (self);
				IdleAnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Idle Animation", IdleAnimationProp.objectReferenceValue, typeof(AnimationClip), true);

				if (PreyOrPredatorProp.intValue == 2)
				{
					EditorGUILayout.Space();

					//bool idleBattleAnimation  = !EditorUtility.IsPersistent (self);
					IdleBattleAnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Idle (Aggressive) Animation", IdleBattleAnimationProp.objectReferenceValue, typeof(AnimationClip), true);

					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("If an AI doesn't have an aggressive idle animation, you can just use your basic idle animation.", MessageType.None, true);
					}
				}

				EditorGUILayout.Space();

				TotalGrazeAnimationsProp.intValue = EditorGUILayout.IntSlider ("Total Graze Animations", TotalGrazeAnimationsProp.intValue, 1, 3);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Total Graze Animations determins how many graze animations your AI will use when wandering. These animations will be picked at random. These can also be Idle animations, if desired. There is a max of 3.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				if (AITypeProp.intValue == 0)
				{
					Graze1AnimationProp.objectReferenceValue = EditorGUILayout.ObjectField ("Graze 1 Animation", Graze1AnimationProp.objectReferenceValue, typeof(AnimationClip), true);
					//EditorGUILayout.PropertyField(Graze1AnimationProp);

					if (TotalGrazeAnimationsProp.intValue == 2 || TotalGrazeAnimationsProp.intValue == 3)
					{
						//bool graze2Animation  = !EditorUtility.IsPersistent (self);
						Graze2AnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Graze 2 Animation", Graze2AnimationProp.objectReferenceValue, typeof(AnimationClip), true);
					}
					
					if (TotalGrazeAnimationsProp.intValue == 3)
					{
						//bool graze3Animation  = !EditorUtility.IsPersistent (self);
						Graze3AnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Graze 3 Animation", Graze3AnimationProp.objectReferenceValue, typeof(AnimationClip), true);
					}
				}

				if (AITypeProp.intValue == 1)
				{
					//bool graze1Animation  = !EditorUtility.IsPersistent (self);
					Graze1AnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Wait 1 Animation", Graze1AnimationProp.objectReferenceValue, typeof(AnimationClip), true);
					
					if (TotalGrazeAnimationsProp.intValue == 2 || TotalGrazeAnimationsProp.intValue == 3)
					{
						//bool graze2Animation  = !EditorUtility.IsPersistent (self);
						Graze2AnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Wait 2 Animation", Graze2AnimationProp.objectReferenceValue, typeof(AnimationClip), true);
					}
					
					if (TotalGrazeAnimationsProp.intValue == 3)
					{
						//bool graze3Animation  = !EditorUtility.IsPersistent (self);
						Graze3AnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Wait 3 Animation", Graze3AnimationProp.objectReferenceValue, typeof(AnimationClip), true);
					}
				}

				EditorGUILayout.Space();

				//bool walkAnimation  = !EditorUtility.IsPersistent (self);
				WalkAnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Walk Animation", WalkAnimationProp.objectReferenceValue, typeof(AnimationClip), true);

				//bool runAnimation  = !EditorUtility.IsPersistent (self);
				RunAnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Run Animation", RunAnimationProp.objectReferenceValue, typeof(AnimationClip), true);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The walk and run animation your AI will use.", MessageType.None, true);
				}

				EditorGUILayout.Space();
				
				UseTurnAnimationProp.boolValue = EditorGUILayout.Toggle ("Use Turn Animation?",UseTurnAnimationProp.boolValue);

				EditorGUILayout.Space();

				if (UseTurnAnimationProp.boolValue)
				{
					//bool turnAnimation  = !EditorUtility.IsPersistent (self);
					TurnAnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Turn Animation", TurnAnimationProp.objectReferenceValue, typeof(AnimationClip), true);
				}

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Trun Animation will play a turn animation when your AI is turning more than a percalculated degree. This applies to wandering, fighting, and fleeing. This animation helps with making AI function more realistic. If your AI doesn't have a turn animation, a walk animation can be used in its place. However, this feature is completely optional.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				UseHitAnimationProp.boolValue = EditorGUILayout.Toggle ("Use Hit Animation?",UseHitAnimationProp.boolValue);

				if (UseHitAnimationProp.boolValue)
				{
					HitAnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Hit Animation", HitAnimationProp.objectReferenceValue, typeof(AnimationClip), true);
				}

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The hit animation that is played when the AI receives damage.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				if (AggressionProp.intValue != 1)
				{
					if (PreyOrPredatorProp.intValue == 2)
					{
						UseRunAttackAnimationsProp.boolValue = EditorGUILayout.Toggle ("Use Run Attack Animations?",UseRunAttackAnimationsProp.boolValue);

						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("If the AI has run attack animation, it can be set here. It will then be played when an AI is attacking a target while running.", MessageType.None, true);
						}
					}
					
					EditorGUILayout.Space();

					if (UseRunAttackAnimationsProp.boolValue && PreyOrPredatorProp.intValue == 2)
					{
						//bool runAttackAnimation  = !EditorUtility.IsPersistent (self);
						RunAttackAnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Run Attack Animation", RunAttackAnimationProp.objectReferenceValue, typeof(AnimationClip), true);
					}
				}


				if (PreyOrPredatorProp.intValue == 2)
				{
					EditorGUILayout.Space();

					TotalAttackAnimationsProp.intValue = EditorGUILayout.IntSlider ("Total Attack Animations", TotalAttackAnimationsProp.intValue, 1, 6);

					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("Here you control how many attack animations your AI uses. There are a max of 6. Each will be used randomly when the AI is attacking. If your AI doesn't have an attack animation, another animation can be used in its place such as idle, walk, etc.", MessageType.None, true);
					}

					EditorGUILayout.Space();

					//bool attackAnimation1  = !EditorUtility.IsPersistent (self);
					AttackAnimation1Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 1", AttackAnimation1Prop.objectReferenceValue, typeof(AnimationClip), true);

					if (TotalAttackAnimationsProp.intValue == 2)
					{
						//bool attackAnimation2  = !EditorUtility.IsPersistent (self);
						AttackAnimation2Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 2", AttackAnimation2Prop.objectReferenceValue, typeof(AnimationClip), true);
					}

					if (TotalAttackAnimationsProp.intValue == 3)
					{
						//bool attackAnimation2  = !EditorUtility.IsPersistent (self);
						AttackAnimation2Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 2", AttackAnimation2Prop.objectReferenceValue, typeof(AnimationClip), true);

						//bool attackAnimation3  = !EditorUtility.IsPersistent (self);
						AttackAnimation3Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 3", AttackAnimation3Prop.objectReferenceValue, typeof(AnimationClip), true);
					}

					if (TotalAttackAnimationsProp.intValue == 4)
					{
						//bool attackAnimation2  = !EditorUtility.IsPersistent (self);
						AttackAnimation2Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 2", AttackAnimation2Prop.objectReferenceValue, typeof(AnimationClip), true);

						//bool attackAnimation3  = !EditorUtility.IsPersistent (self);
						AttackAnimation3Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 3", AttackAnimation3Prop.objectReferenceValue, typeof(AnimationClip), true);

						//bool attackAnimation4  = !EditorUtility.IsPersistent (self);
						AttackAnimation4Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 4", AttackAnimation4Prop.objectReferenceValue, typeof(AnimationClip), true);
					}

					if (TotalAttackAnimationsProp.intValue == 5)
					{
						//bool attackAnimation2  = !EditorUtility.IsPersistent (self);
						AttackAnimation2Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 2", AttackAnimation2Prop.objectReferenceValue, typeof(AnimationClip), true);
						
						//bool attackAnimation3  = !EditorUtility.IsPersistent (self);
						AttackAnimation3Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 3", AttackAnimation3Prop.objectReferenceValue, typeof(AnimationClip), true);

						//bool attackAnimation4  = !EditorUtility.IsPersistent (self);
						AttackAnimation4Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 4", AttackAnimation4Prop.objectReferenceValue, typeof(AnimationClip), true);

						//bool attackAnimation5  = !EditorUtility.IsPersistent (self);
						AttackAnimation5Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 5", AttackAnimation5Prop.objectReferenceValue, typeof(AnimationClip), true);
					}

					if (TotalAttackAnimationsProp.intValue == 6)
					{
						//bool attackAnimation2  = !EditorUtility.IsPersistent (self);
						AttackAnimation2Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 2", AttackAnimation2Prop.objectReferenceValue, typeof(AnimationClip), true);
						
						//bool attackAnimation3  = !EditorUtility.IsPersistent (self);
						AttackAnimation3Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 3", AttackAnimation3Prop.objectReferenceValue, typeof(AnimationClip), true);
						
						//bool attackAnimation4  = !EditorUtility.IsPersistent (self);
						AttackAnimation4Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 4", AttackAnimation4Prop.objectReferenceValue, typeof(AnimationClip), true);
						
						//bool attackAnimation5  = !EditorUtility.IsPersistent (self);
						AttackAnimation5Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 5", AttackAnimation5Prop.objectReferenceValue, typeof(AnimationClip), true);

						//bool attackAnimation6  = !EditorUtility.IsPersistent (self);
						AttackAnimation6Prop.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Attack Animation 6", AttackAnimation6Prop.objectReferenceValue, typeof(AnimationClip), true);
					}

					EditorGUILayout.Space();
				}


				if (UseDeadReplacementProp.boolValue)
				{
					EditorGUILayout.HelpBox("You have Use Dead Replacement enabled so the death animation will not be used. To disable Use Dead Replacement, go to the Health Options.", MessageType.Info, true);
				}


				DeathAnimationProp.objectReferenceValue = (AnimationClip)EditorGUILayout.ObjectField ("Death Animation", DeathAnimationProp.objectReferenceValue, typeof(AnimationClip), true);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Death Animation is the animation that plays when your AI's health reaches 0.", MessageType.None, true);
				}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				WalkAnimationSpeedProp.floatValue = EditorGUILayout.Slider ("Walk Animation Speed", (float)WalkAnimationSpeedProp.floatValue, 0.5f, 2.0f);
				RunAnimationSpeedProp.floatValue = EditorGUILayout.Slider ("Run Animation Speed", (float)RunAnimationSpeedProp.floatValue, 0.5f, 4.0f);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("These control the walk and run animations speed. Use these are useful to help match your animations match your movement speed.", MessageType.None, true);
				}
			}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

		}


		if (TabNumberProp.intValue == 9 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			if (PreyOrPredatorProp.intValue == 2 && AITypeProp.intValue == 0)
			{

				EditorGUILayout.LabelField("Tag Options", EditorStyles.boldLabel);

				EditorGUILayout.HelpBox("In order for Emerald to work correctly, your AI will need to have a tag other than the standard Untagged. This is based on your custom Tag Options below. The tag of this object should match up with your other AI's tags.", MessageType.None, true);
				EditorGUILayout.Space();

				PreyTagNameProp.stringValue = EditorGUILayout.TagField("Prey Tag", PreyTagNameProp.stringValue);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Prey Tag is the tag of animals that are Prey. If a prey is within your animals's Hunt Radius, it will attempt to hunt the animal or NPC.", MessageType.None, true);
				}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				PlayerTagNameProp.stringValue = EditorGUILayout.TagField("Player Tag", PlayerTagNameProp.stringValue);
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Player Tag is the tag of your player. If your player is within your animals's Hunt Radius, it will attempt to hunt the player.", MessageType.None, true);
				}
			}

			EditorGUILayout.Space();

			if (PreyOrPredatorProp.intValue == 1 && AITypeProp.intValue == 0)
			{
				EditorGUILayout.LabelField("Tag Options", EditorStyles.boldLabel);

				EditorGUILayout.HelpBox("In order for Emerald to work correctly, your AI will need to have a tag other than the standard Untagged. This is based on your custom Tag Options below. The tag of this object should match up with your other AI's tags.", MessageType.None, true);
				EditorGUILayout.Space();

				PredatorTagNameProp.stringValue = EditorGUILayout.TagField("Predator Tag", PredatorTagNameProp.stringValue);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Predator Tag is the tag of animals that are Predators. If a predator is within your animals's Flee Radius, it will flee to escape danager.", MessageType.None, true);
				}

				EditorGUILayout.Space();
				EditorGUILayout.Space();
				
				PlayerTagNameProp.stringValue = EditorGUILayout.TagField("Player Tag", PlayerTagNameProp.stringValue);
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Player Tag is the tag of your player. If a player is within your animals's Flee Radius, it will flee to escape danager.", MessageType.None, true);
				}

				if (AggressionProp.intValue == 2)
				{
					EditorGUILayout.Space();

					FollowTagNameProp.stringValue = EditorGUILayout.TagField("Food Tag", FollowTagNameProp.stringValue);
					
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("The Food Tag is the tag that will allow an animal to start following a player. Once this happens, the player can trigger it to breed. Once 2 animals of the same type are triggered to breed, the will go into Breed Mode.", MessageType.None, true);
					}
				}
			}

			if (AITypeProp.intValue == 1 && AggressionProp.intValue == 3 || AITypeProp.intValue == 1 && AggressionProp.intValue == 4)
			{
				EditorGUILayout.LabelField("Tag Options", EditorStyles.boldLabel);

				EditorGUILayout.HelpBox("In order for Emerald to work correctly, your AI will need to have a tag other than the standard Untagged. This is based on your custom Tag Options below. The tag of this object should match up with your other AI's tags.", MessageType.None, true);
				EditorGUILayout.Space();

				EnemyTagNameProp.stringValue = EditorGUILayout.TagField("Enemy Tag", EnemyTagNameProp.stringValue);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Enemy Tag is the tag name your AI will consider a threat. Anything with this tag your AI will attack as soon as they are within their Attack Radius.", MessageType.None, true);
				}

				if (AggressionProp.intValue == 4)
				{
					EditorGUILayout.HelpBox("A Defensive AI will only attack things on sight that have the above Enemy Tag. However, if they are attacked first by an unknown tag or AI, they will defend themselves by attacking and killing the attacker.", MessageType.Info, true);
				}
				
				EditorGUILayout.Space();
				EditorGUILayout.Space();

				PlayerTagNameProp.stringValue = EditorGUILayout.TagField("Player Tag", PlayerTagNameProp.stringValue);

				if (showHelpOptions == true && AggressionProp.intValue == 3)
				{
					EditorGUILayout.HelpBox("The Player Tag is the tag name for your Player that your AI will consider a threat. Anything with this tag your AI will attack as soon as they are within their Attack Radius.", MessageType.None, true);
				}

				if (showHelpOptions == true && AggressionProp.intValue == 4)
				{
					EditorGUILayout.HelpBox("The Player Tag is the tag name for your Player that your AI will consider a threat, only if the player attacks this AI first.", MessageType.None, true);
				}
			}
			
			if (AITypeProp.intValue == 1 && AggressionProp.intValue == 0)
			{
				EditorGUILayout.LabelField("Tag Options", EditorStyles.boldLabel);

				EditorGUILayout.HelpBox("In order for Emerald to work correctly, your AI will need to have a tag other than the standard Untagged. This is based on your custom Tag Options below. The tag of this object should match up with your other AI's tags.", MessageType.None, true);
				EditorGUILayout.Space();

				EnemyTagNameProp.stringValue = EditorGUILayout.TagField("Enemy Tag", EnemyTagNameProp.stringValue);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Enemy Tag is the tag name your AI will consider a threat. Anything with this tag will cause your AI to flee. ", MessageType.None, true);
				}

				EditorGUILayout.Space();
				
				PlayerTagNameProp.stringValue = EditorGUILayout.TagField("Player Tag", PlayerTagNameProp.stringValue);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The Player Tag is the tag name for your Player that your AI will consider a threat. Anything with this tag will cause your AI to flee. ", MessageType.None, true);
				}
			}

			/*
			if (PreyOrPredatorProp.intValue == 3 && AITypeProp.intValue == 1)
			{
				EditorGUILayout.LabelField("Tag Options", EditorStyles.boldLabel);

				EditorGUILayout.LabelField("Enemy Tag Name", EditorStyles.miniLabel);
				EnemyTagNameProp.stringValue = EditorGUILayout.TextField(EnemyTagNameProp.stringValue, GUILayout.MaxHeight(75));
				
				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The tag name of Enemies that are your NPC will target.", MessageType.None, true);
				}
			}
			*/

				EditorGUILayout.Space();
				EditorGUILayout.Space();

		}


		if (TabNumberProp.intValue == 10 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Effect Options", EditorStyles.boldLabel);

			EditorGUILayout.Space();

			UseBloodProp.boolValue = EditorGUILayout.Toggle ("Use Blood Effect?",UseBloodProp.boolValue);
			
			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Use Blood Effect determins if your player uses a blood effect when hit.", MessageType.None, true);
			}
			
			if (UseBloodProp.boolValue)
			{
				EditorGUILayout.Space();
				
				//bool bloodEffect = !EditorUtility.IsPersistent (self);
				BloodEffectProp.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField ("Blood Effect", BloodEffectProp.objectReferenceValue, typeof(GameObject), true);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The particle effect that is used to play a blood effect when an AI is hit.", MessageType.None, true);
				}
			}


			EditorGUILayout.Space();

			UseDustEffectProp.boolValue = EditorGUILayout.Toggle ("Use Dust Effect?",UseDustEffectProp.boolValue);

			if (showHelpOptions == true)
			{
				EditorGUILayout.HelpBox("Use Dust Effect determins if your AI uses a dust effect when running.", MessageType.None, true);
			}

			if (UseDustEffectProp.boolValue)
			{
				EditorGUILayout.Space();

				//bool dustEffect = !EditorUtility.IsPersistent (self);
				DustEffectProp.objectReferenceValue = (ParticleSystem)EditorGUILayout.ObjectField ("Dust Effect", DustEffectProp.objectReferenceValue, typeof(ParticleSystem), true);

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("The particle effect that is used when an AI is running. The particle effect will only play when an AI is using its running animation. Its effects are controlled automatically by Emerald. It is recommended that the prefab's Emission Rate is set 0.", MessageType.None, true);
				}
			}


			EditorGUILayout.Space();
			EditorGUILayout.Space();

		}

		if (TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			EditorGUILayout.LabelField("Editor Options", EditorStyles.boldLabel);
			string showOrHide = "Show";
			if(showHelpOptions)
				showOrHide = "Hide";
			if(GUILayout.Button(showOrHide+ " Help Boxes", GUILayout.Width(thirdOfScreen*2), GUILayout.Height(sizeOfHideButtons)) )
			{
				showHelpOptions = !showHelpOptions;
			}

				EditorGUILayout.Space();
				EditorGUILayout.Space();

		}

		if (TabNumberProp.intValue == 11 || TabNumberProp.intValue == 12) 
		{
			//temp.Update ();
			//tabTemp.Update ();
			EditorGUILayout.LabelField("Breeding Options", EditorStyles.boldLabel);
			
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (AggressionProp.intValue != 2 || AITypeProp.intValue == 1)
			{
				EditorGUILayout.HelpBox("The Breeding Options are only for the Animal AIType with a Passive Behavior Type. This will be availble to all Animals and NPCs with Emerald 1.3." , MessageType.Info, true);
				EditorGUILayout.Space();
				EditorGUILayout.Space();
			}
			
			if (AggressionProp.intValue == 2 && AITypeProp.intValue == 0)
			{
				editorUseBreeding = (UseBreeding)UseBreedingProp.intValue;
				editorUseBreeding = (UseBreeding)EditorGUILayout.EnumPopup("Use Breeding?", editorUseBreeding);
				UseBreedingProp.intValue = (int)editorUseBreeding;

				EditorGUILayout.Space();

				if (showHelpOptions == true)
				{
					EditorGUILayout.HelpBox("Use Breeding controls whether or not this animal will use the breeding feature. Animals first need to be triggered before they can breed. Note: The demo scene Animal Breeding Demo demonstrates this and includes a demonstration script.", MessageType.None, true);
				}

				EditorGUILayout.Space();

				if (UseBreedingProp.intValue == 1)
				{
					IsBabyProp.boolValue = EditorGUILayout.Toggle ("Is Baby?", IsBabyProp.boolValue);
					
					if (showHelpOptions == true)
					{
						EditorGUILayout.HelpBox("Is Baby determines whether or not your animal is a baby. If you are using the Breeding System, your prefabed babies must have Is Baby checked.", MessageType.None, true);
					}

					if (!IsBabyProp.boolValue)
					{
						EditorGUILayout.Space();

						BreedSecondsProp.floatValue = EditorGUILayout.Slider("Breed Seconds", (float)BreedSecondsProp.floatValue, 1f, 100f);

						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("The Breed Seconds determines how many seconds must pass before the baby is spawned.", MessageType.None, true);
						}

						EditorGUILayout.Space();

						BreedCoolDownSecondsProp.floatValue = EditorGUILayout.FloatField("Breed Cool Down Seconds", (float)BreedCoolDownSecondsProp.floatValue);

						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("Breed Cool Down Seconds determines how many seconds must pass before your animal can breed again.", MessageType.None, true);
						}
					}

					if (IsBabyProp.boolValue)
					{
						EditorGUILayout.Space();
						
						BabySecondsProp.floatValue = EditorGUILayout.FloatField("Baby Seconds", (float)BabySecondsProp.floatValue);
						
						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("The Baby Seconds determines how many seconds your baby will stay a baby. Once this amount is exceeded, it will turn into a full grown animal. This full grown animal is based off of your full grown prefab.", MessageType.None, true);
						}

						EditorGUILayout.Space();

						FullGrownPrefabProp.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField ("Full Grown Prefab", FullGrownPrefabProp.objectReferenceValue, typeof(GameObject), true);

						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("The Full Grown Prefab is the prefab object your baby will turn into after it has exceeded its Baby Seconds.", MessageType.None, true);
						}

						if (FullGrownPrefabProp.objectReferenceValue == null)
						{
							EditorGUILayout.HelpBox("Your Animal is marked as a baby, but there is no Full Grown Prefab. Please apply a prefab to the Full Grown Prefab slot.", MessageType.Warning, true);
						}

						EditorGUILayout.Space();
					}

					if (!IsBabyProp.boolValue)
					{
						EditorGUILayout.Space();

						UseBreedEffectProp.boolValue = EditorGUILayout.Toggle ("Use Breed Effect?", UseBreedEffectProp.boolValue);

						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("Use Breed Effect determines whether or not your animal will use the Breed Effect option.", MessageType.None, true);
						}

						if (UseBreedEffectProp.boolValue)
						{
							EditorGUILayout.Space();
							
							BreedEffectProp.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField ("Breed Effect", BreedEffectProp.objectReferenceValue , typeof(GameObject), true);
							
							if (showHelpOptions == true)
							{
								EditorGUILayout.HelpBox("The Breed Effect is the effect that is spawned indicating that the two animals are in Breed Mode.", MessageType.None, true);
							}

							EditorGUILayout.Space();

							BreedEffectOffSetProp.vector3Value = EditorGUILayout.Vector3Field ("Breed Effect Offset", BreedEffectOffSetProp.vector3Value);

							if (showHelpOptions == true)
							{
								EditorGUILayout.HelpBox("The Breed Effect Offset allows you to adjust the spawning position of the Breed Effect to help match your model.", MessageType.None, true);
							}
						}

						EditorGUILayout.Space();
						EditorGUILayout.Space();
						EditorGUILayout.Space();

						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("In order for a Baby Prefab to work properly, you will need need to have Is Baby on the prefab object. This is located at the top of the Breeding Options. Your baby will then grow into a full grown animal when its Baby Seconds have been exceeded. Emerlad caluclates the sliders so they don't exceed 100%. When you've calculated your odds, ensure the sum of all your odds are equal to 100% for the most accurate results.", MessageType.Info, true);
						}

						EditorGUILayout.Space();

						GUI.backgroundColor = Color.green;
						BabyPrefabCommonProp.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField ("Common Baby Prefab", BabyPrefabCommonProp.objectReferenceValue, typeof(GameObject), true);

						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("The Common Baby Prefab is the prefab that will spawn when a Common baby is created between two animals. There is a " + CommonOddsProp.intValue + "% chance of receiving a Common baby." , MessageType.None, true);
						}

						EditorGUILayout.Space();

						CommonOddsProp.intValue = EditorGUILayout.IntSlider("Common Odds", CommonOddsProp.intValue, 0, 100);

						EditorGUILayout.Space();

						GUI.backgroundColor = Color.blue + Color.grey;
						EditorGUILayout.Space();

						BabyPrefabUncommonProp.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField ("Uncommon Baby Prefab", BabyPrefabUncommonProp.objectReferenceValue, typeof(GameObject), true);
						
						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("The Uncommon Baby Prefab is the prefab that will spawn when a Uncommon baby is created between two animals. There is a " + UncommonOddsProp.intValue + "% chance of receiving an Uncommon baby." , MessageType.None, true);
						}


						EditorGUILayout.Space();

						UncommonOddsProp.intValue = EditorGUILayout.IntSlider("Uncommon Odds", UncommonOddsProp.intValue, 0, 100 - (CommonOddsProp.intValue));

						EditorGUILayout.Space();

						GUI.backgroundColor = Color.red;
						BabyPrefabRareProp.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField ("Rare Baby Prefab", BabyPrefabRareProp.objectReferenceValue, typeof(GameObject), true);
						
						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("The Rare Baby Prefab is the prefab that will spawn when a Rare baby is created between two animals. There is a " + RareOddsProp.intValue + "% chance of receiving a Rare baby." , MessageType.None, true);
						}

						EditorGUILayout.Space();

						RareOddsProp.intValue = EditorGUILayout.IntSlider("Rare Odds", RareOddsProp.intValue, 0, 100 - (CommonOddsProp.intValue + UncommonOddsProp.intValue));

						EditorGUILayout.Space();

						GUI.backgroundColor = Color.yellow;
						BabyPrefabSuperRareProp.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField ("Super Rare Baby Prefab", BabyPrefabSuperRareProp.objectReferenceValue, typeof(GameObject), true);
						
						if (showHelpOptions == true)
						{
							EditorGUILayout.HelpBox("The Super Rare Baby Prefab is the prefab that will spawn when a Super Rare baby is created between two animals. There is a " + SuperRareOddsProp.intValue + "% chance of receiving a Super Rare baby." , MessageType.None, true);
						}

						SuperRareOddsProp.intValue = EditorGUILayout.IntSlider("Super Rare Odds", SuperRareOddsProp.intValue, 0 , 100 - (CommonOddsProp.intValue + UncommonOddsProp.intValue + RareOddsProp.intValue));
					}
				}
			}


		}




		if (GUI.changed) 
		{ 
			//EditorUtility.SetDirty(target);
			//EditorUtility.SetDirty(tabTemp.targetObject);
			EditorApplication.MarkSceneDirty();

			tabTemp.ApplyModifiedProperties ();
			temp.ApplyModifiedProperties ();
		}

	}


	void OnSceneGUI () 
	{
		Emerald_Animal_AI self = (Emerald_Animal_AI)target;

		//Cowardly
		if (AggressionProp.intValue == 1 && UseVisualRadiusProp.boolValue || AggressionProp.intValue == 0 && UseVisualRadiusProp.boolValue)
		{
			Handles.color = self.fleeRadiusColor;
			Handles.DrawSolidDisc(self.transform.position, Vector3.up, self.fleeRadius * self.transform.localScale.x);

			Handles.color = self.wanderRangeColor;
			Handles.DrawSolidDisc(self.transform.position, Vector3.up, self.wanderRange);

			//Stop new
			Handles.color = self.stoppingRangeColor;
			Handles.DrawSolidDisc(self.transform.position, Vector3.up, self.stoppingDistance);
		}

		//Passive
		if (AggressionProp.intValue == 2 && UseVisualRadiusProp.boolValue)
		{
			Handles.color = self.wanderRangeColor;
			Handles.DrawSolidDisc(self.transform.position, Vector3.up, self.wanderRange);

			//Stop new
			Handles.color = self.stoppingRangeColor;
			Handles.DrawSolidDisc(self.transform.position, Vector3.up, self.stoppingDistance);
		}

		//Aggresive
		if (AggressionProp.intValue == 3 && UseVisualRadiusProp.boolValue || AggressionProp.intValue == 4 && UseVisualRadiusProp.boolValue)
		{
			Handles.color = self.huntRadiusColor;
			Handles.DrawSolidDisc(self.transform.position, Vector3.up, self.huntRadius * self.transform.localScale.x);
			
			Handles.color = self.wanderRangeColor;
			Handles.DrawSolidDisc(self.transform.position, Vector3.up, self.wanderRange);

			//Stop new
			Handles.color = self.stoppingRangeColor;
			Handles.DrawSolidDisc(self.transform.position, Vector3.up, self.stoppingDistance);
		}

		/*
		if (GUI.changed) 
		{ 
			EditorUtility.SetDirty(self);
			EditorApplication.MarkSceneDirty();
			
			
		}
		*/
		
		//temp.ApplyModifiedProperties ();

		//SceneView.lastActiveSceneView.Repaint();
		SceneView.RepaintAll();
		//SceneView.currentDrawingSceneView.autoRepaintOnSceneChange = true;


	}
	
}


