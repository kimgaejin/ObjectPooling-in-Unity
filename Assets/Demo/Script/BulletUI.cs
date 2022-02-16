using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the number of bullets currently being managed.
/// </summary>
public class BulletUI : MonoBehaviour
{
    public ObjectPool<Bullet> objectPool;
    public Text uiText;

    private void Update()
    {
        string msg = string.Format("totalQueue: {0}\nrunQueue: {1}\nrestQueue: {2}", objectPool.CountAllQueue(), objectPool.CountRunQueue(), objectPool.CountRestQueue());
        uiText.text = msg;
    }

}
