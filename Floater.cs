using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在漂浮物体上
/// </summary>
public class Floater : MonoBehaviour
{
    /// <summary> 是否位于水中 </summary>
    private bool isInWater=false;
    /// <summary> 水 </summary>
    private GameObject water;
    /// <summary> 物体位于水中的纵坐标 </summary>
    private float waterY;
    /// <summary> 浮力 </summary>
    //private const float floatageForce=0;
    /// <summary> 水的密度 </summary>
    private const float density = 1;
    /// <summary> 重力加速度 </summary>
    private const float g = 9.8f;
    /// <summary> 水的阻力 </summary>
    private const float waterDrag = 5;

    // Start is called before the first frame update
    void Start()
    {
        water = GameObject.FindWithTag("water");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (isInWater)
        {
            calFloatage();
            GetComponent<Rigidbody>().drag = waterDrag;
        }
    }
    /// <summary>
    /// 计算物体受到的浮力，严格按照浮力计算公式来算
    /// </summary>
    void calFloatage()
    {
        waterY = water.transform.position.y+water.transform.localScale.y/2;//水面的Y坐标
        //如果水面的Y坐标大于掉入水面的物体的下边缘的Y坐标的话，就证明物体掉水里了
        if (waterY > (transform.position.y -transform.localScale.y))//如果掉到水里
        {
            float h = waterY - (transform.position.y - transform.localScale.y / 2) > transform.localScale.y ? transform.localScale.y :
                waterY - (transform.position.y - transform.localScale.y / 2);//物体在水中的高度
            float floatageForce = density * g * transform.localScale.x * transform.localScale.z * h;//计算浮力
            GetComponent<Rigidbody>().AddForce(0, floatageForce, 0);//给物体施加浮力

        }
    }
    public void SetIsInWater(bool inWater)
    {
        isInWater = inWater;
    }
    public bool GetIsInWater()
    {
        return isInWater;
    }



}
