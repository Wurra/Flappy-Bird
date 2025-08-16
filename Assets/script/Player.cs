using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator ami;
    public float force = 100f; // 小鸟向上的初始力    
    private bool death = false; // 死亡标志，创建一个布尔值  

    public delegate void DeathNotify(); // 修正委托定义，添加 void 返回类型  
    public event DeathNotify onDeath;
    private Vector3 initialPosition; // 小鸟的初始位置，用于重置小鸟位置

    public UnityAction<int> OnScore; 
    // Start is called before the first frame update    
    void Start()
    {
        rigidbodyBird.Sleep(); // 确保小鸟在开始时处于静止状态  
        this.ami = GetComponent<Animator>(); // 获取小鸟的动画组件  
        this.Idle(); // 设置小鸟的初始动画为 idle  
        initialPosition = this.transform.position; // 记录小鸟的初始位置
    }
    public void Init()
    {
        this.transform.position = initialPosition; // 重置小鸟位置到初始位置
        this.Idle(); // 重置小鸟动画为 idle
        this.death = false; // 重置死亡标志为 false
    }
    // Update is called once per frame    
    void Update()
    {
        if (this.death) return; // 如果小鸟已经死亡，则不再处理输入和动画  ，返回空值！！！！！！
        if (Input.GetMouseButtonDown(0))    //鼠标左键触发
        {
            rigidbodyBird.velocity = Vector2.zero; // 重置小鸟的速度    
            rigidbodyBird.AddForce(new Vector2(0, force), ForceMode2D.Force); // 给小鸟一个向上的初始力  
        }
    }

    public void Idle()
    {
        this.rigidbodyBird.simulated=false; // 停止小鸟的物理运动  
        this.ami.SetTrigger("Idle"); // 设置小鸟的动画为 idle  
    }

    public void Fly()
    {
        this.rigidbodyBird.simulated = true ; // 唤醒小鸟的物理运动  
        this.ami.SetTrigger("Fly"); // 设置小鸟的动画为 fly  
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("ScoreArea")) // 检测是否进入得分区域  
        {
            Debug.Log("ScoreArea"); // 输出调试信息，表示进入得分区域
            if (this.OnScore != null)
            {
                this.OnScore(1); // 触发得分事件，通知订阅者得分
            }
         
        }
        else
        this.Die(); // 设置小鸟的动画为 idle  
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("ScoreArea")) // 检测是否进入得分区域  
        {

        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        this.Die(); // 设置小鸟的动画为 idle  
    }

    public void Die()
    {
        this.death = true; // 设置死亡标志为 true  
      if(this.onDeath != null)//if (this.onDeath != null) 的作用是检查是否有其他对象订阅了 onDeath 事件。如果没有订阅者，直接调用事件会导致错误，因此需要先进行空值检查。
        {
            this.onDeath(); // 触发死亡事件，通知订阅者

        }
       
    }
}
