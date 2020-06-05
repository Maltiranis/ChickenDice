using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class sc_Transition : MonoBehaviour
{
    [SerializeField] private Image _Fade = null;
    [SerializeField] private float _FadeSpeed = 0f;
    [SerializeField] private bool _GoFade;
    [SerializeField] private GameObject _Self;

    //void Start()
    //{
    //    StartCoroutine(Fade());
    //}

    private void Update()
    {
        if(_GoFade == false)
        {
            StartCoroutine(Fade());
            _GoFade = true;
        }
    }

    private IEnumerator Fade()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        yield return new WaitForSeconds(_FadeSpeed);
        FadeFonction();
      
    }


    //Fonction avec un fill Amount, a changer avec le shader

    private void FadeFonction()
    {
        if(_Fade.fillAmount > 0)
        {
            _Fade.fillAmount = _Fade.fillAmount + 0.1f;
            StartCoroutine(Fade());
        }

    }
}
