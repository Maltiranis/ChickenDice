using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_AnimManagement : MonoBehaviour
{
    [Header("Speed variation")]
    [Range(0f, 1f)]
    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _maxVelDiv = 6f;
    [Space(10)]
    [Header("Kind of Death")]
    [Range(0, 3)]
    [SerializeField] private int _deaths = 100;
    public bool _canRandomHead = true;
    [Space(10)]
    [Header("Combat State")]
    [SerializeField] public bool _onHit;
    [SerializeField] public bool _onPeck;
    [SerializeField] public bool _onTaunt;

    [SerializeField] private GameObject chicken;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private sc_ChickenController _cc;
    [SerializeField] private sc_Peck _pk;
    [SerializeField] private sc_LifeEngine _le;
    [HideInInspector]
    public Animator anim;

    float actualVelocity;
    [HideInInspector]
    [SerializeField] public int newDeath = 100;
    [HideInInspector]
    [SerializeField] public int newHead = 100;
    Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ChickenBrain(1f));
        StartCoroutine(CalcVelocity());
        _canRandomHead = true;
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
    }

    public void CanRandHead()
    {
        _canRandomHead = true;
    }
    public void CantRandHead()
    {
        _canRandomHead = false;
    }

    public void SendStates ()
    {
        //Vector3 normVel = Vector3.ClampMagnitude(rb.velocity.normalized, 1);
        //actualVelocity = (Mathf.Abs(normVel.x) + Mathf.Abs(normVel.z)) / 2;
        anim.SetBool("CanHaveDisease", _canRandomHead);

        SetSpeed(actualVelocity / _maxVelDiv);
    }

    public void SetSpeed (float _newSpeed)
    {
        anim.SetFloat("floatSpeed", _newSpeed);
    }

    public void DoRandomHead()
    {
        newHead = Random.Range(0, 4);

        if (_canRandomHead == true)
        {
            switch (newHead)
            {
                case 0:
                    anim.SetTrigger("headLeft");
                    break;
                case 1:
                    anim.SetTrigger("headRight");
                    break;
                case 2:
                    anim.SetTrigger("headUp");
                    break;
                case 3:
                    anim.SetTrigger("headCot");
                    break;
                default:
                    break;
            }
        }

        StartCoroutine(ChickenBrain(Random.Range(1f, 2.0f)));
    }

    public void DisapearOnDeath()
    {
        _le.DisapearOnDeath();
        newDeath = 100;
    }

    public void RandomDeath()
    {
        newDeath = Random.Range(0, 3);

        anim.SetInteger("intDeath", newDeath);
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
        DoRandomHead();
    }

    IEnumerator CalcVelocity()
    {
        while (Application.isPlaying)
        {
            lastPos = chicken.transform.position;
            yield return new WaitForFixedUpdate();
            actualVelocity = Mathf.RoundToInt(Vector3.Distance(chicken.transform.position, lastPos) / Time.fixedDeltaTime);
            //Debug.Log(actualVelocity);
        }
    }
}
