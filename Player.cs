using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///二维角色移动跳跃+翻转
///</summary>
class Player : MonoBehaviour
{
    public float m_MaxSpeed=10f;
    public float m_JumpForce = 400f;
    public LayerMask m_WhatIsGround;

    public Transform m_GroundCheck;
    const float k_GroundedRadius = 0.5f;
    public bool m_Grounded;
    private Animator m_Anim;
    private Rigidbody2D rigbody;
    /// <summary>面向右边</summary>
    public bool m_FacingRight = true;
    public bool m_Jump;

    private void Awake()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        m_Anim = GetComponent<Animator>();
        rigbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //进行是否在地面上的判定
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for(int i = 0; i < colliders.Length; i++)
        {
            //排除检查到的是自身的可能
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
        //m_Anim.SetBool("ground", m_Grounded);
        float h = Input.GetAxis("Horizontal");
        if (!m_Jump)
        {
            m_Jump = Input.GetKeyDown(KeyCode.Space);
        }
        Move(h, m_Jump);
        m_Jump = false;
    }
    /// <summary>
    /// 角色在地面上移动的代码
    /// </summary>
    /// <param name="move"></param>
    /// <param name="jump"></param>
    public void Move(float move,bool jump)
    {
        //在地面上移动的操作
        if (m_Grounded)
        {
            //m_Anim.SetFloat("speed", Mathf.Abs(move));
            rigbody.velocity = new Vector2(move * m_MaxSpeed, rigbody.velocity.y);
            if (move < 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move > 0 && m_FacingRight)
            {
                Flip();
            }
        }
        //关于跳跃的操作
        if (m_Grounded && jump)//&& m_Anim.GetBool("ground")
        {
            m_Grounded = false;
            //m_Anim.SetBool("ground", false);
            rigbody.AddForce(new Vector2(0, m_JumpForce));
        }
    }
    /// <summary>
    /// 翻转图像
    /// </summary>
    public void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
