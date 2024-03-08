using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    // TODO
    // 플레이어가 트리거 존에 돌입시 장면 전환 필요
    // 장면 포인터 잡고 스택구조?
    [SerializeField] Transform[] points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
         * 1에서 2로 갈 수 있음
         * 2에서 1, 3으로 갈 수 있음
         * 3에서 2, 4, 5로 갈 수 있음
         * outfield랑 infield 구분하기?
         * 
         * outfield에서 infield로 들어갈 땐 스택구조
         * outfield를 0으로 하면?
         */
    }

}
