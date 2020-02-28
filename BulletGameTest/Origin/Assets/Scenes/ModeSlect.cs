using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSlect : MonoBehaviour {

    public static float time;
    public static string Mode;
    public Animator Gate;
    public AudioSource BGM;
    public bool Break;
    public float timer;
    // Use this for initialization
    void Start () {
		
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
                SceneManager.LoadScene("ModeSlect");
            }
        }
    }

    public void Survive()
    {
        Gate.SetBool("CloseGate", true);
        Mode = "Survive Mode";
        Invoke("LoadScene", 8);

    }

    public void Normal()
    {
        Gate.SetBool("CloseGate", true);
        Mode = "Normal Mode";
        Invoke("LoadScene", 8);

    }

    void LoadScene()
    {
        if (Mode == "Survive Mode")
            SceneManager.LoadScene("SurviveMode");
        else
            SceneManager.LoadScene("Stage1");
    }



}
