using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹打到玩家的效果：挂在子弹预制体身上
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
