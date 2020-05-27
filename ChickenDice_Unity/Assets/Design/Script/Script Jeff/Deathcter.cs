using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathcter : MonoBehaviour
{
    [SerializeField] private GameObject _Player01 = null;
    [SerializeField] private GameObject _Player02 = null;
    [SerializeField] private GameObject _Player03 = null;
    [SerializeField] private GameObject _Player04 = null;

    [SerializeField] public int _CounterPlayer01 = 0;
    [SerializeField] public int _CounterPlayer02 = 0;
    [SerializeField] public int _CounterPlayer03 = 0;
    [SerializeField] public int _CounterPlayer04 = 0;

    [SerializeField] private int _ValeurToWin = 0;
    [SerializeField] private float _TimeBeforeRestart = 0f;

    [SerializeField] private PhaseManager _scPhaseManagement;

    private void Start()
    {
        _Player01 = _scPhaseManagement.GetComponent<PhaseManager>()._Player01;
        _Player02 = _scPhaseManagement.GetComponent<PhaseManager>()._Player02;
        _Player03 = _scPhaseManagement.GetComponent<PhaseManager>()._Player03;
        _Player04 = _scPhaseManagement.GetComponent<PhaseManager>()._Player04;
        
    }

    private void FixedUpdate()
    {
        //MODE SURVIE
        if(_Player01.GetComponent<sc_LifeEngine>()._life <= 0)
        {
            _CounterPlayer01 = +1;
        }
    }

}
