using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code to create bullets.
/// </summary>
public class Shooter : MonoBehaviour
{
    public BulletManager bulletManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    /// <summary>
    /// Fires bullets in a random direction and speed.
    /// </summary>
    public void Fire()
    {
        PoolingObject<Bullet> bullet = bulletManager.AllowcateInstance();
        Vector3 vector = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        vector = vector.normalized * Random.Range(0.5f, 2.0f);
        bullet.script.Fire(transform.position, vector);
    }
}
