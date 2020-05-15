using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_ChickenController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _sensibility = 0.19f;

    float leftVertAxe = 0f;
    float leftHorizAxe = 0f;
    float rightVertAxe = 0f;
    float rightHorizAxe = 0f;

    Vector3 lookVector;
    Vector3 brutAppliedForce;
    Vector3 lastAngle;
    Rigidbody rb;
    public GameObject _skin;

    // Start is called before the first frame update
    void Start()
    {
        leftVertAxe = Input.GetAxis("LJoyVertical");
        leftHorizAxe = Input.GetAxis("LJoyHorizontal");
        rightVertAxe = Input.GetAxis("RJoyVertical");
        rightHorizAxe = Input.GetAxis("RJoyHorizontal");
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
        if (Input.GetAxis("LJoyVertical") > _sensibility ||
            Input.GetAxis("LJoyVertical") < -_sensibility)
        {
            leftVertAxe = -Input.GetAxis("LJoyVertical");
        }
        else
        {
            leftVertAxe = 0f;
        }

        if (Input.GetAxis("LJoyHorizontal") > _sensibility ||
            Input.GetAxis("LJoyHorizontal") < -_sensibility)
        {
            leftHorizAxe = Input.GetAxis("LJoyHorizontal");
        }
        else
        {
            leftHorizAxe = 0f;
        }

        brutAppliedForce = new Vector3(Input.GetAxis("LJoyHorizontal"), 0, -Input.GetAxis("LJoyVertical"));
    }
    void RightJoy ()
    {
        if (Input.GetAxis("RJoyVertical") != 0f)
        {
            rightVertAxe = -Input.GetAxis("RJoyVertical");
        }
        else
        {
            rightVertAxe = brutAppliedForce.z;
        }

        if (Input.GetAxis("RJoyHorizontal") != 0f)
        {
            rightHorizAxe = Input.GetAxis("RJoyHorizontal");
        }
        else
        {
            rightHorizAxe = brutAppliedForce.x;
        }

        lookVector = new Vector3(rightHorizAxe, 0, rightVertAxe);
    }
    void FourButtons ()
    {

    }
    void Cross ()
    {

    }
    void Triggers ()
    {

    }
    void Bumpers ()
    {

    }
    void Movements ()
    {
        Vector3 dirVector = new Vector3(leftHorizAxe, 0, leftVertAxe);
        rb.AddForce(_moveSpeed * dirVector, ForceMode.Acceleration);
    }
    void SkinRotation ()
    {
        if (lookVector != Vector3.zero)
        {
            lastAngle = lookVector; // PROBLEME DE RETOUR A ZERO
        }
        else
        {
            lastAngle = new Vector3(0f, _skin.transform.rotation.y, 0f);
        }
        Quaternion rot = Quaternion.LookRotation(lastAngle, Vector3.up);
        _skin.transform.rotation = rot;
    }
}
