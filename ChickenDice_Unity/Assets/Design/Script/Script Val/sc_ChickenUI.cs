using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_ChickenUI : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] public GameObject[] _satellites;
    [SerializeField] public GameObject[] _barElements;
    [SerializeField] public GameObject[] _sockets;

    [Header("UI Materials")]
    [SerializeField] public Material[] _matsAim;//Aim

    [SerializeField] public Material[] _matsSlot;//Slot

    [SerializeField] public Material[] _matsBar;//LifeBar

    [SerializeField] public Material[] _matsSa;//Slot Active
    [SerializeField] public Material[] _matsSs;//Slot Support

    int id;

    [SerializeField] public GameObject[] _cam;

    public GameObject[] _objectSwitchOnPhase;
    int phase = 0;
    GameObject pm;

    // Start is called before the first frame update
    void Start()
    {
        id = GetComponent<sc_Chicken_ID>().ID;

        GetAndMerge(_satellites, 0, _matsAim, id);
        GetAndMerge(_satellites, 1, _matsSlot, id);
        GetAndMerge(_barElements, 0, _matsBar, id);

        for (int i = 0; i < _sockets.Length; i++)
        {
            SocketsSelection(i, 0);
        }

        foreach (GameObject c in _cam)
        {
            c.SetActive(false);
        }

        _cam[id].SetActive(true);
        if (GameObject.Find("PhaseManager") != null)
        {
            pm = GameObject.Find("PhaseManager");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pm != null)
        {
            phase = pm.GetComponent<PhaseManager>()._Phase;
        }
        foreach (GameObject g in _objectSwitchOnPhase)
        {
            g.SetActive(false);
        }
        _objectSwitchOnPhase[phase].SetActive(true);
    }

    public void SocketsSelection (int socket, int mat)
    {
        //Socket 0, 1, 2, 3 = AR SR SL AL
        //Mat 0 Common, 1 Rare, 2 Leg

        switch (socket)
        {
            case 0:
                GetAndMerge(_sockets, socket, _matsSa, mat);
                break;
            case 1:
                GetAndMerge(_sockets, socket, _matsSs, mat);
                break;
            case 2:
                GetAndMerge(_sockets, socket, _matsSs, mat);
                break;
            case 3:
                GetAndMerge(_sockets, socket, _matsSa, mat);
                break;
            default:
                break;
        }
    }

    public void GetAndMerge (GameObject[] g, int gIndex, Material[] m, int mIndex)
    {
        MatForYou(ObjectAttribution(g, gIndex), m[mIndex]);
    }

    GameObject ObjectAttribution (GameObject[] gArray, int index)
    {
        return gArray[index];
    }

    public void MatForYou(GameObject O, Material M)
    {
        O.GetComponent<Renderer>().material = M;
    }
}
