using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 endPosition = new Vector3(-3,7,0);
    Vector3 originalPosition;
    bool mouseDown = false; 
    int tick = 50;

    void Start() {
        originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        Debug.Log("here");
        mouseDown = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            transform.position = originalPosition;
            Quaternion upright = Quaternion.Euler(0,0,0);
            transform.rotation = upright;
        }
        if (mouseDown) {
            transform.position += (endPosition-transform.position)/tick;
            transform.Rotate(0,0,180f/(tick*5));
        }
        
    }
}