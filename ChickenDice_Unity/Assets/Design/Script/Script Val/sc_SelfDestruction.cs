using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SelfDestruction : MonoBehaviour
{
    float newRange;

    public void KillEverybody(int _dmg, int _index, float _range)
    {
        if (_range < 1)
        {
            _range += 1;
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
