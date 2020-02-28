using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour {
    public BossDataCreator BossData;
    public AudioClip BossBGM;
    public AudioSource BGM;
    public int HP;
    public static bool BossExploded;
    public Animator BossAni;

    public GameObject BossHPBar, BossHPBG;
    public Text BossHP;

    public static bool BossDead;
    [Header("神的恩惠")]
    public float st_time;
    public float passtime;

    public GameObject Part;
    // Use this for initialization
    void Start () {
        BossDead = false;
        BossHP.text = "HP: " + HP +" / " + BossData.Boss.HP;
        passtime = 0;
        BossBGM = BossData.Boss.BGM;
        HP = BossData.Boss.HP;
        BGM.clip = BossBGM;
        BGM.loop = false;


        BGM.time = st_time;
        passtime = st_time;
        BGM.Play();
        for (int i = 0; i < BossData.Boss.Bullet.Count; i++)
        {
            if (BossData.Boss.Bullet[i].StartTime >= st_time)
            {
                StartCoroutine(ShootBullet(i, BossData.Boss.Bullet[i].StartTime - st_time));
            }
        }
    }
	
    IEnumerator ShootBullet(int BulletNum,float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        Instantiate(BossData.Boss.Bullet[BulletNum].BossBulletOb, transform.position, Quaternion.identity);
    }


    // Update is called once per frame
    void Update () {
        if (passtime >= 147&&!BossDead)
        {
            if (BossDead)
                StageModeManager.TLE = true;
            else
                Player.GameClear = true;

        }
        if (HP <= 0)
        {
            BossDead = true;
        }
        if (!BossDead)
            BossHP.text = "HP: " + HP + " / " + BossData.Boss.HP;
        else
            BossHP.text = "- Bouns Time -";
        BossHPBar.transform.localScale = new Vector3(  ( (float)HP)/(float)BossData.Boss.HP , 1,1);
        if (BossHPBG.gameObject.transform.localScale.x > BossHPBar.gameObject.transform.localScale.x)
        {
            float Sc = BossHPBG.gameObject.transform.localScale.x;
            if(Sc- BossHPBar.transform.localScale.x > 0.1f)
            {
                Sc -= 0.1f;
            }
            else
                Sc -= 0.0001f;
            BossHPBG.gameObject.transform.localScale = new Vector2(Sc, 1);
        }
        else
        {
            BossHPBG.gameObject.transform.localScale = new Vector2(BossHPBar.gameObject.transform.localScale.x, 1);
        }
        passtime += Time.deltaTime;

        if (Player.GameOver)
        {
            StopAllCoroutines();
        }
        if (Player.GameClear)
        {
            BossAni.SetBool("Exploded", true);
            Invoke("ChangeClear", 5);
        }
	}

    void ChangeClear()
    {
        BossExploded = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
            if(!BossDead)
                HP -= Player.BulletDamagerStatic;
            else
                StageManager.Score+=5;
            StageManager.Score++;
        }
        else if (collision.gameObject.CompareTag("Lazer"))
        {
            if(!BossDead)
                HP -= Player.slevel*3;
            else
                StageManager.Score += Player.slevel;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lazer"))
        {
            if (!BossDead)
                HP -= Player.slevel;
            else
                StageManager.Score += Player.slevel;
        }
    }
}
