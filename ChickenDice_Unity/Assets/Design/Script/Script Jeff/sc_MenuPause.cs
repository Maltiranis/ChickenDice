using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sc_MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject _MenuPause = null;
    int _Id;

    private void Update()
    {
        if (Input.GetButtonDown("Start_0")|| Input.GetButtonDown("Start_2") || Input.GetButtonDown("Start_2") || Input.GetButtonDown("Start_3"))
        {
            _MenuPause.SetActive(!_MenuPause.activeSelf);

            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            
        }
    }

    public void Resume()
    {
        _MenuPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
