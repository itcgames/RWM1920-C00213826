using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Rigidbody2D firePoint; // the point where spring joint is attached

    public AudioSource soundSource;
    public AudioClip fireSound; 
    public AudioClip ReloadSound;

    // -----------------

    public float maxDistance = 2.0f;    // distance away from fire point
    public float releaseTime = 0.15f;   // time to release when button up

    public float ReloadTime = 5.0f; // auto reload time
    public float FireTime = 1.5f;   // time spent idle when shot is ready
    public bool autoFire = false;   // enables / disables autofire mode


    public bool placementMode = false;      // used to enable and disable mouse placement

	private bool disableInput = false;      // enables / disable input

    private bool isBeingPressed = false;    // if mouse currently is clicking
	private Vector2 autoFirePos;    // point in which auto fire emulates the inputed shot position

    private bool breakSlingshot = false;    // disables payload and breaks the slingshot sprite body


    private bool reloadSoundToggle = true;      // stop reload sound playing multaple times on reload button push
    private bool PlacementModeToggle = true;

    public bool testMode = false;


    void Start()
	{
		soundSource = GetComponent<AudioSource>();
	}

    void Update()
    {
		if (!placementMode)
		{
			if (!disableInput)
			{
                if (!testMode)
                {
                    movePayload(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }

                if (Input.GetKey(KeyCode.Space))
				{
                    if (reloadSoundToggle)
                    {
                        soundSource.PlayOneShot(ReloadSound, 0.7F);
                        Reload();
                        reloadSoundToggle = false;
                    }
				}
                else
                {
                    reloadSoundToggle = true;
                }
			}
        }

        if (!autoFire)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                if (PlacementModeToggle)
                {
                    placementMode = !placementMode;
                    Reload();
                    PlacementModeToggle = false;
                }
            }
            else
            {
                PlacementModeToggle = true;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            breakSlingshot = true;
        }

    }

    void OnMouseDown()
    {
		if (!placementMode)
		{
			if (!disableInput)
			{
	        	isBeingPressed = true;
	        	rb.isKinematic = true;
			}
		}
    }


    public void releasePayload()
    {
        if (!placementMode)
        {
            if (!disableInput)
            {
                isBeingPressed = false;
                rb.isKinematic = false;
                autoFirePos = rb.position;
                StartCoroutine(Release());
            }
        }
        else if (placementMode)
        {
            if (!testMode)
                firePoint.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Reload();
            placementMode = false;
        }
    }


    public void OnMouseUp()
    {
        releasePayload();
    }


	public void movePayload(Vector2 t_pos)
	{
		if (isBeingPressed)
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
        if (!testMode)
            StopCoroutine(Release());
		isBeingPressed = false;
		rb.isKinematic = false;
		rb.position = firePoint.position;
		rb.velocity=Vector2.zero;
		GetComponent<SpringJoint2D>().enabled = true;

	}

    public bool getBreakSlingshot()
    {
        return breakSlingshot;
    }

    public void setBreakSlingshot(bool t_temp)
    {
        breakSlingshot = t_temp;
    }

    public void setIsBeingPressed(bool t_temp)
    {
        isBeingPressed = t_temp;
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;

        if (!testMode)
            soundSource.PlayOneShot(fireSound, 0.7F);

		if (autoFire)
		{
			disableInput = true;
			StartCoroutine(ReloadWait());
		}
        else
        {
            disableInput = false;
        }
    }
		
	IEnumerator ReloadWait()
	{
		yield return new WaitForSeconds(ReloadTime);
		soundSource.PlayOneShot(ReloadSound, 0.7F);
		Reload();
		StartCoroutine(FireWait());
	}

	IEnumerator FireWait()
	{
		rb.position = autoFirePos;
		isBeingPressed = true;
		rb.isKinematic = true;
		yield return new WaitForSeconds(FireTime);
		isBeingPressed = false;
		rb.isKinematic = false;
		StartCoroutine(Release());
	}
}
