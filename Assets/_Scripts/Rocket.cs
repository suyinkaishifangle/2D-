using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    public GameObject explosion;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2);	
	}
    void OnExplode()//自定义炮弹实现
    {
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        //旋转角度，围绕Z轴旋转
        Instantiate(explosion,transform.position,randomRotation);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy") //当炮弹击中敌人时
        {
            col.gameObject.GetComponent<Enemy>().Hurt();//获取col所属游戏对象上的名称为Enemy的脚本组件，然后调用该函数
            OnExplode();
            Destroy(gameObject);
        } else if (col.gameObject.tag != "Player") 
        {
            OnExplode();
            Destroy(gameObject);//销毁炮弹
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
