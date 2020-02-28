using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightUIProfile : MonoBehaviour {
    [Header("Profile")]
    public string 還沒做;
    [Header("StageData")]
    [Header("Score")]
    public Text Score;
    public int DisplayScore;

    [Header("Time")]
    public Text Min;
    public Text Sec;

    [Header("PlayereData")]
    [Header("HP")]
    public Text HPText;
    public Image HPDamageBG;
    public Image HPHaveBG;
    [Header("EXP")]
    public Text EXPText;
    public Image EXPHaveBG;
    [Header("LV")]
    public Text LV;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Min.text = "" + (int)StageManager.StageTime / 60;
        if(((int)StageManager.StageTime % 60)/10 == 0)
            Sec.text = "0" + (int)StageManager.StageTime % 60;
        else
            Sec.text = "" + (int)StageManager.StageTime % 60;
        if (DisplayScore < StageManager.Score)
        {
           if (StageManager.Score - DisplayScore > 30000)
                DisplayScore += 1111;
            else if (StageManager.Score - DisplayScore >3000)
                DisplayScore+=111;
            else if (StageManager.Score - DisplayScore > 300)
                DisplayScore+=110;
            if (Player.GameOver)
            {
                if(StageManager.Score - DisplayScore > 111)
                {
                    DisplayScore += 111;
                }
                else
                {
                    DisplayScore = StageManager.Score;
                }
            }
            else
                DisplayScore += 1;
        }
        else
        {
            DisplayScore = StageManager.Score;
        }
        ScoreTextUpdate();
        HPText.text = Player.HPoint + "/" + Player.MaxHP;
        HPHaveBG.gameObject.transform.localScale = new Vector2((float)Player.HPoint / (1.0f * Player.MaxHP ) , 1);
        if(HPDamageBG.gameObject.transform.localScale.x> HPHaveBG.gameObject.transform.localScale.x)
        {
            float Sc = HPDamageBG.gameObject.transform.localScale.x;
            Sc -= 0.003f;
            HPDamageBG.gameObject.transform.localScale = new Vector2(Sc, 1);
        }
        else
        {
            HPDamageBG.gameObject.transform.localScale = new Vector2(HPHaveBG.gameObject.transform.localScale.x, 1);
        }

            EXPText.text = Player.Expoint + "/" + (Player.slevel * 10 + 17 + Player.slevel);
            EXPHaveBG.gameObject.transform.localScale = new Vector2((float)Player.Expoint / (float)(Player.slevel * 10 + 17 + Player.slevel), 1);


            
        

        LV.text = ""+Player.slevel;



    }
    void ScoreTextUpdate()
    {
        string Add0 = "";
        int CountScore = DisplayScore;
        int ZeroCount = 0;
        while (CountScore / 10!=0)
        {
            CountScore = CountScore / 10;
            ZeroCount++;
        }
        for (int i = 0; i < 7 - ZeroCount; i++)
        {
            Add0 = Add0 + "0";
        }
        
        Score.text = Add0 + DisplayScore;
    }
}
