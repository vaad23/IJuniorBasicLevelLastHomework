﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightFinish : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

