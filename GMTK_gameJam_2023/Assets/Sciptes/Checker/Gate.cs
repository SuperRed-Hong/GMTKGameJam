using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Checker
{
    private int period;
    private GateController gate;
    private int timeCount;
    private bool direction;
    private float upBound;
    private float lowBound;

    public Gate(GateController gate, int period, float upBound, float lowBound, bool direction)
    {
        this.period = period;
        this.gate = gate;
        this.direction = direction;
        enable = true;
        timeCount = 0;
        this.upBound = upBound;
        this.lowBound = lowBound;
    }
    public override int Check()
    {
        if (!enable)
        {
            return 0;
        }
        if (++timeCount >= period)
        {
            if (!direction)
            {
                gate.GateUp();
                direction = true;

            }
            else
            {
                gate.GateDown();

                direction = false;
            }
            timeCount = 0;
        }
        else
        {
            if (direction)
            {
                if (gate.transform.position.y > upBound)
                {
                    gate.GateStop();
                }
            }
            else
            {
                if (gate.transform.position.y < lowBound)
                {
                    gate.GateStop();
                }
            }
        }


        return 1;
    }
    public override void Reset()
    {
        timeCount = 0;
        gate.GateReset();
    }
    public override void Pause()
    {
        enable = false;
        gate.GateStop();
    }
    public override void Resume()
    {
        enable = true;

        if (direction)
        {
            if (gate.transform.position.y < upBound)
            {
                gate.GateUp();
            }
        }
        else
        {
            if (gate.transform.position.y > lowBound)
            {
                gate.GateDown();
            }
        }

    }
}
