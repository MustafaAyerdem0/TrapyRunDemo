using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
 
    private Animator _anim;
    private Rigidbody rb;

    void Start()
    {
        _anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        

        if (Input.touchCount > 0)
        {
            _anim.SetBool("isRunning", true);
        }

    }

    void FixedUpdate()
    {
        if (_anim.GetBool("isRunning") == true && !_anim.GetBool("isDead"))
        {
            enemy.destination = player.position;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "FinishLine"
            || transform.position.y < 0 || collision.gameObject.tag == "Barrier")
        {
            _anim.SetBool("isDead", true);
            Destroy(gameObject, 2);
        }
    }
}
