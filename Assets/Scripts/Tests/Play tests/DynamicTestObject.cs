using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 





public class DynamicTestObject : MonoBehaviour
{
    enum DragTrigger {MouseLeft, MouseRight} 

    // parameters 
    [SerializeField] DragTrigger dragTrigger; 
    // connections 
    new Camera camera; 
    // data 
    Vector2 velocity; 
    bool isDragging; 
    Vector2 dragOffset; 


    // Start is called before the first frame update
    protected void Start()
    {
        camera = Camera.main; 
    }

    // Update is called once per frame
    protected void Update()
    {
        UpdateDragging(); 
    }





    //  Dragging  --------------------------------------------------- 
    void UpdateDragging () 
    {
        if (DragIsPressed()) 
        {
            if (!isDragging) StartDrag(); 
            Drag(); 
        }
        else 
        {
            if (isDragging) StopDrag(); 
        }
    }

    bool DragIsPressed () 
    {
        switch (dragTrigger) 
        {
            case DragTrigger.MouseLeft: 
                return Mouse.current.leftButton.isPressed; 
            case DragTrigger.MouseRight: 
                return Mouse.current.rightButton.isPressed; 
            default: 
                throw new UnityException("Not all options added"); 
        }
    }

    void StartDrag () 
    {
        dragOffset = (Vector2) transform.position - GetMousePos(); 
        isDragging = true; 
    }

    void Drag () 
    {
        Vector2 newPosition = GetMousePos() + dragOffset; 
        MoveTo(newPosition); 
    }

    void StopDrag () 
    {
        isDragging = false; 
    }

    Vector2 GetMousePos () 
    {
        return camera.ScreenToWorldPoint(
            Mouse.current.position.ReadValue() 
        ); 
    }





    //  Motion  ----------------------------------------------------- 
    public Vector2 Velocity => velocity; 
    
    void MoveTo (Vector2 newPosition) 
    {
        Vector2 motion = newPosition - (Vector2) transform.position; 
        velocity = motion / Time.deltaTime; 

        transform.position = newPosition; 
    }

}
