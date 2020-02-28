using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Boss
{
    public int HP;
    public AudioClip BGM;
    public List<BossBulletObject> Bullet;
    public GameObject Body;
}
[System.Serializable]
public struct BossBulletObject
{
    public float StartTime;
    public GameObject BossBulletOb;
}
