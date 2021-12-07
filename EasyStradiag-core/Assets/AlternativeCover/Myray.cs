using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class Myray : MonoBehaviour
{
    private Ray ray;//射线
    // Ray myRay = new Ray(vector3 origin,vector3 direction);
    private RaycastHit hit;//触碰到的物体的触碰点
    public Camera mCamera = null;
    private GameObject obj = null;
    private float rayDistance = 20f;//f:float
    private float fpositionz = -8f;
    private string message = null;

    //void Start()
    //{
    //}

    // Update is called once per frame
    void Update()
    {
        ray = mCamera.ScreenPointToRay(Input.mousePosition);//射线发射向量：从摄像机到鼠标位置
        Vector3 k = (Input.mousePosition - ray.origin) * rayDistance;
        UnityEngine.Debug.DrawRay(ray.origin, k, Color.red);
        //Physics.Raycast(myRay, RaycastHit hitInfo, float distance, int LayerMask);
        //UnityEngine.Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red);
        if (Physics.Raycast(ray, out hit, 1000f, 1 << 5))//如果触碰到了物体，第五层UI层上的物体
        {
            UnityEngine.Debug.Log(hit.collider.gameObject.name);
            //UnityEngine.Debug.Log("hitpoint" + hit.point);
            //UnityEngine.Debug.Log("ray.origin" + ray.origin);
            //Debug.DrawRay(transform.position,Vector3.down*rayDistance,Color.red);
            if (hit.collider.gameObject.CompareTag("Cars"))//需要指定button的layer层以及Tag，指定Tag方便射线照射物体时可以更省内存。
            {
                obj = hit.collider.gameObject;
                //UnityEngine.Debug.Log(obj.transform.position);
                obj.transform.localScale = new Vector3(0.8f, 1.1f, 1.1f);
                var input = obj.transform.position;
                message = input.ToString();
                if (obj.transform.position.z >= fpositionz)
                {
                    obj.transform.Translate(Vector3.forward * Time.deltaTime);//forward 小车的z减小，越接近相机-10
                }
            }
        }
        else
        {
            if (obj != null)
            {
                obj.transform.localScale = new Vector3(0.8f, 1f, 1f);
            }
        }

    }
    void OnGUI()
    {
        if (message != null)
        {
            GUILayout.TextArea(message, 200);
        }
        if (message != null)
        {
            GUILayout.TextArea(message, 200);
        }
    }

}

