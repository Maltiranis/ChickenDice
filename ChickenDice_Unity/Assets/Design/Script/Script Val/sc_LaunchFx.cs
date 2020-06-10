﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_LaunchFx : MonoBehaviour
{
    public GameObject[] _FXs;
    public float _fxLifetime = 2f;
    public GameObject _rootFX;

    public GameObject[] _eyes;
    public GameObject[] _dustRun;

    private void Start()
    {
        foreach (GameObject d in _dustRun)
        {
            d.SetActive(false);
        }
        foreach (GameObject e in _eyes)
        {
            e.SetActive(false);
        }

        //code hyper crade
        _dustRun[transform.parent.parent.gameObject.GetComponent<sc_Chicken_ID>().ID].SetActive(true);
    }

    public void LaunchFx (int i)
    {
        GameObject newFx = Instantiate(_FXs[i], _rootFX.transform.position, _rootFX.transform.rotation);

        Destroy(newFx, _fxLifetime);
    }

    public void SetEye (int i)//0 : mort , 1 : pleure, 2 : plisse
    {
        foreach (GameObject e in _eyes)
        {
            e.SetActive(false);
        }

        _eyes[i].SetActive(true);
        _eyes[i].GetComponent<ParticleSystem>().Play(); //sinon on prends tout
    }
}