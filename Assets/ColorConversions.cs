using UnityEngine;
using System;

public class ColorConversions
{
    public static Vector3 checkHueModel(Vector3 HueModel) {
        if (HueModel.x > 1) {
            HueModel.x -= 1;
        } else if (HueModel.x < 0) {
            HueModel.x += 1;
        }
        if (HueModel.y > 1) {
            HueModel.y = 1;
        } else if (HueModel.y < 0) {
            HueModel.y = 0;
        }
        if (HueModel.z > 1) {
            HueModel.z = 1;
        } else if (HueModel.z < 0) {
            HueModel.z = 0;
        }

        return HueModel;
    }

    public static Color checkRGB(Color RGB) {
        if (RGB.r > 1) {
            RGB.r = 1;
        } else if (RGB.r < 0) {
            RGB.r = 0;
        }
        if (RGB.g > 1) {
            RGB.g = 1;
        } else if (RGB.g < 0) {
            RGB.g = 0;
        }
        if (RGB.b > 1) {
            RGB.b = 1;
        } else if (RGB.b < 0) {
            RGB.b = 0;
        }

        RGB.a = 1;

        return RGB;
    }

    public static Color HSLtoRGB(Vector3 HSL)
    {
        HSL = checkHueModel(HSL);
        Vector3 HSV = new Vector3(0,0,0);
        HSV.x = HSL.x;
        HSV.z = (HSL.z + HSL.y*Math.Min(HSL.z,1-HSL.z));
        HSV.y = HSV.z == 0 ? 0 : 2*(1-(HSL.z/HSV.z));
        return Color.HSVToRGB(HSV.x, HSV.y, HSV.z);
    }
    public static Color HSLtoRGB(float h, float s, float l)
    {
        return HSLtoRGB(new Vector3(h,s,l));
    }

    public static Vector3 RGBtoHSL(Color RGB)
    {
        Vector3 HSV = new Vector3(0,0,0);
        Color.RGBToHSV(RGB, out HSV.x, out HSV.y, out HSV.z);
        Vector3 HSL = new Vector3(0,0,0);
        HSL.x = HSV.x;
        HSL.z = HSV.z*(1-(HSV.y/2));
        HSL.y = (HSL.z == 0 || HSL.z == 1) ? 0 : (HSV.z-HSL.z)/Math.Min(HSL.z,1-HSL.z);
        return checkHueModel(HSL);
    }
}
