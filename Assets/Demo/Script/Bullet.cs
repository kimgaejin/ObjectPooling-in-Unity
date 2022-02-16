using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example used repeatedly to know object pooling.
/// </summary>
public class Bullet : MonoBehaviour
{
    private Vector3 vector;
    private float timer;
    private const float STOP_TIME = 3.0f;

    /// <summary>
    /// Call this function to start the action of the bullet.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="_vector"></param>
    public void Fire(Vector3 start, Vector3 _vector)
    {
        timer = 0.0f;
        transform.position = start;
        vector = _vector;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// If the bullet is alive for more than 3 seconds, it will stop working.
    /// </summary>
    private void Update()
    {
        timer += Time.deltaTime;
        transform.position += vector * Time.deltaTime;
        if (STOP_TIME < timer)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Maybe you can set it to stop working if the bullet collides with something like a wall.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
