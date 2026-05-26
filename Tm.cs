using System;

public class Tm
{
    public double duration;
    double timeLeft;
    public Tm(double t)
    {
        duration = t;
        timeLeft = t;
    }
    public void Update(double dt)
    {
        timeLeft -= dt;
    }
    public bool HasEllapsed()
    {
        return timeLeft <= 0;
    }
    public void Reset(double t=0)
    {
        if(t != 0) duration = t; 
        timeLeft = duration;
    }
}