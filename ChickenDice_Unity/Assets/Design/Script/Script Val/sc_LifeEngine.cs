using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_LifeEngine : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public int _life = 100;
    [SerializeField] public bool _respawn = true;
    [SerializeField] public float _respawnDelay = 2.0f;
    [Space(10)]
    [Header("Who killed me ???")]
    [SerializeField] public int _killer_ID = 100;
    [Space(10)]
    [Header("Respawn Management")]
    public Transform _startPosTransform;
    float startY;
    int startLife;
    [HideInInspector]
    [SerializeField] public bool onRepop = false;
    Rigidbody rb;
    [SerializeField] private sc_AnimManagement _am;
    GameObject skin;
    [SerializeField] public GameObject PolySurface;
    [Header("Chicken Prefab")]
    [SerializeField] public GameObject _myOriginalPrefab;
    string myName;
    Transform myParent;

    // Start is called before the first frame update
    void Start()
    {
        onRepop = false;
        transform.position = _startPosTransform.position;
        startY = _startPosTransform.localEulerAngles.y;
        startLife = _life;
        rb = GetComponent<Rigidbody>();
        skin = GetComponent<sc_ChickenController>().gameObject;

        skin.transform.localEulerAngles = new Vector3(0, startY, 0);
        name = gameObject.name;
        myParent = transform.parent;
        gameObject.name = "A Chicken" + " numeroted " + GetComponent<sc_Chicken_ID>().ID.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        startY = _startPosTransform.localEulerAngles.y;
    }

    public void TakeDamage(int dmg, int id)
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
        }
    }

    public void Death(int id)
    {
        _killer_ID = id;
        _am.RandomDeath();
        if (_respawn == true)
        {
            Respawn();
        }
        else
        {
            UnactivateSystems();
        }
    }

    public void DisapearOnDeath ()
    {
        PolySurface.SetActive(false);
        _am.anim.SetInteger("intDeath", 100);
        GetComponent<CapsuleCollider>().enabled = false;
    }

    void UnactivateSystems ()
    {
        //gameObject.SetActive(false);
        GetComponent<sc_ChickenController>().enabled = false;
        GetComponent<sc_Peck>().enabled = false;

        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Respawn()
    {
        DisapearOnDeath();
        UnactivateSystems();
        if (onRepop == false)
        {
            onRepop = true;
            StartCoroutine(RespawnIEnumerator());
        }
    }

    public void LaunchSystems(GameObject go)
    {
        go.GetComponent<sc_LifeEngine>().PolySurface.SetActive(true);
        go.GetComponent<sc_ChickenController>().enabled = true;
        go.GetComponent<sc_Peck>().enabled = true;
        go.GetComponent<CapsuleCollider>().enabled = true;
        go.GetComponent<sc_LifeEngine>()._life = startLife;
        go.transform.parent = myParent;
        go.name = "A Chicken" + " numeroted " + GetComponent<sc_Chicken_ID>().ID.ToString();
        go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;
    }

    public IEnumerator RespawnIEnumerator()
    {
        yield return new WaitForSeconds(_respawnDelay);
        Quaternion trueRot = Quaternion.Euler(0, startY, 0);
        GameObject jesusChicken = Instantiate(_myOriginalPrefab, _startPosTransform.position, trueRot);
        jesusChicken.GetComponent<sc_Chicken_ID>().ID = gameObject.GetComponent<sc_Chicken_ID>().ID;
        LaunchSystems(jesusChicken);
        Destroy(gameObject);
    }
}
