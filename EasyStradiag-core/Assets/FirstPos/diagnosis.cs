using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {
            public class diagnosis : MonoBehaviour
            {
                EyeData eyeData = new EyeData();
        
                bool is_selected = false;//判断是否做出了选择，选择哪只眼睛作为主视眼
                string selectIndex = null;//选择主视眼的标志
                SingleEyeData selectEye;//用于存放所选择主视眼的数据
                SingleEyeData strabismusEye;//用于存放斜视眼的数据
                private Transform mainCamTransform;//用于转换坐标

                private GameObject obj = null;
                private const float fpositionz_M = -4f;//鱼与相机相距6m时的z轴坐标
                private const float fpositionz_m = -9.67f;//鱼与相机相距33cm时的z轴坐标
                private const float fpositionz_O = -8f;//鱼原始位置
                private const float V = 0.2f;//鱼移动速度0.2m/s


                private Text pupilPosLeftText;
                private Text pupilPosRightText;
                private Text fishPosText;
                private static StreamWriter writer;

                static string leftPuipleStr;
                static string rightPuipleStr;
                static string fishPosStr;


                // Start is called before the first frame update
                void Start()
                {
                    XRDevice.DisableAutoXRCameraTracking(Camera.main, true);
                    Camera.main.transform.position = new Vector3(0, 0, -10);
                    Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
                    
                    StreamReader countReader = new StreamReader("C:/unity/count.rec");
                    string countStr = countReader.ReadLine();
                    countReader.Close();
                    int countNum;
                    if (!int.TryParse(countStr, out countNum))
                    {
                        countNum = 1;
                    }
                    writer = new StreamWriter("C:/unity/resultCsv/yuyu_" + countNum + ".csv");
                    countNum++;
                    StreamWriter countWriter = new StreamWriter("C:/unity/count.rec");
                    countWriter.WriteLine(countNum);
                    countWriter.Close();

                    writer.WriteLine("objectPosX, objectPosY, objectPosZ, leftPupilPosX, leftPupilPosY, rightPupilPosX, rightPupilPosY, " +
                        "leftGazeDirectionX, leftGazeDirectionY, leftGazeDirectionZ, rightGazeDirectionX, rightGazeDirectionY, rightGazeDirectionZ, " +
                        "leftGazeOriginX, leftGazeOriginY, leftGazeOriginZ, rightGazeOriginX, rightGazeOriginY, rightGazeOriginZ, EyeDeviationAngle");

                    mainCamTransform = Camera.main.transform;//赋予mainCamera类
                    //定位text
                    pupilPosLeftText = GameObject.Find("backgroundcanvas/lefteye").GetComponent<Text>();
                    pupilPosRightText = GameObject.Find("backgroundcanvas/righteye").GetComponent<Text>();
                    fishPosText = GameObject.Find("backgroundcanvas/fish").GetComponent<Text>();

                }

                // Update is called once per frame
                void Update()
                {
                    if (Input.GetKeyUp(KeyCode.Escape))
                    {
                        writer.Flush();
                        writer.Close();
                        //UnityEditor.EditorApplication.isPlaying = false;
                        //Application.Quit();
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;//编辑状态下退出
#else
                            Application.Quit();//打包编译后退出
#endif
                        return;
                    }
                    //判断左右眼
                    if (Input.GetKey(KeyCode.L))
                    {//选择左眼作为主视眼
                        if (is_selected == false)
                        {
                            selectIndex = "left";
                            is_selected = true;
                        }
                    }
                    if (Input.GetKey(KeyCode.R))
                    {//选择右眼作为主视眼
                        if (is_selected == false)
                        {
                            selectIndex = "right";
                            is_selected = true;
                        }
                    }
                    //开始采集眼动数据

                    obj = GameObject.Find("CuteFish");
                    if (Input.GetKey(KeyCode.F))
                    {
                        obj.transform.position = new Vector3(0f, 0f, fpositionz_M);//F-far
                    }
                    if (Input.GetKey(KeyCode.N))
                    {
                        obj.transform.position = new Vector3(0f, 0f, fpositionz_m);//N-near
                    }
                    if (Input.GetKey(KeyCode.Space))
                    {
                        obj.transform.position = new Vector3(0f, 0f, fpositionz_O);//O-original
                    }
                    if (Input.GetKey(KeyCode.C))
                    {
                        //obj.transform.position = new Vector3(0f, 0f, fpositionz_M);//C-come                       
                        obj.transform.Translate(Vector3.forward * V * Time.deltaTime);//forward 鱼的z减小，越接近相机-10                        
                    }
                    if (Input.GetKey(KeyCode.G))
                    {
                        //obj.transform.position = new Vector3(0f, 0f, fpositionz_m);//G-go                        
                        obj.transform.Translate(Vector3.back * V * Time.deltaTime);//back 鱼的z增加                        
                    }

                    //获取眼动数据,此处为绑定在camera上的局部坐标，需要转化为全局坐标
                    SRanipal_Eye.GetEyeData(out eyeData);
                    SingleEyeData leftEyeData = eyeData.verbose_data.left;
                    SingleEyeData rightEyeData = eyeData.verbose_data.right;

                    //转化全局坐标
                    Vector2 leftPupilPos = leftEyeData.pupil_position_in_sensor_area;
                    Vector2 rightPupilPos = rightEyeData.pupil_position_in_sensor_area;

                    leftEyeData.gaze_direction_normalized = mainCamTransform.TransformDirection(leftEyeData.gaze_direction_normalized);
                    leftEyeData.gaze_origin_mm = mainCamTransform.TransformPoint(leftEyeData.gaze_origin_mm / 1000);
                    rightEyeData.gaze_direction_normalized = mainCamTransform.TransformDirection(rightEyeData.gaze_direction_normalized);
                    rightEyeData.gaze_origin_mm = mainCamTransform.TransformPoint(rightEyeData.gaze_origin_mm / 1000);

                    Vector3 leftDirection = leftEyeData.gaze_direction_normalized;
                    Vector3 rightDirection = rightEyeData.gaze_direction_normalized;

                    if (selectIndex == "left")
                    {
                        selectEye = leftEyeData;
                        strabismusEye = rightEyeData;
                    }
                    if (selectIndex == "right")
                    {
                        selectEye = rightEyeData;
                        strabismusEye = leftEyeData;
                    }
                    //眼动射线触发，获取射线撞击点                    
                    if (is_selected == true)
                    {
                        Ray rayGlobal = new Ray(selectEye.gaze_origin_mm, selectEye.gaze_direction_normalized);
                        if (Physics.Raycast(rayGlobal, out RaycastHit hitinfo, Mathf.Infinity))
                        {
                            //Debug.Log("xiaoyuxiaoyu");
                            //计算斜视角度
                            Vector3 ComputeDirection = hitinfo.transform.position - strabismusEye.gaze_origin_mm;//计算射线碰撞点与斜视眼形成的向量A

                            double DotMultiply = Vector3.Dot(ComputeDirection, strabismusEye.gaze_direction_normalized);
                            Vector3 CrossMultiply = Vector3.Cross(ComputeDirection, strabismusEye.gaze_direction_normalized);
                            Vector3 CM_normalize = Vector3.Normalize(CrossMultiply);

                            double EyeDeviationRadian = Math.Atan(Vector3.Dot(CrossMultiply, CM_normalize) / DotMultiply);//弧度

                            double EyeDeviationAngle = EyeDeviationRadian * 180 / Math.PI;//角度
                            writer.WriteLine(obj.transform.position.x + "," + obj.transform.position.y + "," + obj.transform.position.z + "," +
                                    leftPupilPos.x + "," + leftPupilPos.y + "," + rightPupilPos.x + "," + rightPupilPos.y + "," + leftDirection.x + "," + leftDirection.y + "," + leftDirection.z + "," +
                                    rightDirection.x + "," + rightDirection.y + "," + rightDirection.z + "," + leftEyeData.gaze_origin_mm.x + "," +
                                    leftEyeData.gaze_origin_mm.y + "," + leftEyeData.gaze_origin_mm.z + "," + rightEyeData.gaze_origin_mm.x + "," +
                                    rightEyeData.gaze_origin_mm.y + "," + rightEyeData.gaze_origin_mm.z + "," + EyeDeviationAngle + "," + selectIndex);
                        }
                    }
                    //放在屏幕
                    fishPosStr = "小鱼坐标：" + obj.transform.position;
                    fishPosText.text = fishPosStr;
                    pupilPosLeftText.text = leftPuipleStr;
                    pupilPosRightText.text = rightPuipleStr;
                    leftPuipleStr = "左瞳孔X坐标：" + leftPupilPos[0] + "\n左瞳孔Y坐标：" + leftPupilPos[1];
                    rightPuipleStr = "右瞳孔X坐标：" + rightPupilPos[0] + "\n右瞳孔Y坐标：" + rightPupilPos[1];

                }
            }

        }
    }
}

