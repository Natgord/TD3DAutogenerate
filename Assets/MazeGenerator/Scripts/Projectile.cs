using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ProjectileStatistics projStats;
    private float playerShootRange;

    private void Awake()
    {
        Physics.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider>(), GetComponent<Collider>());
        GetComponent<Rigidbody>().useGravity = false;

        projStats = GetComponent<ProjectileStatistics>();

        // Listen on Player statistics
        FindObjectOfType<PlayerStatistics>().updateStatsEvent += UpdateStat;
        playerShootRange = FindObjectOfType<PlayerStatistics>().shootRange.GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 plyPos = new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.z);
        Vector2 projPos = new Vector2(transform.position.x, transform.position.z);
        float dist = Vector2.Distance(projPos, plyPos);
        if (Mathf.Abs(dist) >= playerShootRange)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void UpdateStat(PlayerStatistics playerStats)
    {
        playerShootRange = playerStats.shootRate.GetValue();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                other.GetComponent<EnemyStatistics>().health.Remove(projStats.damage.GetValue());
                Destroy(gameObject);
                break;

            case "Player":
                break;

            default:
                Destroy(gameObject);
                break;
        }
    }
}
