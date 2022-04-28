using UnityEngine;
using UnityEngine.Networking;

///<summary>
///角色移动+联网 代码
///</summary>
[System.Obsolete]
class Square : NetworkBehaviour
{
    public Vector2 startPos;
    public float moveSpeed = 5;
    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            transform.Translate(new Vector3(h, v,0) * Time.deltaTime * moveSpeed, Space.World);
        }
    }
    /// <summary>
    /// 玩家重生位置
    /// </summary>
    [ClientRpc]
    void RpcResSpawn()
    {
        if (startPos != null)
        {
            transform.position = startPos;
        }
    }
}
