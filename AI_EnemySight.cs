using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人视野：挂在敌人的子物体box collider身上
/// </summary>
public class AI_EnemySight : MonoBehaviour
{
    /// <summary> 敌人的视野角度</summary>
    public float fieldOfViewAngle = 120f;
    /// <summary>判断玩家是否进入敌人视野</summary>
    public bool playerInsight;
    /// <summary> 最后一次看到玩家的位置</summary>
    public Vector3 personalLastSight;
    /// <summary> 设定默认位置</summary>
    public static Vector3 resetPos = Vector3.back;
    /// <summary> 添加一个trigger检测</summary>
    BoxCollider col;
    /// <summary> 玩家</summary>
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        //在这里写tag，在后面可以直接用player这个物体，方便得多
        player = GameObject.FindGameObjectWithTag("player");
        //默认玩家最初位置
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
            //方向向量：从敌人到玩家
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
