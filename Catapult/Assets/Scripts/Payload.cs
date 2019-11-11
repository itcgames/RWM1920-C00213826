using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D firePoint;
    public float maxDistance = 2.0f;
    public float releaseTime = 0.15f;
    private bool isPressed = false;

    void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            
            if (Vector3.Distance(firePoint.position, mousePos) > maxDistance)
            {
                rb.position = firePoint.position + ( mousePos - firePoint.position).normalized * maxDistance;
            }
            else
            {
                rb.position = mousePos;
            }
        }
    }


    void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());
    }


    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;

        this.enabled = false;

    }

}
