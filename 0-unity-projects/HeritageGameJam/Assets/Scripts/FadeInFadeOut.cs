using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class FadeInFadeOut : MonoBehaviour
{
    public Action CallBackFadeToBlack;      //1st
    //public Action CallBackFadeOutFromBlack; //2nd
    float timer = 0.0f;
    bool toggle = true;
    public Vector3 fadecolour;
    void Awake()
    {
        if (GetComponent<SpriteRenderer>() == null)
        { 
            Debug.Log("fadetoblack for transition does not have sprite renderer");
            Destroy(this.gameObject); // destroy self
        }
        GetComponent<SpriteRenderer>().color = new Color(fadecolour.x, fadecolour.y, fadecolour.z, 0.0f); // transparent
    }
    void Update()
    {
        if(toggle) // fading to black
        { 
            if((timer += Time.deltaTime) <= 1.0f)
                GetComponent<SpriteRenderer>().color = new Color(fadecolour.x, fadecolour.y, fadecolour.z, timer); // black becoming visible
            else
            {
                if(CallBackFadeToBlack != null)
                    CallBackFadeToBlack();
                timer = 0.0f; // reset timer for next fade out from black
                toggle = false;
            }
        }
        else
        {
            if ((timer += Time.deltaTime) <= 1.0f)
                GetComponent<SpriteRenderer>().color = new Color(fadecolour.x, fadecolour.y, fadecolour.z, 1.0f - timer); // black fading
            else
            {
                //if (CallBackFadeOutFromBlack != null)
                //CallBackFadeOutFromBlack();
                Destroy(this.gameObject); // destroy self
            }
        }
    }

}
