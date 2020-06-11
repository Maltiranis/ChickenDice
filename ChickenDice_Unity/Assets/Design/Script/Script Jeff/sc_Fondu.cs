using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sc_Fondu : MonoBehaviour
{
    [SerializeField] private GameObject _CanvasActive = null;
    [SerializeField] private GameObject _Fondu = null;
    [SerializeField] private bool _FonduActive;
    [SerializeField] private float _SpeedCoroutine;
    [SerializeField] private float _SpeedFondu;
    [SerializeField] private float _Index;

    [Header("bool selon ou est le fondu")]
    [SerializeField] private bool _FonduForTuto;
    [SerializeField] private bool _FonduForWin;

    [SerializeField] private bool _FonduForCanvas;
    [SerializeField] private bool _FonduForStartScene;

    [Header("Animation Start")]
    [SerializeField] private Animation _Animation01 = null;
    [SerializeField] private Animation _Animation02 = null;
    [SerializeField] private Animation _Animation03 = null;
    [SerializeField] private Animation _Animation04 = null;



    private void Awake()
    {
        var myMat = _Fondu.GetComponent<Image>().material;
        myMat.SetFloat("_fondu", _Index);
        _FonduActive = false;
    }
    private void Update()
    {
        if(_FonduForCanvas == true)
        {
            if(_CanvasActive.activeSelf && _FonduActive == false && _Index < 1)
            {
                _FonduActive = true;
                StartCoroutine(StartFondu());
            }

            //if (!_CanvasActive.activeSelf && _Index > 0)
            //{
            //    _Index = 0;
            //    var myMat = _Fondu.GetComponent<Image>().material;
            //    myMat.SetFloat("_fondu", _Index);
            //}
        }

        if(_FonduForStartScene == true)
        {
            if(_FonduActive == false && _Index > 0)
            {
                _FonduActive = true;
                StartCoroutine(BackFondu());
            }
        }
    }


    public IEnumerator StartFondu()
    {
        //Debug.Log("dans la coroutine startfondu");
        //Debug.Log("le temp est a "+Time.timeScale);
        var myMat = _Fondu.GetComponent<Image>().material;

        yield return new WaitForSeconds(_SpeedCoroutine);

        if(_Index < 1)
        {
           // Debug.Log("nombre de lancement start fondu");
            _Index = _Index + _SpeedFondu;
            myMat.SetFloat("_fondu",_Index);
            StartCoroutine(StartFondu());
        }
        else
        {
            _FonduActive = false;
            StartAnim();
        }
    }

    public void StartAnim()
    {
        if (_FonduForTuto == true)
        {
            _Animation01.Play("an_ImageGame");
            _Animation02.Play("an_ImageText");
            _Animation03.Play("an_ImageControle");
            _Animation04.Play("an_ImageReady");
        }
        if (_FonduForWin == true)
        {
            _Animation01.Play("an_WinTitle");
            _Animation02.Play("an_WinStats");
            _Animation03.Play("an_WinImage");
            _Animation04.Play("an_ImageReady");
        }

    }

    public void BackAnim()
    {
        //Debug.Log("Nous somme dans la fonction BackAnim");

        if (_FonduForTuto == true)
        {
            _Animation01.Play("an_BackImageGame");
            _Animation02.Play("an_BackImageText");
            _Animation03.Play("an_BackImageControle");
            _Animation04.Play("an_BackImageReady");
        }
        if (_FonduForWin == true)
        {
            _Animation01.Play("an_BackWinTitle");
            _Animation02.Play("an_BackWinStats");
            _Animation03.Play("an_BackWinImage");
            _Animation04.Play("an_BackImageReady");
        }

    }


    public IEnumerator BackFondu()
    {
        var myMat = _Fondu.GetComponent<Image>().material;

        yield return new WaitForSeconds(_SpeedCoroutine);

        if (_Index > 0)
        {
            //Time.timeScale = 0;
            //Debug.Log("nombre de lancement");
            _Index = _Index - _SpeedFondu;
            myMat.SetFloat("_fondu", _Index);
            StartCoroutine(BackFondu());
        }
        else
        {
            //Time.timeScale = 1;
            _FonduActive = false;
        }
    }

   
}
