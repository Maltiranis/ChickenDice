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

    [Header("Object")]
    public GameObject _shootOffset;
    [Header("Spells")]
    [SerializeField] public GameObject[] _spellsActives;
    [SerializeField] public GameObject[] _spellsPassives;

    public GameObject _A;
    public GameObject _P1;
    public GameObject _P2;

    int a = 0;
    int p1 = 0;
    int p2 = 0;
    int impairator = 1;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoShoot());
        //index = gameObject.GetComponent<sc_Chicken_ID>().ID;
        index = 1;
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
            _P1 = _spellsActives[p1];
            _P2 = _spellsPassives[p2];
        }
        //---------------------------------------------------------------------
        Debug.Log("Spell =   A : " + a + " + P1 : " + p1 + " + P2 : " + p2);
        //---------------------------------------------------------------------
        if (a < _spellsActives.Length - 1)
            a += 1;
        else
            a = 0;
        /*//---------------------------------------------------------------------
        if (p1 < _spellsPassives.Length + 1 && _spellsPassives.Length > 0)
            p1 += 1;
        else
            p1 = 0;
        //---------------------------------------------------------------------
        if (p2 + 1 + impairator < _spellsPassives.Length + 1 && _spellsPassives.Length > 0)
            p2 += 1 + impairator;
        else if (p2 + 1 + impairator > _spellsPassives.Length + 1)
        {
            p2 = (p2 + 1 + impairator) - _spellsPassives.Length + 1;
        }
        else if (p2 + 1 + impairator == _spellsPassives.Length + 1)
        {
            p2 = 0;
        }
        //---------------------------------------------------------------------
        impairator *= -1;*/
    }

    public void Shoot(GameObject A, GameObject P1, GameObject P2)
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

        if (P1 != null)
        {
            p1_svm = P1.GetComponent<sc_VariablesManager>();
            dmg += p1_svm._damage;
            range += p1_svm._range;
            speed += p1_svm._speed;
            radius += p1_svm._radius;
            lifeTime += p1_svm._lifeTime;
        }
        if (P2 != null)
        {
            p2_svm = P2.GetComponent<sc_VariablesManager>();
            dmg += p2_svm._damage;
            range += p2_svm._range;
            speed += p2_svm._speed;
            radius += p2_svm._radius;
            lifeTime += p2_svm._lifeTime;
        }
        if (A != null)
        {
            a_svm = A.GetComponent<sc_VariablesManager>();
            shot = Instantiate(A, _shootOffset.transform.position, _shootOffset.transform.rotation);

            dmg += a_svm._damage;
            range += a_svm._range;
            speed += a_svm._speed;
            radius += a_svm._radius;
            lifeTime += a_svm._lifeTime;

            sc_VariablesManager s_svm = shot.GetComponent<sc_VariablesManager>();
            s_svm._damage = dmg;
            s_svm._range = range;
            s_svm._speed = speed;
            s_svm._radius = radius;
            s_svm._lifeTime = lifeTime;
            s_svm.id = index;
            s_svm._host = gameObject;
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

    IEnumerator AutoShoot ()
    {
        yield return new WaitForSeconds(_coolDown);
        SpellsAssembler();
        transform.Rotate(0, 45, 0);
        Shoot(_A, _P1, _P2);
        StartCoroutine(AutoShoot());
    }
}
