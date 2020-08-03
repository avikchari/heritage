using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

// hansoo
// drag and dropfeature
// continuously called by MousePointer.OnMouseDrag, only stops if mouse click is let off
public class DragDropFeature : MonoBehaviour
{
    Vector3 offset;
    public void EnableOffset()
    {
        offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
    public void FollowMouse() 
    {
        float val = transform.position.z;
        Vector2 pos = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 10.0f);
        transform.position = new Vector3(pos.x - offset.x, pos.y - offset.y, val);
    }
    public void FollowMouseConstraintOnX()
    {
        float val = transform.position.z;
        float YY = transform.position.y;
        Vector2 pos = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 10.0f);
        transform.position = new Vector3(pos.x - offset.x, YY, val);
    }
    public void FollowMouseConstraintOnY()
    {
        float val = transform.position.z;
        float XX = transform.position.x;
        Vector2 pos = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 10.0f);
        transform.position = new Vector3(XX, pos.y - offset.y, val);
    }
}
