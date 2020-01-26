using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakSlingshot : MonoBehaviour
{
    GameObject m_payload;
    GameObject m_FirePoint;
    bool playsound = true;

    public AudioSource source;

    void Start()
    {
        m_payload = transform.GetChild(0).gameObject;
        m_FirePoint = transform.GetChild(1).gameObject;
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (m_payload.GetComponent<Payload>().getBreakSlingshot())
        {
            if (playsound)
            {
                source.Play();
                playsound = false;
            }

            m_payload.SetActive(false);
            m_FirePoint.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().simulated = true;
            m_FirePoint.transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().simulated = true;
            m_FirePoint.transform.GetChild(2).gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }
    }
}
