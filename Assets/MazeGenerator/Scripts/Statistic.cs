using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Statistic
{
    [SerializeField]
    private float value = 0;

    public delegate void StatUpdateDelegate();
    public event StatUpdateDelegate statEvent;

    public float GetValue()
    {
        return value;
    }

    public void SetValue(float toSet)
    {
        value = toSet;
        statEvent();
    }

    public void Add(float toAdd)
    { 
        value += toAdd;
        statEvent();
    }

    public void Remove(float toRemove)
    {
        value -= toRemove;
        statEvent();
    }
}
