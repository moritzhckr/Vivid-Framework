using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class Clock : MonoBehaviour
{
    public bool showTimeOnUI;
    public Text textClock;
    public DateTime time;
    void Awake()
    {
       if(textClock == null)
        {
            Debug.Log("'There is no UI text asset for displaying time, please add one");
            showTimeOnUI = false;
        }
    }
    void Update()
    {
         time = DateTime.Now;
        string hour = LeadingZero(time.Hour);
        string minute = LeadingZero(time.Minute);
        string second = LeadingZero(time.Second);
        if (showTimeOnUI)
        {
            textClock.text = hour + ":" + minute + ":" + second;
        }
       

    }
    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}