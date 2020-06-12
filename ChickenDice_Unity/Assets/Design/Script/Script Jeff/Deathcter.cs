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

    private bool _P01IsDeath;
    private bool _P02IsDeath;
    private bool _P03IsDeath;
    private bool _P04IsDeath;
    private GameObject _PlayerWin;
    private bool _havewin;

    [SerializeField] private float _ValeurToWin = 0;
    [SerializeField] private float _TimeRespawnPlayer = 5.0f;
    private float _TimeBeforeRestart = 0f;

    [SerializeField] private PhaseManager _scPhaseManagement;
    [Header("UI")]
    [SerializeField] private Image[] _bar;
    [SerializeField] private TextMeshProUGUI[] _PointText;
    [SerializeField] private GameObject _UIMode;

    [Header("UI WINER")]
    [SerializeField] private GameObject[] _CameraWiner;
    [SerializeField] private GameObject _CanvasWiner;
    [SerializeField] private GameObject[] _UIWiner;
    [SerializeField] private Image[] _barInWin;
    [SerializeField] private TextMeshProUGUI[] _PointTextInWin;


    private void FixedUpdate()
    {
        if (_Player01 == null)
        {
            _Player01 = GameObject.Find("A Chicken numeroted 0");
        }
        if (_Player02 == null)
        {
            _Player02 = GameObject.Find("A Chicken numeroted 1");
        }
        if (_Player03 == null)
        {
            _Player03 = GameObject.Find("A Chicken numeroted 2");
        }
        if (_Player04 == null)
        {
            _Player04 = GameObject.Find("A Chicken numeroted 3");
        }

        if (_Player01.GetComponent<sc_LifeEngine>()._life <= 0 && _P01IsDeath == false)
        {
            _P01IsDeath = true;
            _TheKillerID = _Player01.GetComponent<sc_LifeEngine>()._killer_ID;
            StartCoroutine(SetPoint0());
        }
        if (_Player02.GetComponent<sc_LifeEngine>()._life <= 0 && _P02IsDeath == false)
        {
            _P02IsDeath = true;
            _TheKillerID = _Player02.GetComponent<sc_LifeEngine>()._killer_ID;
            StartCoroutine(SetPoint1());
        }
        if (_Player03.GetComponent<sc_LifeEngine>()._life <= 0 && _P03IsDeath == false)
        {
            _P03IsDeath = true;
            _TheKillerID = _Player03.GetComponent<sc_LifeEngine>()._killer_ID;
            StartCoroutine(SetPoint2());
        }
        if (_Player04.GetComponent<sc_LifeEngine>()._life <= 0 && _P04IsDeath == false)
        {
            _P04IsDeath = true;
            _TheKillerID = _Player04.GetComponent<sc_LifeEngine>()._killer_ID;
            StartCoroutine(SetPoint3());
        }
    }

    private IEnumerator SetPoint0()
    {
        if (_CounterPlayer[_TheKillerID] < _ValeurToWin && _havewin == false) {
            _CounterPlayer[_TheKillerID] = _CounterPlayer[_TheKillerID] + 1;
            _PointText[_TheKillerID].text = _CounterPlayer[_TheKillerID].ToString();
            _PointTextInWin[_TheKillerID].text = _PointText[_TheKillerID].text;
            float Amont = _CounterPlayer[_TheKillerID] / _ValeurToWin;
            _bar[_TheKillerID].fillAmount = Amont;
            _barInWin[_TheKillerID].fillAmount = Amont;
        }

        yield return new WaitForSeconds(_TimeRespawnPlayer);
        _P01IsDeath = false;
        if(_CounterPlayer[_TheKillerID] >= _ValeurToWin && _havewin == false)
        {
            // Afficher le player qui win
            _havewin = true;
            _CanvasWiner.SetActive(true);
            _UIWiner[_TheKillerID].SetActive(true);
            _PlayerWin = GameObject.Find("A Chicken numeroted "+_TheKillerID.ToString());
            StartCoroutine(TpPlayerWin());
            _UIMode.SetActive(false);
            _CameraWiner[_TheKillerID].SetActive(true);
        }        
    }
    private IEnumerator SetPoint1()
    {
        if(_CounterPlayer[_TheKillerID] < _ValeurToWin && _havewin == false) {
            _CounterPlayer[_TheKillerID] = _CounterPlayer[_TheKillerID] + 1;
            _PointText[_TheKillerID].text = _CounterPlayer[_TheKillerID].ToString();
            _PointTextInWin[_TheKillerID].text = _PointText[_TheKillerID].text;
            float Amont = _CounterPlayer[_TheKillerID] / _ValeurToWin;
            _bar[_TheKillerID].fillAmount = Amont;
            _barInWin[_TheKillerID].fillAmount = Amont;
        }

        yield return new WaitForSeconds(_TimeRespawnPlayer);
        _P02IsDeath = false;
        if(_CounterPlayer[_TheKillerID] >= _ValeurToWin && _havewin == false)
        {
            // Afficher le player qui win
            _havewin = true;
            _CanvasWiner.SetActive(true);
            _UIWiner[_TheKillerID].SetActive(true);
            _UIMode.SetActive(false);
            _PlayerWin = GameObject.Find("A Chicken numeroted " + _TheKillerID.ToString());
            StartCoroutine(TpPlayerWin());
            _CameraWiner[_TheKillerID].SetActive(true);
        }        
    }
    private IEnumerator SetPoint2()
    {
        if (_CounterPlayer[_TheKillerID] < _ValeurToWin && _havewin == false)
        {
            _CounterPlayer[_TheKillerID] = _CounterPlayer[_TheKillerID] + 1;
            _PointText[_TheKillerID].text = _CounterPlayer[_TheKillerID].ToString();
            _PointTextInWin[_TheKillerID].text = _PointText[_TheKillerID].text;
            float Amont = _CounterPlayer[_TheKillerID] / _ValeurToWin;
            _bar[_TheKillerID].fillAmount = Amont;
            _barInWin[_TheKillerID].fillAmount = Amont;
        }

        yield return new WaitForSeconds(_TimeRespawnPlayer);
        _P03IsDeath = false;
        if(_CounterPlayer[_TheKillerID] >= _ValeurToWin && _havewin == false)
        {
            // Afficher le player qui win
            _havewin = true;
            _CanvasWiner.SetActive(true);
            _UIWiner[_TheKillerID].SetActive(true);
            _UIMode.SetActive(false);
            _PlayerWin = GameObject.Find("A Chicken numeroted " + _TheKillerID.ToString());
            StartCoroutine(TpPlayerWin());
            _CameraWiner[_TheKillerID].SetActive(true);
        }        
    }
    private IEnumerator SetPoint3()
    {
        if (_CounterPlayer[_TheKillerID] < _ValeurToWin && _havewin == false) {
            _CounterPlayer[_TheKillerID] = _CounterPlayer[_TheKillerID] + 1;
            _PointText[_TheKillerID].text = _CounterPlayer[_TheKillerID].ToString();
            _PointTextInWin[_TheKillerID].text = _PointText[_TheKillerID].text;
            float Amont = _CounterPlayer[_TheKillerID] / _ValeurToWin;
            _bar[_TheKillerID].fillAmount = Amont;
            _barInWin[_TheKillerID].fillAmount = Amont;

        }

        yield return new WaitForSeconds(_TimeRespawnPlayer);
        _P04IsDeath = false;
        if(_CounterPlayer[_TheKillerID] >= _ValeurToWin && _havewin == false)
        {
            // Afficher le player qui win
            _havewin = true;
            _CanvasWiner.SetActive(true);
            _UIWiner[_TheKillerID].SetActive(true);
            _UIMode.SetActive(false);
            _PlayerWin = GameObject.Find("A Chicken numeroted " + _TheKillerID.ToString());
            StartCoroutine(TpPlayerWin());
            _CameraWiner[_TheKillerID].SetActive(true);
        }        
    }

    private IEnumerator TpPlayerWin()
    {
        yield return new WaitForSeconds(3f);

        _PlayerWin.transform.position = _PlayerWin.transform.position + new Vector3(_PlayerWin.transform.position.x, 100, _PlayerWin.transform.position.z);

    }

}
