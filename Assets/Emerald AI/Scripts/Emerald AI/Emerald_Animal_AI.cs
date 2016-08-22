//Emerald AI by: Black Horizon Studios
//Version 1.2

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (BoxCollider))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (AudioSource))]

[System.Serializable]
public class Emerald_Animal_AI : MonoBehaviour 
{
	public int fleeRadius = 15;
	public int huntRadius = 15;
	public int wanderRange = 20;
	public int fleeRange = 25;
	public int turnSpeed = 800;
	public float stoppingDistance = 2;
	public int extraFleeSeconds = 25;
	
	public int grazeLengthMin = 2;
	public int grazeLengthMax = 6;
	
	public float walkSpeed = 4.0f;
	public float runSpeed = 8.0f;
	
	public float walkAnimationSpeed = 1.0f;
	public float runAnimationSpeed = 1.0f;
	
	public AnimationClip idleAnimation;
	public AnimationClip idleBattleAnimation;
	public AnimationClip graze1Animation;
	public AnimationClip graze2Animation;
	public AnimationClip graze3Animation;
	public AnimationClip walkAnimation;
	public AnimationClip runAnimation;
	public AnimationClip turnAnimation;
	public AnimationClip deathAnimation;
	
	public AnimationClip[] grazeAnimations;
	
	public string playerTagName = "Player";
	public string preyTagName = "Huntable";
	public string predatorTagName = "Predator";
	public string followTagName = "Follow";
	public string enemyTagName = "";
	
	public float pathWidth = 0.25f;
	public Color pathColor = Color.green;
	
	public bool drawWaypoints = false;
	public bool drawPaths = false;
	public bool useDustEffect = false;
	
	public ParticleSystem dustEffect;
	
	public Color wanderRangeColor = new Color(0f, 0.8f, 0, 0.1f);
	public Color fleeRadiusColor = new Color(1.0f, 1.0f, 0, 0.1f);
	public Color huntRadiusColor = new Color(1.0f, 1.0f, 0, 0.1f);
	public Color fleeRangeColor = new Color(1.0f, 0, 0, 0.1f);
	public Color stoppingRangeColor = new Color(0, 1.0f, 0, 0.1f);
	
	public bool isFleeing = false;
	public bool isGrazing = false;
	private NavMeshAgent navMeshAgent;
	private float timer = 0;
	private int grazeLength = 10;
	private Vector3 startPosition;
	private Vector3 currentPlayerTransform;
	private float playerZPos;
	private Vector3 destination;
	private float pathWidthAdjusted;
	
	private GameObject currentWaypoint;
	private GameObject fleePoint;
	
	private LineRenderer line;
	private Transform target; 
	private ParticleSystem clone;
	private Animation anim;
	public SphereCollider triggerCollider;
	private BoxCollider boxCollider;
	
	public int aggression = 1;
	public int grazeAnimationNumber;
	public int totalGrazeAnimations;
	public Material pathMaterial;
	public float lineYOffSet;
	public bool useVisualRadius = true;
	public bool useAnimations = true;
	public bool rotateFlee = false;
	public Vector3 direction;
	public Quaternion playerRotation;
	public Quaternion predatorRotation;
	public float rotateTimer;
	public float fleeTimer;
	public bool startFleeTimer = false;
	
	public Terrain terrain;
	public GameObject terrainGameObeject;
	public float steepness;
	public float dest;
	public GameObject thing;
	
	public bool huntMode = false;
	public GameObject currentAnimal;
	public int preySize = 2;
	public int predatorSize = 2;
	public bool startHuntTimer = false;
	public float huntTimer = 30;
	public float huntSeconds = 5;
	public int preyOrPredator = 1;
	public bool preySizeMatched = false;
	public bool enableDebugLogs = false;
	public float attackTimer = 0;
	public float attackTime = 1;
	public float attackTimeMin = 1;
	public float attackTimeMax = 1;
	public float attackAnimationSpeedMultiplier = 1.5f;
	public bool withinAttackDistance = false;
	
	public int totalAttackAnimations;
	public int currentAttackAnimation;
	public AnimationClip currentAttackAnimationClip;
	public AnimationClip attackAnimation1;
	public AnimationClip attackAnimation2;
	public AnimationClip attackAnimation3;
	public AnimationClip attackAnimation4;
	public AnimationClip attackAnimation5;
	public AnimationClip attackAnimation6;
	public AnimationClip hitAnimation;
	public AnimationClip runAttackAnimation;
	
	public GameObject hitEffect;
	public bool damageDealt = false;
	public bool damageTaken = false;
	public bool useRunAttackAnimations = false;
	public int startingHealth = 15;
	public int currentHealth = 15;
	public GameObject deadObject;
	
	public int offSetPosition;
	public int offSetDistance;
	public bool currentlyBeingPursued = false;

	public int attackDamage = 5;
	public int attackDamageMin = 5;
	public int attackDamageMax = 5;
	
	//public AudioClip AttackSound;
	public List<AudioClip> attackSounds = new List<AudioClip>();
	public List<bool> foldOutList = new List<bool>();
	public int attackSoundsSize = 0;

	public List<AudioClip> getHitSounds = new List<AudioClip>();
	public List<bool> foldOutListHits = new List<bool>();
	public int hitSoundsSize;

	public AudioClip weaponSound;
	public AudioSource audioSource;
	public AudioClip getHitSound;
	public AudioClip dieSound;
	public float minSoundPitch = 0.8f;
	public float maxSoundPitch = 1.2f;
	public float updateSpeedTimer = 0;
	public float updateSpeed = 0.1f;
	
	public float velocity;
	public bool attackWhileRunning = false;
	public bool attackWhileRunningEnabled = false;
	public bool isCoolingDown = false;
	public float coolDownTimer;
	public float coolDownSeconds = 25;
	public Quaternion lookRotation;
	public Quaternion originalLookRotation;
	
	public float freezeSecondsMin = 0.25f;
	public float freezeSecondsMax = 1;
	public float freezeSecondsTotal;
	public float freezeSecondsTimer = 0;
	public bool isFrozen = false;
	
	//Global Stats
	public int maxNumberOfActiveAnimals = 10;
	public static int currentNumberOfActiveAnimals;
	public bool systemOn = false;
	public Renderer objectsRender;
	
	public float updateSystemSpeed = 1;
	public float updateSystemTimer = 1;
	public float navMeshCountDownTimer = 0;
	public bool navMeshCountDown = false;
	public bool navMeshDisabled = true;
	
	public bool inHerd = false;
	public Transform animalToFollow;
	public int isAlpha;
	public int isAlphaOrNot = 2;
	public float offSetHerdX;
	public float offSetHerdXMin = -10;
	public float offSetHerdXMax = 10;
	public float offSetHerdZ;
	public float offSetHerdZMin = -10;
	public float offSetHerdZMax = 10;
	public string animalNameType = "";
	public bool threatIsOutOfTigger = false;
	public List <GameObject> herdList = new List<GameObject>();
	public int herdNumber;
	public bool markInPack = false;
	public GameObject temp;
	public bool isDead = false;
	
	public GameObject fleeTarget;
	public bool calculateFlee = false; 
	public bool playSoundOnFlee = false;
	public AudioClip fleeSound;
	public Vector3 Direction;
	public bool distantFlee;
	public bool calculateWander = false;
	public float footStepSeconds = 0.15f;
	public float footStepSecondsWalk = 0.5f;
	public float runTimer = 0;
	public AudioClip runSound;
	public AudioClip walkSound;
	public int maxPackSize = 5;
	public bool packCreated = false;
	public int maxDistanceFromHerd = 100;
	public bool hasPack = false;
	public bool isExhausted = false;
	public float chaseTimer;
	public int chaseSeconds = 60;
	public bool herdFull = false;
	public bool waitingForHerd = false;
	public bool alignAI = true;
	public bool terrainFound = false;
	public Transform alignTarget;
	public bool alphaWaitForHerd = false;
	public bool attackingEnabled = false;
	
	public bool useHitSound = false;
	public bool useAttackSound = false;
	public bool useWeaponSound = false;
	public bool useRunSound = false;
	public bool useWalkSound = false;
	public bool autoGenerateAlpha = true;

	//New from here
	public bool useDeadReplacement = false;
	public bool animationPlayed = false;
	public bool isFollowing = false;
	public Transform followTransform;

	public bool isReadyForBreeding = false;
	public bool spawnedBreedEffect = false;
	public GameObject spawnedObject;
	public GameObject breedEffect;
	public bool mateFound = false;
	public Transform mateATransform;
	public Transform mateBTransform;
	public bool breedCoolDown = false;
	public float breedCoolDownTimer = 0;
	public float breedCoolDownSeconds = 60;		//How much time is needed before the animal can breed again
	public bool isBaby = false;
	public float breedEffectOffSetX = 0;
	public float breedEffectOffSetY = 0;
	public float breedEffectOffSetZ = 0;
	public Vector3 breedEffectOffSet;

	public float breedTimer = 0;
	public float breedSeconds = 5;		//How long does the breeding process take?

	public float cancelBreedTimer = 0;
	public float cancelBreedSeconds = 30;		//How many seconds does the animal have before the breeding process is canceled?

	public float babyTimer = 0;		
	public float babySeconds = 30;		//How long does it take for a baby to become an adult?

	public GameObject babyPrefabCommon;
	public GameObject babyPrefabUncommon;
	public GameObject babyPrefabRare;
	public GameObject babyPrefabSuperRare;

	public GameObject spawnedBabyObject;
	public GameObject fullGrownPrefab;
	public bool isBabyGiver = false;		//This is handled automatically. The first animal to be given the components to mate will have the baby.

	//Adding sounds
	public List<AudioClip> animalSounds = new List<AudioClip>();
	public bool isPlayingAnimalSound = false;
	public bool useAnimalSounds = false;
	public float animalSoundTimer = 0;
	public float animalSoundMin = 4;
	public float animalSoundMax = 10;
	public float animalSoundWaitTime = 4;

	public double generatedOdds;

	public Rigidbody arrowPref;
	public Rigidbody arrowObject;

	public Transform firePos;

	public int AIType = 0;

	public Emerald_Animal_AI currentTargetSystem;

	public int CurrentAggro = 0;
	public int MaxAggro = 0;

	public bool usingAggro = true;

	public	float checkPositionTimer = 0;

	public GameObject bloodEffect;
	public bool useBlood = false;

	public GameObject ragdollObject;
	public bool useRagdoll = false;

	public bool hitFromPlayer = false;

	public bool isPlayer = false;
	public float attackDelaySeconds = 0.75f;
	public float changeTargetDistance = 10;
	public float targetCurrentDistance;
	public float changeTargetTimer;
	public bool searchForTarget = false;
	public int TabNumber;
	public int TabNumberAll;
	public bool returnsToStart = false;
	public bool useBreedEffect = false;
	
	public AudioClip AttackSound1;
	public AudioClip AttackSound2;
	public AudioClip AttackSound3;
	public AudioClip AttackSound4;
	public AudioClip AttackSound5;
	public int totalAttackSounds = 0;

	public AudioClip HitSound1;
	public AudioClip HitSound2;
	public AudioClip HitSound3;
	public AudioClip HitSound4;
	public AudioClip HitSound5;
	public int totalHitSounds = 0;

	public AudioClip AnimalSound1;
	public AudioClip AnimalSound2;
	public AudioClip AnimalSound3;
	public int totalAnimalSounds = 0;

	public bool deathTrigger = false;
	public bool isTurning = false;
	public bool useTurnAnimation = false;
	public bool useDieSound = false;
	public float angle = 0;
	public int fleeType = 0;
	public bool targetInRange = false;
	public float deathAnimationTimer = 0;
	public bool deathAnimationFinished = false;
	public int UseBreeding = 1;
	public float followAlphaTimer = 0;

	public int commonOdds = 60;
	public int uncommonOdds = 25;
	public int rareOdds = 10;
	public int superRareOdds = 5;

