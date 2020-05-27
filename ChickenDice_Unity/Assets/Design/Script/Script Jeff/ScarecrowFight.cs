using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScarecrowFight : MonoBehaviour
{
    [SerializeField] public int _CounterPlayer01 = 0;
    [SerializeField] public int _CounterPlayer02 = 0;
    [SerializeField] public int _CounterPlayer03 = 0;
    [SerializeField] public int _CounterPlayer04 = 0;

    [SerializeField] private int _ValeurToWin = 0;
    [SerializeField] private float _TimeBeforeRestart = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<sc_Peck>() != null)
        {
            //Debug.Log(other.gameObject.GetComponent<sc_Peck>()._id);
            int IdPlayer = other.gameObject.GetComponent<sc_Peck>()._id;

            if(IdPlayer == 0)
            {
                _CounterPlayer01 = _CounterPlayer01+1;
            }if(IdPlayer == 1)
            {
                _CounterPlayer02 = _CounterPlayer02+1;
            }if(IdPlayer == 2)
            {
                _CounterPlayer03 = _CounterPlayer03+1;
            }if(IdPlayer == 3)
            {
                _CounterPlayer04 = _CounterPlayer04+1;
            }
        }

        if(_CounterPlayer01 >= _ValeurToWin)
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

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(_TimeBeforeRestart);
        SceneManager.LoadScene("Scenes_Jeff");
    }
}
