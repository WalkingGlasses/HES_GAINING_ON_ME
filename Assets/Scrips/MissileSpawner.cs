using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject missilePrefab;
    public Transform spawnPoint;

    [Header("Starting Difficulty")]
    public int startingMissiles = 1;

    [Header("Difficulty")]
    public float increaseEverySeconds = 10f;
    public int additionalMissiles = 1;

    private float survivalTimer;
    private int currentMissileCount;

    void Start()
    {
        currentMissileCount = startingMissiles;

        SpawnMissiles(currentMissileCount);
    }

    void Update()
    {
        survivalTimer += Time.deltaTime;

        if (survivalTimer >= increaseEverySeconds)
        {
            survivalTimer -= increaseEverySeconds;

            currentMissileCount += additionalMissiles;

            Debug.Log(
                "Difficulty increased! Total missiles: "
                + currentMissileCount
            );

            SpawnMissiles(additionalMissiles);
        }
    }

    void SpawnMissiles(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(
                missilePrefab,
                spawnPoint.position,
                Quaternion.identity
            );
        }
    }
}