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

        GetSPTouch();

        //GetTouch();

    }

    private void GetSPTouch()
    {
        int fingerCount = 0;
        bool Moved = false;

        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    LeftFripper.SetAngle(flickAngle);
                    touch_now_left = true;
                }
                else
                {
                    RightFripper.SetAngle(flickAngle);
                    touch_now_right = true;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    LeftFripper.SetAngle(defaultAngle);
                    touch_now_left = false;
                }
                else
                {
                    RightFripper.SetAngle(defaultAngle);
                    touch_now_right = false;
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Moved = true;

            }
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                fingerCount++;
        }

        if (Moved)
        {
            bool touch_temp_left = false;
            bool touch_temp_right = false;
            if (touch_now_left)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        touch_temp_left = true;
                    }
                }
                if (touch_temp_left == false)
                {
                    LeftFripper.SetAngle(defaultAngle);
                    touch_now_left = false;
                }
            }
            if (touch_now_right)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.position.x >= Screen.width / 2)
                    {
                        touch_temp_right = true;
                    }
                }
                if (touch_temp_right == false)
                {
                    RightFripper.SetAngle(defaultAngle);
                    touch_now_right = false;
                }
            }

        }


            if (fingerCount > 0)
            print("User has " + fingerCount + " finger(s) touching the screen");

    }
    private void GetTouch()
    {
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
        // ドラッグ中の処理
        else if (touch_id == TouchInfo.Moved)
        {
            Vector3 touch_pos = AppUtil.GetTouchPosition();
            if (touch_now_left)
            {
                if (touch_pos.x >= Screen.width / 2)
                {
                    LeftFripper.SetAngle(defaultAngle);
                    touch_now_left = false;
                }
            }
            if (touch_now_right)
            {
                if (touch_pos.x < Screen.width / 2)
                {
                    RightFripper.SetAngle(defaultAngle);
                    touch_now_right = false;
                }
            }

        }
        else if (touch_id == TouchInfo.Ended)
        {
            Vector3 touch_pos = AppUtil.GetTouchPosition();
            Debug.Log("タッチ離した");
            if (touch_now_left && (touch_pos.x < Screen.width / 2))
            {
                LeftFripper.SetAngle(defaultAngle);
                touch_now_left = false;
            }
            else if (touch_now_right && (touch_pos.x >= Screen.width / 2))
            {
                RightFripper.SetAngle(defaultAngle);
                touch_now_right = false;
            }

        }
    }
}

