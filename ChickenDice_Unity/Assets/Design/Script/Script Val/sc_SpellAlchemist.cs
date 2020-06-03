using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class sc_SpellAlchemist : MonoBehaviour
{
    [Header("Chicken ID")]
    [SerializeField] private sc_Chicken_ID _myID;
    int _id;
    [Space(10)]
    [Header("Objects")]
    public GameObject _shootOffset;
    [Space(10)]
    [Header("Variables")]
    private float _refreshValue1 = 1.0f;
    private float _refreshValue2 = 1.0f;
    [SerializeField] private float _refillSpeed = 0.1f;

    [SerializeField] private bool leftTriggerCD = false;
    [SerializeField] private bool rightTriggerCD = false;
    //
    [SerializeField] public float _coolDownR = 1f;
    [SerializeField] public float _coolDownL = 1f;
    //
    [Space(10)]
    [Header("UI")]
    [SerializeField] private Image[] _bar;
    [SerializeField] private TextMeshProUGUI[] _timeText;

    [SerializeField] private GameObject[] _UI;

    // Start is called before the first frame update
    void Start()
    {
        _id = _myID.ID;
        _refreshValue1 = _coolDownL;
        _refreshValue2 = _coolDownR;

        foreach (GameObject ui in _UI)
        {
            ui.SetActive(false);
        }
        _UI[_id].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        OnCoolDown();
    }

    public void OnCoolDown()
    {
        if (leftTriggerCD)
        {
            float amount = _refreshValue1 / _coolDownL;
            float buttonAngle = amount * 360;

            _bar[_id].fillAmount = amount;

            if (_refreshValue1 < _coolDownL)
            {
                _refreshValue1 += Time.deltaTime * _refillSpeed;
                _timeText[_id].text = (_coolDownL - System.Math.Round(10 * _refreshValue1, 1)).ToString();
            }
            if (_refreshValue1 >= _coolDownL)
            {
                _refreshValue1 = _coolDownL;
                _timeText[_id].text = "Fire !";
                leftTriggerCD = false;
            }
        }
        if (rightTriggerCD)
        {
            float amount = _refreshValue2 / _coolDownR;
            float buttonAngle = amount * 360;

            _bar[_id].fillAmount = (1/_coolDownR) / amount;

            if (_refreshValue2 < _coolDownR)
            {
                _refreshValue2 += Time.deltaTime * _refillSpeed;
                _timeText[_id].text = (_coolDownR - System.Math.Round(10 * _refreshValue2, 1)).ToString();
            }
            if (_refreshValue2 >= _coolDownR)
            {
                _refreshValue2 = _coolDownR;
                _timeText[_id].text = "Fire !";
                rightTriggerCD = false;
            }
        }
    }

    public void Shoot(GameObject A, GameObject P1, GameObject P2, string inputUsed)
    {
        GameObject shot = null;
        sc_SpellBehaviours s_sbA = A.GetComponent<sc_SpellBehaviours>();
        sc_SpellBehaviours s_sbP1 = A.GetComponent<sc_SpellBehaviours>();
        sc_SpellBehaviours s_sbP2 = A.GetComponent<sc_SpellBehaviours>();

        if (P1 != null)
        {
            //Profile = PASSIVE

        }
        if (P2 != null)
        {
            //Profile = PASSIVE

        }
        if (A != null)
        {
            if (leftTriggerCD == false && inputUsed == "LT")
            {
                _coolDownL = s_sbA._v._coolDown/10;
                shot = Instantiate(A, _shootOffset.transform.position, _shootOffset.transform.rotation);

                _refreshValue1 = 0f;
                leftTriggerCD = true;
            }
            if (rightTriggerCD == false && inputUsed == "RT")
            {
                _coolDownR = s_sbA._v._coolDown/10;
                shot = Instantiate(A, _shootOffset.transform.position, _shootOffset.transform.rotation);

                _refreshValue2 = 0f;
                rightTriggerCD = true;
            }

            if (shot != null)
            {
                //si P1 ou P2 c'est la bombe on parente "shot"

            }
        }
    }
}
