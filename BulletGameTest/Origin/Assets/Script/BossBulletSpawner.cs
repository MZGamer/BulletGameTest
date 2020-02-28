using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletSpawner : MonoBehaviour {
    public GameObject Bullet, Deadpar, Part;
    public float time;
    public float shootCD;
    public bool Startshoot,OneShoot,CanDestroy,normalUse;
    private bool hasShoot;
    public int HP;


	// Use this for initialization
	void Start () {
        time = 0;
        if (normalUse)
        {
            if(Player.slevel==3)
                HP = HP * (Player.slevel+1);
            else if(Player.slevel>3)
                HP = HP * (Player.slevel + 2);
            else
                HP = HP * (Player.slevel);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Startshoot)
        {
            if (!OneShoot && !hasShoot)
            {
                hasShoot = true;
                Instantiate(Bullet, transform.position, Quaternion.identity);
            }

            if(!OneShoot)
                time += Time.deltaTime;
            if (!OneShoot&& time >= shootCD)
            {
                Instantiate(Bullet, transform.position, Quaternion.identity);
                time = 0;
            }
            else if (OneShoot && !hasShoot)
            {
                hasShoot = true;
                Instantiate(Bullet, transform.position, Quaternion.identity);
            }
        }

        if (CanDestroy)
        {
            if (HP <= 0)
            {
                StageManager.Score += 10;
                if(normalUse)
                    Player.Expoint+=2;
                Player.Expoint++;
                Vector3 vector = this.transform.position;
                Instantiate(Deadpar, vector, Quaternion.identity);
                this.gameObject.SetActive(false);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanDestroy)
        {
            if (collision.gameObject.CompareTag("PlayerBullet"))
            {
                Vector3 vector = collision.transform.position;
                vector.z = -1;
                GameObject Partical = Instantiate(Part, vector, Quaternion.identity);
                ParticleSystem Par = Partical.GetComponent<ParticleSystem>();
                var col = Par.colorOverLifetime;
                col.enabled = true;
                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(collision.GetComponent<SpriteRenderer>().color, 0.0f), new GradientColorKey(Color.white, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
                Player.Energy++;

                col.color = grad;

                Partical.transform.parent = null;
                Destroy(collision.gameObject);
                HP -= Player.BulletDamagerStatic;
            }
            else if (collision.gameObject.CompareTag("Lazer"))
            {
                HP -= Player.LazerDamagerStatic;
            }
        }
 
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lazer"))
        {
            HP -= Player.LazerDamagerStatic;
        }
    }

}
