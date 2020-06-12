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
    [Header("Offsets")]
    public GameObject _shootOffset_0;
    public GameObject _shootOffset_30;
    public GameObject _shootOffset_120;
    public GameObject _shootOffset_180;
    public GameObject _shootOffset_240;
    public GameObject _shootOffset_330;
    [Space(10)]
    [Header("Variables")]
    private float _refreshValue1 = 1.0f;
    private float _refreshValue2 = 1.0f;
    [HideInInspector] public Vector3 forceDirection;
    [SerializeField] private float _refillSpeed = 0.1f;

    [SerializeField] private bool leftTriggerCD = false;
    [SerializeField] private bool rightTriggerCD = false;
    //
    [SerializeField] public float _coolDownR = 1f;
    [SerializeField] public float _coolDownL = 1f;
    [Space(10)]
    [Header("Additional Objects")]
    [SerializeField] public GameObject[] _dashGhost;
    [SerializeField] public float _objectTimer = 0.5f;
    [SerializeField] public float _rateOverDist = 2f;
    GameObject pivot;
    ParticleSystem.EmissionModule em;

    float sizeP1 = 0f;
    float sizeP2 = 0f;
    float speedP1 = 0f;
    float speedP2 = 0f;
    int dmgP1 = 0;
    int dmgP2 = 0;
    float distP1 = 0f;
    float distP2 = 0f;

    //
    //[Space(10)]
    //[Header("UI")]
    //[SerializeField] private Image[] _bar;
    //[SerializeField] private TextMeshProUGUI[] _timeText;

    //[SerializeField] private GameObject[] _UI;

    // Start is called before the first frame update
    void Start()
    {
        _id = _myID.ID;
        _refreshValue1 = _coolDownL;
        _refreshValue2 = _coolDownR;

        pivot = new GameObject("pivot Chicken " + _myID.ID.ToString());
        pivot.transform.SetParent(null);
        foreach (GameObject d in _dashGhost)
        {
            ParticleSystem.EmissionModule dem = d.GetComponent<ParticleSystem>().emission;
            dem.rateOverDistance = 0;
        }
        em = _dashGhost[_id].GetComponent<ParticleSystem>().emission;
        /*foreach (GameObject ui in _UI)
        {
            ui.SetActive(false);
        }
        _UI[_id].SetActive(true);*/
    }

    public void DestroyPivot()
    {
        em.rateOverDistance = 0f;
        Destroy(pivot);
    }

    // Update is called once per frame
    void Update()
    {
        OnCoolDown();
        if (pivot != null)
            pivot.transform.position = Vector3.Lerp(pivot.transform.position, transform.position, Time.deltaTime * 100000000);
    }

    public void OnCoolDown()
    {
        if (leftTriggerCD)
        {
            float amount = _refreshValue1 / _coolDownL;
            float buttonAngle = amount * 360;

            //_bar[_id].fillAmount = amount;

            if (_refreshValue1 < _coolDownL)
            {
                _refreshValue1 += Time.deltaTime * _refillSpeed;
                //_timeText[_id].text = (_coolDownL - System.Math.Round(10 * _refreshValue1, 1)).ToString();
            }
            if (_refreshValue1 >= _coolDownL)
            {
                _refreshValue1 = _coolDownL;
                //_timeText[_id].text = "Fire !";
                leftTriggerCD = false;
            }
        }
        if (rightTriggerCD)
        {
            float amount = _refreshValue2 / _coolDownR;
            float buttonAngle = amount * 360;

            //_bar[_id].fillAmount = (1/_coolDownR) / amount;

            if (_refreshValue2 < _coolDownR)
            {
                _refreshValue2 += Time.deltaTime * _refillSpeed;
                //_timeText[_id].text = (_coolDownR - System.Math.Round(10 * _refreshValue2, 1)).ToString();
            }
            if (_refreshValue2 >= _coolDownR)
            {
                _refreshValue2 = _coolDownR;
                //_timeText[_id].text = "Fire !";
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
                    speedP1 = s_sbP1._v._speed;
                    dmgP1 = s_sbP1._v._damage;
                    distP1 = s_sbP1._v._maxDistance;

                    //Spinning
                    if (s_sbP1._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning ||
                        s_sbP1._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                    {
                        if (shot != null)
                            shot.transform.SetParent(pivot.transform);
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

                    //Size
                    if (s_sbP1._getPassives_Left == sc_SpellBehaviours.Passives_L.Size ||
                        s_sbP1._getPassives_Right == sc_SpellBehaviours.Passives_R.Size)
                    {
                        s_sbS._getPassives_Left = sc_SpellBehaviours.Passives_L.Size;
                        sizeP1 = s_sbP1._v._addThisSize;
                    }
                }
                if (P2 != null)
                {
                    sc_SpellBehaviours s_sbP2 = P2.GetComponent<sc_SpellBehaviours>();
                    speedP2 = s_sbP2._v._speed;
                    dmgP2 = s_sbP2._v._damage;
                    distP2 = s_sbP2._v._maxDistance;

                    //Spinning
                    if (s_sbP2._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning ||
                        s_sbP2._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                    {
                        if (shot != null)
                            shot.transform.SetParent(pivot.transform);
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

                    //Size
                    if (s_sbP2._getPassives_Left == sc_SpellBehaviours.Passives_L.Size ||
                        s_sbP2._getPassives_Right == sc_SpellBehaviours.Passives_R.Size)
                    {
                        s_sbS._getPassives_Right = sc_SpellBehaviours.Passives_R.Size;
                        sizeP2 = s_sbP2._v._addThisSize;
                    }
                }
                float sizeSums = sizeP1 + sizeP2 + s_sbS._v._mySize.x;
                float speedSums = speedP1 + speedP2 + s_sbS._v._speed;
                int dmgSums = dmgP1 + dmgP2 + s_sbS._v._damage;
                float distSums = distP1 + distP2 + s_sbS._v._maxDistance;

                //TOUT CE JOUE ICI AVEC LES PASSIF
                Vector3 bistromatic = new Vector3
                (
                    s_sbS._v._startSize.x + sizeSums,
                    s_sbS._v._startSize.y + sizeSums,
                    s_sbS._v._startSize.z + sizeSums
                );
                s_sbS._v._mySize = bistromatic;
                s_sbS._v._speed = speedSums * 10;//enlever les x10
                s_sbS._v._spreadSpeed = speedSums / 2 * 10;
                s_sbS._v._orbitSpeed = speedSums * 10;
                s_sbS._v._damage = dmgSums;
                s_sbS._v._maxDistance = distSums;
                //ET C'EST TRES HOT !!!
                //
                if (s_sbS._getProfile == sc_SpellBehaviours.Profile.PumpGun)
                {
                    //ShootInstance(A, P1, P2, 30);
                    //ShootInstance(A, P1, P2, 330);
                    if (shot != null)
                        newShootInstance(shot, 30);
                    if (shot != null)
                        newShootInstance(shot, 330);
                }

                //Dash Shield Heal
                if (s_sbS._getProfile == sc_SpellBehaviours.Profile.Shield ||
                    s_sbS._getProfile == sc_SpellBehaviours.Profile.Heal ||
                    s_sbS._getProfile == sc_SpellBehaviours.Profile.Dash)
                {
                    s_sbS._getPassives_Left = sc_SpellBehaviours.Passives_L.EMPTY;
                    s_sbS._getPassives_Right = sc_SpellBehaviours.Passives_R.EMPTY;


                    if (s_sbS._getProfile == sc_SpellBehaviours.Profile.Dash)
                    {
                        em.rateOverDistance = _rateOverDist;
                        StartCoroutine(DashDuration(_objectTimer));

                        GetComponent<Rigidbody>().AddForce
                        (
                            _shootOffset_0.transform.forward * s_sbS._v.speedDash, ForceMode.Impulse
                        );
                    }

                    if (shot != null)
                        shot.transform.SetParent(pivot.transform);

                    if (s_sbS._getProfile == sc_SpellBehaviours.Profile.Heal)
                    {
                        sc_LifeEngine le = GetComponent<sc_LifeEngine>();
                        if (le._life >= le.startLife)
                        {
                            le._life = le.startLife;
                        }
                        else
                        {
                            if (le._life + s_sbS._v.healValue > le.startLife)
                            {
                                le._life = le.startLife;
                            }
                            else
                            {
                                le._life += s_sbS._v.healValue;
                            }
                        }
                    }
                }

                #region M + Null / Null + M
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.EMPTY)
                {
                    s_sbS._v._iterationOnLaunch = 1;
                    //StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
                    if (shot != null)
                        StartCoroutine(newMultipleCasts(s_sbS._v._timeToWait, shot));

                }

                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.EMPTY &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                {
                    s_sbS._v._iterationOnLaunch = 1;
                    //StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
                    if (shot != null)
                        StartCoroutine(newMultipleCasts(s_sbS._v._timeToWait, shot));
                }
                #endregion
                #region M + C / C + M
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Chain)
                {
                    s_sbS._v._iterationOnLaunch = 1;
                    s_sbS._v._iterationOnDestroyed = 1;
                    //StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
                    if (shot != null)
                        StartCoroutine(newMultipleCasts(s_sbS._v._timeToWait, shot));
                }

                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Chain &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                {
                    s_sbS._v._iterationOnLaunch = 1;
                    s_sbS._v._iterationOnDestroyed = 1;
                    //StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
                    if (shot != null)
                        StartCoroutine(newMultipleCasts(s_sbS._v._timeToWait, shot));
                }
                #endregion
                #region S + C / C + S
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Chain)
                {
                    s_sbS._v._iterationOnDestroyed = 1;
                    //ShootInstance(A, P1, P2, 180);
                    if (shot != null)
                        newShootInstance(shot, 180);
                }

                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Chain &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                {
                    s_sbS._v._iterationOnDestroyed = 1;
                    //ShootInstance(A, P1, P2, 180);
                    if (shot != null)
                        newShootInstance(shot, 180);
                }
                #endregion
                #region S + M / M + S
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                {
                    //ShootInstance(A, P1, P2, 180);
                    if (shot != null)
                        newShootInstance(shot, 180);
                }
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                {
                    //ShootInstance(A, P1, P2, 180);
                    if (shot != null)
                        newShootInstance(shot, 180);
                }
                #endregion
                #region M + M
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Multiple &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Multiple)
                {
                    s_sbS._v._iterationOnLaunch = 2;
                    //StartCoroutine(MultipleCasts(s_sbS._v._timeToWait, A, P1, P2));
                    //StartCoroutine(MultipleCasts(2*s_sbS._v._timeToWait, A, P1, P2));
                    if (shot != null)
                        StartCoroutine(newMultipleCasts(s_sbS._v._timeToWait, shot));
                    if (shot != null)
                        StartCoroutine(newMultipleCasts(2 * s_sbS._v._timeToWait, shot));
                }
                #endregion
                #region S + S
                if (s_sbS._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning &&
                    s_sbS._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
                {
                    s_sbS._v._iterationOnLaunch = 2;
                    //ShootInstance(A, P1, P2, 120);
                    //ShootInstance(A, P1, P2, 240);
                    if (shot != null)
                        newShootInstance(shot, 120);
                    if (shot != null)
                        newShootInstance(shot, 240);
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

    public void newShootInstance(GameObject shot, int angleOffset)
    {
        GameObject nshot = null;

        if (angleOffset == 0)
            nshot = Instantiate(shot, _shootOffset_0.transform.position, _shootOffset_0.transform.rotation);
        if (angleOffset == 30)
            nshot = Instantiate(shot, _shootOffset_30.transform.position, _shootOffset_30.transform.rotation);
        if (angleOffset == 120)
            nshot = Instantiate(shot, _shootOffset_120.transform.position, _shootOffset_120.transform.rotation);
        if (angleOffset == 180)
            nshot = Instantiate(shot, _shootOffset_180.transform.position, _shootOffset_180.transform.rotation);
        if (angleOffset == 240)
            nshot = Instantiate(shot, _shootOffset_240.transform.position, _shootOffset_240.transform.rotation);
        if (angleOffset == 330)
            nshot = Instantiate(shot, _shootOffset_330.transform.position, _shootOffset_330.transform.rotation);

        sc_SpellBehaviours s_sb = shot.GetComponent<sc_SpellBehaviours>();
        //Spinning
        if (s_sb._getPassives_Left == sc_SpellBehaviours.Passives_L.Spinning ||
            s_sb._getPassives_Right == sc_SpellBehaviours.Passives_R.Spinning)
        {
            nshot.transform.SetParent(pivot.transform);
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
            if (angleOffset == 30)
                shot = Instantiate(A, _shootOffset_30.transform.position, _shootOffset_30.transform.rotation);
            if (angleOffset == 120)
                shot = Instantiate(A, _shootOffset_120.transform.position, _shootOffset_120.transform.rotation);
            if (angleOffset == 180)
                shot = Instantiate(A, _shootOffset_180.transform.position, _shootOffset_180.transform.rotation);
            if (angleOffset == 240)
                shot = Instantiate(A, _shootOffset_240.transform.position, _shootOffset_240.transform.rotation);
            if (angleOffset == 330)
                shot = Instantiate(A, _shootOffset_330.transform.position, _shootOffset_330.transform.rotation);

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
                    shot.transform.SetParent(pivot.transform);
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
                    shot.transform.SetParent(pivot.transform);
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
        }
    }

    public IEnumerator MultipleCasts(float timer, GameObject A, GameObject P1, GameObject P2)
    {
        yield return new WaitForSeconds(timer);

        ShootInstance(A, P1, P2, 0);
    }
    public IEnumerator newMultipleCasts(float timer, GameObject shot)
    {
        yield return new WaitForSeconds(timer);

        if (shot != null)
            newShootInstance(shot, 0);
    }
    public IEnumerator DashDuration(float timer)
    {
        yield return new WaitForSeconds(timer);
        em.rateOverDistance = 0f;
    }
}
