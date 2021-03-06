﻿using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour
{
    public GameObject pause;
    public GameObject box;

    private bool isPaused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }
        if (!isPaused)
        {
            box.transform.Rotate(0, 1, 0);
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
        isPaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
        isPaused = false;
    }

}
