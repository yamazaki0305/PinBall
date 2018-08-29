using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TouchManager : MonoBehaviour {

    public FripperController LeftFripper;
    public FripperController RightFripper;
    private float defaultAngle = 20;
    private float flickAngle = -20;
    
    bool touch_now_left;
    bool touch_now_right;


    // Use this for initialization
    void Start () {
        // タッチ中
        touch_now_left = false;
        touch_now_right = false;
	}
	
	// Update is called once per frame
	void Update () {

        var touch_id = AppUtil.GetTouch();

        if (touch_id == TouchInfo.Began)
        {
            Vector3 touch_pos = AppUtil.GetTouchPosition();
            Debug.Log("タッチした");
            if (touch_pos.x < Screen.width / 2)
            {
                touch_now_left = true;
                LeftFripper.SetAngle(flickAngle);
            }
            else
            {
                touch_now_right = true;
                RightFripper.SetAngle(flickAngle);
            }

        }
        else if (touch_id == TouchInfo.Ended)
        {
            Vector3 touch_pos = AppUtil.GetTouchPosition();
            Debug.Log("タッチ離した");
            if (touch_now_left)
                LeftFripper.SetAngle(defaultAngle);
            else if (touch_now_right)
                RightFripper.SetAngle(defaultAngle);

            touch_now_left = false;
            touch_now_right = false;
        }

    }
}
