using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class CameraFix : MonoBehaviour
{

    public float pos_x = 0;
    public float pos_y = 0;
    public float pos_z = 0;
    public float rot_x = 0;
    public float rot_y = 0;
    public float rot_z = 0;

    // Start is called before the first frame update
    void Start()
    {
        Camera camera = gameObject.GetComponent<Camera>();
        XRDevice.DisableAutoXRCameraTracking(camera, true);
        camera.transform.position = new Vector3(pos_x, pos_y, pos_z);
        camera.transform.rotation = Quaternion.Euler(rot_x, rot_y, rot_z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
