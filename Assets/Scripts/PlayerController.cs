using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;

    void Start()
    {

    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 targetDirection = new Vector3(x, 0, y);
        Vector3 targetPosition = transform.position + targetDirection;
        if (targetDirection.magnitude > Mathf.Epsilon)
        {
            transform.LookAt(targetPosition);
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }
    }

    public void Hit(GameObject other)
    {
        Debug.Log("Gracz trafiony");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Gracz trafiony");
            GameObject.Find("LevelManager").GetComponent<LevelManager>().GameOver();
        }
    }
}
