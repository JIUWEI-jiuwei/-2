using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// ����Ѳ�ߣ����ڵ�������
/// </summary>
public class AI_Enemy : MonoBehaviour
{
    /// <summary> ����Ѳ���ٶ�</summary>
    public float patrolSpeed=2f;
    /// <summary> ������Ŀ���ͣ��ʱ��</summary>
    public float patrolWaitTime = 1f;
    /// <summary> Ѳ�ߵ�ĸ�����</summary>
    public Transform patrolWayPoints;
    /// <summary> �������</summary>
    private NavMeshAgent agent;
    /// <summary> ��ʱ��</summary>
    private float patrolTimer;
    /// <summary> Ѳ�ߵ�����</summary>
    private int wayPointIndex=0;

    /// <summary> ������ת�ٶ�</summary>
    public float shootRotSpeed = 5f;
    /// <summary> �ӵ�����CD</summary>
    public float shootFreeTime = 2f;
    /// <summary> ��ʱ��</summary>
    public float shootTimer = 0f;
    /// <summary> ʵ������Ұ��</summary>
    private AI_EnemySight enemySight;
    /// <summary> �ӵ�Ԥ����-�ø����ȡ</summary>
    public Rigidbody bullet;
    /// <summary> ���</summary>
    private Transform player;
    /// <summary> �Ƿ�ʼ׷��</summary>
    public bool chase;

    /// <summary> ׷���ٶ�</summary>
    public float chaseSpeed = 5f;
    /// <summary> ׷��֮��Ĺ۲�ʱ��</summary>
    public float chaseWaitTime = 5f;
    private float chaseTimer;
    /// <summary> ��ֵ��ƽ��Ϊ3</summary>
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
    /// ׷�����
    /// </summary>
    void Chase()
    {
        agent.isStopped = false;

        //������һ�γ����ڵ�����Ұ�е�λ�ú͵���λ��֮���������Ŀ�ģ��õ��������ĳ���
        Vector3 sightingDeltaPos = enemySight.personalLastSight - transform.position;

        //�������ȵ�ƽ��������ֵ
        if (sightingDeltaPos.sqrMagnitude > sqrPlayerDist)
        {
            //��Ŀ�ĵظ�ֵΪ������һ�γ�������Ұ�е�λ��
            agent.destination = enemySight.personalLastSight;
        }

        //���ٶȸ�ֵΪ׷���ٶ�
        agent.speed = chaseSpeed;

        //���׷����ң�����۲�ʱ��
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
            //���������׷��״̬�£���ʱ����Ҫʱ�̹���
            chaseTimer = 0f;
        }
    }
    /// <summary>
    /// ���
    /// </summary>
    void Shooting()
    {
        Vector3 lookPos = player.position;
        lookPos.y = transform.position.y;//���λ�ú͵���λ�����ó�һ����������˽������µ�ͷ
        
        //�����ӵ�������
        Vector3 targetDir = lookPos - transform.position;

        //��Ҫ��ת����ȷ�ķ���Ȼ���䣺��Ԫ�����������Բ�ֵ����ת�����壬����ķ��򣬲�ֵϵ��-��Сֵ��
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir),
            Mathf.Min(1, Time.deltaTime * shootRotSpeed));//��1��ʱ�����ȫת��ȥ��
        agent.isStopped = true;
        
        //�Ѿ�ת����ҵķ��򣬿�ʼ���
        if (Vector3.Angle(transform.forward, targetDir) < 100)
        {
            Debug.Log("start");
            
            if (shootTimer > shootFreeTime)
            {
                //ʵ�����ӵ���������ҵķ���
                Instantiate(bullet, transform.position, Quaternion.LookRotation(player.position- transform.position));
                shootTimer = 0;
                Debug.Log("shoot");
            }
            
        }
    }
    /// <summary>
    /// Ѳ��
    /// </summary>
    void Patrolling()
    {
        agent.isStopped = false;
        agent.speed = patrolSpeed;
        if (agent.remainingDistance <= agent.stoppingDistance)//����Ŀ���
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)//��ͣʱ��������
            {
                //ʵ���ĸ�����ظ�ѭ��
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
        //����������ȡ�������λ�ò���ΪĿ�ĵ�
        agent.destination = patrolWayPoints.GetChild(wayPointIndex).position;
    }
}
