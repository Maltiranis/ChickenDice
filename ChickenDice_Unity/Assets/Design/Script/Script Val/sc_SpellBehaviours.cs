using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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
    [SerializeField] private Profile _getProfile;
    [Space(15)]
    private float _speed;
    private float _damages;
    private float _range;
    private GameObject _aVFXparent;
    private GameObject _dVFXparent;
    private float _dVFX_lifetime = 2.0f;
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
    [SerializeField] private Passives_L _getPassives_Left;
    [SerializeField] private Passives_R _getPassives_Right;
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
        [SerializeField] public float _maxDistance = 10.0f;
        [HideInInspector] public Vector3 startPos;
        [HideInInspector] public int _id;//l'ID du Joueur ayant caster
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
            GameObject fxs = Instantiate(_aVFXparent, transform.position, Quaternion.identity);
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
    }

    private void CallingMethod() //Start
    {
        /*
         * 0 + 0 = 0 empty
         * 0 + 1000 = 1000 spin (rotate around player)
         * 0 + 2000 = 2000 multi (shot a second projectile)
         * 0 + 3000 = 3000 chain (shot a clone projectile in closest enemy direction)
         * 
         * 10 + 0 = 10 
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

        _v.startPos = transform.position;

        Destroy(gameObject, _v._lifeTime);
    }

    private void StartMovementBehaviour() //Update
    {
        switch (_v._mix)
        {
            case 0:
                DefaultMovement();
                break;
            case 1000:
                break;
            case 2000:
                break;
            case 3000:
                break;
            case 10:
                break;
            case 1010:
                break;
            case 2010:
                break;
            case 3010:
                break;
            case 20:
                break;
            case 1020:
                break;
            case 2020:
                break;
            case 3020:
                break;
            case 30:
                break;
            case 1030:
                break;
            case 2030:
                break;
            case 3030:
                break;
            default:
                break;
        }
    }

    private void UpdateMovementBehaviour() //Update
    {
        switch (_v._mix)
        {
            case 0:
                break;
            case 1000:
                break;
            case 2000:
                break;
            case 3000:
                break;
            case 10:
                break;
            case 1010:
                break;
            case 2010:
                break;
            case 3010:
                break;
            case 20:
                break;
            case 1020:
                break;
            case 2020:
                break;
            case 3020:
                break;
            case 30:
                break;
            case 1030:
                break;
            case 2030:
                break;
            case 3030:
                break;
            default:
                break;
        }
    }

    void DefaultMovement()
    {
        GetComponent<Rigidbody>().AddForce
        (
            transform.forward * _speed, ForceMode.Impulse
        );

        if (Vector3.Distance(_v.startPos, transform.position) >= _v._maxDistance)
            Destroy(gameObject);
    }

    void OrbitalMovement()
    {
        GetComponent<Rigidbody>().AddForce
        (
            transform.forward * _speed, ForceMode.Impulse
        );

        if (Vector3.Distance(_v.startPos, transform.position) >= _v._maxDistance)
            Destroy(gameObject);
    }

    void ActionDone()
    {
        CallDeathVFX();
        Destroy(gameObject);
    }

    void CallDeathVFX()
    {
        if (_getProfile != Profile.PASSIVE)
        {
            GameObject fxs = Instantiate(_dVFXparent, transform.position, Quaternion.identity);
            fxs.name = _dVFXparent.name;
            fxs.transform.parent = gameObject.transform;
            fxs.transform.forward = transform.forward;
        }
    }
}
