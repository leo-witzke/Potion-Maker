using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static ColorConversions;
using TMPro;

public class BeakerColorChange : MonoBehaviour
{
    public GameObject goodBeaker;
    public GameObject badBeaker;

    public GameObject goodSimilarity;
    public GameObject badSimilarity;

    public GameObject winScreen;
    public GameObject loseScreen;

    float rate = 0.1f;

    void OnTriggerStay2D(Collider2D col)
    {
        string ColorRep = col.gameObject.GetComponent<ScaleLiquid>().ColorRepresentation;
        Color LiquidColor = col.gameObject.GetComponent<SpriteRenderer>().material.color;
        Color BeakerColor = gameObject.GetComponent<SpriteRenderer>().color;

        void shiftHSV(float h, float s, float v) { 
            Vector3 HSVShift = new Vector3(h,s,v);
            Vector3 HSV = new Vector3(0,0,0);
            Color.RGBToHSV(BeakerColor, out HSV.x, out HSV.y, out HSV.z);
            HSV += HSVShift*rate*Time.deltaTime;
            HSV = checkHueModel(HSV);
            gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(HSV.x, HSV.y, HSV.z);
        }

        void shiftHSL(float h, float s, float l) {
            Vector3 HSLShift = new Vector3(h,s,l);
            gameObject.GetComponent<SpriteRenderer>().color = checkRGB(HSLtoRGB(RGBtoHSL(BeakerColor) + HSLShift*rate*Time.deltaTime));
        }

        if (ColorRep.IndexOf("RGB") == 0) {
            gameObject.GetComponent<SpriteRenderer>().color = checkRGB(gameObject.GetComponent<SpriteRenderer>().color - (Color.white-2*LiquidColor)*(Time.deltaTime*rate));
        }
        else if (ColorRep.IndexOf("HSV") == 0) {
            if (ColorRep.IndexOf("Hue") != -1) { shiftHSV(1,0,0); }
            else if (ColorRep.IndexOf("Saturation") != -1) { shiftHSV(0,1,0); }
            else if (ColorRep.IndexOf("Value") != -1) { shiftHSV(0,0,1); }
        } else if (ColorRep.IndexOf("HSL") == 0) {
            if (ColorRep.IndexOf("Hue") != -1) { shiftHSL(1,0,0); }
            else if (ColorRep.IndexOf("Saturation") != -1) { shiftHSL(0,1,0); }
            else if (ColorRep.IndexOf("Lightness") != -1) { shiftHSL(0,0,1); }
        }
    }

    int similarity(Color ColorOne, Color ColorTwo)
    {
        Color ColorDifferenceRGB = ColorOne - ColorTwo;
        Vector3 ColorOneHSV = new Vector3(0,0,0);
        Vector3 ColorTwoHSV = new Vector3(0,0,0);
        Color.RGBToHSV(ColorOne, out ColorOneHSV.x, out ColorOneHSV.y, out ColorOneHSV.z);
        Color.RGBToHSV(ColorTwo, out ColorTwoHSV.x, out ColorTwoHSV.y, out ColorTwoHSV.z);
        Vector3 ColorDifferenceHSV = ColorOneHSV - ColorTwoHSV;
        Vector3 ColorDifferenceHSL = RGBtoHSL(ColorOne) - RGBtoHSL(ColorTwo);
        // float similar = Math.Min(
        //     Math.Min(Math.Abs(ColorDifferenceRGB.r) + Math.Abs(ColorDifferenceRGB.g) + Math.Abs(ColorDifferenceRGB.b),
        //     Math.Min(Math.Abs(ColorDifferenceHSL.x), 1-Math.Abs(ColorDifferenceHSL.x)) + Math.Abs(ColorDifferenceHSV.y) + Math.Abs(ColorDifferenceHSV.z)),
        //     Math.Min(Math.Abs(ColorDifferenceHSL.x), 1-Math.Abs(ColorDifferenceHSL.x)) + Math.Abs(ColorDifferenceHSL.y) + Math.Abs(ColorDifferenceHSL.z)
        // );
        float similar = Math.Abs(ColorDifferenceRGB.r) + Math.Abs(ColorDifferenceRGB.g) + Math.Abs(ColorDifferenceRGB.b);
        return (int)(100*(1-similar/3));
    }

    void setIndicatorColor(Color color)
    {
        GameObject indicator = GameObject.Find("Neutral Indicator");
        indicator.GetComponent<SpriteRenderer>().color = color;
    }
    
    void clearScreenColors() {
        if (goodSimilarity != null) {
            goodSimilarity.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        if (badSimilarity != null) {
            badSimilarity.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }

    void win() {
        clearScreenColors();
        setIndicatorColor(Color.green);
        goodSimilarity.GetComponent<TextMeshProUGUI>().color = Color.green;
        if (GameObject.Find("FlowingLiquid(Clone)") == null) {
            StartCoroutine(fadeScreen(winScreen));
        }
    }

    void lose() {
        clearScreenColors();
        setIndicatorColor(Color.red);
        badSimilarity.GetComponent<TextMeshProUGUI>().color = Color.red;
        StartCoroutine(fadeScreen(loseScreen));
    }

    void neutral() {
        setIndicatorColor(new Color32(171,171,171,255));
        clearScreenColors();
    }

    IEnumerator fadeScreen(GameObject screen) {
        void setImageAlpha(float alpha) {
            Color temp = screen.transform.Find("Walter White").gameObject.GetComponent<SpriteRenderer>().color;
            temp.a = alpha;
            screen.transform.Find("Walter White").gameObject.GetComponent<SpriteRenderer>().color = temp;
        }
        screen.SetActive(true);
        float timeToFade = 3;
        for (float i = 0; i <= timeToFade; i += Time.deltaTime)
        {
            screen.GetComponent<CanvasGroup>().alpha = i/timeToFade;
            setImageAlpha(i/timeToFade);
            yield return null;
        }
    }

    void Update() {
        int goodSimilarPercent;
        int badSimilarPercent;
        
        if (goodSimilarity != null) {
            goodSimilarPercent = similarity(GetComponent<SpriteRenderer>().color, goodBeaker.transform.Find("BeakerLiquid").GetComponent<SpriteRenderer>().color);
            goodSimilarity.GetComponent<TextMeshProUGUI>().text = "Good Beaker: "+goodSimilarPercent;
        } else {
            goodSimilarPercent = 0;
        }
        if (badSimilarity != null) {
            badSimilarPercent = similarity(GetComponent<SpriteRenderer>().color, badBeaker.transform.Find("BeakerLiquid").GetComponent<SpriteRenderer>().color);
            badSimilarity.GetComponent<TextMeshProUGUI>().text = "Bad Beaker: "+badSimilarPercent;
        } else {
            badSimilarPercent = 0;
        }

        if (goodBeaker != null && goodSimilarPercent >= 90 && badSimilarPercent < 90) {
            win();
        } else if (badBeaker != null && badSimilarPercent >= 90 && goodSimilarPercent < 90) {
            lose();
        } else if (badSimilarPercent >= 90 && goodSimilarPercent >= 90) {
            if (badSimilarPercent >= goodSimilarPercent) {
                lose();
            } else {
                win();
            }
        } else if (goodSimilarPercent != 0 || badSimilarPercent != 0) {
            neutral();
        }
    }
}
