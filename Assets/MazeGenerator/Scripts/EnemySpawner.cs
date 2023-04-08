using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    public float spawnDelay;
    public int spawningNumber;
    public int maxInstantiatedEnemy;
    private GameObject[] instantiatedEnemies;
    int firstEmpty = 0;

    private float lastSpawn;
    

    // Start is called before the first frame update
    void Start()
    {
        instantiatedEnemies = new GameObject[maxInstantiatedEnemy];
        lastSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (verifySpawnConstraints())
        {
            int randomEnemy = Random.Range(0, enemies.Count - 1);
            instantiatedEnemies[firstEmpty] = Instantiate(enemies[randomEnemy], transform.position, Quaternion.identity);
            lastSpawn = Time.time;
        }

    }

    private bool verifySpawnConstraints()
    {
        firstEmpty = System.Array.IndexOf(instantiatedEnemies, null);

        if (spawnDelay <= Time.time - lastSpawn
            && firstEmpty != -1)
        {
            return true;
        }

        return false;
    }
}
