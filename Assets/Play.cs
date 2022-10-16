using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorConversions;

public class Play : MonoBehaviour
{
    public GameObject Flask;
    public GameObject Beaker;

    public GameObject winScreen;
    public GameObject loseScreen;

    GameObject CreateFlask(Color color, string colorRep, Vector3 location)
    {
        GameObject flask = Instantiate(Flask);
        GameObject liquid = flask.transform.Find("Liquid").gameObject;
        liquid.GetComponent<SpriteRenderer>().material.color = color;
        liquid.GetComponent<ColorRepresentation>().ColorRep = colorRep;
        GameObject flaskWrapper = new GameObject();
        flaskWrapper.name = "FlaskWrapper";
        flask.transform.parent = flaskWrapper.transform;
        flaskWrapper.transform.position = location;
        return flask.gameObject;
    }

    GameObject CreateBeaker(Color color, Vector3 location, string size)
    {
        GameObject beaker = Instantiate(Beaker);
        GameObject beakerLiquid = beaker.transform.Find("BeakerLiquid").gameObject;
        beakerLiquid.GetComponent<SpriteRenderer>().color = color;
        if (size == "big") {
            beaker.transform.localScale = new Vector3(0.45f,0.45f,0);
        } else if (size == "small") {
            beaker.transform.localScale = new Vector3(0.18f,0.15f,0);
        }
        beaker.transform.position = location;
        return beaker.gameObject;
    }

    public void Scene(string difficulty)
    {
        GameObject bigBeaker = CreateBeaker(Color.white, new Vector3(-3.35f,-0.1f,0), "big");
        GameObject goodbeaker = CreateBeaker(UnityEngine.Random.ColorHSV(0f,1f,0.8f,1f,0.8f,1f), new Vector3(-1.7f,-0.77f,0), "small");
        GameObject bigBeakerLiquid = bigBeaker.transform.Find("BeakerLiquid").gameObject;
        bigBeakerLiquid.GetComponent<BeakerColorChange>().goodBeaker = goodbeaker;
        bigBeakerLiquid.GetComponent<BeakerColorChange>().goodSimilarity = GameObject.Find("Good Beaker");
        if (difficulty == "hard") {
            GameObject badbeaker = CreateBeaker(UnityEngine.Random.ColorHSV(0f,1f,0.6f,1f,0.5f,0.8f), new Vector3(-0.6f,-0.77f,0), "small");
            bigBeakerLiquid.GetComponent<BeakerColorChange>().badBeaker = badbeaker;
            bigBeakerLiquid.GetComponent<BeakerColorChange>().badSimilarity = GameObject.Find("Bad Beaker");
        } else {
            GameObject.Find("Bad Beaker").SetActive(false);
            GameObject.Find("Bad Percent").SetActive(false);
        }
        bigBeakerLiquid.GetComponent<BeakerColorChange>().winScreen = winScreen;
        bigBeakerLiquid.GetComponent<BeakerColorChange>().loseScreen = loseScreen;

        // RGB
        CreateFlask(new Color(1, 0, 0), "RGB_Red", new Vector3(4.1f, 1.25f, 0));
        CreateFlask(new Color(0, 1, 0), "RGB_Green", new Vector3(5.1f, 1.25f, 0));
        CreateFlask(new Color(0, 0, 1), "RGB_Blue", new Vector3(6.1f, 1.25f, 0));

        // HSV
        CreateFlask(Color.HSVToRGB(1, 1, 1), "HSV_Hue", new Vector3(4.1f, -0.15f, 0));
        CreateFlask(Color.HSVToRGB(0, 0, 0.5f), "HSV_Saturation", new Vector3(5.1f, -0.15f, 0)).GetComponent<ColorOscillator>().Beaker = bigBeaker;
        CreateFlask(Color.HSVToRGB(0, 0, 1), "HSV_Value", new Vector3(6.1f, -0.15f, 0));

        // HSL
        CreateFlask(HSLtoRGB(1, 1, 0.5f), "HSL_Hue", new Vector3(4.1f, -1.55f, 0));
        CreateFlask(HSLtoRGB(0.1f, 0.5f, 0.5f), "HSL_Saturation", new Vector3(5.1f, -1.55f, 0)).GetComponent<ColorOscillator>().Beaker = bigBeaker;
        CreateFlask(HSLtoRGB(0, 0, 1), "HSL_Lightness", new Vector3(6.1f, -1.55f, 0));

        gameObject.SetActive(false);
        GameObject.Find("Start Screen Background").SetActive(false);
    }
}
