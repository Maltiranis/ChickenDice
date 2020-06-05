using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SelectionPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _SkinP01 = null;
    [SerializeField] private GameObject _SkinP02 = null;
    [SerializeField] private GameObject _SkinP03 = null;
    [SerializeField] private GameObject _SkinP04 = null;
    [SerializeField] private bool _canbeselect;

    [SerializeField] private GameObject _StartButton = null;
    [SerializeField] private GameObject _CanvasTuto = null;

    private void Update()
    {
        if (Input.GetButtonDown("xA_0") &&   _canbeselect == false)
        {
            _SkinP01.SetActive(!_SkinP01.activeSelf);
        }
        if (Input.GetButtonDown("xA_1") &&   _canbeselect == false)
        {
            _SkinP02.SetActive(!_SkinP01.activeSelf);           
        }
        if (Input.GetButtonDown("xA_2") &&   _canbeselect == false)
        {
            _SkinP03.SetActive(!_SkinP01.activeSelf);           
        }
        if (Input.GetButtonDown("xA_3") &&   _canbeselect == false)
        {
            _SkinP04.SetActive(!_SkinP01.activeSelf);          
        }

        if(_SkinP01.activeSelf && _SkinP02.activeSelf && _SkinP03.activeSelf && _SkinP04.activeSelf && _canbeselect == false)
        {
            _StartButton.SetActive(true);
        }
        else
        {
            _StartButton.SetActive(false);
        }

        if(Input.GetButtonDown("Start_0")&& _StartButton.activeSelf && _canbeselect == false)
        {
            //Debug.Log("start");
            CanvasTuto();
        }

        if (Input.GetButtonDown("Select") && _canbeselect == false)
        {
            //Debug.Log("select");
            CanvasTuto();
        }
    }

    public void CanvasTuto()
    {
        _canbeselect = true;
        _StartButton.SetActive(false);
        _CanvasTuto.SetActive(true);

    }
}
