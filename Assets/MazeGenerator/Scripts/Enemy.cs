using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PlayerStatistics plyStats;
    EnemyStatistics stats;

    // Start is called before the first frame update
    void Start()
    {
        plyStats = FindObjectOfType<PlayerStatistics>();
        stats = GetComponent<EnemyStatistics>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                plyStats.health.Remove(stats.damage.GetValue());
                break;
        }
    }
}
