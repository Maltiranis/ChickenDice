using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc_Fondu : MonoBehaviour
{
    [SerializeField] private GameObject _CanvasActive = null;
    [SerializeField] private GameObject _Fondu = null;
    [SerializeField] private GameObject _AnimationCanvas = null;
    [SerializeField] private bool _FonduActive;
    [SerializeField] private float _SpeedCoroutine;
    [SerializeField] private float _SpeedFondu;
    [SerializeField] private float _Index;
    private void Update()
    {
        if(_CanvasActive.activeSelf && _FonduActive == false && _Index < 1)
        {
            _FonduActive = true;
            StartCoroutine(StartFondu());
        }

        if (!_CanvasActive.activeSelf && _Index > 0)
        {
            _Index = 0;
            var myMat = _Fondu.GetComponent<Image>().material;
            myMat.SetFloat("_fondu",_Index);
            _AnimationCanvas.SetActive(false);
        }
    }


    private IEnumerator StartFondu()
    {
        var myMat = _Fondu.GetComponent<Image>().material;

        yield return new WaitForSeconds(_SpeedCoroutine);

        if(_Index < 1)
        {
            //Debug.Log("nombre de lancement");
            _Index = _Index + _SpeedFondu;
            myMat.SetFloat("_fondu",_Index);
            StartCoroutine(StartFondu());
        }
        else
        {
            _FonduActive = false;
            _AnimationCanvas.SetActive(true);

        }
    }

    private IEnumerator BackFondu()
    {
        var myMat = _Fondu.GetComponent<Image>().material;

        yield return new WaitForSeconds(_SpeedCoroutine);

        if (_Index > 0)
        {
            //Debug.Log("nombre de lancement");
            _Index = _Index - _SpeedFondu;
            myMat.SetFloat("_fondu", _Index);
            StartCoroutine(BackFondu());
        }
        else
        {
            _FonduActive = false;
        }
    }
}
