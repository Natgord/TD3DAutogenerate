using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStatistics : MonoBehaviour
{
    public Statistic damage;

    // Start is called before the first frame update
    void Start()
    {
        damage.statEvent += OnStatUpdated;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStatUpdated()
    {
        
    }
}
