using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private float m_Time;
    private float m_TimeLimit;

    private UnityAction m_onTimerFinished;

    static Queue<Timer> timerPool = new Queue<Timer>();

    static public Timer CreateTimer(float timeLimit, UnityAction onTimerFinished, string id = "Timer")
    {
        Timer timer = null;
        if (timerPool.Count > 0)
        {
            timer = timerPool.Dequeue();
            timer.Initialize(timeLimit, onTimerFinished);
            timer.name = id;
            return timer;
        }

        timer = new GameObject(id).AddComponent<Timer>();
        timer.Initialize(timeLimit, onTimerFinished);

        return timer;
    }

    private void Initialize(float timeLimit, UnityAction onTimerFinished)
    { 
        this.m_TimeLimit = timeLimit;
        this.m_onTimerFinished = onTimerFinished;
        this.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time > m_TimeLimit)
        {
            m_Time = 0;
            m_onTimerFinished?.Invoke();
            this.enabled = false;
            timerPool.Enqueue(this);
        }
    }
}
