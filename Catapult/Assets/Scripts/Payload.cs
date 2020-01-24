using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D firePoint;

    public float maxDistance = 2.0f;
    public float releaseTime = 0.15f;

	public float ReloadTime = 5.0f;
	public float FireTime = 1.5f;
	public bool autoFire = false;
	private bool disableInput = false;

    private bool isPressed = false;
	private Vector2 autoFirePos;

	public bool placementMode = false;

	public AudioClip fireSound;
	public AudioClip ReloadSound;
	private AudioSource sound;


	void Start()
	{
		sound = GetComponent<AudioSource>();
	}

    void Update()
    {
		if (!placementMode)
		{
			if (!disableInput)
			{
				movePayload(Camera.main.ScreenToWorldPoint(Input.mousePosition));
				if (Input.GetKey(KeyCode.Space))
				{
					print("Reload pressed");
					sound.PlayOneShot(ReloadSound, 0.7F);
					Reload();
				}
			}
		}
    }


    void OnMouseDown()
    {
		if (!placementMode)
		{
			if (!disableInput)
			{
	        	isPressed = true;
	        	rb.isKinematic = true;
			}
		}
    }

    public void OnMouseUp()
    {
		if (!placementMode)
		{
			if (!disableInput)
			{
	        	isPressed = false;
	        	rb.isKinematic = false;
				autoFirePos = rb.position;
	        	StartCoroutine(Release());
			}
		}
		else if (placementMode)
		{
			firePoint.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			Reload();
			placementMode = false;
		}
    }


	void movePayload(Vector2 t_pos)
	{
		if (isPressed)
		{
			Vector2 mousePos = t_pos;

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

	public void Reload()
	{
		StopCoroutine(Release());
		isPressed = false;
		rb.isKinematic = false;
		rb.position = firePoint.position;
		rb.velocity=Vector2.zero;
		GetComponent<SpringJoint2D>().enabled = true;

	}

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;

		sound.PlayOneShot(fireSound, 0.7F);

		//this.enabled = false;
		if (autoFire)
		{
			disableInput = true;
			StartCoroutine(ReloadWait());
		}
    }
		
	IEnumerator ReloadWait()
	{
		yield return new WaitForSeconds(ReloadTime);
		sound.PlayOneShot(ReloadSound, 0.7F);
		Reload();
		StartCoroutine(FireWait());
	}

	IEnumerator FireWait()
	{
		rb.position = autoFirePos;
		isPressed = true;
		rb.isKinematic = true;
		yield return new WaitForSeconds(FireTime);
		isPressed = false;
		rb.isKinematic = false;
		StartCoroutine(Release());
	}
}
