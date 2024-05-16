using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    GameObject player;
    NavMeshAgent enemy;
    [SerializeField] int jarakStop;
    [SerializeField] string anim;
    [SerializeField] Animator zombieAnim;
    Health health;
    Gun gun;
    bool udahSampai, serangPlayer;
    Vector3 sisaJarak, posisi;
    Quaternion rotasi;

    void Start () 
    { 
        gun = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<NavMeshAgent>();
        health = this.gameObject.GetComponent<Health>();
    }
   
    void Update () 
    {
        sisaJarak = this.transform.position;
        enemy.destination = player.transform.position;

        if (this.transform.position.z < jarakStop && !udahSampai && !health.sudahMati)
        {
            posisi = this.transform.position;
            rotasi = this.transform.rotation;
            udahSampai = true;
            zombieAnim.Play(anim);
            enemy.Stop();
        }

        if (udahSampai)
        {
            this.transform.position = posisi;
            this.transform.rotation = rotasi; 
            if (!serangPlayer)
            {
                StartCoroutine(AttackPlayer()); 
                serangPlayer = true;
            }
                
            IEnumerator AttackPlayer()
            {
                yield return new WaitForSeconds(1.2f);
                gun.playerHealth -= 4;
                serangPlayer = false;
            }
        }
    }
}
