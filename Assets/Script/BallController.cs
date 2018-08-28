using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    public GameObject GameText;
    public GameObject ScoreText;

    private int score;

    //ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.5f;

    // Use this for initialization
    void Start () {

        score = 0;
        ScoreText.GetComponent<Text>().text = score + "点";

        GameText.SetActive(false);

        // ボールの位置を初期化
        this.transform.position = new Vector3(3.07f, 2.74f, 4.08f); 

    }
	
	// Update is called once per frame
	void Update () {
        //ボールが画面外に出た場合
        if (this.transform.position.z < this.visiblePosZ)
        {
            //GameoverTextにゲームオーバを表示
            GameText.SetActive(true);
            GameText.GetComponent<Text>().text = "Game Over";
        }
        //ゲームを再スタート
        if (this.transform.position.z < this.visiblePosZ*3)
        {
            Start();
        }
    }

    // 衝突時に呼ばれる関数
    void OnCollisionEnter(Collision other)
    {
        
        switch(other.gameObject.tag)
        {
            case "SmallStarTag":
                score += 10;
                break;
            case "LargeStarTag":
                score += 20;
                break;
            case "SmallCloudTag":
                score += 30;
                break;
            case "LargeCloudTag":
                score += 40;
                break;
        }
        ScoreText.GetComponent<Text>().text = score + "点";

    }
}
