using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    PlayerStatistics playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStatistics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerStats.movSpeed.Add(1f);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            playerStats.shootSpeed.Add(0.5f);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            playerStats.shootRate.Remove(0.5f);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            playerStats.shootRange.Add(0.5f);
        }
    }
}
