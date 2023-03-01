using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left_up_sensor : MonoBehaviour
{
    public float distance;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (transform.right + transform.up), 100);
        if (hit.collider != null)
        {
            distance = hit.distance;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (transform.right + transform.up) * 100);
    }
}
