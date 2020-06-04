﻿/*
SI cooldown disponible = Tire un/plusieur projectils (après délais)
qui se déplace dans l'axe R3 et qui se détruit au 1er contact
+ lance le cooldown + UI.
Si touche un Player = applique effets (Explosion) SI atteint max Range explose
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class sc_Shooting : MonoBehaviour
{
    [Header("Chicken ID")]
    [SerializeField] private sc_Chicken_ID _myID;
    int _id;
    [Space(10)]
    [Header("Objects")]
    public GameObject _projectilePrefab;
    public GameObject _shootOffset;
    [Space(10)]
    [Header("Variables")]
    private float _refreshValue1 = 1.0f;
    private float _refreshValue2 = 1.0f;
    [SerializeField] private float _refillSpeed = 0.1f;

    [SerializeField] private bool leftTriggerCD = false;
    [SerializeField] private bool rightTriggerCD = false;
    //
    [Header("Minimal bullet")]
    [SerializeField] private int _minimalDamage = 10;
    [SerializeField] private float _minimalRange = 10f;
    [SerializeField] private float _minimalSpeed = 10f;
    [SerializeField] private float _minimalRadius = 1f;
    [SerializeField] public float _coolDown = 1f;
    [SerializeField] private float _minimalLifeTime = 1f;
    //
    [Space(10)]
    [Header("UI")]
    [SerializeField] private Image[] _bar;
    [SerializeField] private TextMeshProUGUI[] _timeText;

    [SerializeField] private GameObject[] _UI;

    // Start is called before the first frame update
    void Start()
    {
        _id = _myID.ID;
        _refreshValue1 = _coolDown;
        _refreshValue2 = _coolDown;

        foreach (GameObject ui in _UI)
        {
            ui.SetActive(false);
        }
        _UI[_id].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        OnCoolDown();
    }

    public void OnCoolDown ()
    {
        if (leftTriggerCD)
        {
            float amount = _refreshValue1;
            float buttonAngle = amount * 360;

            _bar[_id].fillAmount = amount;

            if (_refreshValue1 < _coolDown)
            {
                _refreshValue1 += Time.deltaTime * _refillSpeed;
                _timeText[_id].text = (_coolDown - System.Math.Round(_refreshValue1, 1)).ToString();
            }
            if (_refreshValue1 >= _coolDown)
            {
                _refreshValue1 = _coolDown;
                _timeText[_id].text = "Fire !";
                leftTriggerCD = false;
            }
        }
        if (rightTriggerCD)
        {
            float amount = _refreshValue2;
            float buttonAngle = amount * 360;

            _bar[_id].fillAmount = amount;

            if (_refreshValue2 < _coolDown)
            {
                _refreshValue2 += Time.deltaTime * _refillSpeed;
                _timeText[_id].text = (_coolDown - System.Math.Round(_refreshValue2, 1)).ToString();
            }
            if (_refreshValue2 >= _coolDown)
            {
                _refreshValue2 = _coolDown;
                _timeText[_id].text = "Fire !";
                rightTriggerCD = false;
            }
        }
    }

    public void Shoot(GameObject A, GameObject P1, GameObject P2, string inputUsed)
    {
        GameObject shot = null;

        sc_VariablesManager a_svm = null;
        sc_VariablesManager p1_svm = null;
        sc_VariablesManager p2_svm = null;

        int dmg = 0;
        float range = 0f;
        float speed = 0f;
        float radius = 0f;
        float lifeTime = 0f;
        float explosionRadius = 0f;

        if (P1 != null)
        {
            p1_svm = P1.GetComponent<sc_VariablesManager>();
            dmg += p1_svm._damage;
            range += p1_svm._range;
            speed += p1_svm._speed;
            radius += p1_svm._radius;
            lifeTime += p1_svm._lifeTime;
            explosionRadius += p1_svm._explosionRadius;
        }
        if (P2 != null)
        {
            p2_svm = P2.GetComponent<sc_VariablesManager>();
            dmg += p2_svm._damage;
            range += p2_svm._range;
            speed += p2_svm._speed;
            radius += p2_svm._radius;
            lifeTime += p2_svm._lifeTime;
            explosionRadius += p2_svm._explosionRadius;
        }
        if (A != null)
        {
            a_svm = A.GetComponent<sc_VariablesManager>();
            if (leftTriggerCD == false && inputUsed == "LT")
            {
                shot = Instantiate(A, _shootOffset.transform.position, _shootOffset.transform.rotation);

                _refreshValue1 = 0f;
                leftTriggerCD = true;
            }
            if (rightTriggerCD == false && inputUsed == "RT")
            {
                shot = Instantiate(A, _shootOffset.transform.position, _shootOffset.transform.rotation);

                _refreshValue2 = 0f;
                rightTriggerCD = true;
            }

            dmg += a_svm._damage;
            range += a_svm._range;
            speed += a_svm._speed;
            radius += a_svm._radius;
            lifeTime += a_svm._lifeTime;
            explosionRadius += a_svm._explosionRadius;

            if (shot != null)
            {
                sc_VariablesManager s_svm = shot.GetComponent<sc_VariablesManager>();

                if (shot.GetComponent<sc_SpinningBomb>() != null)
                {
                    sc_SpinningBomb s_spb;
                    s_spb = shot.GetComponent<sc_SpinningBomb>();
                    s_spb._host = gameObject;
                }

                s_svm._damage = dmg;
                s_svm._range = range;
                s_svm._speed = speed;
                s_svm._radius = radius;
                s_svm._lifeTime = lifeTime;
                s_svm._explosionRadius = explosionRadius;
                s_svm.id = _id;
                s_svm._host = gameObject;
            }
            /*
            dmg += _minimalDamage;
            range += _minimalRange;
            speed += _minimalSpeed;
            radius += _minimalRadius;
            lifeTime += _minimalLifeTime;
            */
        }
        else
        {
            Debug.Log("Y'a rien frère !");
            shot = null;
        }
    }
}