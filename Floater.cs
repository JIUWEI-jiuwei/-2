using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ư��������
/// </summary>
public class Floater : MonoBehaviour
{
    /// <summary> �Ƿ�λ��ˮ�� </summary>
    private bool isInWater=false;
    /// <summary> ˮ </summary>
    private GameObject water;
    /// <summary> ����λ��ˮ�е������� </summary>
    private float waterY;
    /// <summary> ���� </summary>
    //private const float floatageForce=0;
    /// <summary> ˮ���ܶ� </summary>
    private const float density = 1;
    /// <summary> �������ٶ� </summary>
    private const float g = 9.8f;
    /// <summary> ˮ������ </summary>
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
    /// ���������ܵ��ĸ������ϸ��ո������㹫ʽ����
    /// </summary>
    void calFloatage()
    {
        waterY = water.transform.position.y+water.transform.localScale.y/2;//ˮ���Y����
        //���ˮ���Y������ڵ���ˮ���������±�Ե��Y����Ļ�����֤�������ˮ����
        if (waterY > (transform.position.y -transform.localScale.y))//�������ˮ��
        {
            float h = waterY - (transform.position.y - transform.localScale.y / 2) > transform.localScale.y ? transform.localScale.y :
                waterY - (transform.position.y - transform.localScale.y / 2);//������ˮ�еĸ߶�
            float floatageForce = density * g * transform.localScale.x * transform.localScale.z * h;//���㸡��
            GetComponent<Rigidbody>().AddForce(0, floatageForce, 0);//������ʩ�Ӹ���

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
