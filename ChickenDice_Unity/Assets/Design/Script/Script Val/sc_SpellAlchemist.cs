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
    public GameObject _shootOffset_0;
    public GameObject _shootOffset_120;
    public GameObject _shootOffset_180;
    public GameObject _shootOffset_240;
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

        if (A != null)
        {
            if (leftTriggerCD == false && inputUsed == "LT")
            {
                _coolDownL = s_sbA._v._coolDown / 10;
                shot = Instantiate(A, _shootOffset_0.transform.position, _shootOffset_0.transform.rotation);

                _refreshValue1 = 0f;
                leftTriggerCD = true;
            }
            if (rightTriggerCD == false && inputUsed == "RT")
            {
                _coolDownR = s_sbA._v._coolDown / 10;
                shot = Instantiate(A, _shootOffset_0.transform.position, _shootOffset_0.transform.rotation);

                _refreshValue2 = 0f;
                rightTriggerCD = true;
            }

            if (shot != null)
            {
                //si P1 ou P2 c'est la bombe on parente "shot"
                sc_SpellBehaviours s_sbS = shot.GetComponent<sc_SpellBehaviours>();
                s_sbS._v._id = _id;

                if (P1 != null)
                {
                    sc_SpellBehaviours s_sbP1 = P1.GetComponent<sc_SpellBehaviours>();
                    //Spinning
                    if (s_sbP1._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning ||
                        s_sbP1._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                    {
                        shot.transform.parent = transform;
                        s_sbS._getPassives_Left = sc_SpellBehaviours.Passives_L.Spinning;
                    }

                    //Multiple
                    if (s_sbP1._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple ||
                        s_sbP1._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                    {
                        s_sbS._getPassives_Left = sc_SpellBehaviours.Passives_L.Multiple;
                    }

                    //Chain
                    if (s_sbP1._getPassives_Left == sc_SpellBehaviours.Passives_L.Chain ||
                        s_sbP1._getPassives_Right == sc_SpellBehaviours.Passives_R.Chain)
                    {
                        s_sbS._getPassives_Left = sc_SpellBehaviours.Passives_L.Chain;
                    }
                }
                if (P2 != null)
                {
                    sc_SpellBehaviours s_sbP2 = P2.GetComponent<sc_SpellBehaviours>();
                    //Spinning
                    if (s_sbP2._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning ||
                        s_sbP2._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                    {
                        shot.transform.parent = transform;
                        s_sbS._getPassives_Right = sc_SpellBehaviours.Passives_R.Spinning;
                    }

                    //Multiple
                    if (s_sbP2._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple ||
                        s_sbP2._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                    {
                        s_sbS._getPassives_Right = sc_SpellBehaviours.Passives_R.Multiple;
                    }

                    //Chain
                    if (s_sbP2._getPassives_Left == sc_SpellBehaviours.Passives_L.Chain ||
                        s_sbP2._getPassives_Right == sc_SpellBehaviours.Passives_R.Chain)
                    {
                        s_sbS._getPassives_Right = sc_SpellBehaviours.Passives_R.Chain;
                    }
                }
                #region M + Null / Null + M
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.EMPTY)
                {
                    s_sbS._v._iterationOnLaunch = 1;
                    StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
                }

                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.EMPTY &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                {
                    s_sbS._v._iterationOnLaunch = 1;
                    StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
                }
                #endregion
                #region M + C / C + M
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Chain)
                {
                    s_sbS._v._iterationOnLaunch = 1;
                    s_sbS._v._iterationOnDestroyed = 1;
                    StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
                }

                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Chain &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                {
                    s_sbS._v._iterationOnLaunch = 1;
                    s_sbS._v._iterationOnDestroyed = 1;
                    StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
                }
                #endregion
                #region S + C / C + S
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Chain)
                {
                    s_sbS._v._iterationOnDestroyed = 1;
                    ShootInstance(A, P1, P2, 180);
                }

                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Chain &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                {
                    s_sbS._v._iterationOnDestroyed = 1;
                    ShootInstance(A, P1, P2, 180);
                }
                #endregion
                #region S + M / M + S
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                {
                    ShootInstance(A, P1, P2, 180);
                }
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                {
                    ShootInstance(A, P1, P2, 180);
                }
                #endregion
                #region M + M
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                {
                    s_sbS._v._iterationOnLaunch = 2;
                    StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
                    StartCoroutine(MultipleCasts(2*s_sbS._v._timeToWait, A, P1, P2));
                }
                #endregion
                #region S + S
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                {
                    s_sbS._v._iterationOnLaunch = 2;
                    ShootInstance(A, P1, P2, 120);
                    ShootInstance(A, P1, P2, 240);
                }
                #endregion
                #region C + C
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Chain &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Chain)
                {
                    s_sbS._v._iterationOnDestroyed = 2;
                }
                #endregion
            }
        }
    }

    public void ShootInstance(GameObject A, GameObject P1, GameObject P2, int angleOffset)
    {
        GameObject shot = null;
        sc_SpellBehaviours s_sbA = A.GetComponent<sc_SpellBehaviours>();

        if (A != null)
        {
            if (angleOffset == 0)
                shot = Instantiate(A, _shootOffset_0.transform.position, _shootOffset_0.transform.rotation);
            if (angleOffset == 120)
                shot = Instantiate(A, _shootOffset_120.transform.position, _shootOffset_120.transform.rotation);
            if (angleOffset == 180)
                shot = Instantiate(A, _shootOffset_180.transform.position, _shootOffset_180.transform.rotation);
            if (angleOffset == 240)
                shot = Instantiate(A, _shootOffset_240.transform.position, _shootOffset_240.transform.rotation);

                //si P1 ou P2 c'est la bombe on parente "shot"
                sc_SpellBehaviours s_sbS = shot.GetComponent<sc_SpellBehaviours>();
                s_sbS._v._id = _id;

            if (P1 != null)
            {
                sc_SpellBehaviours s_sbP1 = P1.GetComponent<sc_SpellBehaviours>();
                //Spinning
                if (s_sbP1._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning ||
                    s_sbP1._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                {
                    shot.transform.parent = transform;
                    s_sbS._getPassives_Left = sc_SpellBehaviours.Passives_L.Spinning;
                }

                //Multiple
                if (s_sbP1._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple ||
                    s_sbP1._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                {
                    s_sbS._getPassives_Left = sc_SpellBehaviours.Passives_L.Multiple;
                }

                //Chain
                if (s_sbP1._getPassives_Left == sc_SpellBehaviours.Passives_L.Chain ||
                    s_sbP1._getPassives_Right == sc_SpellBehaviours.Passives_R.Chain)
                {
                    s_sbS._getPassives_Left = sc_SpellBehaviours.Passives_L.Chain;
                }
            }
            if (P2 != null)
            {
                sc_SpellBehaviours s_sbP2 = P2.GetComponent<sc_SpellBehaviours>();
                //Spinning
                if (s_sbP2._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning ||
                    s_sbP2._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                {
                    shot.transform.parent = transform;
                    s_sbS._getPassives_Right = sc_SpellBehaviours.Passives_R.Spinning;
                }

                //Multiple
                if (s_sbP2._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple ||
                    s_sbP2._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                {
                    s_sbS._getPassives_Right = sc_SpellBehaviours.Passives_R.Multiple;
                }

                //Chain
                if (s_sbP2._getPassives_Left == sc_SpellBehaviours.Passives_L.Chain ||
                    s_sbP2._getPassives_Right == sc_SpellBehaviours.Passives_R.Chain)
                {
                    s_sbS._getPassives_Right = sc_SpellBehaviours.Passives_R.Chain;
                }
            }
            /*#region M + Null / Null + M
            if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.EMPTY)
            {
                s_sbS._v._iterationOnLaunch = 1;
                StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
            }

            if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.EMPTY &&
                s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
            {
                s_sbS._v._iterationOnLaunch = 1;
                StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
            }
            #endregion
            #region M + C / C + M
            if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Chain)
            {
                s_sbS._v._iterationOnLaunch = 1;
                s_sbS._v._iterationOnDestroyed = 1;
                StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
            }

            if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Chain &&
                s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
            {
                s_sbS._v._iterationOnLaunch = 1;
                s_sbS._v._iterationOnDestroyed = 1;
                StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
            }
            #endregion
            #region S + C / C + S
            if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning &&
                s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Chain)
            {
                s_sbS._v._iterationOnDestroyed = 1;
                ShootInstance(A, P1, P2, 180);
            }

            if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Chain &&
                s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
            {
                s_sbS._v._iterationOnDestroyed = 1;
                ShootInstance(A, P1, P2, 180);
            }
            #endregion
            #region S + M / M + S
            if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning &&
                s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
            {
                ShootInstance(A, P1, P2, 180);
            }
            if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
            {
                ShootInstance(A, P1, P2, 180);
            }
            #endregion
            #region M + M
            if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
            {
                s_sbS._v._iterationOnLaunch = 2;
                StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
                StartCoroutine(MultipleCasts(2 * s_sbS._v._timeToWait, A, P1, P2));
            }
            #endregion
            #region S + S
            if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning &&
                s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
            {
                s_sbS._v._iterationOnLaunch = 2;
                ShootInstance(A, P1, P2, 120);
                ShootInstance(A, P1, P2, 240);
            }
            #endregion*/
        }
    }

    public IEnumerator MultipleCasts (float timer, GameObject A, GameObject P1, GameObject P2)
    {
        yield return new WaitForSeconds(timer);

        ShootInstance(A, P1, P2, 0);
    }
}
