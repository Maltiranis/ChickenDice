using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sc_Menu : MonoBehaviour
{
    [SerializeField] private GameObject _CreditMenu = null;
    [SerializeField] private GameObject _StartMenu = null;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Credit()
    {
        _CreditMenu.SetActive(true);
        _StartMenu.SetActive(false);
    }

    public void BackToStart()
    {
        _StartMenu.SetActive(true);
        _CreditMenu.SetActive(false);

    }
}
