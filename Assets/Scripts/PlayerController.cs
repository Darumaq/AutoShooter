using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    void Start()
    {
        
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector3 movement = Vector3.right * x;

        float y = Input.GetAxisRaw("Vertical");
        movement += Vector3.forward * y;

        movement = movement.normalized;

        movement *= Time.deltaTime;

        movement *= moveSpeed;

        transform.position += movement;
        
    }
    public void Hit(GameObject other)
    {
        Debug.Log("Gracz trafiony");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Gracz trafiony");
        }
    }
}
