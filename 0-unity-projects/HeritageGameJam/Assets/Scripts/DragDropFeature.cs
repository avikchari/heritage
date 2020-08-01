using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// hansoo
// drag and dropfeature
public class DragDropFeature : MonoBehaviour
{
    public void FollowMouse() // continuously called by MousePointer.OnMouseDrag, only stops if mouse click is let off
    {
        float val = transform.position.z;
        Vector2 pos = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 20.0f);
        transform.position = new Vector3(pos.x, pos.y, val);
    }
}
