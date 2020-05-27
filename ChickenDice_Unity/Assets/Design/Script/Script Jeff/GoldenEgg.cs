using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldenEgg : MonoBehaviour
{
    [SerializeField] public int _CounterPlayer01 = 0;
    [SerializeField] public int _CounterPlayer02 = 0;
    [SerializeField] public int _CounterPlayer03 = 0;
    [SerializeField] public int _CounterPlayer04 = 0;

    [SerializeField] private int _ValeurToWin = 0;
    [SerializeField] private float _TimeBeforeRestart = 0f;

    [SerializeField] private GameObject _GoldenEgg = null;
    //[SerializeField] private Transform _TargetFollowStart = null;
    [SerializeField] private GameObject _TargetFollow = null;

    [SerializeField] private bool _IsPick = false;
   
    private void FixedUpdate()
    {
        if(_IsPick == true)
        {
            _GoldenEgg.transform.position = _TargetFollow.transform.position + new Vector3(0,1,0);

            if(_TargetFollow.GetComponent<sc_LifeEngine>()._life <= 0)
            {
                //_GoldenEgg.transform.position = _TargetFollowStart.position;
                _IsPick = false;
            }

            if(_TargetFollow.GetComponent<sc_Peck>()._id == 0)
            {
                _CounterPlayer01 = _CounterPlayer01 + 1;
            }
            if (_TargetFollow.GetComponent<sc_Peck>()._id == 1)
            {
                _CounterPlayer02 = _CounterPlayer01 + 1;
            }
            if (_TargetFollow.GetComponent<sc_Peck>()._id == 2)
            {
                _CounterPlayer03 = _CounterPlayer01 + 1;
            }
            if (_TargetFollow.GetComponent<sc_Peck>()._id == 3)
            {
                _CounterPlayer04 = _CounterPlayer01 + 1;
            }

        }

        if(_TargetFollow == null)
        {
            _IsPick = false;
        }


        if (_CounterPlayer01 >= _ValeurToWin)
        {
            //Afficher player 1 WIN
            StartCoroutine(RestartGame());
        }
        if (_CounterPlayer02 >= _ValeurToWin)
        {
            //Afficher player 2 WIN
            StartCoroutine(RestartGame());
        }
        if (_CounterPlayer03 >= _ValeurToWin)
        {
            //Afficher player 3 WIN
            StartCoroutine(RestartGame());
        }
        if (_CounterPlayer04 >= _ValeurToWin)
        {
            //Afficher player 4 WIN
            StartCoroutine(RestartGame());
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
