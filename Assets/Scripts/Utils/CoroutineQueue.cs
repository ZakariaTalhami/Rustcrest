using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineQueue
{

    private MonoBehaviour coroutineOwner;
    private Coroutine internalCorouting;
    protected Queue<IEnumerator> actionQueue = new Queue<IEnumerator>();
    public float defaultWaitAmount { get; set; } = 1f;

    public CoroutineQueue(MonoBehaviour owner)
    {
        this.coroutineOwner = owner;
    }

    public void StartLoop()
    {
        this.internalCorouting = this.coroutineOwner.StartCoroutine(Loop());
    }

    public void StopLoop()
    {
        this.coroutineOwner.StopCoroutine(this.internalCorouting);
        this.internalCorouting = null;
    }

    public void EnqueueAction(IEnumerator action)
    {
        this.actionQueue.Enqueue(action);
    }

    public void EnqueueActionWithWait(IEnumerator action)
    {
        this.actionQueue.Enqueue(action);
        this.EnqueueWait();
    }

    public void EnqueueActionWithWait(IEnumerator action, float waitAmount)
    {
        this.actionQueue.Enqueue(action);
        this.EnqueueWait(waitAmount);
    }

    public void EnqueueWait()
    {
        this.actionQueue.Enqueue(Wait(defaultWaitAmount));
    }

    public void EnqueueWait(float waitAmount)
    {
        this.actionQueue.Enqueue(Wait(waitAmount));
    }

    public bool isStopped()
    {
        return this.internalCorouting == null;
    }

    protected virtual IEnumerator Wait(float waitAmount)
    {
        yield return new WaitForSeconds(waitAmount);
    }

    protected virtual IEnumerator Loop()
    {
        while (true)
        {
            if (this.actionQueue.Count > 0)
            {
                yield return this.coroutineOwner.StartCoroutine(this.actionQueue.Dequeue());
            }
            else
            {
                yield return null;
            }
        }
    }
}