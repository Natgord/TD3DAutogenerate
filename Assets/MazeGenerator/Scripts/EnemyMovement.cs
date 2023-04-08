using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject hearth;

    UnityEngine.AI.NavMeshAgent agent;

    public bool canTrackPlayer;
    public float trackZoneRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        hearth = GameObject.Find("Hearth");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (canTrackPlayer)
        {
            Vector2 plyPosition = new Vector2(player.transform.position.x, player.transform.position.z);
            Vector2 enmyPosition = new Vector2(transform.position.x, transform.position.z);
            float plyDistance = Vector2.Distance(plyPosition, enmyPosition);
            if (Mathf.Abs(plyDistance) <= trackZoneRange)
            {
                agent.destination = player.transform.position;
                return;
            }
        }

        agent.destination = hearth.transform.position;
    }
}
