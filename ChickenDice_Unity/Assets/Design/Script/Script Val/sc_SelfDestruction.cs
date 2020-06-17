using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SelfDestruction : MonoBehaviour
{
    float newRange;
    public bool timedDestroy = false;
    public float _timeLeft = 2f;

    public void KillEverybody(float _dmg, int _index, float _range)
    {
        if (_range < 1f)
        {
            _range += 0.5f;
        }
        //chopper tout les joueurs à porter et leur coller un TakeDamage + dégat
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            if (Vector3.Distance(transform.position, p.transform.position) <= _range &&
            p.GetComponent<sc_Chicken_ID>().ID != _index)
                p.GetComponent<sc_LifeEngine>().TakeDamage(_dmg, _index);
        }
        newRange = _range;
    }

    public void Start()
    {
        if (timedDestroy == true)
        {
            DestroyMyself(_timeLeft);
        }
    }

    public void DestroyMyself(float _timer)
    {
        Destroy(gameObject, _timer);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, newRange);
    }
}
