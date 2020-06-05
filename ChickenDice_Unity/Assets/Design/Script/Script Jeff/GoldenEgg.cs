using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GoldenEgg : MonoBehaviour
{

    [SerializeField] private float _ValeurToWin = 0f;
    [SerializeField] private float _TimeBeforeSetPoint = 0f;
    [SerializeField] private GameObject _GoldenEgg = null;
   
    [Header("auto")]
    [SerializeField] private GameObject _TargetFollow = null;
    [SerializeField] private bool _PlayerWinPoint = false;
    [SerializeField] private bool _IsPick = false;
    private bool _havewin;
    [SerializeField] public float[] _CounterPlayer;
    [SerializeField] public int _IdPlayerHaveEgg;
    [Header("UI")]
    [SerializeField] private Image[] _bar;  
    [SerializeField] private TextMeshProUGUI[] _PointText;
    [SerializeField] private GameObject _UIMode;

    [Header("UI WINER")]
    [SerializeField] private GameObject _CanvasWiner;
    [SerializeField] private GameObject[] _UIWiner;
    [SerializeField] private Image[] _barInWin;
    [SerializeField] private TextMeshProUGUI[] _PointTextInWin;

    private void Update()
    {
        if(_TargetFollow == null)
        {
            _IsPick = false;
        }

        if(_IsPick == true)
        {
           

            _GoldenEgg.transform.position = _TargetFollow.transform.position + new Vector3(0,1,0);

            if (_PlayerWinPoint == false)
            {
                _PlayerWinPoint = true;
                StartCoroutine(SetPoint());
            }

            ////LA POULE CREVE ELLE PERD L'OEUF
            //if(_TargetFollow.GetComponent<sc_LifeEngine>()._life <= 0)
            //{
            //    _IsPick = false;
            //}

            //LA POULE SE FAIT TOUCHER ELLE PERD L'OEUF
            if (_TargetFollow.GetComponentInChildren<sc_AnimManagement>()._onHit == true)
            {
                _GoldenEgg.transform.position = new Vector3(_GoldenEgg.transform.position.x, 0.5f, _GoldenEgg.transform.position.z);
                _IsPick = false;

            }

        }
    }

    private IEnumerator SetPoint()
    {
        yield return new WaitForSeconds(_TimeBeforeSetPoint);

        if(_IsPick == true)
        {
            //Set 1 Point + en UI
            if (_CounterPlayer[_IdPlayerHaveEgg] < _ValeurToWin && _havewin == false)
            {
                _CounterPlayer[_IdPlayerHaveEgg] = _CounterPlayer[_IdPlayerHaveEgg] + 1;
                _PointText[_IdPlayerHaveEgg].text = _CounterPlayer[_IdPlayerHaveEgg].ToString();
                _PointTextInWin[_IdPlayerHaveEgg].text = _PointText[_IdPlayerHaveEgg].text;
                float Amont = _CounterPlayer[_IdPlayerHaveEgg] /_ValeurToWin;
                _bar[_IdPlayerHaveEgg].fillAmount = Amont;
                _barInWin[_IdPlayerHaveEgg].fillAmount = Amont;
            }
            // Si ke joueur a Gagné
            if(_CounterPlayer[_IdPlayerHaveEgg] >= _ValeurToWin && _havewin == false)
            {
                _havewin = true;
                _UIMode.SetActive(false);
                _CanvasWiner.SetActive(true);
                _UIWiner[_IdPlayerHaveEgg].SetActive(true);
            }
            //Si le joueur a toujour l'oeuf + pas  gagné
            else
            {
                StartCoroutine(SetPoint());
            }
        }else
        {
            _PlayerWinPoint = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.GetComponent<sc_Peck>()._id);

        if (other.gameObject.GetComponent<sc_Peck>() != null && _IsPick == false)
        {
            _TargetFollow = other.gameObject;
            _IdPlayerHaveEgg = _TargetFollow.gameObject.GetComponentInParent<sc_Chicken_ID>().ID;
            _IsPick = true;
        }
        
    }
}
