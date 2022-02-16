using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Managed instances are considered gameObjects.
/// If the objects are only created and not removed, there is no need to reuse the object, so the game object and the script are bundled together.
/// </summary>
/// <typeparam name="T"> A script managing game object. </typeparam>
public class PoolingObject<T>
{
    public T script;
    public GameObject gameObject;

    public PoolingObject(T _script, GameObject _gameObject)
    {
        script = _script;
        gameObject = _gameObject;
    }
}

/// <summary>
/// Script to reuse objects that appear repeatedly.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> : MonoBehaviour
{
    public GameObject instancePrefab; // 
    private Queue<PoolingObject<T>> restQueue; // not using objects yet. but it is already created and deactivated
    private Queue<PoolingObject<T>> runQueue;  // running objects in scene

    protected virtual void Awake()
    {
        restQueue = new Queue<PoolingObject<T>>();
        runQueue = new Queue<PoolingObject<T>>();
    }

    /// <summary>
    /// Allocated an enable object.
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public virtual PoolingObject<T> AllowcateInstance(params int[] param)
    {
        // if no object is enable, create an instance object.
        if (restQueue.Count == 0)
        {
            int[] createParameter = { };    // you can delete this phrase.
            PoolingObject<T> instance = CreateInstance(createParameter);
            restQueue.Enqueue(instance);
        }

        // allocate a first object in rest queue.
        PoolingObject<T> target = null;
        while (restQueue.Count > 0)
        {
            target = restQueue.Dequeue();
            runQueue.Enqueue(target);
            break;
        }

        return target;
    }

    /// <summary>
    /// Create an instance object contains T script.
    /// <remarks> if you hope to change type of parameter, do overloading with new type of parameter. </remarks>
    /// <remarks> if you hope to set parent of object, modify parameter of Instantiate in overriding. </remarks>
    /// <remarks> if target object haven't T script, use GameObject.AddComponent() in overriding. </remarks>>
    /// </summary>
    /// <param name="param"></param>
    /// <returns> return a created instance with a pair of gameObject and script. </returns>
    protected virtual PoolingObject<T> CreateInstance(params int[] param)
    {
        GameObject targetObj = Instantiate(instancePrefab);
        targetObj.SetActive(false);
        T script = targetObj.GetComponent<T>();

        PoolingObject<T> target = new PoolingObject<T>(script, targetObj);
        return target;
    }

    /// <summary>
    /// Check whether gameObjects have finished using then arrange queues.
    /// <remarks> it is based on first object will go out first. so if first object is permanent or lives longer than others, it is not optimized.</remarks>
    /// </summary>
    public virtual void CheckObjectsEnable ()
    {
        while (runQueue.Count > 0)
        {
            PoolingObject<T> target = runQueue.Peek();
            if (target.gameObject.activeSelf == false)
            {
                runQueue.Dequeue();
                restQueue.Enqueue(target);
            }
            else
            {
                break;
            }
        }
    }

    public int CountRunQueue()
    {
        return runQueue.Count;
    }

    public int CountRestQueue()
    {
        return restQueue.Count;
    }

    public int CountAllQueue()
    {
        return runQueue.Count + restQueue.Count;
    }
}
