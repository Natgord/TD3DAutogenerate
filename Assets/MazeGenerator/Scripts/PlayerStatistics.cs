using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public delegate void PlayerStatsUpdateDelegate(PlayerStatistics p);
    public event PlayerStatsUpdateDelegate updateStatsEvent;

    public Statistic movSpeed;
    public Statistic shootSpeed;
    public Statistic shootRate;
    public Statistic shootRange;
    public Statistic health;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        VerifyHealth();
    }

    void Initialize()
    {
        movSpeed.statEvent += OnStatUpdated;
        shootSpeed.statEvent += OnStatUpdated;
        shootRate.statEvent += OnStatUpdated;
        shootRange.statEvent += OnStatUpdated;
    }

    void OnStatUpdated()
    {
        updateStatsEvent(this);
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
}
