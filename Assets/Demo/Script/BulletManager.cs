using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sample script as object pooling manager
/// </summary>
public class BulletManager : ObjectPool<Bullet>
{
    private float timer = 0;

    /// <summary>
    /// check once per second wherther bullets have been used.
    /// if a bullet becomes inactive after being used, the status is updated here.
    /// </summary>
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (1.0f < timer)
        {
            CheckObjectsEnable();
            timer = 0;
        }
    }

    public override PoolingObject<Bullet> AllowcateInstance(params int[] param)
    {
        return base.AllowcateInstance(param);
    }

    public override void CheckObjectsEnable()
    {
        base.CheckObjectsEnable();
    }
    
    protected override PoolingObject<Bullet> CreateInstance(params int[] param)
    {
        return base.CreateInstance(param);
    }
}
