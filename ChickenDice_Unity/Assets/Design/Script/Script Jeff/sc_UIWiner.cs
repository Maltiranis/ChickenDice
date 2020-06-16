using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sc_UIWiner : MonoBehaviour
{
    [SerializeField] private float _TimeBeforeRestart;
    private bool _havePressSelect;
    public sc_Fondu _ScFondu;
    [Header("UI")]
    [SerializeField] private GameObject[] _NotReady;
    [SerializeField] private GameObject[] _Ready;

    [Header("SFX")]
    [SerializeField] private AudioSource[] _SFX;

    [Header("MenuPause")]
    [SerializeField] private GameObject _MenuPause;

    private void Start()
    {
        if(_MenuPause != null)
        {
            _MenuPause.SetActive(false);

        }
    }
    private void Update()
    {
        if (Input.GetButton("Start_0") && !_Ready[0].activeSelf)
        {
            _NotReady[0].SetActive(false);
            _Ready[0].SetActive(true);
            _SFX[0].Play();
        }
        if (Input.GetButton("Start_1") && !_Ready[1].activeSelf)
        {
            _NotReady[1].SetActive(false);
            _Ready[1].SetActive(true);
            _SFX[0].Play();
        }
        if (Input.GetButton("Start_2") && !_Ready[2].activeSelf)
        {
            _NotReady[2].SetActive(false);
            _Ready[2].SetActive(true);
            _SFX[0].Play();
        }
        if (Input.GetButton("Start_3") && !_Ready[3].activeSelf)
        {
            _NotReady[3].SetActive(false);
            _Ready[3].SetActive(true);
            _SFX[0].Play();
        }

        // POUR 4 PLAYER


        if (_Ready[0].activeSelf && _Ready[1].activeSelf && _Ready[2].activeSelf && _Ready[3].activeSelf && _havePressSelect == false)
        {
            _SFX[1].Play();
            StartCoroutine(RestartGame());
            _ScFondu.BackAnim();
            _havePressSelect = true;
      
        }

        //POUR TEST A 1 Player


        if (Input.GetButtonDown("Select") && _havePressSelect == false)
        {
            _SFX[1].Play();
            //Debug.Log("ok");
            _havePressSelect = true;
            _ScFondu.BackAnim();
            StartCoroutine(RestartGame());

        }
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(_TimeBeforeRestart);
        _havePressSelect = false;
        SceneManager.LoadScene(2);
    }
}
