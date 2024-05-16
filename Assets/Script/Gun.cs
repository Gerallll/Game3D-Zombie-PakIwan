using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 1.5f)]
    private float fireRate;
    [SerializeField]
    [Range(1, 50)]
    private int damage;
    [SerializeField]
    [Range(1, 30)]
    private int pelor;
    bool lagiReload;
    float timer;
    GameObject holdFlash;
    public GameObject muzzelspawn;
    public GameObject[] muzzelFlash;
    [SerializeField] Animator police, camAnim;

    public float playerHealth;
    [SerializeField] Slider slider;
    [SerializeField] GameObject uiKalah;
    public bool playerDead;

    public int enemKilled;
    public TextMeshProUGUI killed;
    [SerializeField] TextMeshProUGUI ammo;

    void Update () 
    {
        ammo.text = pelor.ToString() + "/30";

        timer += Time.deltaTime;
        if (timer > fireRate)
        { 
            if (Input.GetButton("Fire1"))
            {
                if (pelor == 0 && !lagiReload)
                {
                    lagiReload = true;
                    StartCoroutine(Reload());;
                }
                else if (pelor <= 30 && !lagiReload)
                {
                    pelor -= 1;
                    timer = 0f;
                    FireGun();
                }
            }
            if (Input.GetKeyDown(KeyCode.R) && pelor < 30)
            {
                lagiReload = true;
                StartCoroutine (Reload()); ;
            }
        }

        if (playerHealth <= 0 && !playerDead)
        {
            playerDead = true;
            uiKalah.SetActive(true);
            camAnim.enabled = true;
            police.Play("Death");
            camAnim.Play("deathCam");
            StartCoroutine (DeathAnim());
        }
        slider.value = playerHealth;
    }

    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds (1.15f);
        Time.timeScale = 0;
    }

    public void retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void FireGun()
    {
        Ray ray = Camera.main.ViewportPointToRay (Vector3.one * 0.5f);
        Debug.DrawRay (ray.origin, ray.direction * 100, Color.red, 2f);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 10000))
        {
            int randomNumberForMuzzelFlash = Random.Range(0, 5);
            holdFlash = Instantiate (muzzelFlash [randomNumberForMuzzelFlash], muzzelspawn.transform.position ,muzzelspawn.transform.rotation * Quaternion. Euler(0, 0, 90)) as GameObject;
            holdFlash.transform.parent = muzzelspawn.transform;
            Destroy(holdFlash, 0.1f);
            var health = hitInfo.collider.GetComponent<Health>();
            if (health != null)
            {
                health. TakeDamage (damage);
            }
        }
    }

    IEnumerator Reload()
    {
        police.Play("reload");
        yield return new WaitForSeconds(1.5f);
        pelor = 30;
        lagiReload = false;
    }
}
