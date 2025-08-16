using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pIpeline : MonoBehaviour
{
    public float speed=2f; // 管道移动速度
    public float heightOffset = 2f; // 管道高度偏移量
    // Start is called before the first frame update
    void Start()
    {
        this.Init();
       
    }
    float t = 0;
    public void Init()
    {
        float y = Random.Range(-heightOffset, heightOffset); // 随机生成管道的高度偏移
        this.transform.localPosition = new Vector3(0, y, 0); // 设置管道的初始位置
       
    }
    
    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.left * speed * Time.deltaTime; // 管道向左移动
        t += Time.deltaTime;
        if (t > 6f)
        {
            t = 0;
            this.Init();
        }    
    }
}