	public float deathTimer = 0; 
	public int totalAIAround;
	int tempDamage;
	public float maxChaseDistance = 50;
	public bool returnBackToStartingPointProtection = false;
	public string CurrentString;
	public int maxFleeDistance = 10;
	public bool canGetTired = false;
	public float fleeRandomnessTimer;
	public float fleeRandomness;
	public int stuck = 1;
	public string NPCName = "Joe";
	public bool autoCalculateDelaySeconds = false;
	public bool useHitAnimation = false;
	public float baseOffsetNav = 0;
	public int maximumWalkingVelocity = 30;
	public bool triggerColliderAutoGenerated = false;
	
	void Awake()
	{
		startPosition = this.transform.position;
		
		if (!isFleeing)
		{
			grazeLength = Random.Range(grazeLengthMin, grazeLengthMax);
		}
		
		if (isFleeing)
		{
			grazeLength = Random.Range(grazeLengthMin, grazeLengthMax);
		}

		objectsRender = GetComponentInChildren<Renderer>();

		if (triggerCollider == null)
		{
			//If our AI doesn't have a renderer as a child (a character model/skinned mesh renderer) create a one for it.
			if (objectsRender == null || objectsRender == this.gameObject.GetComponent<Renderer>())
			{
				GameObject tempSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				tempSphere.transform.parent = this.gameObject.transform;
				tempSphere.transform.localPosition = new Vector3(0,0,0);
				tempSphere.gameObject.layer = 2;
				tempSphere.GetComponent<Renderer>().enabled = false;
				triggerColliderAutoGenerated = true;
			}

			//Find our AI's renderer and use it for optimizations
			if (objectsRender != null && !triggerColliderAutoGenerated)
			{
				objectsRender.gameObject.AddComponent<SphereCollider>();
				objectsRender.gameObject.layer = 2;
			}
		}
	}


	void Start () 
	{
		//Apply and modify needed components on Start
		navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
		triggerCollider = GetComponentInChildren<SphereCollider>();
		boxCollider = GetComponent<BoxCollider>();
		gameObject.GetComponent<Rigidbody>().isKinematic = true;
		gameObject.GetComponent<Rigidbody>().useGravity = false;
		audioSource = GetComponent<AudioSource>();
		terrainGameObeject = GameObject.Find("Terrain");

		//Apply our serialized attack sounds to the AI's AttackSound List
		if (useAttackSound)
		{
			if (totalAttackSounds == 1)
			{
				attackSounds.Insert(0, AttackSound1);
			}

			if (totalAttackSounds == 2)
			{
				attackSounds.Insert(0, AttackSound1);
				attackSounds.Insert(1, AttackSound2);
			}

			if (totalAttackSounds == 3)
			{
				attackSounds.Insert(0, AttackSound1);
				attackSounds.Insert(1, AttackSound2);
				attackSounds.Insert(2, AttackSound3);
			}

			if (totalAttackSounds == 4)
			{
				attackSounds.Insert(0, AttackSound1);
				attackSounds.Insert(1, AttackSound2);
				attackSounds.Insert(2, AttackSound3);
				attackSounds.Insert(3, AttackSound4);
			}

			if (totalAttackSounds == 5)
			{
				attackSounds.Insert(0, AttackSound1);
				attackSounds.Insert(1, AttackSound2);
				attackSounds.Insert(2, AttackSound3);
				attackSounds.Insert(3, AttackSound4);
				attackSounds.Insert(4, AttackSound5);
			}
		}

		//Apply our serialized hit sounds to the AI's HitSound List
		if (useHitSound)
		{
			if (totalHitSounds == 1)
			{
				getHitSounds.Insert(0, HitSound1);
			}
			
			if (totalHitSounds == 2)
			{
				getHitSounds.Insert(0, HitSound1);
				getHitSounds.Insert(1, HitSound2);
			}
			
			if (totalHitSounds == 3)
			{
				getHitSounds.Insert(0, HitSound1);
				getHitSounds.Insert(1, HitSound2);
				getHitSounds.Insert(2, HitSound3);
			}
			
			if (totalHitSounds == 4)
			{
				getHitSounds.Insert(0, HitSound1);
				getHitSounds.Insert(1, HitSound2);
				getHitSounds.Insert(2, HitSound3);
				getHitSounds.Insert(3, HitSound4);
			}
			
			if (totalHitSounds == 5)
			{
				getHitSounds.Insert(0, HitSound1);
				getHitSounds.Insert(1, HitSound2);
				getHitSounds.Insert(2, HitSound3);
				getHitSounds.Insert(3, HitSound4);
				getHitSounds.Insert(4, HitSound5);
			}
		}

		//Apply our serialized attack sounds to the AI's AttackSound List
		if (useAnimalSounds)
		{
			if (totalAnimalSounds == 1)
			{
				animalSounds.Insert(0, AnimalSound1);
			}
			
			if (totalAnimalSounds == 2)
			{
				animalSounds.Insert(0, AnimalSound1);
				animalSounds.Insert(1, AnimalSound2);
			}
			
			if (totalAnimalSounds == 3)
			{
				animalSounds.Insert(0, AnimalSound1);
				animalSounds.Insert(1, AnimalSound2);
				animalSounds.Insert(2, AnimalSound3);
			}
		}

		//Randomize our Animal's sounds so they aren't playing on Start
		if (useAnimalSounds)
		{
			animalSoundWaitTime = (int)Random.Range(animalSoundMin, animalSoundMax);
		}

		//If our terrain is null, disable certain features
		if (terrainGameObeject == null)
		{
			terrainFound = false;
			alignAI = false;
		}
		
		if (terrainGameObeject != null)
		{
			terrainFound = true;
		}
		
		if (terrainFound && alignAI)
		{
			terrain  = terrainGameObeject.GetComponent<Terrain>();
		}
		
		huntTimer = huntSeconds;

		//Apply a LineRender if using the Draw Paths feature
		if (GetComponent<LineRenderer>() == null && drawPaths)
		{
			this.gameObject.AddComponent<LineRenderer>();
		}
		
		triggerCollider.isTrigger = true;

		if (aggression == 0)
		{
			triggerCollider.radius = fleeRadius;
		}
		
		if (aggression == 1)
		{
			triggerCollider.radius = fleeRadius;
		}

		if (aggression == 2)
		{
			triggerCollider.radius = wanderRange;
		}
		
		if (aggression == 3)
		{
			triggerCollider.radius = huntRadius;
		}

		if (aggression == 4)
		{
			triggerCollider.radius = huntRadius;
		}


		//If Draw Waypoints is enabled, apply and adjust needed components
		if (drawWaypoints)
		{
			currentWaypoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			currentWaypoint.gameObject.transform.localScale = new Vector3 (0.75f, 0.75f, 0.75f);
			currentWaypoint.GetComponent<Renderer>().material.color = pathColor;
			currentWaypoint.name = this.gameObject.name + " Waypoint";

			currentWaypoint.GetComponent<SphereCollider>().enabled = false;
			currentWaypoint.AddComponent<AlignWaypoint>();
		}

		//Apply the custom Line material for drawing the AI's path
		if (drawPaths)
		{
			line = GetComponent<LineRenderer>();
			line.material = pathMaterial;
		}

		//Set our AI's settings from the Editor
		navMeshAgent.angularSpeed = turnSpeed;
		navMeshAgent.stoppingDistance = stoppingDistance;
		navMeshAgent.baseOffset = baseOffsetNav;
		
		currentHealth = startingHealth;
		currentAttackAnimation = Random.Range(1, totalAttackAnimations+1);
		
		if (useAnimations)
		{
			anim = GetComponent<Animation>();
			
			if (anim != null)
			{
				anim[walkAnimation.name].speed = walkAnimationSpeed;
				anim[runAnimation.name].speed = runAnimationSpeed;
				anim[deathAnimation.name].wrapMode = WrapMode.ClampForever;

				if (useHitAnimation)
				{
					anim[hitAnimation.name].wrapMode = WrapMode.Once;
				}
			}
		}
		
		if (anim == null)
		{
			useAnimations = false;
		}
		
		if(useDustEffect)
		{
			clone = Instantiate(dustEffect, new Vector3 (transform.position.x, transform.position.y + 0.35f, transform.position.z + 1.0f), Quaternion.identity) as ParticleSystem;
			clone.transform.parent = transform;
			clone.emissionRate = 0;
		}
		
		if (drawPaths)
		{
			pathWidthAdjusted = pathWidth * 0.01f;
			
			line.SetWidth(pathWidthAdjusted, pathWidthAdjusted);
			line.SetColors(pathColor, pathColor);
		}
		
		if (drawPaths)
		{
			GetPath();
		}
		
		if (aggression == 2)
		{
			Wander();
		}

		//Setup our animations
		ApplyAttackAnimations();

		grazeAnimationNumber = Random.Range(1,totalGrazeAnimations+1);
		audioSource.pitch = Random.Range(minSoundPitch, maxSoundPitch);
		attackTime = Random.Range(attackTimeMin, attackTimeMax);
		
		triggerCollider.enabled = false;
		navMeshAgent.enabled = false;
		boxCollider.enabled = false;


		//Setup or generate our Alphas 
		if (autoGenerateAlpha)
		{
			isAlpha = Random.Range(0,5);
			
			//Not Alpha
			if (isAlpha <= 3)
			{
				isAlpha = 0;
			}
			
			//Is Alpha
			if (isAlpha > 3)
			{
				isAlpha = 1;
			}
		}
		
		if (!autoGenerateAlpha)
		{
			if (isAlphaOrNot == 1)
			{
				isAlpha = 1;
			}
			
			if (isAlphaOrNot == 2)
			{
				isAlpha = 0;
			}
		}
		

		offSetHerdX = Random.Range(offSetHerdXMin,offSetHerdXMax);
		offSetHerdZ = Random.Range(offSetHerdZMin,offSetHerdZMax);
		
		fleeTimer = (float)extraFleeSeconds;
		maxPackSize = maxPackSize - 1;
		
		audioSource.enabled = false;
		
		//Waypoints need to be enabled in order for paths to be drawn
		if (drawPaths)
		{
			drawWaypoints = true;
		}
		
		if (useAnimations)
		{
			anim.enabled = false;
		}
		
		if (alignAI)
		{
			originalLookRotation = transform.rotation; 
			alignTarget = this.transform;
		}

		//If you're using Emerald, your AI will have to have a tag according to your AI's tag settings. 
		if (this.gameObject.tag == "Untagged")
		{
			Debug.Log("Your AI's tag is marked as Untagged. You need to apply a proper tag name according to your AI's Tag Options.");
		}
	}

	//When the appropriate time is reached, play an animal sound. This only happens for Passive Animals
	public void PlayAnimalSound ()
	{
		audioSource.PlayOneShot(animalSounds[Random.Range(0,animalSounds.Count)]);
		animalSoundWaitTime = (int)Random.Range(animalSoundMin, animalSoundMax);
		animalSoundTimer = 0;
		isPlayingAnimalSound = false;
	}

	//This follow function is used when a Player uses food to get an animal to follow them. This function can also be used for custom purposes, if desired.
	public void Follow()
	{
		if (systemOn && !isFleeing && isFollowing)
		{
			if (Vector3.Distance(this.transform.position, followTransform.position) > navMeshAgent.stoppingDistance)
			{
				destination = new Vector3(followTransform.position.x, this.transform.position.y, followTransform.position.z);
				NewDestination(destination);
			}
			
			if (useAnimations && !isCoolingDown)
			{
				anim.CrossFade(walkAnimation.name);
			}
			
			isGrazing = false;
			
			navMeshAgent.speed = walkSpeed;
			
			if (drawWaypoints)
			{
				currentWaypoint.transform.position = destination;
			}
		}
	}

	//An example of a Scatter function. This function is currently unused. However, it maybe called externally if AI get too bunch up and need to spread out.
	void Scatter()
	{
		if (systemOn)
		{

			destination = startPosition + new Vector3(Random.Range (-wanderRange * 0.5f, wanderRange * 0.5f), 0, Random.Range (-wanderRange * 0.5f, wanderRange * 0.5f));
			NewDestination(destination);
			
			if (useAnimations)
			{
				anim.CrossFade(runAnimation.name);
			}
			
			if (drawWaypoints)
			{
				currentWaypoint.transform.position = destination;
			}
		}
	}

