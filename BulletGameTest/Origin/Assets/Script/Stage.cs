using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stage
{
    public int time;
    public AudioClip BGM;
    public List<EnymeObject> EnymeList;
    public BossDataCreator Boss;

}
[System.Serializable]
public struct EnymeObject
{
    public GameObject Enyme;
    public float time;
}
