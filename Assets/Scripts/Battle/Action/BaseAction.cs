using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction : MonoBehaviour
{
    // ���߿� �ڽĿ��� �����ϴ� �߻�Ŭ������ ����
    protected Pokemon owner;
    [SerializeField] private string actionName;

    public string ActionName { get => actionName; }
    public virtual void Execute() // �߻��Լ��� ����� 1
    {
        Debug.Log("�ڽĿ��� ���ǵ��� ����");
        Debug.Log("�����Լ�");
    }

    // ���� ��ų�� ��� �ִ� ��ü
    public void SetOwner(Pokemon owner)
    {
        this.owner = owner;
    }
}
