using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindows : MonoBehaviour
{
    public GameObject RGB_UI;
    public GameObject HSV_UI;
    public GameObject HSL_UI;

    public void OpenRGB() {
        RGB_UI.SetActive(true);
    }
    public void OpenHSV() {
        HSV_UI.SetActive(true);
    }
    public void OpenHSL() {
        HSL_UI.SetActive(true);
    }
    
}
