using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;

    private float playerShootVelocity;
    private float playerShootDelay;
    private float lastShoot;

    // Start is called before the first frame update
    void Start()
    {
        lastShoot = 0f;

        // Listen on Player statistics
        FindObjectOfType<PlayerStatistics>().updateStatsEvent += UpdateStat;
        playerShootVelocity = FindObjectOfType<PlayerStatistics>().shootSpeed.GetValue();
        playerShootDelay = FindObjectOfType<PlayerStatistics>().shootRate.GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerShootDelay <= Time.time - lastShoot)
        {
            Vector3 direction = Vector3.zero;
            Vector3 rotation = Vector3.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = Vector3.right;
                rotation = new Vector3(0f, 90f, 0f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction = Vector3.left;
                rotation = new Vector3(0f, -90f, 0f);
            }
           else if (Input.GetKey(KeyCode.DownArrow))
            {
                direction = Vector3.back;
                rotation = new Vector3(0f, 180f, 0f);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                direction = Vector3.forward;
            }

            if (direction != Vector3.zero)
            {
                GameObject tmp;
                Vector3 newPos = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
                tmp = Instantiate(projectile, newPos, Quaternion.identity);
                tmp.transform.rotation = Quaternion.Euler(rotation);
                tmp.GetComponent<Rigidbody>().AddForce(direction * 100 * playerShootVelocity);
                lastShoot = Time.time;
            }
        }
    }

    public void UpdateStat(PlayerStatistics playerStats)
    {
        playerShootVelocity = playerStats.shootSpeed.GetValue();
        playerShootDelay = playerStats.shootRate.GetValue();
    }
}
