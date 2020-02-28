using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour {
    public Text Score, time, Mode, Rank;
    public Animator Gate;
    public AudioSource BGM;
    public bool Break;
    public float timer;

	// Use this for initialization
	void Start () {
        Mode.text = "- " + ModeSlect.Mode + " -";

        string Add0 = "";
        int CountScore = StageManager.Score;
        int ZeroCount = 0;
        while (CountScore / 10 != 0)
        {
            CountScore = CountScore / 10;
            ZeroCount++;
        }
        for (int i = 0; i < 7 - ZeroCount; i++)
        {
            Add0 = Add0 + "0";
        }

        Score.text = Add0 + StageManager.Score;

        int Min, Sec;
        Min=  (int)StageManager.StageTime / 60;
        Sec = (int)StageManager.StageTime % 60;
        if (((int)StageManager.StageTime % 60) / 10 == 0)
            time.text = Min + " : 0" + Sec;
        else
            time.text = Min + " : " + Sec;

        if (ModeSlect.Mode == "Normal Mode")
        {
            if(StageManager.Score >= 35000)
            {
                Rank.text = "S";
            }
            else if (StageManager.Score >= 15500)
            {
                Rank.text = "A";
            }
            else if (StageManager.Score >= 7500)
            {
                Rank.text = "B";
            }
            else if (StageManager.Score >= 2500)
            {
                Rank.text = "C";
            }
            else
            {
                Rank.text = "D";
            }
        }
        else
        {
            if (StageManager.StageTime >= 600)
            {
                Rank.text = "S";
            }
            else if (StageManager.StageTime >= 480)
            {
                Rank.text = "A";
            }
            else if(StageManager.StageTime >= 360)
            {
                Rank.text = "B";
            }
            else if(StageManager.StageTime >= 120)
            {
                Rank.text = "C";
            }
            else 
            {
                Rank.text = "D";
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (Input.anyKeyDown)
        {
            timer = 0;
        }
        if (timer > 180)
        {
            Gate.SetBool("CloseGate", true);
            BGM.Stop();
            Break = true;
        }
        if (Break)
        {
            if (Input.anyKeyDown)
            {
                Invoke("BacktoModeSlect",0);
            }
        }
	}

    public void Next()
    {
        Gate.SetBool("CloseGate", true);

        ModeSlect.time += StageManager.StageTime;

            Invoke("BacktoModeSlect", 2f);

    }

    void BacktoModeSlect()
    {
        SceneManager.LoadScene("ModeSlect");
    }
    void BackToStart()
    {

    }

}
