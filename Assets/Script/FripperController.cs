using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {

    private HingeJoint myHingeJoint;

    private float defaultAngle = 20;
    private float flickAngle = -20;
    
    // Use this for initialization
    void Start() {

        myHingeJoint = this.GetComponent<HingeJoint>();

        SetAngle(defaultAngle);
        
	}
	
	// Update is called once per frame
	void Update () {
		
        //左矢印キーを押した時左フリッパーを動かす
        if(Input.GetKeyDown(KeyCode.LeftArrow) && this.tag == "LeftFripperTag")
        {
            SetAngle(flickAngle);

        }
        //右矢印キーを押した時左フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.RightArrow) && this.tag == "RightFripperTag")
        {
            SetAngle(flickAngle);
        }

        //矢印キー離された時フリッパーを元に戻す
        if(Input.GetKeyUp(KeyCode.LeftArrow) && this.tag == "LeftFripperTag")
        {
            SetAngle(defaultAngle);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && this.tag == "RightFripperTag")
        {
            SetAngle(defaultAngle);
        }

    }

    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;

    }
}
