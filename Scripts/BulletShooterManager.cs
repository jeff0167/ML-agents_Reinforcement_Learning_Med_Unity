using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooterManager : MonoBehaviour
{
    [SerializeField]
    GameObject bullet, agent;

    [SerializeField]
    float spawnRateMin, spawnRateMax;


    List<GameObject> bullets;


    void Start()
    {
        bullets = new List<GameObject>();
        //Invoke("Shoot",1);
        //InvokeRepeating("Shoot", 0, 1);
        StartCoroutine("Shoot");
    }

    public void DestroyBullets()
    {
        foreach (var item in bullets)
        {
            Destroy(item);
        }
        bullets = new List<GameObject>();
    }

    void Bang()
    {
        Vector3 spawnPos2 = Quaternion.Euler(0, Random.Range(0,360), 0) * Vector3.left * 35 + transform.position;
        Vector3 dir = agent.transform.position - spawnPos2;

        GameObject g = Instantiate(bullet, spawnPos2, Quaternion.LookRotation(dir));
        bullets.Add(g);

        //if (IsInvoking())
        //{
        //    CancelInvoke();
        //}

        //Invoke("Shoot", Random.Range(spawnRateMin, spawnRateMax));
    }


    IEnumerator Shoot()
    {
        float r = Random.Range(spawnRateMin, spawnRateMax);
        while (true)
        {
            Bang();
            yield return new WaitForSeconds(r);
        }
    }
}
