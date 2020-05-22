using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_LifeEngine : MonoBehaviour
{
    [SerializeField] public int _life = 100;
    [SerializeField] public bool _respawn = true;
    [SerializeField] public float _respawnDelay = 2.0f;

    Vector3 startPos;
    float startY;
    int startLife;
    [HideInInspector]
    [SerializeField] public bool onRepop = false;
    Rigidbody rb;
    [SerializeField] private sc_AnimManagement _am;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startY = transform.localEulerAngles.y;
        startLife = _life;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

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
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(0f, startY, 0f);
        _life = startLife;
        GetComponent<sc_ChickenController>().enabled = true;
        GetComponent<sc_Peck>().enabled = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;
        onRepop = false;
    }
}
