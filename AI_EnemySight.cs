using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������Ұ�����ڵ��˵�������box collider����
/// </summary>
public class AI_EnemySight : MonoBehaviour
{
    /// <summary> ���˵���Ұ�Ƕ�</summary>
    public float fieldOfViewAngle = 120f;
    /// <summary>�ж�����Ƿ���������Ұ</summary>
    public bool playerInsight;
    /// <summary> ���һ�ο�����ҵ�λ��</summary>
    public Vector3 personalLastSight;
    /// <summary> �趨Ĭ��λ��</summary>
    public static Vector3 resetPos = Vector3.back;
    /// <summary> ���һ��trigger���</summary>
    BoxCollider col;
    /// <summary> ���</summary>
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        //������дtag���ں������ֱ����player������壬����ö�
        player = GameObject.FindGameObjectWithTag("player");
        //Ĭ��������λ��
        personalLastSight = resetPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player){
            playerInsight = false;
            //�����������ӵ��˵����
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position,direction.normalized,out hit, col.size.z))
                {
                    if (hit.collider.gameObject == player)
                    {
                        Debug.Log("I saw the player!");
                        playerInsight = true;
                        personalLastSight = player.transform.position;
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInsight = false;
        }
    }
}
