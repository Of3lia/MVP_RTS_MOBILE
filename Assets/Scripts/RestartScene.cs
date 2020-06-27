﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void RestartSceneButton()
    {
        StepCounter.currentStep = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
