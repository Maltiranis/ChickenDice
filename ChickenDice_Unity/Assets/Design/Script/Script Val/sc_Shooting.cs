/*
SI cooldown disponible = Tire un/plusieur projectils (après délais)
qui se déplace dans l'axe R3 et qui se détruit au 1er contact
+ lance le cooldown + UI.
Si touche un Player = applique effets (Explosion) SI atteint max Range explose
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class sc_Shooting : MonoBehaviour
{
    [Header("Chicken ID")]
    [SerializeField] private sc_Chicken_ID _myID;
    int _id;
    [Space(10)]
    [Header("Objects")]
    public GameObject _projectilePrefab;
    public GameObject _shootOffset;
    [Space(10)]
    [Header("Variables")]
    [SerializeField] private float _coolDown = 1.0f;
    private float _refreshValue = 1.0f;
    [SerializeField] private float _refillSpeed = 0.1f;
    //
    [Header("Minimal bullet")]
    [SerializeField] private int _minimalDamage = 10;
    [SerializeField] private float _minimalSpeed = 10f;
    [SerializeField] private float _minimalRadius = 1f;
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
        _refreshValue = _coolDown;

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

    public void OnCoolDown ()
    {
        float amount = _refreshValue;
        float buttonAngle = amount * 360;

        _bar[_id].fillAmount = amount;

        if (_refreshValue < _coolDown)
        {
            _refreshValue += Time.deltaTime * _refillSpeed;
            _timeText[_id].text = (_coolDown - System.Math.Round(_refreshValue,1)).ToString();
        }
        if (_refreshValue >= _coolDown)
        {
            _refreshValue = _coolDown;
            _timeText[_id].text = "Fire !";
        }
    }

    public void Shoot (GameObject A, GameObject P1, GameObject P2)
    {
        if (_refreshValue == _coolDown)
        {
            GameObject shot = null;

            sc_VariablesManager s_svm = null;
            sc_VariablesManager p1_svm = null;
            sc_VariablesManager p2_svm = null;

            int dmg;
            float speed;
            float radius;

            dmg = _minimalDamage;
            speed = _minimalSpeed;
            radius = _minimalRadius;

            if (P1 != null)
            {
                p1_svm = P1.GetComponent<sc_VariablesManager>();
                dmg += p1_svm._damage;
                speed += p1_svm._speed;
                radius += p1_svm._radius;
            }
            if (P2 != null)
            {
                p2_svm = P2.GetComponent<sc_VariablesManager>();
                dmg += p2_svm._damage;
                speed += p2_svm._speed;
                radius += p2_svm._radius;
            }
            if (A != null)
            {
                shot = Instantiate(A, _shootOffset.transform.position, _shootOffset.transform.rotation);
                s_svm = shot.GetComponent<sc_VariablesManager>();

                s_svm._damage = dmg;
                s_svm._speed = speed;
                s_svm._radius = radius;
            }
            else
            {
                Debug.Log("Y'a rien frère !");
                shot = null;
            }
            _refreshValue = 0.0f;
        }
    }
}
