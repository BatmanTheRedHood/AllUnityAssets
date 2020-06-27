using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private EnemyMovement enemyMovement;

    public float force = 250f;
    public GameObject bulletPrefab;
    public Transform firePosition;

    // Start is called before the first frame update
    void Start()
    {
        this.enemyMovement = this.GetComponent<EnemyMovement>();
        StartCoroutine(StartFiring());
    }
    
    public void Fire()
    {
        // Vector2 offset = this.enemyMovement.lookDirection * 1.5f;
        GameObject bulletObject = Instantiate(this.bulletPrefab, this.firePosition.position /*+ new Vector3(offset.x, offset.y, 0)*/, Quaternion.identity);

        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.Launch(this.enemyMovement.lookDirection, this.force);
    }


    IEnumerator StartFiring()
    {
        // yield return new WaitForSeconds(.5f);

        while (true)
        {
            float randomInterval = Random.Range(1f, 2f);
            //Invoke("InvokeRandom", randomInterval);
            yield return new WaitForSeconds(randomInterval);

            this.Fire();
        }
    }
}
