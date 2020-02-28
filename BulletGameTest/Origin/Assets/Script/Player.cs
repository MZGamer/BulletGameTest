using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("玩家數值")]
    public Rigidbody2D PlayerRB;
    public int Speed;
    public int HP;
    public static int HPoint,Expoint,slevel,MaxLevel,MaxHP;
    public int Level;
    public static bool GameOver,GameClear;
    [Header("子彈數值")]
    public List<GameObject> Bullet = new List<GameObject>();
    public float BulletSpeed;
    public float BulletSpawnSpeed;
    public int BulletDamage;
    [Header("蓄能雷射")]
    public int LazerDamage;
    public GameObject Lazer;
    public GameObject LazerBullet;
    public static int Energy;
    public int EnergyDeBuff;
    public GameObject EnergyPart;
    public List<GameObject> LazerEnergy = new List<GameObject>();
    public int EnergyCount;
    public bool LazerMode;
    public bool LazerBulletShoot;
    public float LazerTimer;


    public static float BulletSpeedStatic;
    public static int BulletDamagerStatic;

    public static int LazerDamagerStatic;
    public float time;
    public GameObject EnymeBulletPar,PlayerDeadPar;
    public static Vector3 Pos;

	// Use this for initialization
	void Start () {
        BulletSpeedStatic = BulletSpeed;
        BulletDamagerStatic = BulletDamage;
        LazerDamagerStatic = LazerDamage;
        Energy = 0;
        EnergyCount = 0;
        Level = 1;
        HP = 50;
        MaxHP = 50;
        Expoint = 0;
        GameOver = false;
        GameClear = false;
        EnergyDeBuff = 0;

}
	
	// Update is called once per frame
	void Update () {

        //Debug

    
        Pos = gameObject.transform.position;
        Move();
        BulletSpeedStatic = BulletSpeed;
        BulletDamagerStatic = BulletDamage;
        time += Time.deltaTime;
        if(time>= BulletSpawnSpeed && !LazerMode)
        {
            GameObject PlayerBullet;
            float x, y;
            x = PlayerRB.position.x;
            y = PlayerRB.position.y;
            time = 0;
            if (Level > Bullet.Count)
            {
                PlayerBullet = Instantiate(Bullet[Bullet.Count-1], transform.position, Quaternion.identity);
            }
            else
                PlayerBullet= Instantiate(Bullet[Level-1], transform.position,Quaternion.identity);
            PlayerBullet.transform.position = new Vector3(PlayerBullet.transform.position.x, PlayerBullet.transform.position.y,0);
        }

        if(Energy>= EnergyDeBuff + 100 + (200 * Level-1)&&EnergyCount<=5)
        {
            Energy = 0;
            LazerEnergy[EnergyCount].SetActive(true);
            EnergyCount++;
            EnergyDeBuff += 200;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !LazerMode&&EnergyCount!=0)
        {
            LazerMode = true;
            LazerEnergy[EnergyCount - 1].SetActive(false);
            Vector3 vector = LazerEnergy[EnergyCount-1].transform.position;
            vector.z = -1;
            GameObject Partical = Instantiate(EnergyPart, vector, Quaternion.identity);
            ParticleSystem Par = Partical.GetComponent<ParticleSystem>();
            var col = Par.colorOverLifetime;
            col.enabled = true;
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(LazerEnergy[EnergyCount - 1].GetComponent<SpriteRenderer>().color, 0.0f), new GradientColorKey(Color.white, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

            col.color = grad;

            Partical.transform.parent = null;
            EnergyCount--;
        }
        LazerShoot();

        HPoint = HP;
        slevel = Level;
        levelup();

        if(HP <= 0)
        {
            HP = 0;
            HPoint = HP;
            if (!BossScript.BossDead)
            {
                GameOver = true;
                Instantiate(PlayerDeadPar, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject.transform.parent.gameObject);
            }
            else
            {
                GameClear = true;
                GameOver = true;
                Instantiate(PlayerDeadPar, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject.transform.parent.gameObject);

            }
            StopAllCoroutines();

        }







    }


    public void Move()
    {
        Vector2 v;
        int x, y;
        x = 0;
        y = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            y = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            y = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            x = 1;
        }
        v.x = x;
        v.y = y;
        PlayerRB.velocity = v*Speed;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (collision.gameObject.tag != "SmallLazer" && collision.gameObject.tag != "PlayerBullet" &&!collision.gameObject.CompareTag("EnymeLazer"))
            {
                Vector3 vector = collision.transform.position;
                vector.z = -1;
                GameObject Partical = Instantiate(EnymeBulletPar, vector, Quaternion.identity);
                Partical.transform.parent = null;
                if(collision.transform.parent.CompareTag("Rotation"))
                    Destroy(collision.gameObject.transform.parent.gameObject);
                Destroy(collision.gameObject);
                HP -= 5;
            }
            else if (collision.gameObject.CompareTag("EnymeLazer"))
            {
                HP--;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (collision.gameObject.CompareTag("EnymeLazer"))
            {
                HP--;
            }
        }
    }
    public void LazerShoot()
    {
        if(LazerMode)
        {
            Lazer.SetActive(true);
            LazerTimer += Time.deltaTime;
            if(LazerTimer >= 2.5f&&!LazerBulletShoot)
            {
                Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, 1);
                LazerBulletShoot = true;
                Instantiate(LazerBullet, pos, Quaternion.identity);
            }

            if (LazerTimer >= 6f)
            {
                LazerTimer = 0;
                Lazer.SetActive(false);
                LazerMode = false;
                LazerBulletShoot = false;
            }

        }
    }

    public void levelup()
    {
        if(Expoint >= slevel * 10 + 17 + slevel)
        {
            Expoint = 0;
            Level++;
            MaxHP += 10;
            EnergyDeBuff += 50*(Level-1);
            if (HP + (int)(MaxHP * 0.3f) > MaxHP)
            {
                HP = MaxHP;
            }
            else
                HP += (int)(MaxHP * 0.3f);
        }
    }
}
