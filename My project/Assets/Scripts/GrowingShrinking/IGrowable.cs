using UnityEngine;

public interface IGrowable
{
    public Vector3 scaleFactor {get;set;}
    public void Grow ();
    public void Shrink ();
}