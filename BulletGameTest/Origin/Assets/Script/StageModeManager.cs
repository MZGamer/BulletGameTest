using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageModeManager : MonoBehaviour {
    [Header("關卡狀態")]

    public StageDataCreator StageData;
    public bool GameStart;
    public float GameSpeed;
    public float time;

    public static float GameSpeedStatic;
    public static float StageTime;
    public static int Score;
    public static bool TLE;

    public GameObject BossOb;
    public AudioClip BGM;
    public AudioSource Source;
    [Header("特效")]
    public AudioClip Fail;
    public AudioClip Clear;
    public GameObject GameClear;
    public GameObject GameOver;
    public Text TLEWarn;
    public Animator GameOverAni;
    public GameObject BossHp;
    [Header("神的恩惠")]
    public float st_time;
    public float passtime;

    // Use this for initialization
    void Start()
    {
        TLE = false;
        BossScript.BossDead = false;
        BossScript.BossExploded = false;
        StageManager.Score = 0;
        GameStart = false;
        GameSpeedStatic = GameSpeed;
        StageTime = 0;
        Score = 0;

        BGM = StageData.Stage.BGM;
        Source.clip = BGM;
        Source.time = st_time;
        passtime = st_time;
        StageTime = st_time;
        Source.Play();
        BossOb.GetComponent<BossScript>().BossData = StageData.Stage.Boss;
        Invoke("GameStartTrue", 8);


    }

    // Update is called once per frame
    void Update()
    {
        GameSpeedStatic = GameSpeed;
        if (!Player.GameOver && GameStart)
        {
            StageTime += Time.deltaTime;
            time += Time.deltaTime;
            StageManager.Score ++;
        }
        StageManager.StageTime = StageTime;
        if (TLE)
        {
            TLEWarn.text = "Code:TLE";
            GameOverAni.SetBool("GameOver", true);
            if (Source.clip != Fail)
            {
                Source.clip = Fail;
                Source.PlayDelayed(0.25f);
            }
            Invoke("LoadScene", 6);
        }
        if (Player.GameOver&&!BossScript.BossDead)
        {
            StopAllCoroutines();
            GameOverAni.SetBool("GameOver", true);
            if (Source.clip != Fail)
            {
                Source.clip = Fail;
                Source.PlayDelayed(0.25f);
            }
            Invoke("LoadScene", 6);

        }
        if (BossScript.BossExploded)
        {
            if (Source.clip != Clear)
            {
                Source.clip = Clear;
                Source.Play();
            }


            GameClear.SetActive(true);
            Invoke("LoadScene", 6);
        }
        if(StageTime >= StageData.Stage.time)
        {
            BossOb.SetActive(true);
            BossHp.SetActive(true);
}
    }
    IEnumerator EnymeSpawn(float time,GameObject Enyme)
    {
        yield return new WaitForSeconds(time);
        Instantiate(Enyme, Enyme.transform.position, Quaternion.identity);

    }

    public void GameStartTrue()
    {
        GameStart = true;
        for (int i = 0; i <StageData.Stage.EnymeList.Count; i++)
        {
            if (StageData.Stage.EnymeList[i].time >= st_time)
            {
                StartCoroutine(EnymeSpawn(StageData.Stage.EnymeList[i].time - st_time, StageData.Stage.EnymeList[i].Enyme));
            }
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("Result");
    }
}

