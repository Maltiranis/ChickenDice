using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_LifeEngine : MonoBehaviour
{
    [SerializeField] public int _life = 100;
    [SerializeField] public bool _respawn = true;
    [SerializeField] public float _respawnDelay = 2.0f;

    public Transform _startPosTransform;
    float startY;
    int startLife;
    [HideInInspector]
    [SerializeField] public bool onRepop = false;
    Rigidbody rb;
    [SerializeField] private sc_AnimManagement _am;
    GameObject skin;
    [SerializeField] public GameObject PolySurface;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _startPosTransform.position;
        startY = _startPosTransform.localEulerAngles.y;
        startLife = _life;
        rb = GetComponent<Rigidbody>();
        skin = GetComponent<sc_ChickenController>().gameObject;

        skin.transform.localEulerAngles = new Vector3(0, startY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        startY = _startPosTransform.localEulerAngles.y;
    }

    public void TakeDamage(int dmg)
    {
        if (_life - dmg <= 0)
        {
            _life = 0;
            Death();
        }
        else
        {
            _life -= dmg;
            _am.Hitted();
        }
    }

    public void Death()
    {
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
        UnactivateSystems();
        StartCoroutine(RespawnIEnumerator());
    }

    public IEnumerator RespawnIEnumerator()
    {
        yield return new WaitForSeconds(_respawnDelay);
        _am.newDeath = 100;
        _am.newHead = 100;
        transform.position = _startPosTransform.position;
        skin.transform.localEulerAngles = new Vector3(0, startY, 0);
        _life = startLife;
        GetComponent<sc_ChickenController>().enabled = true;
        GetComponent<sc_Peck>().enabled = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;
        PolySurface.SetActive(true);
        onRepop = false;
    }
}
