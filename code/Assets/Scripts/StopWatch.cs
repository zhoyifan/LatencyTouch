using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StopWatch : MonoBehaviour
{
    private Text timerText;
    private float timeBefore;
    private float startTime;
    private bool isTiming;
    private float t;
    // Start is called before the first frame update
    void Start()
    {
        timerText=GetComponent<Text>();
        timerText.text="0:00";
        timeBefore=0f;
        startTime=0f;
        isTiming=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTiming){
            t=Time.time-startTime+timeBefore;
            string minutes=(((int)t)/60).ToString();
            string seconds=(t%60).ToString("f2");
            timerText.text=minutes+":"+seconds;
        }
    }
    public void Timing(){
        startTime=Time.time;
        isTiming=true;
    }
    public void Pause(){
        isTiming=false;
        timeBefore=t;
        startTime=0f;
    }
}
