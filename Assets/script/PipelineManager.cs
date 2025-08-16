using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public GameObject template;
    List<pIpeline> pipelines = new List<pIpeline>(); // 管道列表  ,<>里边是类名

    

    Coroutine coroutine = null; // 定义一个协程变量  

    public void StartRun()
    {
        coroutine = StartCoroutine(GeneratePipelines()); // 开始协程，生成管道  
    }
    public void Init()
    {
        for (int i = 0; i < pipelines.Count; i++) // 遍历管道列表  
        {
            Destroy(pipelines[i].gameObject); // 销毁管道对象
        }
        pipelines.Clear(); // 清空管道列表
    }
    public void Stop()
    {
       StopCoroutine(coroutine); // 停止协程
        for(int i = 0; i < pipelines.Count; i++) // 遍历管道列表  
        {
            pipelines[i].enabled = false; 
        }
    }

    IEnumerator GeneratePipelines() // 延时生成管道的协程方法  
    {
       for (int i = 0; i < 3; i++) 
        {
            if (pipelines.Count < 3)
                GeneratePipeline();
            else
            {
                pipelines[i].enabled = true; // 启用管道
                pipelines[i].Init(); 
            }
            yield return new WaitForSeconds(2f); // 等待2秒  
        }
    }

    void GeneratePipeline()
    {
        if (pipelines.Count < 3)
        {
            GameObject obj = Instantiate(template, this.transform); // 按照这个模板创建一个新的实例，并且生成在当前对象的子物体下  
            pIpeline p = obj.GetComponent<pIpeline>();//原来这个是 pIpeline类创建的对象p
            pipelines.Add(p);//将生成的管道对象添加到 pipelines 列表中，这样，列表中才会有不断生成的管道对象，不然列表中永远都是0个对象
        }
    }
}
