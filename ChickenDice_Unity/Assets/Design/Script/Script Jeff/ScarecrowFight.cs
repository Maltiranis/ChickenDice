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
    [SerializeField] private float _TimeBeforeRestart = 0f;
    [Header("UI")]
    [SerializeField] private Image[] _bar;
    [SerializeField] private TextMeshProUGUI[] _PointText;
    [SerializeField] private GameObject[] _UI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<sc_Peck>() != null)
        {
            int IdPlayer = other.gameObject.GetComponentInParent<sc_Chicken_ID>().ID;

            Debug.Log(IdPlayer);

            _CounterPlayer[IdPlayer] = _CounterPlayer[IdPlayer] + 1;

            _PointText[IdPlayer].text = _CounterPlayer[IdPlayer].ToString();
            float Amont = _CounterPlayer[IdPlayer] / _ValeurToWin;
            _bar[IdPlayer].fillAmount = Amont;
            if (_CounterPlayer[IdPlayer] >= _ValeurToWin)
            {
                StartCoroutine(RestartGame());
            }
        }
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(_TimeBeforeRestart);
        SceneManager.LoadScene("Scenes_Jeff");
    }
}
