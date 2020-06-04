using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_AutoCaster_AI : MonoBehaviour
{
    //dégât, porté, speed, scale cooldown, délais
    [Header("Minimal bullet")]
    [SerializeField] private int _minimalDamage = 10;
    [SerializeField] private float _minimalRange = 10f;
    [SerializeField] private float _minimalSpeed = 10f;
    [SerializeField] private float _minimalRadius = 1f;
    [SerializeField] private float _coolDown = 1f;
    [SerializeField] private float _minimalLifeTime = 1f;

    [Header("Offsets")]
    public GameObject _shootOffset_0;
    public GameObject _shootOffset_30;
    public GameObject _shootOffset_120;
    public GameObject _shootOffset_180;
    public GameObject _shootOffset_240;
    public GameObject _shootOffset_330;
    [Header("Spells")]
    [SerializeField] public GameObject[] _spellsActives;
    [SerializeField] public GameObject[] _spellsPassives;

    public GameObject _A;
    public GameObject _P1;
    public GameObject _P2;
    [Space(20)]
    [SerializeField] private bool _rotate = false;

    int a = 0;
    int p1 = 0;
    int p2 = 0;
    int impairator = 1;

    [HideInInspector] public int _id = 1;

    // Start is called before the first frame update
    void Start()
    {
        _id = gameObject.GetComponent<sc_Chicken_ID>().ID;
        StartCoroutine(AutoShoot());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpellsAssembler()
    {
        if (_spellsActives.Length > 0)
            _A = _spellsActives[a];
        if (_spellsPassives.Length > 0)
        {
            _P1 = _spellsPassives[p1];
            _P2 = _spellsPassives[p2];
        }
        //---------------------------------------------------------------------
        //Debug.Log("Spell =   A : " + a + " + P1 : " + p1 + " + P2 : " + p2);
        //---------------------------------------------------------------------
        if (a < _spellsActives.Length - 1)
            a += 1;
        else
            a = 0;
        //---------------------------------------------------------------------
        if (p1 < _spellsPassives.Length - 1)
            p1 += 1;
        else
            p1 = 0;
        //---------------------------------------------------------------------
        if (p2 < _spellsPassives.Length - 1)
            p2 += 1;
        else
            p2 = 0;

        if (p2 < _spellsPassives.Length - 1 && impairator == 1)
            p2 += 1;
        else
            p2 = 0;
        //---------------------------------------------------------------------
        impairator *= -1;
    }

    IEnumerator AutoShoot ()
    {
        yield return new WaitForSeconds(_coolDown);
        SpellsAssembler();
        if (_rotate)
            transform.Rotate(0, 45, 0);
        GetComponent<sc_SkillManagement>().s_sa.Shoot(_A, _P1, _P2, "RT");
        StartCoroutine(AutoShoot());
    }
}
