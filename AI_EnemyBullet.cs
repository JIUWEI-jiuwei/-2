using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ӵ�����ҵ�Ч���������ӵ�Ԥ��������
/// </summary>
public class AI_EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
