using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///·¢Éä×Óµ¯
///</summary>
class Spawner : MonoBehaviour
{
    public float timeInterval = 3f;
    float timer = 0f;
    [SerializeField]
    public GameObject bullet;

    private void Start()
    {
        
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeInterval)
        {
            Instantiate(bullet,gameObject.transform);
            timer = 0;
        }

    }
}
