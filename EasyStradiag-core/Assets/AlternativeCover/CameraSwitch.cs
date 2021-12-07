using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour
{
    private float totalTime = 0;

    public GameObject[] cameras;
    public string[] shotcuts;
    public bool changeAudioListener = true;

    void Update()
    {
        //InvokeRepeating("timer1", 0, 4);
        //InvokeRepeating("timer2", 2, 4);

        totalTime += Time.deltaTime;
        if (totalTime >= 0 && totalTime < 2)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                SwitchCamera(3);
            }   
        }
        if (totalTime >= 2 && totalTime < 4)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                SwitchCamera(2);             
            }
        }
        if (totalTime >= 4)
        {
            totalTime = 0;
        }

        //可替换代码
        //int i = 0;
        //for (i = 0; i < cameras.Length; i++)
        //{
            //    if (Input.GetKeyUp(shotcuts[i]))
            //        SwitchCamera(i);
            //}
    }
    //timer1、timer2

    //private void timer1()
    //{
    //    SwitchCamera(1);
    //}
    //private void timer2()
    //{
    //    SwitchCamera(2);
    //}

    void SwitchCamera(int index)
    {
        int i = 0;
        for (i = 2; i < cameras.Length; i++)
        {
            if (i != index)
            {
                if (changeAudioListener)
                {
                    cameras[i].GetComponent<AudioListener>().enabled = false;
                }
                cameras[i].GetComponent<Camera>().enabled = false;
            }
            else
            {
                if (changeAudioListener)
                {
                    cameras[i].GetComponent<AudioListener>().enabled = true;
                }
                cameras[i].GetComponent<Camera>().enabled = true;
            }
        }
        
        if(1 != index && 0 != index) 
        {
            if(2 != index && 0 != index && 1 != index) 
            {
                if (changeAudioListener)
                {
                    cameras[0].GetComponent<AudioListener>().enabled = true;
                }
                cameras[0].GetComponent<Camera>().enabled = true;
                if (changeAudioListener)
                {
                    cameras[1].GetComponent<AudioListener>().enabled = false;
                }
                cameras[1].GetComponent<Camera>().enabled = false;
            }

            if (3 != index && 0 != index && 1 != index)
                
            {
                if (changeAudioListener)
                {
                    cameras[1].GetComponent<AudioListener>().enabled = true;
                }
                cameras[1].GetComponent<Camera>().enabled = true;
                if (changeAudioListener)
                {
                    cameras[0].GetComponent<AudioListener>().enabled = false;
                }
                cameras[0].GetComponent<Camera>().enabled = false;
            }

        }

    }
}