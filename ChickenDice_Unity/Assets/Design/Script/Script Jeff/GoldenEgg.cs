using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GoldenEgg : MonoBehaviour
{

    [SerializeField] public float[] _CounterPlayer;
    [SerializeField] public int _IdPlayerHaveEgg;

    [SerializeField] private float _ValeurToWin = 0f;
    [SerializeField] private float _TimeBeforeRestart = 0f;
    [SerializeField] private float _TimeBeforeSetPoint = 0f;

    [SerializeField] private GameObject _GoldenEgg = null;
    [SerializeField] private GameObject _TargetFollow = null;
    [SerializeField] private bool _IsPick = false;
    [SerializeField] private bool _PlayerWinPoint = false;

    [Header("UI")]
    [SerializeField] private Image[] _bar;  
    [SerializeField] private TextMeshProUGUI[] _PointText;
    [SerializeField] private GameObject[] _UI;

    private void FixedUpdate()
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
                _IdPlayerHaveEgg = _TargetFollow.gameObject.GetComponentInParent<sc_Chicken_ID>().ID;
                StartCoroutine(SetPoint());
            }

            //LA POULE CREVE ELLE PERD L'OEUF
            if(_TargetFollow.GetComponent<sc_LifeEngine>()._life <= 0)
            {
                _IsPick = false;
            }

            //LA POULE SE FAIT TOUCHER ELLE PERD L'OEUF
            if (_TargetFollow.GetComponentInChildren<sc_AnimManagement>()._onHit == true)
            {
                _IsPick = false;

            }

        }
    }

    private IEnumerator SetPoint()
    {
        yield return new WaitForSeconds(_TimeBeforeSetPoint);

        if(_IsPick == true)
        {
            _CounterPlayer[_IdPlayerHaveEgg] = _CounterPlayer[_IdPlayerHaveEgg] + 1;
            _PointText[_IdPlayerHaveEgg].text = _CounterPlayer[_IdPlayerHaveEgg].ToString();
            float Amont = _CounterPlayer[_IdPlayerHaveEgg] /_ValeurToWin;
            _bar[_IdPlayerHaveEgg].fillAmount = Amont;
            if(_CounterPlayer[_IdPlayerHaveEgg] >= _ValeurToWin)
            {
                StartCoroutine(RestartGame());
            }else
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
        Debug.Log(other.gameObject.GetComponent<sc_Peck>()._id);

        if (other.gameObject.GetComponent<sc_Peck>() != null && _IsPick == false)
        {
            _TargetFollow = other.gameObject;
            _IsPick = true;
        }
        
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(_TimeBeforeRestart);
        SceneManager.LoadScene("Scenes_Jeff");
    }

}
