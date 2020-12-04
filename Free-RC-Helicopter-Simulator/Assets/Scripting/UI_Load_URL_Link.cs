using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Load_URL_Link : MonoBehaviour
{
    public void OpenGitHub()
    {
        Application.OpenURL("https://github.com/zulugithub/Free-RC-Helicopter-Simulator");
    }

    public void OpenBlender()
    {
        Application.OpenURL("https://www.blender.org/");
    }

    public void OpenUnity()
    {
        Application.OpenURL("https://unity.com/");
    }
}
