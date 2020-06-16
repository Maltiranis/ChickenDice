using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_LifeEngine : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public int _life = 100;
    [SerializeField] public bool _respawn = true;
    [SerializeField] public float _respawnDelay = 2.0f;
    [SerializeField] public float _whiteBarSpeed = 0.5f;
    [Space(10)]
    [Header("Who killed me ???")]
    [SerializeField] public int _killer_ID = 100;
    [Space(10)]
    [Header("Respawn Management")]
    public Transform _startPosTransform;
    float startY;
    [HideInInspector] public int startLife;
    [HideInInspector]
    [SerializeField] public bool onRepop = false;
    Rigidbody rb;
    [SerializeField] private sc_AnimManagement _am;
    GameObject skin;
    [SerializeField] public GameObject PolySurface;
    [SerializeField] public GameObject _UItoHide1;
    [SerializeField] public GameObject _UItoHide2;
    [Header("Chicken Prefab")]
    [SerializeField] public GameObject _myOriginalPrefab;
    string myName;
    Transform myParent;
    //[HideInInspector] public GameObject jesusChicken = null;
    GameObject _lifeBar;
    float percentLife = 0f;
    float whiteLife = 0f;

    // Start is called before the first frame update
    void Start()
    {
        onRepop = false;
        transform.position = _startPosTransform.position;
        startY = _startPosTransform.localEulerAngles.y;
        startLife = _life;
        rb = GetComponent<Rigidbody>();
        skin = GetComponent<sc_ChickenController>().gameObject;

        transform.localEulerAngles = new Vector3(0, startY, 0);

        _UItoHide2.transform.localEulerAngles = new Vector3(0, startY, 0);

        name = gameObject.name;
        myParent = transform.parent;
        gameObject.name = "A Chicken" + " numeroted " + GetComponent<sc_Chicken_ID>().ID.ToString();

        _lifeBar = _UItoHide2.transform.GetChild(0).gameObject;

        percentLife = _life / 100f;
        whiteLife = percentLife;
    }

    // Update is called once per frame
    void Update()
    {
        startY = _startPosTransform.localEulerAngles.y;

        percentLife = _life / 100f;
        whiteLife = Mathf.Lerp(whiteLife, percentLife, Time.deltaTime * _whiteBarSpeed);

        _lifeBar.GetComponent<Renderer>().material.SetFloat("_life", percentLife);
        _lifeBar.GetComponent<Renderer>().material.SetFloat("_life2", whiteLife);

        _UItoHide2.transform.forward = Vector3.forward;
    }

    public void TakeDamage(int dmg, int id)
    {
        if (onRepop == false)
        {
            if (_life - dmg <= 0)
            {
                _life = 0;
                Death(id);
            }
            else
            {
                _life -= dmg;
                _am.Hitted();

                if (_am.gameObject.GetComponent<sc_LaunchFx>() != null)
                {
                    _am.gameObject.GetComponent<sc_LaunchFx>().SetEye(1);
                }
            }
        }
    }

    public void Death(int id)
    {
        _killer_ID = id;
        _am.RandomDeath();

        if (_am.gameObject.GetComponent<sc_LaunchFx>() != null)
        {
            _am.gameObject.GetComponent<sc_LaunchFx>().CleanEye();
        }

        if (_respawn == true)
        {
            Respawn();
        }
        else
        {
            UnactivateSystems();
        }
    }

    void UnactivateSystems ()
    {
        _UItoHide1.SetActive(false);
        _UItoHide2.SetActive(false);
        GetComponent<sc_ChickenController>().enabled = false;
        GetComponent<sc_Peck>().enabled = false;
        GetComponent<sc_SpellAlchemist>().DestroyPivot();
        PolySurface.SetActive(false);
        _am.anim.SetInteger("intDeath", 100);
        GetComponent<CapsuleCollider>().enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Respawn()
    {
        UnactivateSystems();
        if (onRepop == false)
        {
            onRepop = true;

            StartCoroutine(RespawnVFX_IEnumerator());
        }
    }

    public void LaunchSystems(GameObject go)
    {
        go.GetComponent<sc_LifeEngine>().PolySurface.SetActive(true);
        go.GetComponent<sc_ChickenController>().enabled = true;
        go.GetComponent<sc_Peck>().enabled = true;
        go.GetComponent<CapsuleCollider>().enabled = true;
        go.GetComponent<sc_LifeEngine>()._life = 100;
        go.name = "A Chicken" + " numeroted " + GetComponent<sc_Chicken_ID>().ID.ToString();
        go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;
        go.GetComponent<sc_LifeEngine>()._UItoHide1.SetActive(true);
        go.GetComponent<sc_LifeEngine>()._UItoHide2.SetActive(true);
    }

    public IEnumerator RespawnVFX_IEnumerator()
    {
        yield return new WaitForSeconds(_respawnDelay);

        Quaternion trueRot = Quaternion.Euler(0, startY, 0);

        GameObject jesusChicken = Instantiate(_myOriginalPrefab, _startPosTransform.position, Quaternion.Inverse(trueRot));
        jesusChicken.GetComponent<sc_ChickenController>()._skin.transform.rotation = trueRot;
        jesusChicken.GetComponent<sc_Chicken_ID>().ID = gameObject.GetComponent<sc_Chicken_ID>().ID;
        jesusChicken.transform.position = _startPosTransform.position;
        jesusChicken.transform.parent = myParent;

        jesusChicken.GetComponent<sc_LifeEngine>()._am.gameObject.GetComponent<sc_LaunchFx>().LaunchFx(3);

        if (jesusChicken.GetComponent<sc_LifeEngine>()._am.gameObject.GetComponent<sc_LaunchFx>() != null)
        {
            jesusChicken.GetComponent<sc_LifeEngine>()._am.gameObject.GetComponent<sc_LaunchFx>().SetEye(2);
        }

        LaunchSystems(jesusChicken); 
        Destroy(gameObject);
    }
}
