using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_AnimManagement : MonoBehaviour
{
    [Header("Speed variation")]
    [Range(0f, 1f)]
    [SerializeField] private float _speed = 0f;
    [Space(10)]
    [Header("Kind of Death")]
    [Range(0, 3)]
    [SerializeField] private int _deaths = 100;
    [Space(10)]
    [Header("Mental Disease")]
    [Range(0, 3)]
    [SerializeField] private int _head = 100;
    [Space(10)]
    [Header("Combat State")]
    [SerializeField] public bool _onHit;
    [SerializeField] public bool _onPeck;
    [SerializeField] public bool _onTaunt;

    private GameObject chicken;
    Rigidbody rb;
    sc_ChickenController _cc;
    sc_Peck _pk;
    sc_LifeEngine _le;
    Animator anim;

    float actualVelocity;
    int newDeath = 100;
    int newHead = 100;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        chicken = gameObject;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ChickenBrain(2f));
    }

    // Update is called once per frame
    void Update()
    {
        GetStates();
        SendStates();
    }

    public void GetStates ()
    {
        _speed = anim.GetFloat("floatSpeed");
        _deaths = anim.GetInteger("intDeath");
        _head = anim.GetInteger("intHead");
    }

    public void SendStates ()
    {
        actualVelocity = (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z)) / 2;
        Debug.Log(actualVelocity);

        if (_le._life == 0 && _le.onRepop == false)
        {
            _le.onRepop = true;

            newDeath = Random.Range(0, 3);

        }
        else
        {
            newDeath = 100;
        }

        SetStates(actualVelocity, newDeath, newHead);
    }

    public void SetStates (float _newSpeed, int _newDeath, int _newHead)
    {
        anim.SetFloat("floatSpeed", _newSpeed);
        anim.SetInteger("intDeath", _newDeath);
        anim.SetInteger("intHead", _newHead);
    }

    public void Hitted()
    {
        anim.SetTrigger("triggerHit");
    }
    public void Pecked()
    {
        anim.SetTrigger("triggerPeck");
    }
    public void Taunted()
    {
        anim.SetTrigger("triggerTaunt");
    }

    public IEnumerator ChickenBrain(float newDealay)
    {
        yield return new WaitForSeconds(newDealay);

        newHead = Random.Range(0, 4);
        StartCoroutine(ChickenBrain(Random.Range(1.5f, 4.0f)));
    }
}
