using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int startHealth;
    [SerializeField]
    int currentHealth;
    [SerializeField] Animator anim;
    [SerializeField] string animName;
    [SerializeField] CapsuleCollider capCol;
    public bool sudahMati;
    Gun gS;

    void Start()
    {
        gS = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
    }
   
    void OnEnable()
    {
        currentHealth = startHealth;
    }
 
    public void TakeDamage (int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            if (!sudahMati)
            {
                sudahMati = true;
                gS.enemKilled++;
                gS.killed.text = gS.enemKilled.ToString();
                Die();
            }
        }
    }

    private void Die()
    {
        anim.Play(animName);
        StartCoroutine(destroyEnemy());
    }

    IEnumerator destroyEnemy()
    {
        yield return new WaitForSeconds(0.2f);
        capCol.enabled = false;
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
}
