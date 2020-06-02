using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SelfDestruction : MonoBehaviour
{
    public void KillEverybody(int _dmg, int _index, float _range)
    {
        //chopper tout les joueurs à porter et leur coller un TakeDamage + dégat
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            if (Vector3.Distance(transform.position, p.transform.position) <= _range &&
            p.GetComponent<sc_Chicken_ID>().ID != _index)
                p.GetComponent<sc_LifeEngine>().TakeDamage(_dmg, _index);
        }
    }

    public void DestroyMyself(float _timer)
    {
        Destroy(gameObject, _timer);
    }
}
