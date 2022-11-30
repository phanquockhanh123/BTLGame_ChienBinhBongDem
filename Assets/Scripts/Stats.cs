using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SirializeableVector3
{
    public float x;
    public float y;
    public float z;

    public Vector3 getpos()
    {
        return new Vector3 (x,y,z);
    }
}

[System.Serializable]
public class Stats 
{
    public int sts_Heath;
    public int sts_Attack;
    public int sts_gem;
    public float sts_Speed;
    public float sts_view;
    public SirializeableVector3 sts_pos;
}