	//The wandering function AI use to dynamically generate waypoints
	void Wander()
	{
		if (systemOn && !isFleeing && !isFollowing)
		{
			destination = startPosition + new Vector3(Random.Range (-wanderRange * 0.5f, wanderRange * 0.5f), 0, Random.Range (-wanderRange * 0.5f, wanderRange * 0.5f));

			NewDestination(destination);
			
			if (useAnimations && !isCoolingDown)
			{
				anim.CrossFade(walkAnimation.name);
			}
			
			isGrazing = false;
			
			navMeshAgent.speed = walkSpeed;
			
			if (drawWaypoints)
			{
				currentWaypoint.transform.position = destination;
			}
		}
	}

	//The fleeing function AI use. AI will generate waypoints in the opposite direction of their chaser to avoid danger.
	void Flee()
	{
		isGrazing = false;

		//If our velocity drops near 0, play our idle animation. This is to stop our AI from running or walking when not in motion
		if (useAnimations && !withinAttackDistance)
		{
			velocity = navMeshAgent.velocity.sqrMagnitude;

			
			if (velocity <= 0.05f && anim.IsPlaying(runAnimation.name))
			{
				anim.CrossFade(idleAnimation.name);
			}
		}

		//If our AI are in a herd, have the herd members have the same fleeing target as the Alpha
		if (isAlpha == 0 && inHerd && fleeTarget == null && preyOrPredator == 1)
		{
			fleeTarget = animalToFollow.GetComponent<Emerald_Animal_AI>().fleeTarget;
		}

		//If we become stuck, or our waypoint leads to nowhere, generate a new waypoint.
		if (fleeTarget != null && isAlpha == 0 && !inHerd || fleeTarget != null && isAlpha == 1)
		{
			if (stuck == 2)
			{
				fleeRandomnessTimer += Time.deltaTime;
			}

			if (fleeRandomnessTimer >= 0.25f)
			{
				fleeRandomness = Random.Range(-1,-15);
				fleeRandomnessTimer = 0;
				stuck = 1;
			}

			if (navMeshAgent.remainingDistance < 5)
			{
				Vector3 direction = (fleeTarget.transform.position - transform.position).normalized;
				destination = transform.position + -direction * 40;
				navMeshAgent.SetDestination(destination);
			}

			if (!navMeshAgent.hasPath && stuck == 1 || navMeshAgent.isPathStale && stuck == 1 || navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid && stuck == 1 || navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial && stuck == 1)
			{
				navMeshAgent.Stop();
				navMeshAgent.ResetPath();

				Vector3 direction = (fleeTarget.transform.position - transform.position).normalized;
				destination = transform.position + -direction * 60;
				NewDestination(destination);

				if (useAnimations)
				{
					anim.CrossFade(idleAnimation.name);
				}

				stuck = 2;
			}

			//If our path isn't blocked, continuously generate waypoints when within distance of 15 units or less.
			if (navMeshAgent.hasPath && stuck == 1 || !navMeshAgent.isPathStale && stuck == 1 || navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid && stuck == 1 || navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial && stuck == 1)
			{
				if (Vector3.Distance(transform.position, destination) < 15)
				{
					Vector3 direction = (fleeTarget.transform.position - transform.position).normalized;
					destination = transform.position + -direction * 80;
					NewDestination(destination);

					if (useAnimations && velocity > maximumWalkingVelocity && !isTurning)
					{
						anim.CrossFade(runAnimation.name);
						navMeshAgent.speed = runSpeed;
					}
					
					if (!useAnimations)
					{
						navMeshAgent.speed = runSpeed;
					}
				}
			}

			//If our AI move slower than our maxiumum walking velocity, play our walking animation.
			if (useAnimations && velocity > 0.15f && velocity < maximumWalkingVelocity && !isTurning)
			{
				anim.CrossFade(walkAnimation.name);
			}
		}
	}

