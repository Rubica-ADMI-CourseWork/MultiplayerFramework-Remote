using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginUtility : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] TMP_Text loginBtnText;
    [SerializeField] LaunchManager launchManager;

    public void LoginPlayer()
    {
        loginBtnText.text = "CONNECTING....";

        string name = "";

        if (string.IsNullOrEmpty(nameInput.text))
        {
            name = "default player";
        }

        else
        {
            name = nameInput.text;
        }

        if (launchManager != null)
        {
            launchManager.ConnectToServerWithName(name);

        }
    }
}
