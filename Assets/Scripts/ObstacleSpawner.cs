using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;   // Reference to the obstacle prefab
    public float spawnInterval = 5f;    // Time interval between spawns
    public Vector3 spawnOffset = new Vector3(0, 0.75f, -15); // Offset from the target object's position
    public GameObject obstacle;
    public JumpAgent jumpAgent;
    public Material obstacleMaterial;
    public Material rewardMaterial;

    private void Start()
    {
        obstacle = jumpAgent.obstacle;
    }

    public void ResetObstacle()
    {
        // Calculate the spawn position relative to the target object
        Vector3 spawnPosition = transform.position + spawnOffset;


        // Set a random speed for the obstacle
        float randomSpeed = Random.Range(2f, 8f); // Speed range
        obstacle.GetComponent<Obstacle>().SetSpeed(randomSpeed);

        if (Random.Range(0f, 3f) < 2)
        {
            obstacle.GetComponent<MeshRenderer>().material = obstacleMaterial;
            obstacle.tag = "Obstacle";
        }
        else
        {
            obstacle.GetComponent<MeshRenderer>().material = rewardMaterial;
            obstacle.tag = "Reward";
        }


        obstacle.GetComponent<Obstacle>().SetPosition(spawnPosition);
    }
}