using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorConversions;

public class ColorOscillator : MonoBehaviour
{
    public GameObject Beaker;

    void Update()
    {
        GameObject liquid = transform.Find("Liquid").gameObject;
        Color liquidColor = liquid.GetComponent<SpriteRenderer>().material.color;
        string colorRep = liquid.GetComponent<ColorRepresentation>().ColorRep;
        if (colorRep.IndexOf("Hue") != -1) {
            Vector3 HSV = new Vector3(0,0,0);
            Color.RGBToHSV(liquidColor, out HSV.x, out HSV.y, out HSV.z);
            liquid.GetComponent<SpriteRenderer>().material.color = Color.HSVToRGB(HSV.x+Time.deltaTime/5, HSV.y, HSV.z);
        } else if (colorRep.IndexOf("Saturation") != -1) {
            Color beakerLiquidColor = Beaker.transform.Find("BeakerLiquid").GetComponent<SpriteRenderer>().color;
            if (colorRep.IndexOf("HSV") == 0) {
                Vector3 HSV = new Vector3(0,0,0);
                Color.RGBToHSV(beakerLiquidColor, out HSV.x, out HSV.y, out HSV.z);
                HSV.y = 0.5f;
                HSV.z = 0.5f;
                liquid.GetComponent<SpriteRenderer>().material.color = Color.HSVToRGB(HSV.x, HSV.y, HSV.z);
            } else if (colorRep.IndexOf("HSL") == 0) {
                Vector3 HSL = RGBtoHSL(beakerLiquidColor);
                HSL.y = 0.5f;
                HSL.z = 0.5f;
                liquid.GetComponent<SpriteRenderer>().material.color = HSLtoRGB(HSL);
            }
        }
    }
}
