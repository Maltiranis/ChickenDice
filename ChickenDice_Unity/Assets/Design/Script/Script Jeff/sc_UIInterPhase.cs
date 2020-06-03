using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class sc_UIInterPhase : MonoBehaviour
{
    int _Id;
    public PhaseManager _ScPhaseManager;
    [Header("UI")]
    [SerializeField] private GameObject[] _NotReady;
    [SerializeField] private GameObject[] _Ready;
    [SerializeField] private GameObject[] _ModeToAffiche;
    bool _Isaffiche;


    private void Update()
    {
        if (Input.GetButton("xA_0"))
        {
            _NotReady[0].SetActive(false);
            _Ready[0].SetActive(true);
        }
        if (Input.GetButton("xA_1"))
        {
            _NotReady[1].SetActive(false);
            _Ready[1].SetActive(true);
        }
        if (Input.GetButton("xA_2"))
        {
            _NotReady[2].SetActive(false);
            _Ready[2].SetActive(true);
        }
        if (Input.GetButton("xA_3"))
        {
            _NotReady[3].SetActive(false);
            _Ready[3].SetActive(true);
        }

        if(_ScPhaseManager._ModeWin != 0 && _Isaffiche == false)
        {
            _Isaffiche = true;
            _ModeToAffiche[_ScPhaseManager._ModeWin -1].SetActive(true);
        }

        // POUR 4 PLAYER
        if(_Ready[0].activeSelf && _Ready[1].activeSelf && _Ready[2].activeSelf && _Ready[3].activeSelf)
        {
            _ScPhaseManager.PhaseSetup();
            _Isaffiche = false;
        }
        //POUR TEST A 1 Player
        if (Input.GetButton("Start"))
        {
            _ScPhaseManager.PhaseSetup();

        }
        
    }


}
