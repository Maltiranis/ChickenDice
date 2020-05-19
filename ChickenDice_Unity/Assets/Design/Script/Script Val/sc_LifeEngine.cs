using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_LifeEngine : MonoBehaviour
{
    [SerializeField] public int _life = 100;
    [SerializeField] public bool _respawn = true;
    [SerializeField] public float _respawnDelay = 2.0f;
    Vector3 startPos;
    float startY;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startY = transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int dmg)
    {
        if (_life - dmg <= 0)
        {
            Death();
        }
        else
        {
            _life -= dmg;
        }
    }

    public void Death()
    {
        Respawn();
    }

    public void Respawn()
    {

    }

    public IEnumerator DamageIEnumerator()
    {
        yield return new WaitForSeconds(_respawnDelay);
        transform.position = startPos;
        //transform.rotation = new Vector3(0f, startY, 0f, 0f);
    }
}
