using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    // TODO
    // �÷��̾ Ʈ���� ���� ���Խ� ��� ��ȯ �ʿ�
    // ��� ������ ��� ���ñ���?
    [SerializeField] Transform[] points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
         * 1���� 2�� �� �� ����
         * 2���� 1, 3���� �� �� ����
         * 3���� 2, 4, 5�� �� �� ����
         * outfield�� infield �����ϱ�?
         * 
         * outfield���� infield�� �� �� ���ñ���
         * outfield�� 0���� �ϸ�?
         */
    }

}
