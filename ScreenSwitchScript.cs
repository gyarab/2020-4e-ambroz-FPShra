﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSwitchScript : MonoBehaviour
{
    public void Switch(){
        SceneManager.LoadScene("SampleScene");
    }
}
