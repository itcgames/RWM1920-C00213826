using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTextScript : MonoBehaviour
{
    public Text ControlText;
    public Text HelpText;

    public GameObject slingshot;

    // Start is called before the first frame update
    void Start()
    {
        if (!slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().autoFire)
        {
            ControlText.text = "'Left Mouse' = Fire" + "\n" + "'Space' = Reload" + "\n" + "'Q' = Placement Mode" + "\n" + "'W' = Self Destruct";
        }
        else
        {
            ControlText.text = "'Left Mouse' = Template Shot" + "\n" + "'W' = Self Destruct";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().placementMode)
        {
            HelpText.text = "Placement Mode : Enabled ";
        }
        else
        {
            HelpText.text = "";
        }
       

        if (slingshot.transform.GetChild(0).gameObject.GetComponent<Payload>().getBreakSlingshot())
        {
            HelpText.text = "";
            ControlText.text = "";
        }

    }
}



/*
 * Placement mode : 
 *      Drag the Bucket to your desired location for the slingshot.
 * 
 * Auto shot mode:
 *      manually input a shot then the slingshot will emulate it.
 *      
 * Break Slingshot:
 *      'W' will self destruct the sling shot
 *      
 * To Use:
 *      Click and Drag the Bucket with 'Left Mouse Button' in a direction and release for it to fire.
 *      
 * Reload:
 *      'Space' Reloads the Bucket if it has been fired prior.
 */