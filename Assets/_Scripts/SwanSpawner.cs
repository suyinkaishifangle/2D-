using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanSpawner : MonoBehaviour {
    public Rigidbody2D backgroudProp;//刚体
    public float leftRpawnPosX;//右边界
    public float rightRpawnPosX;//左边界
    public float minRpawnPosY;//最低高度
    public float maxRpawnPosY;//最高高度
    public float minTimeBetweenSpawns;//最短时间间隔
    public float maxTimeBetweenSpawns;//最长时间间隔
    public float minSpeed;//最小速度
    public float maxSpeed;//最大速度
    
    void Start () {
        //
        Random.seed = System.DateTime.Today.Millisecond;//设置随机种子数，使用时间的毫秒数作为随机种子
        StartCoroutine("Spawn");//启用协程函数
	}
	IEnumerator Spawn()
    {
        float waitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);//等待waitTime延迟
        yield return new WaitForSeconds(waitTime);//天鹅朝左的状态参数
        bool facingLeft = Random.Range(0, 2) == 0;//确定实例化X的位置，如果facingLeft为真，从右边界实例化，否则左边界实例化
        float posX = facingLeft ? rightRpawnPosX : leftRpawnPosX;//确定实例化的高度，在最小与最大高度之间
        float posY = Random.Range(minRpawnPosY, maxRpawnPosY);//设置实例化的位置
        Vector3 spawnPos = new Vector3(posX, posY, transform.position.z);//在指定位置实例化
        Rigidbody2D propInstance = Instantiate(backgroudProp, spawnPos, Quaternion.identity) as Rigidbody2D;
        if(!facingLeft)//如果向右飞行，控制天鹅的反向（镜像处理）
        {
            Vector3 scale = propInstance.transform.localScale;
            scale.x *= -1;
            propInstance.transform.localScale = scale;
        }
        float speed = Random.Range(minSpeed, maxSpeed);
        speed *= facingLeft ? -1f : 1f;//根据facingLeft值的真假确定speed的符号
        propInstance.velocity = new Vector2(speed, 0);//设置刚体速度
        StartCoroutine(Spawn());//启用协程函数  递归调用
        while(propInstance !=null)//以下控制天鹅废除边界销毁
        {
            if(facingLeft )//朝左飞，若x下雨左边界-0.5则销毁
            {
                if (propInstance.transform.position.x < leftRpawnPosX - 0.5f)
                    Destroy(propInstance.gameObject);
            }
            else//朝右飞，若x大于右边界0.5则销毁
            {
                if (propInstance.transform.position.x > rightRpawnPosX + 0.5f)
                    Destroy(propInstance.gameObject);
            }
            yield return null;
        }

    }
	void Update () {
		
	}
}
