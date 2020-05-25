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
    [SerializeField] private float _Lsensibility = 0.19f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 1f;
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

    bool Jleft = false;
    bool Jright = false;

    // Start is called before the first frame update
    void Start()
    {
        _id = _myID.ID;
        leftVertAxe = Input.GetAxis("LJoyVertical_" + _id.ToString());
        leftHorizAxe = Input.GetAxis("LJoyHorizontal_" + _id.ToString());
        rightVertAxe = Input.GetAxis("RJoyVertical_" + _id.ToString());
        rightHorizAxe = Input.GetAxis("RJoyHorizontal_" + _id.ToString());
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        LeftJoy();
        RightJoy();
        FourButtons();
        Cross();
        Triggers();
        Bumpers();

        Movements();
        SkinRotation();
    }

    void LeftJoy ()
    {
        if (Input.GetAxis("LJoyVertical_" + _id.ToString()) > _Lsensibility ||
            Input.GetAxis("LJoyVertical_" + _id.ToString()) < -_Lsensibility ||
            Input.GetAxis("LJoyHorizontal_" + _id.ToString()) > _Lsensibility ||
            Input.GetAxis("LJoyHorizontal_" + _id.ToString()) < -_Lsensibility)
        {
            leftHorizAxe = Input.GetAxis("LJoyHorizontal_" + _id.ToString());
            leftVertAxe = -Input.GetAxis("LJoyVertical_" + _id.ToString());

            brutAppliedForce = new Vector3(Input.GetAxis("LJoyHorizontal_" + _id.ToString()), 0, -Input.GetAxis("LJoyVertical_" + _id.ToString()));
            brutAppliedForce.Normalize();
            brutAppliedForce = Vector3.ClampMagnitude(brutAppliedForce, 1);
            Jleft = true;
        }
        else
        {
            leftVertAxe = 0f;
            leftHorizAxe = 0f;
            Jleft = false;
        }
    }
    void RightJoy ()
    {
        if (Input.GetAxis("RJoyVertical_" + _id.ToString()) > 0 ||
            Input.GetAxis("RJoyVertical_" + _id.ToString()) < 0 ||
            Input.GetAxis("RJoyHorizontal_" + _id.ToString()) > 0 ||
            Input.GetAxis("RJoyHorizontal_" + _id.ToString()) < 0)
        {
            rightVertAxe = -Input.GetAxis("RJoyVertical_" + _id.ToString());
            rightHorizAxe = Input.GetAxis("RJoyHorizontal_" + _id.ToString());

            lookVector = new Vector3(rightHorizAxe, 0, rightVertAxe);
            Jright = true;
        }
        else
        {
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
        }
        if (Input.GetButtonUp("xA_" + _id.ToString()))
        {
            scp.Unpeck();
        }
        //Taunt
        if (Input.GetButtonDown("xB_" + _id.ToString()))
        {
            scp._am.Taunted();
        }
    }
    void Cross ()
    {

    }
    void Triggers ()
    {

    }
    void Bumpers ()
    {
        //Scripts
        sc_Shooting scs = GetComponent<sc_Shooting>();
        //Shoot
        if (Input.GetButtonDown("LB_" + _id.ToString()) ||
            Input.GetButtonDown("RB_" + _id.ToString()))
        {
            scs.Shoot();
        }
    }
    void Movements ()
    {
        Vector3 dirVector = new Vector3(leftHorizAxe, 0, leftVertAxe);
        rb.AddForce(_moveSpeed * dirVector, ForceMode.Acceleration);
    }
    void SkinRotation ()
    {
        Quaternion actualRot = _skin.transform.rotation;

        Quaternion brutRot = Quaternion.LookRotation(brutAppliedForce, Vector3.up);
        Quaternion lookRot = Quaternion.LookRotation(lookVector.normalized, Vector3.up);

        Quaternion lerpedBrutRot = Quaternion.RotateTowards(actualRot, brutRot, Time.deltaTime * _rotationSpeed);
        Quaternion lerpedLookRot = Quaternion.RotateTowards(actualRot, lookRot, Time.deltaTime * _rotationSpeed);

        if (Jleft)
        {
            if (Jright == false)
            {
                rot = lerpedBrutRot;
                lookVector = brutAppliedForce;
            }
            else
            {
                rot = lerpedLookRot;
            }
        }
        else
        {
            rot = lerpedLookRot;
        }

        _skin.transform.rotation = rot;
    }
}