	//The Breed function is what allows an AI to breed, if the proper conditions have been met. 
	//The function will roll a number to decide what animal will be bred. This is based off the user's percentages in the Emerald Editor.
	public void Breed ()
	{
		//If mate A is killed or lost, reset settings and start cooldown. 
		if (mateFound && !breedCoolDown && isBabyGiver && mateATransform == null)
		{
			isReadyForBreeding = false;
			spawnedBreedEffect = false;
			isBabyGiver = false;
			breedTimer = 0;
			Destroy(spawnedObject);
			Wander ();
			mateFound = false;
			breedCoolDown = true;
		}

		//If mate B is killed or lost, reset settings and start cooldown. 
		if (mateFound && !breedCoolDown && !isBabyGiver && mateBTransform == null)
		{
			isReadyForBreeding = false;
			spawnedBreedEffect = false;
			isBabyGiver = false;
			breedTimer = 0;
			Destroy(spawnedObject);
			Wander ();
			mateFound = false;
			breedCoolDown = true;
		}


		if (mateFound && !breedCoolDown && isBabyGiver && mateATransform != null)
		{
			destination = new Vector3(mateATransform.position.x + 1, mateATransform.position.z);
			NewDestination(destination);

			if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
			{
				breedTimer += Time.deltaTime;

				if (breedTimer >= breedSeconds)
				{
					//Roll a random float between 0 and 1
					generatedOdds = System.Math.Round(Random.value,2);

					//Calculate our breeding odds by converting the Editor numbers to decimals then finding the percentage difference
					float CalculatedUncommon = (((uncommonOdds * 0.01f)) - 1) * -1;
					float CalculatedRare = (((rareOdds * 0.01f)) - 1) * -1;
					float CalculatedSuperRare = (((superRareOdds * 0.01f)) - 1) * -1;

					//Spawn babies according to calculated Editor odds
					if(generatedOdds <= commonOdds*0.01f || generatedOdds <= CalculatedUncommon) 
					{ 
						spawnedBabyObject = Instantiate(babyPrefabCommon, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
					}

					if(generatedOdds > CalculatedUncommon && generatedOdds < CalculatedRare) 
					{ 
						spawnedBabyObject = Instantiate(babyPrefabUncommon, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
					}

					if(generatedOdds >= CalculatedRare && generatedOdds < CalculatedSuperRare) 
					{ 
						spawnedBabyObject = Instantiate(babyPrefabRare, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
					}

					if(generatedOdds >= CalculatedSuperRare) 
					{ 
						spawnedBabyObject = Instantiate(babyPrefabSuperRare, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
					}

					//Set needed setting after successful breeding
					isReadyForBreeding = false;
					spawnedBreedEffect = false;
					isBabyGiver = false;
					breedTimer = 0;
					Destroy(spawnedObject);
					Wander ();
					mateATransform.gameObject.GetComponent<Emerald_Animal_AI>().isReadyForBreeding = false;
					mateATransform.gameObject.GetComponent<Emerald_Animal_AI>().spawnedBreedEffect = false;
					mateATransform.gameObject.GetComponent<Emerald_Animal_AI>().mateFound = false;
					mateATransform.gameObject.GetComponent<Emerald_Animal_AI>().breedCoolDown = true;
					Destroy(mateATransform.gameObject.GetComponent<Emerald_Animal_AI>().spawnedObject);
					mateFound = false;
					breedCoolDown = true;
				}
			}
		}
	}
	
	public void NewDestination(Vector3 targetPoint)
	{
		if (navMeshAgent.enabled)
		{
			navMeshAgent.SetDestination (targetPoint);
		}
	}
	
	//Calculates our path lines, if they are enabled
	void GetPath()
	{
		if (drawPaths)
		{
			line.SetPosition(0, new Vector3(transform.localPosition.x, transform.position.y + lineYOffSet, transform.position.z)); //set the line's origin
			DrawPath(navMeshAgent.path);
		}
	}
	
	//Draws our path lines
	public void DrawPath(NavMeshPath path)
	{
		if (drawPaths)
		{
			if(path.corners.Length < 1) 
				return;
			
			line.SetVertexCount(path.corners.Length); 
			
			for(int i = 1; i < path.corners.Length; i++)
			{
				line.SetPosition(i, path.corners[i]);
			}
		}
	}
	
	//The SystemOptimizerUpdater checks to see if our AI are still in view every few seconds.
	//If they are no longer in view, according to Unity's LOD System, disable all components until back in view.
	//Using the NavMesh timer, check to see that our AI are off screen for at least 10 seconds before deactivating.
	//If the AI are out of view, set systemOn to false, which stops Emerald from calculating
	void SystemOptimizerUpdater ()
	{
		updateSystemTimer += Time.deltaTime;			//Use this to keep track of the amount of seconds until we can update
		
		if (updateSystemTimer >= updateSystemSpeed && !isDead)			//If the update seconds have reached the amount of desired update seconds, update our system optimizer						
		{
			if (objectsRender.isVisible)					//If the AI's renderer is visible, enabled our components
			{
				systemOn = true;
				navMeshAgent.enabled = true;
				navMeshDisabled = false;
				triggerCollider.enabled = true;
				boxCollider.enabled = true;
				audioSource.enabled = true;
				
				if (useAnimations)
				{
					anim.enabled = true;
				}
				
				navMeshCountDownTimer = 0;
				updateSystemTimer = 0;						//The updateSystemTimer has be ticked, restart.
			}
			
			if (!objectsRender.isVisible && !huntMode)				//If the AI's renderer is not visible, disable our components so that they aren't waisting performance when they aren't visible		
			{
				navMeshCountDown = true;				
				updateSystemTimer = 0;						//The updateSystemTimer has be ticked, restart.
			}
		}
		
		if (navMeshCountDown && !navMeshDisabled)	
		{													//If not visible, enabled our navMeshCountDown (This only allows the NavMesh component to be enabled when 15 seconds have passed)												
			navMeshCountDownTimer += Time.deltaTime;		//This is to avoid the NavMesh from being disabled from simply looking away from the AI for a second or two
			
			if (navMeshCountDownTimer >= 40)
			{
				navMeshAgent.enabled = false;
				audioSource.enabled = false;
				systemOn = false;
				navMeshDisabled = true;

				triggerCollider.enabled = false;
				boxCollider.enabled = false;
				
				if (useAnimations)
				{
					anim.Stop();
				}
				
				navMeshCountDownTimer = 0;
				updateSystemTimer = 0;						//The updateSystemTimer has be ticked, restart.
			}
		}
	}
	
	void MainSystem ()
	{
		//Calls our following function, if the proper conditions have been met.
		if (isFollowing && !mateFound && !breedCoolDown && !isBaby && followTransform.gameObject.tag == followTagName)
		{
			Follow ();

			if (followTransform.gameObject.activeSelf == false)
			{
				isFollowing = false;
			}
		}

		//If our AI is a baby, count to the necessary babySeconds. Once they are reached, transform into a full grown adult. 
		//If this causes an error, it's because you don't have an object in the fullGrownPrefab slot in the Editor.
		if (isBaby)
		{
			babyTimer += Time.deltaTime;

			if (babyTimer >= babySeconds)
			{
				Instantiate(fullGrownPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
				Destroy(gameObject);
			}
		}

		//If Use Animal Sounds is enabled, play a random animal sound according to the animal sound wait time.
		if (useAnimalSounds)
		{
			animalSoundTimer += Time.deltaTime;

			if (animalSoundTimer >= animalSoundWaitTime)
			{
				PlayAnimalSound();
			}
		}

		//Spawn our breeding effect, when an animal is breeding, if it is enabled.
		if (!isBaby && isReadyForBreeding && !breedCoolDown)
		{
			if (!spawnedBreedEffect)
			{
				boxCollider.enabled = false;
				spawnedObject = Instantiate(breedEffect, new Vector3(transform.position.x + breedEffectOffSet.x, transform.position.y + breedEffectOffSet.y, transform.position.z + breedEffectOffSet.z), Quaternion.Euler(0, 0, 0)) as GameObject;
				spawnedObject.transform.parent = this.transform;
				spawnedObject.transform.position = new Vector3(transform.position.x + breedEffectOffSet.x, transform.position.y + breedEffectOffSet.y, transform.position.z + breedEffectOffSet.z);
				boxCollider.enabled = true;
				spawnedBreedEffect = true;
			}

			//If no animal is found when breed mode is activated by the time cancelBreedSeconds is met, cancel the breeding and set isReadyForBreeding to false
			cancelBreedTimer += Time.deltaTime;

			if (cancelBreedTimer >= cancelBreedSeconds)
			{
				cancelBreedTimer = 0;
				Destroy(spawnedObject);
				isReadyForBreeding = false;
			}

			//If the proper conditions are met, call a successful breed.
			Breed();
		}

		//After a successful breed, wait for the breedCoolDownSeconds to be met for it to be possible for an AI to breed again.
		if (breedCoolDown)
		{
			breedCoolDownTimer += Time.deltaTime;

			if (breedCoolDownTimer >= breedCoolDownSeconds)
			{
				breedCoolDownTimer = 0;
				breedTimer = 0;
				breedCoolDown = false;
			}
		}

		if (navMeshAgent.enabled && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !isFleeing && preyOrPredator == 1 || navMeshAgent.enabled && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !huntMode && preyOrPredator == 2) 
		{
			isGrazing = true;
		}

		//If our animal is grazing, and useAnimations is enabled, pick a random graze animation to play.
		if (isGrazing)
		{
			timer += Time.deltaTime;
			
			if (useAnimations)
			{
				if (grazeAnimationNumber == 1)
				{
					anim.CrossFade(graze1Animation.name);
				}
				
				if (grazeAnimationNumber == 2)
				{
					anim.CrossFade(graze2Animation.name);
				}
				
				if (grazeAnimationNumber == 3)
				{
					anim.CrossFade(graze3Animation.name);
				}
			}
			
			if (timer >= grazeLength)
			{
				isGrazing = false;
				timer = 0;
				grazeAnimationNumber = Random.Range(1,totalGrazeAnimations+1);
				
				if (inHerd && animalToFollow != null)
				{
					offSetHerdX = Random.Range(offSetHerdXMin,offSetHerdXMax);
					offSetHerdZ = Random.Range(offSetHerdZMin,offSetHerdZMax);
					
					NewDestination(new Vector3 (animalToFollow.localPosition.x + offSetHerdX + 5, animalToFollow.localPosition.y, animalToFollow.localPosition.z - offSetHerdZ));
				}
				
				if (!inHerd || isAlpha == 1)
				{
					if (!isDead)
					{
						Wander();
					}
				}
				
			}		
		}
		
		if (isFleeing && !rotateFlee && !inHerd && isAlpha == 0 || isFleeing && !rotateFlee && isAlpha == 1)
		{
			rotateTimer += Time.deltaTime;
			
			if (!isDead && rotateTimer <= 1f && navMeshAgent.enabled)
			{
				Flee();
			}
			
			if (rotateTimer > 1f)
			{
				rotateFlee = true;
			}
		}
		
		//If our animal is playing its run animation, and useDustEffect is enabled, set the particles to 10
		if (useAnimations)
		{
			if (anim.IsPlaying(runAnimation.name) && useDustEffect)
			{
				clone.emissionRate = 10;
			}
		}

		//If our animal is not playing its run animation, and useDustEffect is enabled, set the particles to 0
		if (useAnimations)
		{
			if (!anim.IsPlaying(runAnimation.name) && useDustEffect || useDustEffect && waitingForHerd || velocity <= 0.05f && useDustEffect)
			{
				clone.emissionRate = 0;
			}
		}

		if (!isDead && navMeshAgent.enabled == true)
		{
			if (navMeshAgent.remainingDistance >= navMeshAgent.stoppingDistance && !isFleeing && !isCoolingDown && preyOrPredator == 1 || navMeshAgent.remainingDistance >= navMeshAgent.stoppingDistance && !huntMode && !isCoolingDown && preyOrPredator == 2)
			{
				if (useAnimations)
				{
					anim.CrossFade(walkAnimation.name);
				}
				
				isGrazing = false;
			}
		}


		//If the proper conditions are met, play our running sounds for Animals. 
		if (isFleeing && !isGrazing && systemOn && !isExhausted && velocity > 0.05f && preyOrPredator == 1 || huntMode && !isGrazing && systemOn && !isCoolingDown && velocity > 0.5f && preyOrPredator == 2 && AIType == 0) 
		{
			if (useAnimations && !withinAttackDistance && !attackWhileRunning)
			{
				if (useRunAttackAnimations && !anim.IsPlaying(runAttackAnimation.name) && !isTurning)
				{
					anim.CrossFade(runAnimation.name);
				}
				
				if (anim.IsPlaying(runAnimation.name) && useRunSound)
				{
					runTimer += Time.deltaTime;
					
					if (runTimer >= footStepSeconds && systemOn)
					{
						audioSource.PlayOneShot(runSound);
						runTimer = 0;
					}
				}
			}

			if (!isTurning)
			{
				anim.CrossFade(runAnimation.name);
				navMeshAgent.speed = runSpeed;
			}
		}


		//If the proper conditions are met, play our running sounds for NPCs.
		if (isFleeing && !isGrazing && systemOn && !isExhausted && velocity > 0.05f && preyOrPredator == 1 || huntMode && !isGrazing && systemOn && !isCoolingDown && velocity > 0.5f && preyOrPredator == 2 && AIType == 1) 
		{
			if (useAnimations && !withinAttackDistance && !attackWhileRunning)
			{
				if (useRunAttackAnimations && !anim.IsPlaying(runAttackAnimation.name))
				{
					anim.CrossFade(runAnimation.name);
				}

				if (runAttackAnimation != null && !anim.IsPlaying(runAttackAnimation.name))
				{
					anim.CrossFade(runAnimation.name);
				}
				
				if (anim.IsPlaying(runAnimation.name) && useRunSound)
				{
					runTimer += Time.deltaTime;
					
					if (runTimer >= footStepSeconds && systemOn)
					{
						audioSource.PlayOneShot(runSound);
						runTimer = 0;
					}
				}
			}

			navMeshAgent.speed = runSpeed;
		}

		if (useAnimations)
		{
			//If the proper conditions are met, play our walk sounds for NPCs.
			if (AIType == 1 && !isFleeing && anim.IsPlaying(walkAnimation.name) && useWalkSound)
			{
				runTimer += Time.deltaTime;
				
				if (runTimer >= footStepSecondsWalk && systemOn)
				{
					audioSource.PlayOneShot(walkSound);
					runTimer = 0;
				}
			}
		}

		//If wanderinf, play our walk animation.
		if (!isFleeing && !isGrazing && !huntMode && !isCoolingDown && !isDead)
		{
			if (useAnimations)
			{
				anim.CrossFade(walkAnimation.name);
			}
			
			navMeshAgent.speed = walkSpeed;
		}

		//Draw our current waypoint path, if drawPaths is enabled.
		if (drawPaths)
		{
			NavMeshPath path = new NavMeshPath();
			navMeshAgent.CalculatePath(currentWaypoint.transform.position, path);
			GetPath();
		}

		//If an animal is using the Time based flee type, flee until the time is met. Once time is reached, the animal is exhausted.
		if (startFleeTimer && threatIsOutOfTigger && AIType == 0 && fleeType == 1)
		{
			fleeTimer -= Time.deltaTime;
			
			if (fleeTimer <= 0)
			{
				fleeTarget = null;
				isExhausted = true;
				isFleeing = false;
				calculateFlee = false;
				currentlyBeingPursued = false;
				threatIsOutOfTigger = false;
				startFleeTimer = false;
				distantFlee = false;
			}
		}

		//If an AI is using the Distance based flee type, flee until out of range of the chaser. Once out of range is reached, wander.
		if (isFleeing && fleeType == 0 && fleeTarget != null && threatIsOutOfTigger && !inHerd)
		{
			float distance = Vector3.Distance (this.transform.position, fleeTarget.transform.position);

			if (distance > maxFleeDistance)
			{
				fleeTarget = null;
				isFleeing = false;
				calculateFlee = false;
				currentlyBeingPursued = false;
				threatIsOutOfTigger = false;
				startFleeTimer = false;
				distantFlee = false;
			}
		}

		//Once an AI's huntTimer has reached 0, stop HuntMode and wander.
		if (startHuntTimer && !withinAttackDistance)
		{
			huntTimer -= Time.deltaTime;
			
			if (huntTimer <= 0)
			{
				if (isAlpha == 0 && !inHerd || isAlpha == 1)
				{
					//If returnsToStart is enabled, the AI will return back to its starting position and wander.
					if (returnsToStart)
					{
						ReturnBackToStartingPoint();
					}

					//If returnsToStart is disabled, the AI will wander at the position of when its HuntMode was disabled
					if (!returnsToStart)
					{
						preySizeMatched = false;
						startHuntTimer = false;
						Wander();
					}
				}

				//If the AI is in a herd, continue to follow their Alpha
				if (isAlpha == 0 && inHerd)
				{
					huntMode = false;
					preySizeMatched = false;
					startHuntTimer = false;
					isCoolingDown = true;
					huntTimer = huntSeconds;
					FollowAlpha();
				}
			}
		}
		
		//This is a cool down system that happens after our animal has reached its max chase seconds and has unsuccessfully caught its prey
		if (isCoolingDown)
		{
			coolDownTimer += Time.deltaTime;
			navMeshAgent.speed = runSpeed + 2;

			if (returnBackToStartingPointProtection && returnsToStart)
			{
				currentHealth = startingHealth;
			}
			
			if (useAnimations)
			{
				anim.CrossFade(runAnimation.name);
			}
			
			if (Vector3.Distance(navMeshAgent.destination, navMeshAgent.transform.position) < navMeshAgent.stoppingDistance)
			{
				isCoolingDown = false;
			}
		}
		
		//This is a cool down system that happens after our animal has reached its max chase seconds and unsuccessfully escaped its predator
		if (isExhausted && !isDead)
		{
			coolDownTimer += Time.deltaTime;
			
			if (inHerd && isAlpha == 0)
			{
				NewDestination(new Vector3 (animalToFollow.localPosition.x + offSetHerdX + 5, animalToFollow.localPosition.y, animalToFollow.localPosition.z - offSetHerdZ));
			}
			
			if (useAnimations)
			{
				anim.CrossFade(walkAnimation.name);
			}
			
			navMeshAgent.speed = walkSpeed;
			
			if (coolDownTimer >= coolDownSeconds)
			{
				isExhausted = false;
				coolDownTimer = 0;
			}
		}

		//If our current target is lost, refresh our trigger collider so our PickNewTarget function can update
		if (currentTargetSystem != null && isPlayer == false)
		{
			if (AIType == 1 && huntMode && !currentTargetSystem.enabled)
			{
				currentAnimal = null;
				triggerCollider.enabled = false;
				triggerCollider.enabled = true;
			}
		}

		//If huntMode is enabled, but our target is lost or killed, look for new targets or wander
		if (huntMode && currentAnimal == null)			
		{
			targetInRange = false;
			triggerCollider.enabled = false;
			triggerCollider.enabled = true;

			//If there are no detected targets in range, disable HuntMode and wander.
			if (!targetInRange)
			{
				huntMode = false;
				damageDealt = false;
				withinAttackDistance = false;
				attackWhileRunning = false;
				startHuntTimer = false;
				navMeshAgent.speed = walkSpeed;
				Wander();
			}

			//If there are targets detected in range, find the nearest one by calling PickNewTarget
			if (targetInRange)
			{
				isPlayer = false;
				PickNewTarget();
			}

			if (AIType == 0)
			{
				huntMode = false;
				withinAttackDistance = false;
				attackWhileRunning = false;
				startHuntTimer = false;
				navMeshAgent.speed = walkSpeed;

				triggerCollider.enabled = false;
				triggerCollider.enabled = true;
				
				if (!inHerd || isAlpha == 1)
				{
					if (!isDead)
					{
						Wander();
					}
				}
				
				if (inHerd && animalToFollow != null && navMeshAgent.enabled)
				{
					if (!isDead)
					{ 
						FollowAlpha();
					}
				}
			}
		}


		//Calculate our animations so they aren't overlapping.
		if (huntMode && currentAnimal != null && !isCoolingDown && !isDead)
		{
			if (useAnimations && Vector3.Distance (destination, currentAnimal.transform.position) > 1.0f && velocity > 0.5f)
			{
				anim.CrossFade(runAnimation.name);
			}

			if (useHitAnimation)
			{
				if (useAnimations && currentAttackAnimationClip != null && !anim.IsPlaying(hitAnimation.name) && !anim.IsPlaying(currentAttackAnimationClip.name) && Vector3.Distance (destination, currentAnimal.transform.position) < 1.0f && velocity < 0.5f)
				{
					anim.CrossFade(idleBattleAnimation.name);
				}
			}

			if (!useHitAnimation)
			{
				if (useAnimations && currentAttackAnimationClip != null && !anim.IsPlaying(currentAttackAnimationClip.name) && Vector3.Distance (destination, currentAnimal.transform.position) < 1.0f && velocity < 0.5f)
				{
					anim.CrossFade(idleBattleAnimation.name);
				}
			}

			if (Vector3.Distance (destination, currentAnimal.transform.position) > 1.0) 
			{
				destination = currentAnimal.transform.position;
				navMeshAgent.destination = destination;
				withinAttackDistance = false;
			}

			if (Vector3.Distance(navMeshAgent.destination, navMeshAgent.transform.position) < navMeshAgent.stoppingDistance)
			{
				withinAttackDistance = true;
			}
	
			/*
			if (attackWhileRunning && withinAttackDistance && AIType == 0)
			{
				//AttackWhileRunning();
			}
			*/
			
			//Attack while stationary
			if (withinAttackDistance && !attackWhileRunning && currentAnimal != null && withinAttackDistance) 
			{
				AttackWhileStationary();
			}
			
		}
		
		//What happens when our animal's health reaches 0
		if (currentHealth <= 0)
		{
			//Disable unneeded components and change our tag so the AI is no longer a target
			navMeshAgent.enabled = false;
			this.gameObject.tag = "Untagged";
			triggerCollider.gameObject.tag = "Untagged";
			triggerCollider.enabled = false;
			boxCollider.enabled = false;

			huntMode = false;
			attackTimer = 0;
			isGrazing = false;
			isFleeing = false;
			
			if (isAlpha == 1 && herdList.Count >= 1 && !isDead)
			{
				herdNumber = Random.Range(0, herdList.Count);						//If our alpha dies, and it's in a herd, assign alpha status to random memeber in herd and remove alpha from List.
				
				if (herdList[herdNumber].GetComponent<Emerald_Animal_AI>() != null)
				{
					herdList[herdNumber].GetComponent<Emerald_Animal_AI>().isAlpha = 1;
					herdList[herdNumber].GetComponent<Emerald_Animal_AI>().herdList = herdList;
					herdList[herdNumber].GetComponent<Emerald_Animal_AI>().AssignAlphaStatus();
					
					herdList.Remove(herdList[herdNumber]);
				}
				
				isDead = true;
			}

			if (useDeadReplacement)
			{
				Instantiate(deadObject, transform.position, transform.rotation);	//If the AI is out of health, instantiate its deadObject replacement.
				Destroy(this.gameObject);											//Destroy the current AI to instaniate the deadObject replacement.
			}

			//When dying, we only want the code to be called once.
			if (!useDeadReplacement)
			{
				if (!deathTrigger)
				{
					if (useDieSound)
					{
						audioSource.PlayOneShot(dieSound);
					}

					if (GetComponent<EmeraldLootSystem>() != null)
					{
						GetComponent<EmeraldLootSystem>().GenerateLoot();
					}

					//Remove ourselves as a target from attacker and refresh their trigger collider to help pick a new one. 
					if (!isPlayer && fleeTarget != null && aggression <= 2)
					{
						fleeTarget.gameObject.GetComponent<Emerald_Animal_AI>().targetInRange = false;
						fleeTarget.GetComponent<Emerald_Animal_AI>().huntMode = false;
						fleeTarget.GetComponent<Emerald_Animal_AI>().startHuntTimer = false;
						fleeTarget.GetComponent<Emerald_Animal_AI>().Wander();
						fleeTarget.gameObject.GetComponent<Emerald_Animal_AI>().currentAnimal = null;
						fleeTarget.gameObject.GetComponent<Emerald_Animal_AI>().currentTargetSystem = null;

						fleeTarget.GetComponent<Emerald_Animal_AI>().triggerCollider.enabled = false;
						fleeTarget.GetComponent<Emerald_Animal_AI>().triggerCollider.enabled = true;
					}

					deathTrigger = true;
				}

				if (useDustEffect)
				{
					clone.enableEmission = false;
				}

				if (useAnimations && !deathAnimationFinished)
				{
					deathAnimationTimer += Time.deltaTime;
					anim.CrossFade(deathAnimation.name);

					if (deathAnimationTimer >= deathAnimation.length)
					{
						deathAnimationFinished = true;
						anim.enabled = false;
					}
				}
		
				isDead = true;
				huntMode = false;
				attackTimer = 0;
				isGrazing = false;
				isFleeing = false;

				deathTimer += Time.deltaTime;

				//If using die sound, wait to deactivate until the die sound has played.
				if (dieSound != null)
				{
					if (useDieSound && deathTimer >= dieSound.length)
					{
						audioSource.enabled = false;
						GetComponent<Emerald_Animal_AI>().enabled = false; 
					}
				}

				//If not using a die sound, disable after 2 seconds to ensure everyrthing has been deactivated.
				if (!useDieSound && deathTimer >= 2)
				{
					audioSource.enabled = false;
					GetComponent<Emerald_Animal_AI>().enabled = false; 
				}
			}
		}
		
		if (huntMode && navMeshAgent != null && navMeshAgent.enabled && currentAnimal != null)
		{
			HuntMode();			//Call the HuntMode function, if huntMode is enabled.
		}
		
		if (isFrozen)
		{
			Frozen();			//Call the Frozen function, if isFrozen is enabled and all conditions are met
		}
		
		if (animalToFollow == null)
		{
			inHerd = false;
		}
	}


	
	void Update () 
	{
		//Checks to see if our AI are still in view every few seconds.
		SystemOptimizerUpdater ();						//If they are no longer in view, according to Unity's LOD System, disable all components until back in view.
		//Using the NavMesh timer, check to see that our AI are off screen for at least 15 seconds before deactivating.
		//If the AI are out of view, set systemOn to false, which stops Emerald from calculating


		//If systemOn is true, run the Main Emerald System
		if (systemOn)
		{
			MainSystem ();
			CheckSystem();
			
			if (alignAI)
			{
				AlignNavMesh();
			}

			if (calculateFlee && fleeTarget != null && isAlpha == 1 || calculateFlee && fleeTarget != null && !inHerd)
			{
				if (!isDead && navMeshAgent.enabled)
				{
					Flee();
				}
			}
			
			if (inHerd && isAlpha == 0 && animalToFollow != null && navMeshAgent != null && isFleeing || inHerd && isAlpha == 0 && animalToFollow != null && navMeshAgent != null && preyOrPredator == 2 && !huntMode)
			{
				if (!isDead && navMeshAgent.enabled)
				{
					FollowAlpha();
				}
			}
			
			if (herdList.Count == maxPackSize)
			{
				herdFull = true;
			}
		}
	}
	
	//If an alpha is assigned, follow it with various offsets so that each animal has their own space
	public void FollowAlpha ()
	{
		followAlphaTimer += Time.deltaTime;

		if (!isTurning && followAlphaTimer >= 1)
		{
			NewDestination(new Vector3 (animalToFollow.position.x, animalToFollow.position.y, animalToFollow.position.z - offSetHerdZ));
			followAlphaTimer = 0;
		}

		if (useAnimations && velocity > 45 && !isTurning)
		{
			anim.CrossFade(runAnimation.name);
			navMeshAgent.speed = runSpeed;
		}
		
		if (!useAnimations)
		{
			navMeshAgent.speed = runSpeed;
		}

		if (useAnimations && !withinAttackDistance)
		{
			velocity = navMeshAgent.velocity.sqrMagnitude;
			
			if (velocity <= 0.05f && anim.IsPlaying(runAnimation.name))
			{
				anim.CrossFade(idleAnimation.name);
			}
		}
	}
	
	//Calculates our aniaml's alignment to the terrain
	public void AlignNavMesh ()
	{
		Vector3 normal = CalculateRotation();
		Vector3 direction = navMeshAgent.steeringTarget - transform.position;
		direction.y = 0.0f;

		navMeshAgent.updateRotation = false;

		if(direction.magnitude > 0.1f && normal.magnitude > 0.1f) 
		{
			Quaternion quaternionLook = Quaternion.LookRotation(direction, Vector3.up);
			Quaternion quaternionNormal = Quaternion.FromToRotation(Vector3.up, normal);
			originalLookRotation = quaternionNormal * quaternionLook;
		}

		//Calculate our AI's angle so we can use it below.
		angle = Quaternion.Angle(transform.rotation, originalLookRotation);

		//Stop movement if our angle is greater than 50 degrees. This is to ensure that our AI have time to rotate before running. It also stops AI from running in place.
		//If our AI's angle is greater than 50 degrees, play our turn animation, if enabled.
		//Smaller and Medium animals turn faster than larger animals. So, these speeds are higher than the larger AI.
		if (preySize <= 2 || predatorSize <= 2)
		{
			if (angle < 5)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, originalLookRotation, Time.deltaTime*3.5f);
				
				if (isFleeing)
				{
					navMeshAgent.speed = runSpeed;
				}
				
				if (!isFleeing)
				{
					navMeshAgent.speed = walkSpeed;
				}
				
				isTurning = false;
			}
			
			if (angle > 5 && angle < 50)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, originalLookRotation, Time.deltaTime*2f);
				
				if (isFleeing)
				{
					navMeshAgent.speed = runSpeed;
				}
				
				if (!isFleeing)
				{
					navMeshAgent.speed = walkSpeed;
				}
				
				isTurning = false;
			}
			
			if (angle > 50)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, originalLookRotation, Time.deltaTime*3.5f);
				
				navMeshAgent.speed = 0;
				
				if (useTurnAnimation)
				{
					anim.CrossFade(turnAnimation.name);
					isTurning = true;
				}
			}

		}

