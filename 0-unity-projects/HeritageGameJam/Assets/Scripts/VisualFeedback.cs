using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// han soo
// this script is meant for feed back to the user.
// any1 can add more public functions here for custom feedbacks
public class VisualFeedback : MonoBehaviour
{
    Vector3 OriginalScale;
    int OriginalZOrder;
    public void Awake()
    {
        OriginalScale = transform.localScale;
        if(GetComponent<SpriteRenderer>() != null)
            OriginalZOrder = GetComponent<SpriteRenderer>().sortingOrder;
    }
    public void ZorderFront() // to have this object infront of others
    {
        if (GetComponent<SpriteRenderer>() != null)
            GetComponent<SpriteRenderer>().sortingOrder += 1;
    }
    public void ZorderBack()
    {
        if (GetComponent<SpriteRenderer>() != null)
            GetComponent<SpriteRenderer>().sortingOrder = OriginalZOrder;
    }
    public void MoveUp()
    {
        //LeanTween.moveLocalY(gameObject,1.2f, 1f).setEaseOutQuad();
    }
    public void MoveDown()
    {
        //LeanTween.moveLocalY(gameObject, 0.8f, 1f).setEaseOutQuad();
    }
    public void ScaleUp()
    {
        //LeanTween.scale(gameObject, OriginalScale * 1.2f, 0.15f).setEaseOutQuad();
    }
    public void ScaleDownToOriginal()
    {
        //LeanTween.scale(gameObject, OriginalScale, 0.15f).setEaseOutQuad();
    }
}
