using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class Clock : MonoBehaviour
{
    public Text textClock;
    public DateTime time;
    void Awake()
    {
       // textClock = GetComponent<Text>();
    }
    void Update()
    {
         time = DateTime.Now;
        string hour = LeadingZero(time.Hour);
        string minute = LeadingZero(time.Minute);
        string second = LeadingZero(time.Second);
        textClock.text = hour + ":" + minute + ":" + second;
    }
    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}