using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹发射脚本
/// 挂在camera身上
/// </summary>
public class BallFire : MonoBehaviour
{
    public GameObject bullet;
    static float pressTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            pressTime += Time.deltaTime;//按住鼠标的时间
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//获取鼠标点击的位置发送一条垂直屏幕的射线
            GameObject b = Instantiate(bullet, ray.origin, Quaternion.LookRotation(ray.direction))as GameObject;
            //将一个预制体实例化出来,as GameObject作为游戏物体
            var cf = b.GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, pressTime * 10);//在纵深方向加一个力
            pressTime = 0f;
        }




    }
}
