using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;
using ViveSR.anipal.Eye;
using System.Runtime.InteropServices;

public class GazeRay : MonoBehaviour
{
    [SerializeField] private LineRenderer GazeRayRenderer;
    [SerializeField] private GameObject TargetObject;

    private EyeIndex BaseEye = EyeIndex.LEFT;

    // Start is called before the first frame update
    void Start() {
        if (!SRanipal_Eye_Framework.Instance.EnableEye) {
            Debug.Log("Eye is not enabled in SRanipal Eye Framework");
            enabled = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
        //                SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT)
        //    return;

        //if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == true && eye_callback_registered == false) {
        //    SRanipal_Eye.WrapperRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
        //    eye_callback_registered = true;
        //} else if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == false && eye_callback_registered == true) {
        //    SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
        //    eye_callback_registered = false;
        //}

        Vector3 originLocal, directionLocal, originGlobal, directionGlobal;
        EyeData eyeData;
        GazeIndex gazeIndex;
        SingleEyeData leftData, rightData;
        RaycastHit hitInfo;

        gazeIndex = BaseEye == EyeIndex.LEFT ? GazeIndex.LEFT : GazeIndex.RIGHT;
        eyeData = SRanipal_Eye.GetEyeData();
        leftData = eyeData.verbose_data.left;
        rightData = eyeData.verbose_data.right;

        
        SRanipal_Eye.GetGazeRay(gazeIndex, out originLocal, out directionLocal, eyeData);
        var camTransform = GetComponentInParent<Transform>();
        originGlobal = camTransform.TransformPoint(originLocal);
        directionGlobal = camTransform.TransformDirection(directionLocal);
        
        if (Physics.Raycast(originGlobal, directionGlobal, out hitInfo)) {
            TargetObject.SendMessage("Hit");
        } else {
            TargetObject.SendMessage("UnHit");
        }

        GazeRayRenderer.SetPosition(0, Camera.main.transform.position - Camera.main.transform.up * 0.05f);
        GazeRayRenderer.SetPosition(1, Camera.main.transform.position + directionGlobal * 10);
    }
}
