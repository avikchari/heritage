using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// hansoo
// this is script is a general purpose container of actions for mouse interactions, assign any function to this via the inspector
// example, function from another script is attached as component to this entity, reference itself and get that function 
// this script allows 1 mouse interaction to many functions to happen. (1 : many functions)
// ensure you have a 2d collider attached to this otherwise it will not work.

public class MouseInteraction : MonoBehaviour //, IPointerClickHandler, IEventSystemHandler
{
    public List<UnityEvent> MouseDownCalls = new List<UnityEvent>();
    public List<UnityEvent> MouseUpCalls = new List<UnityEvent>();
    public List<UnityEvent> MouseEnterCalls = new List<UnityEvent>();
    public List<UnityEvent> MouseExitCalls = new List<UnityEvent>();
    public List<UnityEvent> MouseDragCalls = new List<UnityEvent>();
    public List<UnityEvent> MouseOverCalls = new List<UnityEvent>();
    public List<UnityEvent> MouseUpAsButtonCalls = new List<UnityEvent>();
    public void Awake()
    {
        if(GetComponent<BoxCollider2D>() == null && GetComponent<CircleCollider2D>() == null) // for debug
            Debug.Log(name + " has no collider for mouse detection");        
    }
    void OnMouseDown()
    {
        foreach (var i in MouseDownCalls)  // tell this object to do what function that is assigned to this
            i.Invoke(); 
    }
   void OnMouseUp()
   {
        foreach (var i in MouseUpCalls)  // tell this object to do what function that is assigned to this
            i.Invoke();
   }
   void OnMouseEnter()
   {
        foreach (var i in MouseEnterCalls)  // tell this object to do what function that is assigned to this
            i.Invoke();
   }
   void OnMouseExit()
   {
        foreach (var i in MouseExitCalls)  // tell this object to do what function that is assigned to this
            i.Invoke();
   }
   void OnMouseDrag()
   {
        foreach (var i in MouseDragCalls)  // tell this object to do what function that is assigned to this
            i.Invoke();
   }
   void OnMouseOver() //Called every frame while the mouse is over the Collider.
   {
        foreach (var i in MouseOverCalls)  // tell this object to do what function that is assigned to this
            i.Invoke();
   }
   void OnMouseUpAsButton()
   {
        foreach (var i in MouseUpAsButtonCalls)  // tell this object to do what function that is assigned to this
            i.Invoke();
   }
}  