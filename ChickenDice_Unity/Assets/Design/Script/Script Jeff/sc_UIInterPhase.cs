using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class sc_UIInterPhase : MonoBehaviour
{
    int _Id;
    public PhaseManager _ScPhaseManager;
    public sc_Fondu[] _ScFondu;
    private bool _havePressSelect;
    [Header("UI")]
    [SerializeField] private GameObject[] _NotReady;
    [SerializeField] private GameObject[] _Ready;
    [SerializeField] private GameObject[] _ModeToAffiche;
    [SerializeField] private GameObject _CanvasReady;

    [Header("SFX")]
    [SerializeField] private AudioSource[] _SFX;
    bool _Isaffiche;

    private void Start()
    {
     
    }
    private void Update()
    {
        if (Input.GetButton("Start_0") && !_Ready[0].activeSelf && _Isaffiche == true)
        {
            _NotReady[0].SetActive(false);
            _Ready[0].SetActive(true);
            _SFX[0].Play();
        }
        if (Input.GetButton("Start_1") && !_Ready[1].activeSelf && _Isaffiche == true)
        {
            _NotReady[1].SetActive(false);
            _Ready[1].SetActive(true);
            _SFX[0].Play();
        }
        if (Input.GetButton("Start_2") && !_Ready[2].activeSelf && _Isaffiche == true)
        {
            _NotReady[2].SetActive(false);
            _Ready[2].SetActive(true);
            _SFX[0].Play();
        }
        if (Input.GetButton("Start_3") && !_Ready[3].activeSelf && _Isaffiche == true)
        {
            _NotReady[3].SetActive(false);
            _Ready[3].SetActive(true);
            _SFX[0].Play();
        }

        if (_ScPhaseManager._ModeWin != 0 && _Isaffiche == false)
        {
            _CanvasReady.SetActive(true);
            _Isaffiche = true;
            _ModeToAffiche[_ScPhaseManager._ModeWin -1].SetActive(true);
        }

        // POUR 4 PLAYER
        if(_Ready[0].activeSelf && _Ready[1].activeSelf && _Ready[2].activeSelf && _Ready[3].activeSelf && _havePressSelect == false)
        {
            _SFX[1].Play();
            _Isaffiche = false;
            _havePressSelect = true;
            _ScFondu[_ScPhaseManager._ModeWin - 1].BackAnim();
            StartCoroutine(_ScPhaseManager.PhaseSetup());
        }
        //POUR TEST A 1 Player
        if (Input.GetButtonDown("Select") && _havePressSelect == false)
        {
            _SFX[1].Play();
            _havePressSelect = true;
            _ScFondu[_ScPhaseManager._ModeWin - 1].BackAnim();
            StartCoroutine(_ScPhaseManager.PhaseSetup());
        }
        
    }


}
