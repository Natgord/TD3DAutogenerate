using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatistics : MonoBehaviour
{
    public Statistic health;
    public Statistic movSpeed;
    public Statistic damage;

    // Start is called before the first frame update
    void Start()
    {
        damage.statEvent += OnStatUpdated;
        movSpeed.statEvent += OnStatUpdated;
        health.statEvent += OnStatUpdated;
    }

    // Update is called once per frame
    void Update()
    {
        VerifyHealth();
    }

    void VerifyHealth()
    {
        if (health.GetValue() <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnStatUpdated()
    {
        
    }
}