		//Stop movement if our angle is greater than 50 degrees. This is to ensure that our AI have time to rotate before running. It also stops AI from running in place.
		//If our AI's angle is greater than 50 degrees, play our turn animation, if enabled.
		//Larger animals turn slower than small or medium AI.
		//For Predators/Enemies
		if (predatorSize == 3 && preyOrPredator == 2)
		{
			if (angle < 5)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, originalLookRotation, Time.deltaTime*3f);

				if (huntMode)
				{
					navMeshAgent.speed = runSpeed;
				}
				
				if (!huntMode)
				{
					navMeshAgent.speed = walkSpeed;
				}

				isTurning = false;
			}
			
			if (angle > 5 && angle < 50)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, originalLookRotation, Time.deltaTime*2f);

				if (huntMode)
				{
					navMeshAgent.speed = runSpeed;
				}
				
				if (!huntMode)
				{
					navMeshAgent.speed = walkSpeed;
				}

				isTurning = false;
			}
			
			if (angle > 50)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, originalLookRotation, Time.deltaTime*3.5f);

				navMeshAgent.speed = 0;

				if (useTurnAnimation)
				{
					anim.CrossFade(turnAnimation.name);
					isTurning = true;
				}
			}
		}

		//Stop movement if our angle is greater than 50 degrees. This is to ensure that our AI have time to rotate before running. It also stops AI from running in place.
		//If our AI's angle is greater than 50 degrees, play our turn animation, if enabled.
		//Larger animals turn slower than small or medium AI.
		//For Prey/NPCs
		if (preySize == 3 && preyOrPredator == 1)
		{
			if (angle < 5)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, originalLookRotation, Time.deltaTime*2.25f);
				
				if (isFleeing)
				{
					navMeshAgent.speed = runSpeed;
				}
				
				if (!isFleeing)
				{
					navMeshAgent.speed = walkSpeed;
				}

				isTurning = false;
			}
			
			if (angle > 5 && angle < 50)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, originalLookRotation, Time.deltaTime*1.1f);

				if (isFleeing)
				{
					navMeshAgent.speed = runSpeed;
				}

				if (!isFleeing)
				{
					navMeshAgent.speed = walkSpeed;
				}

				isTurning = false;
			}
			
			if (angle > 50)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, originalLookRotation, Time.deltaTime*1.5f);
				
				navMeshAgent.speed = 0;
				
				if (useTurnAnimation)
				{
					anim.CrossFade(turnAnimation.name);
					isTurning = true;
				}
			}
		}
	}

	//Find the angle of the terrain
	Vector3 CalculateRotation () 
	{
		Vector3 terrainLocalPos = transform.position - terrain.transform.position;
		Vector2 normalizedPos = new Vector2(terrainLocalPos.x / terrain.terrainData.size.x, terrainLocalPos.z / terrain.terrainData.size.z);
		return terrain.terrainData.GetInterpolatedNormal(normalizedPos.x, normalizedPos.y);
	}
	
	public void CheckSystem()
	{
		//For some reason, if our trigger to start our flee timer was missed, set it to true
		if (threatIsOutOfTigger && isFleeing && !startFleeTimer && fleeType == 1)
		{
			startFleeTimer = true;
		}
		
		if (alphaWaitForHerd && isFleeing && isAlpha == 1 && hasPack && herdList[0] != null)
		{
			float distance = Vector3.Distance (this.transform.position, herdList[0].transform.position);
			
			if (distance >= maxDistanceFromHerd)
			{
				navMeshAgent.speed = 0;
				waitingForHerd = true;
			}
			
			if (distance < maxDistanceFromHerd)
			{
				navMeshAgent.speed = runSpeed;
				waitingForHerd = false;
			}
		}
	}

	//Assign Alpha Status if our Alpha dies.
	public void AssignAlphaStatus ()
	{
		foreach (GameObject G in herdList) 
		{
			if (G != null)
			{
				G.GetComponent<Emerald_Animal_AI>().animalToFollow = gameObject.transform;
			}
		}
	}
	
	//Frozen handles the freeze system for when a prey stops for a random amount of
	//seconds to simulate a stunned or cautious action. Each time this is trigger, it is recalculated
	public void Frozen ()
	{
		freezeSecondsTimer += Time.deltaTime;
		
		if (!isDead && freezeSecondsTimer >= freezeSecondsTotal && navMeshAgent.enabled)
		{
			isFleeing = true;
			currentlyBeingPursued = true;
			Flee();
			freezeSecondsTimer = 0;
			isFrozen = false;
		}
	}
	

	//Allows our AI to return to its starting position, if enabled.
	public void ReturnBackToStartingPoint ()
	{
		NewDestination(startPosition);
		huntMode = false;
		currentlyBeingPursued = false;
		startHuntTimer = false;
		isCoolingDown = true;
		preySizeMatched = false;
		navMeshAgent.speed = runSpeed + 4;
		huntTimer = huntSeconds;

		//Remove our target from the AI if they return to their starting position. This can be commented out if desired.
		currentAnimal = null;
		currentTargetSystem = null;

		//Update trigger collider to make sure no enemies are within range
		triggerCollider.enabled = false;
		triggerCollider.enabled = false;
		
		if (useAnimations)
		{
			anim.CrossFade(runAnimation.name);
		}
	}

	public void SendTarget (GameObject targetToSend)
	{
		currentAnimal = targetToSend;
	}
	
	//Call this function if you want to damage this animal from an external script.
	public void Damage (int damageReceived)
	{
		if (currentHealth >= 1)
		{
			if (currentTargetSystem != null && currentTargetSystem.aggression > 2)
			{
				if (autoCalculateDelaySeconds)
				{
					attackDelaySeconds = currentTargetSystem.currentAttackAnimationClip.length/2 - 0.25f;
				}
			}

			if (aggression > 2 && !withinAttackDistance)
			{
				PickNewTarget ();
			}

			if (AIType == 1 && aggression == 0)
			{
				aggression = 1;
				triggerCollider.enabled = false;
				triggerCollider.enabled = true;
			}

			StartCoroutine(Delay());
			tempDamage = damageReceived;

		}
	}

	//Call this function if you want to damage this animal as a player.
	public void DamageFromPlayer (int damageReceived)
	{
		if (currentHealth >= 1)
		{
			//Subtract our damage that we received from our attacker.
			currentHealth -= damageReceived;

			if (useAnimations && currentAttackAnimationClip == null && aggression > 2)
			{
				Debug.Log("Your AI is set to use animations, but there is not an attack animation applied to it. Please see the Animation Options and apply an attack animation.");
			}

			//If using hit animations, play our hit animation when the AI receives damage.
			if (currentAttackAnimationClip != null)
			{
				if (aggression > 2 && !anim.IsPlaying(currentAttackAnimationClip.name) && useAnimations)
				{
					anim.CrossFade(hitAnimation.name);
				}
			}

			//Randomize our sound pitch
			audioSource.pitch = Random.Range(minSoundPitch, maxSoundPitch);

			//If using hit effect, spawn our hit effect when the AI receives damage.
			if (currentAnimal != null)
			{
				if (currentAnimal.gameObject.tag == enemyTagName && useBlood && bloodEffect != null)
				{
					Instantiate(bloodEffect, new Vector3(transform.position.x + (int)Random.Range (-1,1), transform.position.y + (int)Random.Range (1,2), transform.position.z - 1), transform.rotation);
				}
			}

			//If using hit effect, spawn our hit effect when the AI receives damage.
			if (aggression == 2)
			{
				if (useBlood && bloodEffect != null)
				{
					Instantiate(bloodEffect, new Vector3(transform.position.x + (int)Random.Range (-1,1), transform.position.y + (int)Random.Range (1,2), transform.position.z - 1), transform.rotation);
				}
			}

			//If using hit sounds, play our hit sound when the AI receives damage.
			if (useHitSound)
			{
				if (!audioSource.isPlaying)
				{
					audioSource.PlayOneShot(getHitSounds[Random.Range(0,getHitSounds.Count)]);
				}
			}

			//If a defensive NPC is hit by the player, attack the player
			if (AIType == 1 && aggression == 4)
			{
				currentAttackAnimation = Random.Range(1, totalAttackAnimations+1);
				aggression = 3;

				triggerCollider.enabled = false;
				triggerCollider.enabled = true;
			}

			//If a coward NPC is hit by the player, flee from the player
			if (AIType == 1 && aggression == 0)
			{
				aggression = 1;
				triggerCollider.enabled = false;
				triggerCollider.enabled = true;
			}

		}

	}

	//Delay our hits based on our attackDelaySeconds to allow for proper timing
	IEnumerator Delay() {

		yield return new WaitForSeconds(attackDelaySeconds);

		//If using hit animations, play our hit animation when the AI receives damage.
		if (useHitAnimation)
		{
			if (aggression > 2 && !anim.IsPlaying(currentAttackAnimationClip.name) && useAnimations && !isDead)
			{
				anim.CrossFade(hitAnimation.name);
			}

			if (aggression <= 2 && useAnimations && !isDead)
			{
				anim.CrossFade(hitAnimation.name);
			}
		}

		//Subtract our damage that we received from our attacker.
		currentHealth -= tempDamage;

		//Randomize our sound pitch
		audioSource.pitch = Random.Range(minSoundPitch, maxSoundPitch);

		//If using hit effect, spawn our hit effect when the AI receives damage.
		if (currentAnimal != null)
		{
			if (currentAnimal.gameObject.tag == enemyTagName && useBlood)
			{
				Instantiate(bloodEffect, new Vector3(transform.position.x + (int)Random.Range (-2,2), transform.position.y + (int)Random.Range (1,4), transform.position.z + 0.5f), transform.rotation);
			}
		}

		//If using hit sounds, play our hit sound when the AI receives damage.
		if (useHitSound)
		{
			if (!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(getHitSounds[Random.Range(0,getHitSounds.Count)]);
			}
		}
	}
	
	//This handles how a predator/NPC hunts. It will only stop to attack if its velocity is around a stationary speed. 
	void HuntMode ()
	{
		updateSpeedTimer += Time.deltaTime;
		isGrazing = false;

		float distance = Vector3.Distance (this.transform.position, currentAnimal.transform.position);

		if (distance > maxChaseDistance)
		{
			if (!targetInRange)
			{
				currentAnimal = null;
				huntMode = false;
				damageDealt = false;
				withinAttackDistance = false;
				attackWhileRunning = false;
				startHuntTimer = false;
				navMeshAgent.speed = walkSpeed;
				Wander();
			}

			if (targetInRange)
			{
				isPlayer = false;
				PickNewTarget();
			}
		}
		
		
		//If our velocity drops near 0, play our idle animation so the animal isn't running in place
		if (useAnimations && !withinAttackDistance)
		{
			velocity = navMeshAgent.velocity.sqrMagnitude;
			
			if (velocity <= 0.05f && anim.IsPlaying(runAnimation.name))
			{
				anim.CrossFade(idleBattleAnimation.name);
			}
		}
		
		if (updateSpeedTimer >= updateSpeed)
		{
			velocity = navMeshAgent.velocity.sqrMagnitude;
			updateSpeedTimer = 0;
		}

		if (velocity >= 0.1f && navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && AIType == 0)  
		{
			attackWhileRunning = true;
		}

		if (velocity < 0.1f || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) 
		{
			attackWhileRunning = false; 
		}

		if (currentAnimal != null)
		{
			if (Vector3.Distance (destination, currentAnimal.transform.position) > 1.0f && useAnimations && !withinAttackDistance )
			{
				if (!anim.IsPlaying(runAttackAnimation.name) || !useRunAttackAnimations)
				{
					anim.CrossFade(runAnimation.name); 
					navMeshAgent.speed = runSpeed; 
				}
			}
		}
	}
	
	//If our AI is within attacking distance, use this function to update its rotations
	//so that it is always facing the player.
	private void RotateTowards (Transform currentPlayer) 
	{
		Vector3 direction = (currentAnimal.transform.position - transform.position).normalized;
		lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 8);

		if (alignAI)
		{
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
		}

		if (!alignAI)
		{
			transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
			angle = Quaternion.Angle(transform.rotation, lookRotation);

			if (angle < 5)
			{	
				if (huntMode)
				{
					navMeshAgent.speed = runSpeed;
				}
				
				if (!huntMode)
				{
					navMeshAgent.speed = walkSpeed;
				}
				
				isTurning = false;
			}
			
			if (angle > 5 && angle < 50)
			{			
				if (huntMode)
				{
					navMeshAgent.speed = runSpeed;
				}
				
				if (!huntMode)
				{
					navMeshAgent.speed = walkSpeed;
				}
				
				isTurning = false;
			}
			
			if (angle > 50)
			{			
				navMeshAgent.speed = 0;
				
				if (useTurnAnimation)
				{
					anim.CrossFade(turnAnimation.name);
					isTurning = true;
				}
			}
		}
		
		if (drawWaypoints)
		{
			currentWaypoint.transform.position = currentAnimal.transform.position;
		}
	}

	//Apply our attack animations
	void ApplyAttackAnimations()
	{
		if (preyOrPredator == 2)
		{
			if (currentAttackAnimation == 1)
			{
				currentAttackAnimationClip = attackAnimation1;
			}
			
			if (currentAttackAnimation == 2)
			{
				currentAttackAnimationClip = attackAnimation2;
			}
			
			if (currentAttackAnimation == 3)
			{
				currentAttackAnimationClip = attackAnimation3;
			}
			
			if (currentAttackAnimation == 4)
			{
				currentAttackAnimationClip = attackAnimation4;
			}
			
			if (currentAttackAnimation == 5)
			{
				currentAttackAnimationClip = attackAnimation5;
			}
			
			if (currentAttackAnimation == 6)
			{
				currentAttackAnimationClip = attackAnimation6;
			}
		}
	}

	//Attack while stopped
	void AttackWhileStationary ()
	{
		if (useAnimations && !alignAI)
		{
			if (currentAnimal != null && withinAttackDistance)
			{
				RotateTowards(currentAnimal.transform);
			}
		}
		
		if (!useAnimations && !alignAI)
		{
			if (currentAnimal != null && withinAttackDistance)
			{
				RotateTowards(currentAnimal.transform);
			}
		}
		
		if (drawWaypoints)
		{
			currentWaypoint.transform.position = currentAnimal.transform.position;
		}
		
		attackTimer += Time.deltaTime;
		

		if (attackTimer >= attackTime)
		{
			if (useAnimations)
			{
				if (!attackWhileRunning)
				{
					anim[currentAttackAnimationClip.name].speed = 1 * attackAnimationSpeedMultiplier;

					if (currentAttackAnimation == 1)
					{
						currentAttackAnimationClip= attackAnimation1;
						anim.CrossFade(attackAnimation1.name);
					}
					
					if (currentAttackAnimation == 2)
					{
						
						currentAttackAnimationClip = attackAnimation2;
						anim.CrossFade(attackAnimation2.name);
					}
					
					if (currentAttackAnimation == 3)
					{
						
						currentAttackAnimationClip = attackAnimation3;
						anim.CrossFade(attackAnimation3.name);
					}
					
					if (currentAttackAnimation == 4)
					{
						
						currentAttackAnimationClip = attackAnimation4;
						anim.CrossFade(attackAnimation4.name);
					}
					
					if (currentAttackAnimation == 5)
					{
						
						currentAttackAnimationClip = attackAnimation5;
						anim.CrossFade(attackAnimation5.name);
					}
					
					if (currentAttackAnimation == 6)
					{
						
						currentAttackAnimationClip = attackAnimation6;
						anim.CrossFade(attackAnimation6.name);
					}
					
					if (!damageDealt)
					{
						//Attack other AI
						if (currentAnimal.gameObject.tag == enemyTagName || currentAnimal.gameObject.tag == preyTagName)
						{
							if (!isPlayer && currentAnimal != null)
							{
								if (currentAnimal.GetComponent<Emerald_Animal_AI>().currentAnimal == null && currentAnimal.GetComponent<Emerald_Animal_AI>().aggression == 4)
								{
									currentAnimal.GetComponent<Emerald_Animal_AI>().currentAnimal = this.gameObject;
									currentAnimal.GetComponent<Emerald_Animal_AI>().currentTargetSystem = this.gameObject.GetComponent<Emerald_Animal_AI>();
									currentAnimal.GetComponent<Emerald_Animal_AI>().currentAttackAnimation = Random.Range(1, totalAttackAnimations+1);
									currentAnimal.GetComponent<Emerald_Animal_AI>().aggression = 3;
									currentAnimal.GetComponent<Emerald_Animal_AI>().huntMode = true;
								}

								if (useAttackSound)
								{
									audioSource.PlayOneShot(attackSounds[Random.Range(0,attackSounds.Count)]);
								}

								if (useWeaponSound)
								{
									audioSource.PlayOneShot(weaponSound);
								}


								if (currentTargetSystem == null && currentAnimal != null)
								{
									currentTargetSystem = currentAnimal.gameObject.GetComponent<Emerald_Animal_AI>();
								}

								if (currentTargetSystem == null && currentAnimal == null)
								{
									triggerCollider.enabled = false;
									triggerCollider.enabled = true;
								}

								//If NPC is cowardly, and the attacker is not a player, set the flee target to its current attacker
								if (currentAnimal.GetComponent<Emerald_Animal_AI>().aggression == 1 && !isExhausted)
								{
									currentAnimal.GetComponent<Emerald_Animal_AI>().isPlayer = false;
									currentAnimal.GetComponent<Emerald_Animal_AI>().isGrazing = false;
									currentAnimal.GetComponent<Emerald_Animal_AI>().calculateFlee = true;
									currentAnimal.GetComponent<Emerald_Animal_AI>().fleeTarget = this.gameObject;
									currentAnimal.GetComponent<Emerald_Animal_AI>().isFleeing = true;
								}

								attackDamage = Random.Range(attackDamageMin, attackDamageMax);
								currentTargetSystem.Damage(attackDamage);
							}

							damageDealt = true;
						}

						//Attack Player
						if (currentAnimal.gameObject.tag == playerTagName)
						{
							if (isPlayer)
							{
								if (currentAnimal == null)
								{
									triggerCollider.enabled = false;
									triggerCollider.enabled = true;
								}
								
								if (useAttackSound)
								{
									audioSource.PlayOneShot(attackSounds[Random.Range(0,attackSounds.Count)]);
								}
								
								if (useWeaponSound)
								{
									audioSource.PlayOneShot(weaponSound);
								}
								
								attackDamage = Random.Range(attackDamageMin, attackDamageMax);
								
								if (currentAnimal.GetComponent<PlayerHealth>() != null)
								{
									currentAnimal.GetComponent<PlayerHealth>().DamagePlayer(attackDamage);
								}
								
								if (currentAnimal.GetComponent<PlayerHealth>() == null)
								{
									Debug.LogError("You are receiving this error because you need to assign a PlayerHealth script to your player object.");
								}
								
								//If you would like your AI to damage the UFPS player damage script, uncomment the code below by removing the // and comment out the currentAnimal.GetComponent<PlayerHealth>() portions using //
								//currentAnimal.GetComponent<vp_FPPlayerDamageHandler>().Damage(attackDamage);
							}
							
							damageDealt = true;
						}	
					}
				}
			}
			
			if (!useAnimations)
			{
				if (!attackWhileRunning)
				{
					if (!damageDealt)
					{
						if (useAttackSound)
						{
							if (useWeaponSound)
							{
								audioSource.PlayOneShot(weaponSound);
							}

							audioSource.PlayOneShot(attackSounds[Random.Range(0,attackSounds.Count)]);
						}
						
						//Attack Prey
						if (currentAnimal.gameObject.tag == enemyTagName || currentAnimal.gameObject.tag == preyTagName)
						{
							currentAnimal.GetComponent<Emerald_Animal_AI>().currentHealth -= attackDamage;
						}
						
						//Attack Player
						if (currentAnimal.gameObject.tag == playerTagName && aggression == 3)
						{
							currentAnimal.GetComponent<PlayerHealth>().DamagePlayer(attackDamage);
							
							//Uncomment if you would like to add a hit effect (Ex: UFPS shake effect)
							//Instantiate(hitEffect, this.transform.position, this.transform.rotation);
						}
						
						damageDealt = true;
						
					}
				}
			}
		}
		
		if (useAnimations && attackTimer >= attackTime + currentAttackAnimationClip.length - 0.15f)
		{
			anim.CrossFade(idleBattleAnimation.name);
			attackTime = Random.Range(attackTimeMin, attackTimeMax);
			currentAttackAnimation = Random.Range(1, totalAttackAnimations);

			damageDealt = false;
			attackTimer = 0;
		}
		
		if (!useAnimations && attackTimer >= attackTime)
		{
			currentAttackAnimation = Random.Range(1, totalAttackAnimations);
			withinAttackDistance = false;
			damageDealt = false;

			attackTime = Random.Range(attackTimeMin, attackTimeMax);
			attackTimer = 0;
		}
	}
	
	
	
	void AttackWhileRunning ()
	{
		attackTimer += Time.deltaTime;
		
		if (drawWaypoints)
		{
			currentWaypoint.transform.position = currentAnimal.transform.position;
		}
		
		if (currentAnimal != null)
		{
			navMeshAgent.SetDestination(new Vector3 (currentAnimal.transform.position.x, currentAnimal.transform.position.y, currentAnimal.transform.position.z + stoppingDistance));
		}
		
		if (attackTimer >= attackTime) 
		{		
			if (!damageDealt)
			{
				if (useAttackSound)
				{
					if (useWeaponSound)
					{
						audioSource.PlayOneShot(weaponSound);
					}

					audioSource.PlayOneShot(attackSounds[Random.Range(0,attackSounds.Count)]);
				}
				
				//Attack Prey
				if (currentAnimal.gameObject.tag == preyTagName)
				{
					currentAnimal.GetComponent<Emerald_Animal_AI>().currentHealth -= attackDamage;

					if (currentTargetSystem != null)
					{
						if (currentTargetSystem.aggression == 1 || currentTargetSystem.aggression == 0)
						{
							currentTargetSystem.fleeTarget = this.gameObject;
							currentTargetSystem.isFleeing = true;
						}
					}
				}

				//Attack Player
				if (currentAnimal.gameObject.tag == playerTagName)
				{
					currentAnimal.GetComponent<PlayerHealth>().DamagePlayer(attackDamage);

					//If you would like your animals to damage the UFPS player damage script, uncomment the code below by removing the // and comment out the currentAnimal.GetComponent<PlayerHealth>() portions using //
					//currentAnimal.GetComponent<vp_FPPlayerDamageHandler>().Damage(attackDamage);
				}
				
				if (useRunAttackAnimations)
				{
					anim.CrossFade(runAttackAnimation.name);
				}
				
				damageDealt = true;
			}

			if (useAnimations && useRunAttackAnimations)
			{
				if (attackTimer > attackTime + runAttackAnimation.length)
				{
					anim.Play(runAnimation.name);
					attackTime = Random.Range(attackTimeMin, attackTimeMax);
					attackTimer = 0;
					damageDealt = false;
				}
			}

			if (!useAnimations && attackTimer > attackTime)
			{
				attackTime = Random.Range(attackTimeMin, attackTimeMax);
				attackTimer = 0;
				damageDealt = false;
			}
		}
	}
	
	
	void OnTriggerEnter(Collider other) 
	{

		//If the trigger object is our player, and the animal is cowardly, flee.
		if (fleeTarget == null && other.gameObject.tag == playerTagName && aggression == 1 && !isExhausted && !isDead)
		{
			navMeshAgent.ResetPath();
			freezeSecondsTotal = Random.Range(freezeSecondsMin, freezeSecondsMax);
			isFrozen = true;
			fleeTimer = (float)extraFleeSeconds;
			threatIsOutOfTigger = false;
			fleeTarget = other.gameObject;

			if (fleeTarget.GetComponent<PlayerHealth>() != null)
			{
				isPlayer = true;
			}

			if (fleeTarget.GetComponent<PlayerHealth>() == null)
			{
				isPlayer = false;
				Debug.LogError("Your player needs to have a PlayerHealth script attached to it.");
			}
			
			//Checks to make sure tired animals aren't fleeing
			if (!isExhausted)
			{
				calculateFlee = true;
			}
			
			//If our animal is in a heard and a predator approaches, alert the leader to flee as well
			if (inHerd && isAlpha == 0 && animalToFollow != null && preyOrPredator == 1)
			{
				animalToFollow.GetComponent<Emerald_Animal_AI>().fleeTarget = fleeTarget;
				animalToFollow.GetComponent<Emerald_Animal_AI>().isFleeing = true;
				animalToFollow.GetComponent<Emerald_Animal_AI>().calculateFlee = true;
				animalToFollow.GetComponent<Emerald_Animal_AI>().distantFlee = true; 
			}
			
			if (playSoundOnFlee)
			{
				audioSource.PlayOneShot(fleeSound);
			}
			
			if (isAlpha == 1 && preyOrPredator == 1)
			{
				foreach (GameObject G in herdList) 
				{
					if (G != null)
					{
						if (!G.GetComponent<Emerald_Animal_AI>().isExhausted)
						{
							G.GetComponent<Emerald_Animal_AI>().isFleeing = true;
						}
					}
				}
			}
		}

		//If the triggered object is our a follow tag, and the animal is a farm animal, follow.
		if (other.gameObject.tag == followTagName && aggression == 2 && !isExhausted && AIType == 0)
		{
			followTransform = other.gameObject.transform;
			isFollowing = true;
		}
		
		//Flee from predator
		//If the triggered object is a predator, and the animal is cowardly, call the frozen function (this causes a few seconds of randomized delay)
		//Added fleeTarget == null so that AI doesn't pick multiple flee targets resulting in one being far away and it stopping
		if (fleeTarget == null && other.gameObject.tag == predatorTagName && preyOrPredator == 1 && other.gameObject.GetComponent<Emerald_Animal_AI>().predatorSize == preySize && aggression < 2 && !isExhausted && !isDead)
		{
			navMeshAgent.ResetPath();
			freezeSecondsTotal = Random.Range(freezeSecondsMin, freezeSecondsMax);
			isFrozen = true;
			fleeTimer = (float)extraFleeSeconds;
			threatIsOutOfTigger = false;
			preySizeMatched = true;
			
			if (other.gameObject.GetComponent<Emerald_Animal_AI>().inHerd)
			{
				fleeTarget = other.gameObject.GetComponent<Emerald_Animal_AI>().animalToFollow.gameObject;
			}
			
			if (other.gameObject.GetComponent<Emerald_Animal_AI>().inHerd == false)
			{
				fleeTarget = other.gameObject;
			}

			if (inHerd == false)
			{
				fleeTarget = other.gameObject;
			}
			
			//Checks to make sure tired animals aren't fleeing
			if (!isExhausted)
			{
				calculateFlee = true;
			}
			
			//If our animal is in a heard and an predator approaches, alert the leader to flee as well
			if (inHerd && isAlpha == 0 && preyOrPredator == 1 && animalToFollow != null)
			{
				animalToFollow.GetComponent<Emerald_Animal_AI>().fleeTarget = other.gameObject; 
				animalToFollow.GetComponent<Emerald_Animal_AI>().isFleeing = true;
				animalToFollow.GetComponent<Emerald_Animal_AI>().calculateFlee = true;
				animalToFollow.GetComponent<Emerald_Animal_AI>().distantFlee = true; 
			}
		}
		
		//Chase Prey
		//If the triggered object is a prey, and the animal is a proper prey size, enabled huntMode and chase target
		if (currentAnimal == null && AIType == 0 && other.gameObject.tag == preyTagName && aggression == 3 && !huntMode && other.gameObject.GetComponent<Emerald_Animal_AI>().preySize <= predatorSize && !isCoolingDown)
		{
			if (other.gameObject.GetComponent<Emerald_Animal_AI>().currentlyBeingPursued == false || other.gameObject.GetComponent<Emerald_Animal_AI>().fleeTarget == this.gameObject)
			{
				targetInRange = true;
				PickNewTarget ();
			}
		}

		//Chase Player (Animal Only)
		//If the triggered object is our player, and the animal's prey size is large, enabled huntMode and chase target
		if (currentAnimal == null && other.gameObject.tag == playerTagName && aggression == 3 && predatorSize == 3 && AIType == 0)
		{
			targetInRange = true;
			currentAnimal = other.gameObject;
			
			navMeshAgent.speed = runSpeed;
			offSetPosition = Random.Range(-5,5);
			offSetDistance = Random.Range(-7,3) + 2;
			
			huntMode = true;
			startHuntTimer = true;
			
			if (currentAnimal.GetComponent<PlayerHealth>() != null)
			{
				isPlayer = true;
			}
			
			
			if (currentAnimal.GetComponent<PlayerHealth>() == null)
			{
				isPlayer = false;
				Debug.LogError("Your player needs to have a PlayerHealth script attached to it.");
			}
				
			//If our animal is in a heard and an prey approaches, alert the leader to hunt as well
			if (inHerd && isAlpha == 0 && preyOrPredator == 2 && animalToFollow != null)
			{
				animalToFollow.GetComponent<Emerald_Animal_AI>().currentAnimal = currentAnimal;
				animalToFollow.GetComponent<Emerald_Animal_AI>().huntMode = true;
				animalToFollow.GetComponent<Emerald_Animal_AI>().startHuntTimer = true;
			}

		}
		
		//Generates our herd system
		//If the other gameobject is the same animal and they are an alpha, follow heard
		if (other.gameObject.GetComponent<Emerald_Animal_AI>() != null && !inHerd && isAlpha == 0 && other.gameObject.GetComponent<Emerald_Animal_AI>().isAlpha == 1 && other.gameObject.GetComponent<Emerald_Animal_AI>().animalNameType == animalNameType && other.gameObject.GetComponent<Emerald_Animal_AI>().herdFull == false)
		{
			if (other.gameObject.GetComponent<Emerald_Animal_AI>().herdList.Count <= other.gameObject.GetComponent<Emerald_Animal_AI>().maxPackSize)
			{
				animalToFollow = other.gameObject.transform;
				inHerd = true;
			}
			
			if (other.gameObject.GetComponent<Emerald_Animal_AI>().packCreated == false)
			{
				//Add (Alpha) to the alpha's name
				other.gameObject.name = other.gameObject.name + " (Alpha)";
				other.gameObject.GetComponent<Emerald_Animal_AI>().packCreated = true;
			}
		}
		
		//Assign List of memebers of herd for alpha
		if (other.gameObject.GetComponent<Emerald_Animal_AI>() != null && other.gameObject.GetComponent<Emerald_Animal_AI>().animalToFollow == this.gameObject.transform && other.gameObject.GetComponent<Emerald_Animal_AI>().markInPack == false && isAlpha == 1 && other.gameObject.GetComponent<Emerald_Animal_AI>().isAlpha == 0)
		{
			//Limits the number of animals that can be in a pack to what's set with the maxPackSize
			if (herdList.Count <= maxPackSize && !herdFull)
			{
				other.gameObject.GetComponent<Emerald_Animal_AI>().markInPack = true;
				hasPack = true;
				herdList.Add(other.gameObject);
			}
		}

		if (isReadyForBreeding && !breedCoolDown && animalNameType != null)
		{

			if (other.gameObject.tag == this.gameObject.tag)
			{
				if (isReadyForBreeding && !mateFound && other.gameObject.GetComponent<Emerald_Animal_AI>().animalNameType == animalNameType && other.gameObject.GetComponent<Emerald_Animal_AI>().isReadyForBreeding)
				{
					if (!other.gameObject.GetComponent<Emerald_Animal_AI>().mateFound)
					{
						mateATransform = other.gameObject.transform;
						mateFound = true;
						isBabyGiver = true;
						other.gameObject.GetComponent<Emerald_Animal_AI>().mateFound = true;
						other.gameObject.GetComponent<Emerald_Animal_AI>().mateBTransform = this.gameObject.transform;
					}
				}
			}
		}


		//Defenise AI
		if (aggression == 4 && other.gameObject.tag == enemyTagName && !huntMode && !isCoolingDown && other.gameObject.activeSelf)
		{
			targetInRange = true;
			PickNewTarget ();
		}


		//Aggressive AI
		//If the triggered object is an NPC enabled huntMode and chase target
		if (currentAnimal == null && aggression == 3 && other.gameObject.tag == enemyTagName && !huntMode && !isCoolingDown && AIType == 1 && preySize == 3)
		{
			targetInRange = true;
			PickNewTarget ();
		}

		//Chase Player
		//If the triggered object is an NPC enabled huntMode and chase target
		if (currentAnimal == null && other.gameObject.tag == playerTagName && aggression == 3 && !isCoolingDown && AIType == 1)
		{
			targetInRange = true;

			currentAnimal = other.gameObject;
						
			navMeshAgent.speed = runSpeed;
			offSetPosition = Random.Range(-5,5);
			offSetDistance = Random.Range(-7,3) + 2;
						
			huntMode = true;
			startHuntTimer = true;
						
			if (currentAnimal.GetComponent<PlayerHealth>() != null)
			{
				isPlayer = true;
			}

			if (currentAnimal.GetComponent<PlayerHealth>() == null)
			{
				isPlayer = false;
				Debug.LogError("Your player needs to have a PlayerHealth script attached to it.");
			}
		}
	}
	

	void OnTriggerExit(Collider other) 
	{
		if (other.gameObject.tag == playerTagName && aggression == 1)
		{
			startFleeTimer = true;
			threatIsOutOfTigger = true;
		}
		
		if (other.gameObject.tag == predatorTagName && aggression == 1 && other.gameObject.GetComponent<Emerald_Animal_AI>().predatorSize <= preySize)
		{
			startFleeTimer = true;
			threatIsOutOfTigger = true;
		}

		if (other.gameObject.tag == enemyTagName && aggression == 1 && other.gameObject.GetComponent<Emerald_Animal_AI>().predatorSize == preySize)
		{
			startFleeTimer = true;
			threatIsOutOfTigger = true;
		}

		if (other.gameObject.tag == playerTagName && aggression > 2)
		{
			targetInRange = false;
		}

		if (other.gameObject.tag == enemyTagName && aggression > 2 && other.gameObject.GetComponent<Emerald_Animal_AI>().predatorSize == preySize)
		{
			targetInRange = false;
		}

		//If the triggered object is our a follow tag, and the animal is a farm animal, follow.
		if (other.gameObject.tag == followTagName && aggression == 2 && AIType == 0)
		{
			isFollowing = false;
			isGrazing = true;
			Wander();
		}

	
	}

	void PickNewTarget ()
	{
		//NPC Only
		//Find the nearest target if current target dies, a target is out of distance, or an object's reference becomes null.
		//This will only happen if an AI's targetInRange is true. This is only true if an appropriate object is within its huntRadius
		if (targetInRange && AIType == 1)
		{
			GameObject[] targets = GameObject.FindGameObjectsWithTag(enemyTagName);

			Vector3 distanceDifference; 
			float oldDistance = Mathf.Infinity; 
			float currentDistance;  
			
			foreach (GameObject target in targets)
			{
				distanceDifference = target.transform.position - transform.position;
				currentDistance = distanceDifference.sqrMagnitude;
				if (currentDistance < oldDistance)
				{
					currentAnimal = target;
					
					navMeshAgent.speed = runSpeed;
					offSetPosition = Random.Range(-5,5);
					offSetDistance = Random.Range(-7,3) + 2;
					
					huntMode = true;
					startHuntTimer = true;
					
					if (currentAnimal.GetComponent<PlayerHealth>() != null)
					{
						isPlayer = true;
					}
					
					
					if (currentAnimal.GetComponent<PlayerHealth>() == null)
					{
						isPlayer = false;
						
						if (currentAnimal.gameObject.GetComponent<Emerald_Animal_AI>() != null && currentAnimal.gameObject.GetComponent<Emerald_Animal_AI>().enabled)
						{
							currentTargetSystem = currentAnimal.gameObject.GetComponent<Emerald_Animal_AI>();
						}
						
						if (currentAnimal.gameObject.GetComponent<Emerald_Animal_AI>() == null)
						{
							Debug.LogError("The AI " + currentAnimal.gameObject.name + " doesn't have an Emerald AI script attached to it. This is needed in order for Emerlad to function properly. Please apply one.");
						}
					}

					oldDistance = currentDistance;
				}
			}
		}

		//Animal Only
		//Find the nearest target if current target dies, a target is out of distance, or an object's reference becomes null.
		//This will only happen if an AI's targetInRange is true. This is only true if an appropriate object is within its huntRadius
		if (targetInRange && AIType == 0)
		{
			GameObject[] targets = GameObject.FindGameObjectsWithTag(preyTagName);
			
			Vector3 distanceDifference; 
			float oldDistance = Mathf.Infinity; 
			float currentDistance;  
			
			foreach (GameObject target in targets)
			{
				distanceDifference = target.transform.position - transform.position;
				currentDistance = distanceDifference.sqrMagnitude;

				if (currentDistance < oldDistance)
				{
					currentAnimal = target;
					
					navMeshAgent.speed = runSpeed;
					offSetPosition = Random.Range(-5,5);
					offSetDistance = Random.Range(-7,3) + 2;
					
					huntMode = true;
					startHuntTimer = true;

					if (currentAnimal.GetComponent<PlayerHealth>() != null)
					{
						isPlayer = true;
					}
						
					if (currentAnimal.gameObject.GetComponent<Emerald_Animal_AI>() != null && currentAnimal.gameObject.GetComponent<Emerald_Animal_AI>().enabled)
					{
						currentTargetSystem = currentAnimal.gameObject.GetComponent<Emerald_Animal_AI>();
					}
						
					if (currentAnimal.gameObject.GetComponent<Emerald_Animal_AI>() == null)
					{
						Debug.LogError("The AI " + currentAnimal.gameObject.name + " doesn't have an Emerald AI script attached to it. This is needed in order for Emerlad to function properly. Please apply one.");
					}

					//If our animal is in a heard and an prey approaches, alert the leader to hunt as well
					if (inHerd && isAlpha == 0 && preyOrPredator == 2 && animalToFollow != null)
					{
						animalToFollow.GetComponent<Emerald_Animal_AI>().currentAnimal = currentAnimal;
						animalToFollow.GetComponent<Emerald_Animal_AI>().huntMode = true;
						animalToFollow.GetComponent<Emerald_Animal_AI>().startHuntTimer = true;
					}

					oldDistance = currentDistance;
				}
			}

		}
	}
	
}
