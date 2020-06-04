﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Security.Cryptography;

public class sc_SpellBehaviours : MonoBehaviour
{
    #region profile
    public enum Profile
    {
        PASSIVE,
        FireBolt,
        Bowling,
        PumpGun
    }
    [Header("Spell")]
    [SerializeField] public Profile _getProfile;
    [Space(15)]
    private float _speed;
    private float _damages;
    private float _range;
    private GameObject _aVFXparent;
    private GameObject _dVFXparent;
    #endregion
    //Shield + heal + dash à ajouter !
    #region passive proprieties
    public enum Passives_L
    {
        EMPTY,
        Spinning,
        Multiple,
        Chain
    }
    public enum Passives_R
    {
        EMPTY,
        Spinning,
        Multiple,
        Chain
    }
    [Header("Passive")]
    [SerializeField] public Passives_L _getPassives_Left;
    [SerializeField] public Passives_R _getPassives_Right;
    #endregion

    #region hiddenVars
    [System.Serializable]
    public class Variables
    {
        //fire bolt
        [Space(15)]
        [Header("FireBolt")]
        [Space(5)]
        [SerializeField] public float speedFB;
        [SerializeField] public float damagesFB;
        [SerializeField] public float rangeFB;
        [SerializeField] public Vector3 ajustedPosFB = new Vector3(0, 0, 1);
        [SerializeField] public Vector3 ajustedRotFB = new Vector3(270, 0, 0);
        [SerializeField] public GameObject _aVFXparent_FB;
        [SerializeField] public GameObject _dVFXparent_FB;
        [Space(15)]
        //bowling
        [Header("Bowling")]
        [Space(5)]
        [SerializeField] public float speedBL;
        [SerializeField] public float damagesBL;
        [SerializeField] public float rangeBL;
        [SerializeField] public Vector3 ajustedPosBL;
        [SerializeField] public Vector3 ajustedRotBL;
        [SerializeField] public GameObject _aVFXparent_BL;
        [SerializeField] public GameObject _dVFXparent_BL;
        [Space(15)]
        //pump gun
        [Header("PumpGun")]
        [Space(5)]
        [SerializeField] public float speedPG;
        [SerializeField] public float damagesPG;
        [SerializeField] public float rangePG;
        [SerializeField] public Vector3 ajustedPosPG;
        [SerializeField] public Vector3 ajustedRotPG;
        [SerializeField] public GameObject _aVFXparent_PG;
        [SerializeField] public GameObject _dVFXparent_PG;
        [Space(15)]
        //pump gun
        [Header("Global Variable")]
        [Space(5)]
        [SerializeField] public float _coolDown = 1.0f;
        [SerializeField] public float _lifeTime = 5.0f;
        [HideInInspector] public float duration = 0f;
        [SerializeField] public float _maxDistance = 10.0f;
        [HideInInspector] public Vector3 startPos;
        [HideInInspector] public int _id;//l'ID du Joueur ayant caster
        [HideInInspector] public Quaternion qZero = new Quaternion(0, 0, 0, 0);
        //[SerializeField] public GameObject[] nearest;
        [SerializeField] public List<GameObject> nearest = new List<GameObject>();
        [SerializeField] public GameObject bestTarget;
        [SerializeField] public bool _canCollide = true;
        [Space(15)]
        //pump gun
        [Header("Orbital")]
        [Space(5)]
        [SerializeField] public float _spread = 0f;
        [SerializeField] public float _spreadSpeed = 5f;
        [SerializeField] public float _orbitSpeed = 10f;
        [Space(15)]
        [Header("Spell")]
        [SerializeField] public float _dVFX_lifetime = 2.0f;
        [Space(15)]
        //Alchemy
        [Header("Alchemy Mix Result")]
        [Space(5)]
        [SerializeField] public int _P1value = 0;
        [SerializeField] public int _P2value = 0;
        [Header("P1 + P2")]
        [SerializeField] public int _mix = 0;
        [Space(20)]
        [Header("Repeating method")]
        [SerializeField] public int _iterationOnLaunch = 0;
        [SerializeField] public int _iterationOnDestroyed = 0;
        [Space(5)]
        [Header("Waiting Time")]
        [SerializeField] public float _timeToWait = 1.0f;
    }
    public Variables _v;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SpellAttribution();
        GetVFXs();
        PassiveImpliquations();
        CallingMethod();

