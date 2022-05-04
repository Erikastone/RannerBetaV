using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsSpawner : MonoBehaviour
{
    public GameObject[] groundSpawner;
    private List<GameObject> activeObs = new List<GameObject>();
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject obstaclePrefab;
    private float spawnPos = 0;
    private float tileLenght = 1;

    [SerializeField] private Transform player;
    private int startObs = 6;

    private void Start()
    {
        // groundSpawner = FindObjectsOfType<GameObject>();
        for (int i = 0; i < startObs; i++)
        {
            SpawnObstacle(Random.Range(0, groundSpawner.Length));
        }
    }

    void Update()
    {
        if (player.position.z + 60 > spawnPos - (startObs * tileLenght))
        {
            SpawnObstacle(Random.Range(0, groundSpawner.Length));
            DeleteTile();
        }

    }

    public void SpawnObstacle(int ObsIndex)
    {
        // Choose a random point to spawn the obstacle
      //  int obstacleSpawnIndex = Random.Range(1, 3);
        //Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        // Spawn the obstace at the position
       // Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
        
            GameObject nexTile = Instantiate(groundSpawner[ObsIndex], transform.forward * spawnPos, transform.rotation);
            activeObs.Add(nexTile);
            spawnPos += tileLenght;       
    }
    private void DeleteTile()
    {
        Destroy(activeObs[0]);
        activeObs.RemoveAt(0);
    }
}
