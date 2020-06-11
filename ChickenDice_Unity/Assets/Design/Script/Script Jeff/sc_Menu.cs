using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class sc_Menu : MonoBehaviour
{
    [SerializeField] private GameObject _CreditMenu = null;
    [SerializeField] private GameObject _StartMenu = null;

    public GameObject _MenuFirstButton, _CreditFirstButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_MenuFirstButton);
    }
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_CreditFirstButton);
    }

    public void BackToStart()
    {
        _StartMenu.SetActive(true);
        _CreditMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_MenuFirstButton);

    }
}
