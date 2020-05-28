using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_ChickenController : MonoBehaviour
{
    [Header("Chicken ID")]
    [SerializeField] private sc_Chicken_ID _myID;
    int _id;
    [Space(10)]
    [Header("Variables")]
    [SerializeField] private float _sensibility = 0.19f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 1f;
    [Space(5)]
    [SerializeField] private float _gasgasgasTimer = 2f;
    [SerializeField] private float _timerAjustement = 0.8f;
    [SerializeField] private float _gasgasgasSpeed = 2f;
    float oldTimer = 0f;
    private float _newSpeed = 0f;
    [Space(10)]
    [Header("Rotating Child")]
    public GameObject _skin;

    float leftVertAxe = 0f;
    float leftHorizAxe = 0f;
    float rightVertAxe = 0f;
    float rightHorizAxe = 0f;

    Vector3 brutAppliedForce;
    Vector3 lookVector;
    Rigidbody rb;
    Quaternion rot;
    Quaternion actualRot;
    Quaternion brutRot;
    Quaternion lookRot;
    Quaternion lerpedBrutRot;
    Quaternion lerpedLookRot;

    bool Jleft = false;
    bool Jright = false;

    sc_Peck scp;
    sc_SkillManagement scsm;

    // Start is called before the first frame update
    void Start()
    {
        _id = _myID.ID;

        leftVertAxe = Input.GetAxis("LJoyVertical_" + _id.ToString());
        leftHorizAxe = Input.GetAxis("LJoyHorizontal_" + _id.ToString());
        rightVertAxe = Input.GetAxis("RJoyVertical_" + _id.ToString());
        rightHorizAxe = Input.GetAxis("RJoyHorizontal_" + _id.ToString());

        rb = GetComponent<Rigidbody>();
        scp = GetComponent<sc_Peck>();
        scsm = GetComponent<sc_SkillManagement>();

        _newSpeed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        FourButtons();
        Cross();
        Triggers();
        Bumpers();
    }

    void FixedUpdate ()
    {
        LeftJoy();
        RightJoy();

        Movements();
        SkinRotation();
    }

    void LeftJoy ()
    {
        leftHorizAxe = Input.GetAxis("LJoyHorizontal_" + _id.ToString());
        leftVertAxe = -Input.GetAxis("LJoyVertical_" + _id.ToString());

        brutAppliedForce = new Vector3(leftHorizAxe, 0, leftVertAxe);
        if (brutAppliedForce.magnitude > _sensibility)
        {
            brutAppliedForce = brutAppliedForce.normalized * ((brutAppliedForce.magnitude - _sensibility) / (1 - _sensibility));
            Jleft = true;
        }
        else
        {
            brutAppliedForce = Vector3.zero;
            Jleft = false;
        }
        //brutAppliedForce.Normalize();
        brutAppliedForce = Vector3.ClampMagnitude(brutAppliedForce, 1f);
    }
    void RightJoy ()
    {
        rightVertAxe = -Input.GetAxis("RJoyVertical_" + _id.ToString());
        rightHorizAxe = Input.GetAxis("RJoyHorizontal_" + _id.ToString());

        lookVector = new Vector3(rightHorizAxe, 0, rightVertAxe);
        if (lookVector.magnitude > _sensibility)
        {
            lookVector = lookVector.normalized * ((lookVector.magnitude - _sensibility) / (1 - _sensibility));
            Jright = true;
        }
        else
        {
            lookVector = brutAppliedForce;
            Jright = false;
        }
    }
    void FourButtons ()
    {
        //Scripts
        sc_Peck scp = GetComponent<sc_Peck>();
        //Pecking
        if (Input.GetButtonDown("xA_" + _id.ToString()))
        {
            scp.Pecking();
            _newSpeed = _moveSpeed;
            oldTimer = 0f;
        }
        if (Input.GetButtonUp("xA_" + _id.ToString()))
        {
            scp.Unpeck();
            oldTimer = 0f;
        }
        //Taunt
        if (Input.GetButtonDown("xB_" + _id.ToString()))
        {
            scp._am.Taunted();
        }

        //speed ajustement
        if (oldTimer < _gasgasgasTimer)
        {
            oldTimer += Time.fixedDeltaTime * _timerAjustement;
            _newSpeed = _moveSpeed;
        }
        else
        {
            _newSpeed = _gasgasgasSpeed;
        }

        //Get Gem
        if (Input.GetButtonDown("xX_" + _id.ToString()))
        {
            scsm.GetDropGem("X");
        }
        //Drop Gem
        if (Input.GetButtonDown("xY_" + _id.ToString()))
        {
            scsm.GetDropGem("Y");
        }
    }
    void Cross ()
    {

    }
    void Triggers ()
    {
        //Scripts
        sc_Shooting scs = GetComponent<sc_Shooting>();// a mettre dans Triggers ()

        if (Input.GetAxis("RT_" + _id.ToString()) > 0.85f)
        {
            scsm.UseGem("RT");
            //scs.Shoot();
        }
        if (Input.GetAxis("LT_" + _id.ToString()) > 0.85f)
        {
            scsm.UseGem("LT");
        }
    }
    void Bumpers ()
    {
        if (Input.GetButtonDown("LB_" + _id.ToString()))
        {
            scsm.GetDropGem("LB");
        }
        if (Input.GetButtonDown("RB_" + _id.ToString()))
        {
            scsm.GetDropGem("RB");
        }
    }
    void Movements ()
    {
        if (!scp._pecking)
        {
            rb.AddForce(_newSpeed * brutAppliedForce, ForceMode.Acceleration);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    void SkinRotation ()
    {
        actualRot = _skin.transform.rotation;

        if (brutAppliedForce != Vector3.zero)
            brutRot = Quaternion.LookRotation(brutAppliedForce, Vector3.up);
        if (lookVector != Vector3.zero)
            lookRot = Quaternion.LookRotation(lookVector, Vector3.up);

        lerpedBrutRot = Quaternion.RotateTowards(actualRot, brutRot, Time.deltaTime * _rotationSpeed);
        lerpedLookRot = Quaternion.RotateTowards(actualRot, lookRot, Time.deltaTime * _rotationSpeed);

        if (Jleft)
        {
            if (Jright == false && brutAppliedForce != Vector3.zero)
            {
                rot = lerpedBrutRot;
            }
            else
            {
                if (lookVector != Vector3.zero)
                    rot = lerpedLookRot;
            }
        }
        else
        {
            if (lookVector != Vector3.zero)
                rot = lerpedLookRot;
        }

        _skin.transform.rotation = rot;
    }
}
