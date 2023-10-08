using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject target;
    public GameObject bird;
    public GameObject crocodile;

    private readonly float targetLeftBoundZ = 7;
    private readonly float targetRightBoundZ = 20;
    

    private readonly float birdTopBoundY = 9;
    private readonly float birdBottomBoundY = 6;

    private readonly float birdStartDelay = 5f;
    private float birdRepeatDelay = 5f;

    private float targetSpawnPosZ;

    private GameObject targetPrefab;
    private GameObject crocodilePrefab;

    private PlayerMovement playerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        
        // Generate first level
        InvokeRepeating(nameof(BirdSpawn), birdStartDelay, birdRepeatDelay);
        TargetSpawn();
        CrocodileSpawn();
        targetPrefab.SetActive(true);
        crocodilePrefab.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLevel();
    }

    void UpdateLevel()
    {
        // Player steps on next waterlily and code generate another level
        if (playerMovementScript.isTargetChased)
        {
            playerMovementScript.isTargetChased = false;

            var offset = targetPrefab.transform.position.z;

            TargetSpawn();
            CrocodileSpawn();

            // Make offset for best moving illusion
            targetPrefab.transform.position += new Vector3(0, 0, offset);
            crocodilePrefab.transform.position += new Vector3(0, 0, offset);

            targetPrefab.SetActive(true);
            crocodilePrefab.SetActive(true);

            // Starts updating level
            playerMovementScript.isLevelUpdated = false;
        }
    }

    void BirdSpawn()
    {
        var birdPrefab = Instantiate(bird, new Vector3(8.75f, Random.Range(birdBottomBoundY, birdTopBoundY), 30), bird.transform.rotation);
        birdPrefab.tag = "Enemy";
        birdRepeatDelay = Random.Range(9, 11);
    }

    void TargetSpawn()
    {
        targetSpawnPosZ = Random.Range(targetLeftBoundZ, targetRightBoundZ);
        var spawnPos = new Vector3(8.75f, 2, targetSpawnPosZ);
        targetPrefab = Instantiate(target, spawnPos, target.transform.rotation);
        targetPrefab.tag = "Target";
        targetPrefab.SetActive(false);
    }

    void CrocodileSpawn()
    {
        var spawnPos = new Vector3(8.75f, 2, Random.Range(2, targetSpawnPosZ - 2));
        crocodilePrefab = Instantiate(crocodile, spawnPos, crocodile.transform.rotation);
        crocodilePrefab.tag = "Enemy";
        crocodilePrefab.SetActive(false);
    }
}
