using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pIpeline : MonoBehaviour
{
    public float speed=2f; // �ܵ��ƶ��ٶ�
    public float heightOffset = 2f; // �ܵ��߶�ƫ����
    // Start is called before the first frame update
    void Start()
    {
        this.Init();
       
    }
    float t = 0;
    public void Init()
    {
        float y = Random.Range(-heightOffset, heightOffset); // ������ɹܵ��ĸ߶�ƫ��
        this.transform.localPosition = new Vector3(0, y, 0); // ���ùܵ��ĳ�ʼλ��
       
    }
    
    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.left * speed * Time.deltaTime; // �ܵ������ƶ�
        t += Time.deltaTime;
        if (t > 6f)
        {
            t = 0;
            this.Init();
        }    
    }
}
