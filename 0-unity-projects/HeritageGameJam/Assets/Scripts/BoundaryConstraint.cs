using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

//han this script helps drag and drop objects not to go outside of boundaries // values are defaulted to camera aspect ratio boundaries 
public class BoundaryConstraint : MonoBehaviour
{
    public float left = -9.0f;
    public float right = 9.0f;
    public float top = 5.0f;
    public float bottom = -5.0f;
    void Update()
    {
        if(GameMaster.SceneLoaded)
        { 
            if (gameObject.transform.position.x > right)
                gameObject.transform.position = new Vector3(right, gameObject.transform.position.y, gameObject.transform.position.z);
            if (gameObject.transform.position.x < left)
                gameObject.transform.position = new Vector3(left, gameObject.transform.position.y, gameObject.transform.position.z);
            if (gameObject.transform.position.y > top)
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, top, gameObject.transform.position.z);
            if (gameObject.transform.position.y < bottom)
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, bottom, gameObject.transform.position.z);
        }
    }
}
