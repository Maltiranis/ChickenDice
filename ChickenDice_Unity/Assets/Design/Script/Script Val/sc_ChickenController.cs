using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_ChickenController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _Lsensibility = 0.19f;
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
        if (Input.GetAxis("LJoyVertical") > _Lsensibility ||
            Input.GetAxis("LJoyVertical") < -_Lsensibility ||
            Input.GetAxis("LJoyHorizontal") > _Lsensibility ||
            Input.GetAxis("LJoyHorizontal") < -_Lsensibility)
        {
            leftHorizAxe = Input.GetAxis("LJoyHorizontal");
            leftVertAxe = -Input.GetAxis("LJoyVertical");

            brutAppliedForce = new Vector3(Input.GetAxis("LJoyHorizontal"), 0, -Input.GetAxis("LJoyVertical"));
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
        if (Input.GetAxis("RJoyVertical") > 0 ||
            Input.GetAxis("RJoyVertical") < 0 ||
            Input.GetAxis("RJoyHorizontal") > 0 ||
            Input.GetAxis("RJoyHorizontal") < 0)
        {
            rightVertAxe = -Input.GetAxis("RJoyVertical");
            rightHorizAxe = Input.GetAxis("RJoyHorizontal");

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
        if (Jleft)
        {
            if (Jright == false)
            {
                rot = Quaternion.LookRotation(brutAppliedForce.normalized, Vector3.up);
                lookVector = brutAppliedForce;
            }
            else
            {
                rot = Quaternion.LookRotation(lookVector.normalized, Vector3.up);
            }
        }
        else
        {
            rot = Quaternion.LookRotation(lookVector.normalized, Vector3.up);
        }

        _skin.transform.rotation = rot;
    }
}
