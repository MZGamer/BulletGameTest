using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
    // Use this for initialization
    public bool AnimationControll,BossBullet,FollowBullet;
    public float SpeedRate;

    public GameObject EnymeBulletPar;
    public GameObject PlayerBulletPar;

    Vector3 PlayerPosition,MoveVec;

    void Start () {
		if(FollowBullet && !gameObject.CompareTag("PlayerBullet")){
            PlayerPosition = Player.Pos;
            MoveVec = new Vector3((PlayerPosition.x - gameObject.transform.position.x)/50, (PlayerPosition.y - gameObject.transform.position.y)/50);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(!AnimationControll)
            BulletRun();


    }

    public void BulletRun()
    {
        if (!BossBullet)
        {
            if (gameObject.CompareTag("PlayerBullet"))
            {
                transform.Translate(new Vector3(0, 1 * Player.BulletSpeedStatic, 0));
            }
            else
            {
                if (FollowBullet)
                {
                    transform.Translate(MoveVec * StageManager.GameSpeedStatic);
                }
            else
                transform.Translate(new Vector3(0, -1 * StageManager.GameSpeedStatic, 0));
            }
        }
        else
        {
            if (FollowBullet)
            {
                transform.Translate(MoveVec * SpeedRate);
            }
            else
                transform.Translate(new Vector3(0, -1 * SpeedRate, 0));
        }


    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet88"))
        {
            Destroy(this.gameObject);
            if (!gameObject.CompareTag("PlayerBullet")&& transform.parent.CompareTag("Rotation"))
            {
                Destroy(transform.parent.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Lazer")&&!this.gameObject.CompareTag("PlayerBullet"))
        {
            Vector3 vector = this.transform.position;
            vector.z = -1;
            GameObject Partical = Instantiate(EnymeBulletPar, vector, Quaternion.identity);
            Partical.transform.parent = null;
            Destroy(this.gameObject);
            if (!gameObject.CompareTag("PlayerBullet"))
            {
                if(transform.parent.CompareTag("Rotation"))
                    Destroy(transform.parent.gameObject);
            }
        }
    }
     public void OnTriggerStay2D(Collider2D collision)
     {
        if (collision.gameObject.CompareTag("Bullet88"))
        {
            Destroy(this.gameObject);
        }

     }


}
