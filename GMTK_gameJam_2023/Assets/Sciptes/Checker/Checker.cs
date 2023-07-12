using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Checker
{
    protected PlayerManager manager;
    public bool enable;
    public abstract int Check();
    public abstract void Reset();
    public abstract void Pause();
    public abstract void Resume();
}
