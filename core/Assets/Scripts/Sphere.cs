using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField] Renderer Renderer;

    public float distance;
    private float angle;
    private float radius;
    private Vector3 center;

    private float speed = 0.5f;

    private float timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        SetDistanceAngle(0.6f, Mathf.PI * 5 / 36);
        timeCounter = 0;
        transform.Translate(GetPosition(), transform.parent);
    }

    // Update is called once per frame
    void Update()
    { 
        timeCounter += Time.deltaTime;
        transform.localPosition = GetPosition();
    }

    private Vector3 GetPosition() {
        float angle = timeCounter * speed;
        Vector3 relativePos = new Vector3 {
            x = Mathf.Cos(angle) * radius,
            y = Mathf.Sin(angle) * radius,
            z = 0
        };
        return center + relativePos;
    }
    
    private void SetDistanceAngle(float distance, float angle) {
        this.distance = distance;
        this.angle = angle;
        this.radius = Mathf.Tan(angle) * distance;
        this.center = new Vector3(0, 0, distance);
    }

    public void Hit() {
        Renderer.material.color = Color.green;
        //Debug.Log("hit");

    }
    public void UnHit() {
        Renderer.material.color = Color.white;
        //Debug.Log("unHit");
    }

}
