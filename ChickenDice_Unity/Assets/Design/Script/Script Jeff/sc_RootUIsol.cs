﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_RootUIsol : MonoBehaviour
{
    [SerializeField] private GameObject _UILife = null;
    [SerializeField] private GameObject _VfxLife;
    [SerializeField] private GameObject _UISlot = null;
    [SerializeField] private GameObject _LePlayer = null;
    [SerializeField] private GameObject _ROOTUI = null;
    [SerializeField] private int _IDPlayer;
    private bool _Bool;
    private bool _Boolcoroutinelife;

  
    private void Update()
    {

        if (_LePlayer == null)
        {
            _LePlayer = GameObject.Find("A Chicken numeroted "+_IDPlayer);
        }
        else
        {
            _ROOTUI.transform.position = _LePlayer.transform.position;

        }
        

       if( _LePlayer.GetComponent<sc_LifeEngine>()._life <= 0)
        {

            _Bool = false;
            _UILife.SetActive(false);
            _UISlot.SetActive(false);
        }

       if (_LePlayer.GetComponent<sc_LifeEngine>()._life > 0 && _Bool == false)
       {

            StartCoroutine(SpawnUI());
       }

        if (_LePlayer.GetComponentInChildren<sc_AnimManagement>()._onHit == true)
        {
            var myMat = _VfxLife.GetComponent<Renderer>().material;

            myMat.SetFloat("_life", (float)_LePlayer.GetComponent<sc_LifeEngine>()._life / 100);
            

            if(_Boolcoroutinelife == false)
            {
                _Boolcoroutinelife = true;
                StartCoroutine(WhiteLife());
            }

        }

    }

    private IEnumerator SpawnUI()
    {
        _Bool = true;
        yield return new WaitForSeconds(0.1f);
        _UILife.SetActive(true);
        _UISlot.SetActive(true);

    }

    private IEnumerator WhiteLife()
    {

        yield return new WaitForSeconds(1f);

        var myMat = _VfxLife.GetComponent<Renderer>().material;

        myMat.SetFloat("_life2", (float)_LePlayer.GetComponent<sc_LifeEngine>()._life / 100);
        _Boolcoroutinelife = false;
    }
}
