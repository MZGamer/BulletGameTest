using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {
    [Header("關卡狀態")]
    public bool GameStart;
    public int StageLevel;
    public int MaxEnymeCount;
    public float GameSpeed;
    public List<GameObject> EnymeList = new List<GameObject>();
    public float SpawnTime;
    public float time;

    public static int EnymeCount;
    public static float GameSpeedStatic;
    public static float StageTime;
    public static int Score;
    [Header("特效")]
    public GameObject GameOver;
    public Animator GameOverAni;


	// Use this for initialization
	void Start () {
        GameStart = false;
        GameSpeedStatic = GameSpeed;
        StageTime = 0;
        Score = 0;
        StageLevel = 1;
        EnymeCount = 0;
        MaxEnymeCount = 5;

        Invoke("GameStartTrue", 8);


    }
	
	// Update is called once per frame
	void Update () {
        GameSpeedStatic = GameSpeed;
        if (!Player.GameOver && GameStart)
        {
            StageTime += Time.deltaTime;
            time += Time.deltaTime;
            Score += StageLevel;
        }

        if (time > SpawnTime)
        {
            time = 0;
            if(EnymeCount < MaxEnymeCount)
                EnymeSpawn();
        }

        if(Player.GameOver)
        {
            StopAllCoroutines();
            GameOverAni.SetBool("GameOver", true);
            Invoke("LoadScene", 6);
        }
        if(StageLevel < 1 + (int)StageTime / 45)
        {
            StageLevel++;
            if(StageLevel%2 == 0)
                SpawnTime -= 0.05f;
            if (StageLevel % 3 == 0)
                MaxEnymeCount++;

        }
    }

    public void EnymeSpawn()
    {
        EnymeCount++;
        GameObject Enyme;
        float x, y;
        x = Random.Range(-17, 17);
        if (x > 11.6f && x < -11.6f)
        {
            y = Random.Range(0, 7);
        }
        else
        {
            y = Random.Range(5.63f,8);
        }
        Vector3 vector = new Vector3(x, y, 0);
        Enyme = Instantiate(EnymeList[Random.Range(0,EnymeList.Count)], vector, Quaternion.identity);
        Enyme.GetComponent<Enyme>().HP = (int)(Enyme.GetComponent<Enyme>().HP * StageLevel * 0.7);
        Enyme.GetComponent<Enyme>().score = (int)(Enyme.GetComponent<Enyme>().score * StageLevel * 0.7);
    }

    public void GameStartTrue()
    {
        GameStart = true;
        EnymeSpawn();
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("Result");
    }
    
}
