using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private int timeLapse;
    private float count = 0;

    public Timer(int timeLapse)
    {
        this.timeLapse = timeLapse;
    }

    public bool UpdateTimer(float delta)
    {
        count += delta;

        if (count >= timeLapse)
        {
            return true;
        }

        return false;
    }
}
