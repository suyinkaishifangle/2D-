using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public Rigidbody2D rocket;//存储炮弹预制体
    public float speed = 20f;//设置炮弹发射速度
    private PlayerController playerCtrl;//动画控制器类型，获取小豆人上的动画控制器
    private Animator anim;
    /*
     * Star Awake Update FixedUpdate
     * Awake首先调用，创建变量
     * Start函数在update函数第一次调用值卡被调用
     * 在该函数内赋初值，初始化
     */
    // Use this for initialization

    void Start()
    {

    }
    void Awake()
    {
        //获取发射子弹对象上的父对象上的动画控制器组件
        anim = transform.root.gameObject.GetComponent<Animator>();
        //获取发射子对象的父对象小豆人上的名称行为
        playerCtrl = transform.root.GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");
            if (playerCtrl.facingRight)
            {
                Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(speed, 0);
            }
            else
            {
                Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(-speed, 0);
            }
        }
    }
}