        StartMovementBehaviour();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] catched = null;
        catched = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject g in catched)
        {
            if (g.GetComponent<CapsuleCollider>().enabled == true &&
                g.GetComponent<sc_Chicken_ID>().ID != _v._id)
            {
                if (!_v.nearest.Contains(g))
                {
                    _v.nearest.Add(g);
                }
            }
            else
            {
                if (_v.nearest.Contains(g))
                {
                    _v.nearest.Remove(g);
                }
            }
        }
        _v.bestTarget = GetClosest();
        UpdateMovementBehaviour();
    }

    private void SpellAttribution() //Start
    {
        switch (_getProfile)
        {
            case Profile.PASSIVE:

                break;
            case Profile.FireBolt:
                _speed = _v.speedFB;
                _damages = _v.damagesFB;
                _range = _v.rangeFB;
                _aVFXparent = _v._aVFXparent_FB;
                _dVFXparent = _v._dVFXparent_FB;
                break;
            case Profile.Bowling:
                _speed = _v.speedBL;
                _damages = _v.damagesBL;
                _range = _v.rangeBL;
                _aVFXparent = _v._aVFXparent_BL;
                _dVFXparent = _v._dVFXparent_BL;
                break;
            case Profile.PumpGun:
                _speed = _v.speedPG;
                _damages = _v.damagesPG;
                _range = _v.rangePG;
                _aVFXparent = _v._aVFXparent_PG;
                _dVFXparent = _v._dVFXparent_PG;

                _v._iterationOnLaunch = 2;
                break;
            default:
                break;
        }
    }

    private void GetVFXs()
    {
        if (_getProfile != Profile.PASSIVE)
        {
            GameObject fxs = Instantiate(_aVFXparent, transform.position, _v.qZero);
            fxs.name = _aVFXparent.name;
            fxs.transform.parent = gameObject.transform;

            if (_getProfile == Profile.FireBolt)
            {
                fxs.transform.localPosition = _v.ajustedPosFB;
                fxs.transform.localEulerAngles = _v.ajustedRotFB;
            }
            if (_getProfile == Profile.Bowling)
            {
                fxs.transform.localPosition = _v.ajustedPosBL;
                fxs.transform.localEulerAngles = _v.ajustedRotBL;
            }
            if (_getProfile == Profile.PumpGun)
            {
                fxs.transform.localPosition = _v.ajustedPosPG;
                fxs.transform.localEulerAngles = _v.ajustedRotPG;
            }
        }
    }

    private void PassiveImpliquations() //Start
    {
        switch (_getPassives_Left)
        {
            case Passives_L.EMPTY:
                _v._P1value = 0;
                break;
            case Passives_L.Spinning:
                _v._P1value = 10;
                break;
            case Passives_L.Multiple:
                _v._P1value = 20;
                break;
            case Passives_L.Chain:
                _v._P1value = 30;
                break;
            default:
                break;
        }
        switch (_getPassives_Right)
        {
            case Passives_R.EMPTY:
                _v._P2value = 0;
                break;
            case Passives_R.Spinning:
                _v._P2value = 1000;
                break;
            case Passives_R.Multiple:
                _v._P2value = 2000;
                break;
            case Passives_R.Chain:
                _v._P2value = 3000;
                break;
            default:
                break;
        }

        _v._mix = _v._P1value + _v._P2value;
    }

    private void CallingMethod() //Start
    {
        /*
         * 0 + 0 = 0 empty
         * 0 + 1000 = 1000 spin (rotate around player)
         * 0 + 2000 = 2000 multi (shot a second projectile)
         * 0 + 3000 = 3000 chain (shot a clone projectile in closest enemy direction)
         * 
         * 10 + 0 = 10  spin
         * 10 + 1000 = 1010 spin spin (do just P1)
         * 10 + 2000 = 2010 multi spin (spin 2 proj mirrored ?)
         * 10 + 3000 = 3010 chain spin (do just P2 and do a normal chain)
         * 
         * 20 + 0 = 20 multi (shoot a 2nd proj with a delay)
         * 20 + 1000 = 1020 spin multi (shoot a 2nd proj with a delay = 2 * P1, 1sec delay)
         * 20 + 2000 = 2020 multi multi (shoot a 2nd proj with a delay = 3 * P1, 1sec delay)
         * 20 + 3000 = 3020 chain multi (shoot a 2nd proj with a delay = 2 * P2, 1sec delay + 1chain/proj)
         * 
         * 30 + 0 = 30 chain (shot a clone projectile in closest enemy direction)
         * 30 + 1000 = 1030 spin chain (do just P1 and do a normal chain)
         * 30 + 2000 = 2030 multi chain (shoot a 2nd proj with a delay = 2 * P1, 1sec delay + 1chain/proj)
         * 30 + 3000 = 3030 chain chain (shot a clone projectile in closest enemy direction,
         *                               then shot a second at first death)
         * */
    }

    private void StartMovementBehaviour() //Start
    {
        switch (_v._mix)
        {
            case 0:
                DefaultMovement();
                break;
            case 1000:
                break;
            case 2000:
                DefaultMovement();
                break;
            case 3000:
                DefaultMovement();
                _v._iterationOnDestroyed = 1;
                break;
            case 10:
                break;
            case 1010:
                break;
            case 2010:
                break;
            case 3010:
                _v._iterationOnDestroyed = 1;
                break;
            case 20:
                DefaultMovement();
                break;
            case 1020:
                break;
            case 2020:
                DefaultMovement();
                break;
            case 3020:
                DefaultMovement();
                _v._iterationOnDestroyed = 1;
                break;
            case 30:
                DefaultMovement();
                _v._iterationOnDestroyed = 1;
                break;
            case 1030:
                _v._iterationOnDestroyed = 1;
                break;
            case 2030:
                DefaultMovement();
                _v._iterationOnDestroyed = 1;
                break;
            case 3030:
                DefaultMovement();
                _v._iterationOnDestroyed = 2;
                break;
            default:
                break;
        }

        _v.startPos = transform.position;
        _v.duration = 0f;
    }

    private void UpdateMovementBehaviour() //Update
    {
        switch (_v._mix)
        {
            case 0:
                break;
            case 1000:
                OrbitalMovement();
                break;
            case 2000:
                break;
            case 3000:
                break;
            case 10:
                OrbitalMovement();
                break;
            case 1010:
                OrbitalMovement();
                break;
            case 2010:
                OrbitalMovement();
                break;
            case 3010:
                OrbitalMovement();
                break;
            case 20:
                break;
            case 1020:
                OrbitalMovement();
                break;
            case 2020:
                break;
            case 3020:
                break;
            case 30:
                break;
            case 1030:
                OrbitalMovement();
                break;
            case 2030:
                break;
            case 3030:
                break;
            default:
                break;
        }

        _v.duration += Time.deltaTime;

        if (_v.duration >= _v._lifeTime)
        {
            if (_v._iterationOnDestroyed > 0)
            {
                ProjOnDeath(_v._iterationOnDestroyed);
            }
            ActionDone();
        }

        if (Vector3.Distance(_v.startPos, transform.position) >= _v._maxDistance)
        {
            if (_v._iterationOnDestroyed > 0)
            {
                ProjOnDeath(_v._iterationOnDestroyed);
            }
            ActionDone();
        }
    }

    void DefaultMovement()//Start
    {
        GetComponent<Rigidbody>().AddForce
        (
            transform.forward * _speed, ForceMode.Impulse
        );
    }

    void OrbitalMovement()//Update
    {
        _v._spread = _v.duration * _v._spreadSpeed;

        Vector3 chickenCenter = new Vector3(transform.parent.position.x, transform.position.y, transform.parent.position.z);
        Vector3 desiredPosition;

        transform.RotateAround(chickenCenter, Vector3.up, _v._orbitSpeed * Time.deltaTime);
        desiredPosition = (transform.position - chickenCenter).normalized * _v.duration + chickenCenter;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * _v._spread);

        GameObject fxs = transform.GetChild(0).gameObject;

        Vector3 orbitalPosFB = new Vector3(1, 0, 0);
        Vector3 orbitalRotFB = new Vector3(270, 180, 270);

        Vector3 orbitalPosBL = new Vector3(0, 0, 0);
        Vector3 orbitalRotBL = new Vector3(-90, 0, 90);

        if (_getProfile == Profile.FireBolt)
        {
            fxs.transform.localPosition = orbitalPosFB;
            fxs.transform.localEulerAngles = orbitalRotFB;
        }
        if (_getProfile == Profile.Bowling)
        {
            fxs.transform.localPosition = orbitalPosBL;
            fxs.transform.localEulerAngles = orbitalRotBL;
        }
    }

    void ActionDone()
    {
        CallDeathVFX();
    }

    void CallDeathVFX()
    {
        if (_getProfile != Profile.PASSIVE)
        {
            GameObject E = new GameObject("Explosion_FX");
            GameObject Explosion = Instantiate(E, transform.position, _v.qZero);
            Destroy(E);
            Explosion.name = "Explosion de Tangtang";
            Explosion.transform.SetParent(null);
            Explosion.AddComponent<sc_SelfDestruction>();
            GameObject tanguetteFX = Instantiate(_dVFXparent, Explosion.transform.position, _v.qZero);
            tanguetteFX.transform.parent = Explosion.transform;

            Explosion.GetComponent<sc_SelfDestruction>().DestroyMyself(_v._dVFX_lifetime);

            float newDamages = 0f;
            float newRange = 0f;
            if (_getProfile == Profile.FireBolt)
            {
                newDamages = _v.damagesFB;
                newRange = _v.rangeFB;
            }
            if (_getProfile == Profile.Bowling)
            {
                newDamages = _v.damagesBL;
                newRange = _v.rangeBL;
            }
            if (_getProfile == Profile.PumpGun)
            {
                newDamages = _v.damagesPG;
                newRange = _v.rangePG;
            }
            Explosion.GetComponent<sc_SelfDestruction>().KillEverybody(Mathf.RoundToInt(newDamages), _v._id, newRange);
        }
        /*if (_v._iterationOnDestroyed > 0)
        {
            Destroy(gameObject, 2);

        }
        else*/
            Destroy(gameObject);
    }

    public void ProjOnDeath (int iLeft)
    {
        if (_v.bestTarget != null)
        {
            _v.bestTarget = GetClosest();
        }
        //Debug.Log("il reste : " + iLeft + " itération et " + _v._iterationOnDestroyed + " dans les variables");
        GameObject newItmodel = Instantiate(gameObject, transform.position, _v.qZero);
        sc_SpellBehaviours s_sb = newItmodel.GetComponent<sc_SpellBehaviours>();

        if (newItmodel.transform.childCount > 0)
        {
            for (int i = 0; i < newItmodel.transform.childCount; i++)
            {
                Destroy(newItmodel.transform.GetChild(i).gameObject);
            }
        }
        s_sb._getPassives_Right = Passives_R.EMPTY;
        s_sb._getProfile = _getProfile;
        if (_v.bestTarget != null)
            newItmodel.transform.LookAt(_v.bestTarget.transform, Vector3.up);
        Quaternion goodLook = new Quaternion(0, newItmodel.transform.rotation.y, 0, 0);
        GameObject newIt = Instantiate(newItmodel, transform.position, goodLook);
        sc_SpellBehaviours s_sbn = newIt.GetComponent<sc_SpellBehaviours>();
        if (_v.bestTarget != null)
            newIt.transform.LookAt(_v.bestTarget.transform, Vector3.up);

        s_sbn.LaunchEnableCol();

        if (iLeft == 2)
        {
            _v._iterationOnDestroyed = 1;
            s_sbn._v._iterationOnDestroyed = 1;
            s_sb._getPassives_Left = Passives_L.Chain;
            s_sbn._getPassives_Left = Passives_L.Chain;
            s_sb._getPassives_Right = Passives_R.EMPTY;//Empty
            s_sbn._getPassives_Right = Passives_R.EMPTY;//Empty
        }
        if (iLeft == 1)
        {
            _v._iterationOnDestroyed = 0;
            s_sbn._v._iterationOnDestroyed = 0;
            s_sb._getPassives_Right = Passives_R.EMPTY;
            s_sbn._getPassives_Right = Passives_R.EMPTY;
            s_sb._getPassives_Left = Passives_L.EMPTY;
            s_sbn._getPassives_Left = Passives_L.EMPTY;
        }
        Destroy(newItmodel);
        newIt.name = "Chain " + iLeft.ToString();

        Destroy(newIt.transform.GetChild(0).gameObject);
    }

    GameObject GetClosest()
    {
        float lowestDist = Mathf.Infinity;
        GameObject nearest = null;

        for (int i = 0; i < _v.nearest.Count; i++)
        {
            float dist = Vector3.Distance(_v.nearest[i].transform.position, transform.position);

            if (dist < lowestDist)
            {
                lowestDist = dist;
                nearest = _v.nearest[i];
            }
        }
        return nearest;
    }

    public void LaunchEnableCol ()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(enableCol());
    }

    public IEnumerator enableCol()
    {
        yield return new WaitForSeconds(0.2f);
        if (gameObject != null)
            gameObject.GetComponent<SphereCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<sc_Chicken_ID>() != null)
        {
            if (other.GetComponent<sc_Chicken_ID>().ID != _v._id)
            {
                if (_v.nearest.Contains(other.gameObject))
                {
                    _v.nearest.Remove(other.gameObject);
                }
                if (_v._iterationOnDestroyed > 0)
                {
                    ProjOnDeath(_v._iterationOnDestroyed);
                }
                ActionDone();
            }
        }
        if (other.tag == "WorldCollider")
        {
            if (_v._iterationOnDestroyed > 0)
            {
                ProjOnDeath(_v._iterationOnDestroyed);
            }
            ActionDone();
        }
    }
}