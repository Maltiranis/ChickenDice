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
    [SerializeField] private float _projectileSpeed = 5.0f;
    [SerializeField] private float _projDuration = 1f;
    [Space(5)]
    [SerializeField] private float _coolDown = 1.0f;
    private float _refreshValue = 1.0f;
    [SerializeField] private float _refillSpeed = 0.1f;
    [Space(10)]
    [Header("UI")]
    [SerializeField] private Image _bar;
    [SerializeField] private TextMeshProUGUI _timeText;

    // Start is called before the first frame update
    void Start()
    {
        _id = _myID.ID;
        _refreshValue = _coolDown;
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

        _bar.fillAmount = amount;

        if (_refreshValue < _coolDown)
        {
            _refreshValue += Time.deltaTime * _refillSpeed;
            _timeText.text = (_coolDown - System.Math.Round(_refreshValue,1)).ToString();
        }
        if (_refreshValue >= _coolDown)
        {
            _refreshValue = _coolDown;
            _timeText.text = "Fire !";
        }
    }

    public void Shoot ()
    {
        if (_refreshValue == _coolDown)
        {
            GameObject shot = Instantiate(_projectilePrefab, _shootOffset.transform.position, transform.rotation);
            shot.GetComponent<Rigidbody>().AddForce(_shootOffset.transform.forward * _projectileSpeed, ForceMode.Impulse);
            Destroy(shot, _projDuration);
            _refreshValue = 0.0f;
        }
    }
}
