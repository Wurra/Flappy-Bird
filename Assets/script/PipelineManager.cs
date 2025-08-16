using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public GameObject template;
    List<pIpeline> pipelines = new List<pIpeline>(); // �ܵ��б�  ,<>���������

    

    Coroutine coroutine = null; // ����һ��Э�̱���  

    public void StartRun()
    {
        coroutine = StartCoroutine(GeneratePipelines()); // ��ʼЭ�̣����ɹܵ�  
    }
    public void Init()
    {
        for (int i = 0; i < pipelines.Count; i++) // �����ܵ��б�  
        {
            Destroy(pipelines[i].gameObject); // ���ٹܵ�����
        }
        pipelines.Clear(); // ��չܵ��б�
    }
    public void Stop()
    {
       StopCoroutine(coroutine); // ֹͣЭ��
        for(int i = 0; i < pipelines.Count; i++) // �����ܵ��б�  
        {
            pipelines[i].enabled = false; 
        }
    }

    IEnumerator GeneratePipelines() // ��ʱ���ɹܵ���Э�̷���  
    {
       for (int i = 0; i < 3; i++) 
        {
            if (pipelines.Count < 3)
                GeneratePipeline();
            else
            {
                pipelines[i].enabled = true; // ���ùܵ�
                pipelines[i].Init(); 
            }
            yield return new WaitForSeconds(2f); // �ȴ�2��  
        }
    }

    void GeneratePipeline()
    {
        if (pipelines.Count < 3)
        {
            GameObject obj = Instantiate(template, this.transform); // �������ģ�崴��һ���µ�ʵ�������������ڵ�ǰ�������������  
            pIpeline p = obj.GetComponent<pIpeline>();//ԭ������� pIpeline�ഴ���Ķ���p
            pipelines.Add(p);//�����ɵĹܵ�������ӵ� pipelines �б��У��������б��вŻ��в������ɵĹܵ����󣬲�Ȼ�б�����Զ����0������
        }
    }
}
