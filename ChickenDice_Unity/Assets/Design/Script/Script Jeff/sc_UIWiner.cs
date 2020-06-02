using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sc_UIWiner : MonoBehaviour
{
    [SerializeField] private float _TimeBeforeRestart;
    [Header("UI")]
    [SerializeField] private GameObject[] _NotReady;
    [SerializeField] private GameObject[] _Ready;


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

        // POUR 4 PLAYER
        if (_Ready[0].activeSelf && _Ready[1].activeSelf && _Ready[2].activeSelf && _Ready[3].activeSelf)
        {
            StartCoroutine(RestartGame());
      
        }

        //POUR TEST A 1 Player
        if (_Ready[0].activeSelf)
        {
            StartCoroutine(RestartGame());
          
        }
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(_TimeBeforeRestart);
        SceneManager.LoadScene("Scenes_Jeff");
    }
}
