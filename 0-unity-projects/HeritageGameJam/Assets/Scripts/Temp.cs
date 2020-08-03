using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//han just display
public class Temp : MonoBehaviour
{
    void Awake()
    {
        LeanTween.init(1600);
    }
    float abc = 8.0f;
    // Update is called once per frame
    void Update()
    {
        if((abc -= Time.deltaTime) < 0.0f)
        { 
            LeanTween.moveX(gameObject, -20.0f, 1f).setEaseOutQuad();
            //Destroy(gameObject);
        }
    }
}
