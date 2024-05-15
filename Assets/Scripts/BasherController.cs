using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasherController : MonoBehaviour
{
    GameObject player;
    public float walkSpeed = 1f;
    GameObject levelManager;
    bool hasBeenHit = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = GameObject.Find("LevelManager");
    }

    void Update()
    {
        transform.LookAt(player.transform.position);
        transform.position += transform.forward * Time.deltaTime * walkSpeed;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (hasBeenHit) { return; }

        GameObject projectile = collision.gameObject;

        if (projectile.CompareTag("PlayerProjectile"))
        {
            hasBeenHit = true;
            levelManager.GetComponent<LevelManager>().AddPoints(1);
            Destroy(projectile);
            Destroy(transform.gameObject);
        }
    }
} 
