using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.GameClear)
            Destroy(gameObject);
	}
}
