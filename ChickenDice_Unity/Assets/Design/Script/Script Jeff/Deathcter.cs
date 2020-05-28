using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Deathcter : MonoBehaviour
{
    [SerializeField] private GameObject _Player01 = null;
    [SerializeField] private GameObject _Player02 = null;
    [SerializeField] private GameObject _Player03 = null;
    [SerializeField] private GameObject _Player04 = null;
    [SerializeField] public float[] _CounterPlayer;
    [SerializeField] public int _TheKillerID = 0;

    private bool _P01IsDeath = true;
    private bool _P02IsDeath = true;
    private bool _P03IsDeath = true;
    private bool _P04IsDeath = true;

    [SerializeField] private float _ValeurToWin = 0;
    [SerializeField] private float _TimeRespawnPlayer = 5.0f;
    private float _TimeBeforeRestart = 0f;

    [SerializeField] private PhaseManager _scPhaseManagement;
    [Header("UI")]
    [SerializeField] private Image[] _bar;
    [SerializeField] private TextMeshProUGUI[] _PointText;
    [SerializeField] private GameObject[] _UI;


    private void Start()
    {
        _Player01 = _scPhaseManagement.GetComponent<PhaseManager>()._Player01;
        _Player02 = _scPhaseManagement.GetComponent<PhaseManager>()._Player02;
        _Player03 = _scPhaseManagement.GetComponent<PhaseManager>()._Player03;
        _Player04 = _scPhaseManagement.GetComponent<PhaseManager>()._Player04;   
    }

    private void FixedUpdate()
    {
        if(_Player01.GetComponent<sc_LifeEngine>()._life <= 0 && _P01IsDeath == true)
        {
            _P01IsDeath = false;
            _TheKillerID = _Player01.GetComponent<sc_LifeEngine>()._killer_ID;
            StartCoroutine(SetPoint());
        }
        if (_Player02.GetComponent<sc_LifeEngine>()._life <= 0 && _P02IsDeath == true)
        {
            _P02IsDeath = false;
            _TheKillerID = _Player02.GetComponent<sc_LifeEngine>()._killer_ID;
            StartCoroutine(SetPoint());
        }
        if (_Player03.GetComponent<sc_LifeEngine>()._life <= 0 && _P03IsDeath == true)
        {
            _P03IsDeath = false;
            _TheKillerID = _Player03.GetComponent<sc_LifeEngine>()._killer_ID;
            StartCoroutine(SetPoint());
        }
        if (_Player04.GetComponent<sc_LifeEngine>()._life <= 0 && _P04IsDeath == true)
        {
            _P04IsDeath = false;
            _TheKillerID = _Player04.GetComponent<sc_LifeEngine>()._killer_ID;
            StartCoroutine(SetPoint());
        }
    }

    private IEnumerator SetPoint()
    {
        _CounterPlayer[_TheKillerID] = _CounterPlayer[_TheKillerID] + 1;
        _PointText[_TheKillerID].text = _CounterPlayer[_TheKillerID].ToString();
        float Amont = _CounterPlayer[_TheKillerID] / _ValeurToWin;
        _bar[_TheKillerID].fillAmount = Amont;
        yield return new WaitForSeconds(_TimeRespawnPlayer);
        _P01IsDeath = true;
        _P02IsDeath = true;
        _P03IsDeath = true;
        _P04IsDeath = true;

        if(_CounterPlayer[_TheKillerID] >= _ValeurToWin)
        {
            // Afficher le player qui win
            StartCoroutine(RestartGame());
        }        
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(_TimeBeforeRestart);
        SceneManager.LoadScene("Scenes_Jeff");
    }
}
