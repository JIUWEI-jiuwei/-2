using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 敌人巡逻：挂在敌人身上
/// </summary>
public class AI_Enemy : MonoBehaviour
{
    /// <summary> 敌人巡逻速度</summary>
    public float patrolSpeed=2f;
    /// <summary> 敌人在目标点停留时间</summary>
    public float patrolWaitTime = 1f;
    /// <summary> 巡逻点的父物体</summary>
    public Transform patrolWayPoints;
    /// <summary> 导航组件</summary>
    private NavMeshAgent agent;
    /// <summary> 计时器</summary>
    private float patrolTimer;
    /// <summary> 巡逻点索引</summary>
    private int wayPointIndex=0;

    /// <summary> 敌人旋转速度</summary>
    public float shootRotSpeed = 5f;
    /// <summary> 子弹发射CD</summary>
    public float shootFreeTime = 2f;
    /// <summary> 计时器</summary>
    public float shootTimer = 0f;
    /// <summary> 实例化视野类</summary>
    private AI_EnemySight enemySight;
    /// <summary> 子弹预制体-用刚体获取</summary>
    public Rigidbody bullet;
    /// <summary> 玩家</summary>
    private Transform player;
    /// <summary> 是否开始追逐</summary>
    public bool chase;

    /// <summary> 追逐速度</summary>
    public float chaseSpeed = 5f;
    /// <summary> 追到之后的观察时间</summary>
    public float chaseWaitTime = 5f;
    private float chaseTimer;
    /// <summary> 阈值，平方为3</summary>
    public float sqrPlayerDist = 3f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemySight = transform.Find("ViewRange").GetComponent<AI_EnemySight>();
        player = GameObject.FindGameObjectWithTag("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (enemySight.playerInsight)
        {
            Shooting();
            chase = true;
        }
        else if (chase)
        {
            Chase();
        }
        else
        {
            Patrolling();
        }
    }
    /// <summary>
    /// 追逐玩家
    /// </summary>
    void Chase()
    {
        agent.isStopped = false;

        //玩家最后一次出现在敌人视野中的位置和敌人位置之间的向量，目的：得到该向量的长度
        Vector3 sightingDeltaPos = enemySight.personalLastSight - transform.position;

        //向量长度的平方大于阈值
        if (sightingDeltaPos.sqrMagnitude > sqrPlayerDist)
        {
            //给目的地赋值为玩家最后一次出现在视野中的位置
            agent.destination = enemySight.personalLastSight;
        }

        //将速度赋值为追踪速度
        agent.speed = chaseSpeed;

        //如果追上玩家，进入观察时间
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer>=chaseWaitTime)
            {
                chase = false;
                chaseTimer = 0f;
            }
        }
        else
        {
            //如果还是在追踪状态下，计时器需要时刻归零
            chaseTimer = 0f;
        }
    }
    /// <summary>
    /// 射击
    /// </summary>
    void Shooting()
    {
        Vector3 lookPos = player.position;
        lookPos.y = transform.position.y;//玩家位置和敌人位置设置成一样，避免敌人进行上下点头
        
        //发射子弹的向量
        Vector3 targetDir = lookPos - transform.position;

        //需要先转到正确的方向，然后发射：四元数的球面线性插值（旋转的物体，看向的方向，插值系数-最小值）
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir),
            Mathf.Min(1, Time.deltaTime * shootRotSpeed));//到1的时候就完全转过去了
        agent.isStopped = true;
        
        //已经转向玩家的方向，开始射击
        if (Vector3.Angle(transform.forward, targetDir) < 100)
        {
            Debug.Log("start");
            
            if (shootTimer > shootFreeTime)
            {
                //实例化子弹，朝向玩家的方向
                Instantiate(bullet, transform.position, Quaternion.LookRotation(player.position- transform.position));
                shootTimer = 0;
                Debug.Log("shoot");
            }
            
        }
    }
    /// <summary>
    /// 巡逻
    /// </summary>
    void Patrolling()
    {
        agent.isStopped = false;
        agent.speed = patrolSpeed;
        if (agent.remainingDistance <= agent.stoppingDistance)//到达目标点
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)//暂停时间已满足
            {
                //实现四个点的重复循环
                if (wayPointIndex == patrolWayPoints.childCount - 1)
                {
                    wayPointIndex = 0;
                }
                else
                {
                    wayPointIndex++;
                }
                patrolTimer = 0;
            }
        }
        else
        {
            patrolTimer = 0;
        }
        //根据索引获取子物体的位置并设为目的地
        agent.destination = patrolWayPoints.GetChild(wayPointIndex).position;
    }
}
