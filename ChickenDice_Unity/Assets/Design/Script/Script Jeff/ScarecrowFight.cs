using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ScarecrowFight : MonoBehaviour
{
    [SerializeField] public float[] _CounterPlayer;
    [SerializeField] private float _ValeurToWin = 0;
    private bool _havewin;
    private GameObject _PlayerWin;
    private GameObject _PlayerhavePeck;

    [Header("UI Game")]
    [SerializeField] private Image[] _bar;
    [SerializeField] private TextMeshProUGUI[] _PointText;
    [SerializeField] private GameObject _UIMode;

    [Header("UI WINER")]
    [SerializeField] private GameObject[] _CameraWiner;
    [SerializeField] private GameObject _CanvasWiner;
    [SerializeField] private GameObject[] _UIWiner;
    [SerializeField] private Image[] _barInWin;
    [SerializeField] private TextMeshProUGUI[] _PointTextInWin;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<sc_Peck>() != null && _havewin == false)
        {
            int IdPlayer = other.gameObject.GetComponentInParent<sc_Chicken_ID>().ID;
            _PlayerhavePeck = other.gameObject;
            //SDebug.Log(IdPlayer);

            if(_CounterPlayer[IdPlayer] < _ValeurToWin && _havewin == false)
            {
                _CounterPlayer[IdPlayer] = _CounterPlayer[IdPlayer] + 1;
                _PointText[IdPlayer].text = _CounterPlayer[IdPlayer].ToString();
                _PointTextInWin[IdPlayer].text = _PointText[IdPlayer].text;
                float Amont = _CounterPlayer[IdPlayer] / _ValeurToWin;
                _bar[IdPlayer].fillAmount = Amont;
                _barInWin[IdPlayer].fillAmount = Amont;

            }
            if (_CounterPlayer[IdPlayer] >= _ValeurToWin && _havewin == false)
            {
                _havewin = true;
                _CanvasWiner.SetActive(true);
                _UIMode.SetActive(false);
                _UIWiner[IdPlayer].SetActive(true);
                _CameraWiner[IdPlayer].SetActive(true);
                //Tp de la poulet qui win
                _PlayerWin = GameObject.Find("A Chicken numeroted " + IdPlayer.ToString());
                StartCoroutine(TpPlayerWin());
            }
        }
    }

    private IEnumerator TpPlayerWin()
    {
        yield return new WaitForSeconds(3f);

        _PlayerWin.transform.position = _PlayerhavePeck.transform.position + new Vector3(_PlayerhavePeck.transform.position.x, 100, _PlayerhavePeck.transform.position.z);

    }
}
