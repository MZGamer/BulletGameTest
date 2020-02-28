using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enyme : MonoBehaviour {
    [Header("敵人數值")]
    public float BulletSpawnSpeed;
    public GameObject Bullet, Part, Deadpar;
    public int HP,score;
    public float MoveState;
    public Vector3 Pos,MoveTo;

    public float time;



    // Use this for initialization
    void Start () {
        Pos = this.transform.position;
        MoveTo = new Vector3( Random.Range(-8,1) , Random.Range(4.5f,0) , 0);
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position != MoveTo)
        {
            Move();
        }
        else
        {
            if (time >= BulletSpawnSpeed)
            {
                time = 0;
                GameObject EnemeBullet = Instantiate(Bullet, this.transform);

                EnemeBullet.transform.parent = null;
            }
        }
        time += Time.deltaTime;

        if(HP <= 0 ||　Player.HPoint==0)
        {
            StageManager.Score += score;
            Player.Expoint++;
            Vector3 vector = this.transform.position;
            Instantiate(Deadpar, vector, Quaternion.identity);
            StageManager.EnymeCount--;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Vector3 vector = collision.transform.position;
            vector.z = -1;
            GameObject Partical = Instantiate(Part, vector,Quaternion.identity);
            ParticleSystem Par = Partical.GetComponent<ParticleSystem>();
            var col = Par.colorOverLifetime;
            col.enabled = true;
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(collision.GetComponent<SpriteRenderer>().color,0.0f),new GradientColorKey(Color.white,1.0f)},new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
            Player.Energy++;

            col.color = grad;

            Partical.transform.parent = null;
            Destroy(collision.gameObject);
            HP -= Player.BulletDamagerStatic;
            StageManager.Score++;
        }
        else if (collision.gameObject.CompareTag("Lazer"))
        {
            HP -= Player.LazerDamagerStatic;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lazer"))
        {
            HP -= Player.LazerDamagerStatic;
        }
        else if (collision.gameObject.CompareTag("SmallLazer"))
        {
            HP -= Player.BulletDamagerStatic;
        }
    }

    public void Move()
    {
        transform.position = Pos + MoveState * (MoveTo - Pos);
    }



}
