using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHelp : MonoBehaviour
{
    public GameObject HELP_UI;

    public void OpenHelpWindow() {
        HELP_UI.SetActive(true);
    }
}
