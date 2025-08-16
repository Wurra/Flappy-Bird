using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator ami;
    public float force = 100f; // С�����ϵĳ�ʼ��    
    private bool death = false; // ������־������һ������ֵ  

    public delegate void DeathNotify(); // ����ί�ж��壬��� void ��������  
    public event DeathNotify onDeath;
    private Vector3 initialPosition; // С��ĳ�ʼλ�ã���������С��λ��

    public UnityAction<int> OnScore; 
    // Start is called before the first frame update    
    void Start()
    {
        rigidbodyBird.Sleep(); // ȷ��С���ڿ�ʼʱ���ھ�ֹ״̬  
        this.ami = GetComponent<Animator>(); // ��ȡС��Ķ������  
        this.Idle(); // ����С��ĳ�ʼ����Ϊ idle  
        initialPosition = this.transform.position; // ��¼С��ĳ�ʼλ��
    }
    public void Init()
    {
        this.transform.position = initialPosition; // ����С��λ�õ���ʼλ��
        this.Idle(); // ����С�񶯻�Ϊ idle
        this.death = false; // ����������־Ϊ false
    }
    // Update is called once per frame    
    void Update()
    {
        if (this.death) return; // ���С���Ѿ����������ٴ�������Ͷ���  �����ؿ�ֵ������������
        if (Input.GetMouseButtonDown(0))    //����������
        {
            rigidbodyBird.velocity = Vector2.zero; // ����С����ٶ�    
            rigidbodyBird.AddForce(new Vector2(0, force), ForceMode2D.Force); // ��С��һ�����ϵĳ�ʼ��  
        }
    }

    public void Idle()
    {
        this.rigidbodyBird.simulated=false; // ֹͣС��������˶�  
        this.ami.SetTrigger("Idle"); // ����С��Ķ���Ϊ idle  
    }

    public void Fly()
    {
        this.rigidbodyBird.simulated = true ; // ����С��������˶�  
        this.ami.SetTrigger("Fly"); // ����С��Ķ���Ϊ fly  
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("ScoreArea")) // ����Ƿ����÷�����  
        {
            Debug.Log("ScoreArea"); // ���������Ϣ����ʾ����÷�����
            if (this.OnScore != null)
            {
                this.OnScore(1); // �����÷��¼���֪ͨ�����ߵ÷�
            }
         
        }
        else
        this.Die(); // ����С��Ķ���Ϊ idle  
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("ScoreArea")) // ����Ƿ����÷�����  
        {

        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        this.Die(); // ����С��Ķ���Ϊ idle  
    }

    public void Die()
    {
        this.death = true; // ����������־Ϊ true  
      if(this.onDeath != null)//if (this.onDeath != null) �������Ǽ���Ƿ��������������� onDeath �¼������û�ж����ߣ�ֱ�ӵ����¼��ᵼ�´��������Ҫ�Ƚ��п�ֵ��顣
        {
            this.onDeath(); // ���������¼���֪ͨ������

        }
       
    }
}
