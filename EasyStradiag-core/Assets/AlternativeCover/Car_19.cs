using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_19 : MonoBehaviour
{
    private float rotateSpeed = 30f;
    private float movespeed = 4;

    public float Movespeed { get => movespeed; set => movespeed = value; }
    public float RotateSpeed { get => rotateSpeed; set => rotateSpeed = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) //前进
        {
            transform.Translate(Vector3.forward * movespeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) //后退
        {
            transform.Translate(Vector3.back * movespeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))//向左旋转
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))//向右旋转
        { 
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
    }
}
