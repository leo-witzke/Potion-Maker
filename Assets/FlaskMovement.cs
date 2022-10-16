using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskMovement : MonoBehaviour
{
    public GameObject FlowingLiquid;

    bool mouseDown = false; 
    string moveFlask = "Move Flask";
    bool instantiated = false;

    void OnMouseDown()
    {
        GetComponent<Animation>().Play(moveFlask);
        mouseDown = true;
    }

    void OnMouseUp()
    {
        GetComponent<Animation>().Stop(moveFlask);
        this.transform.localPosition = new Vector3(0,0,0);
        this.transform.rotation = Quaternion.Euler(0,0,0);
        mouseDown = false;
        instantiated = false;
    }

    void Update()
    {
        if (mouseDown && !GetComponent<Animation>().IsPlaying(moveFlask)) {
            if (!instantiated) {
                GameObject flowing = Instantiate(FlowingLiquid, new Vector3(-3.25f,5,0), Quaternion.identity);
                flowing.GetComponent<ScaleLiquid>().ColorRepresentation = transform.Find("Liquid").gameObject.GetComponent<ColorRepresentation>().ColorRep;
                flowing.GetComponent<ScaleLiquid>().source = transform.Find("Liquid").gameObject;
                instantiated = true;
            }
        }
    }
}