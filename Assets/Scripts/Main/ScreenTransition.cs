using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField] Transform target; // 이동해야 할 위치

    private void Start()
    {
    }

    public IEnumerator TransitionRoutine()
    {
        yield return new WaitForSeconds(0.5f);
    }

    public Vector2 ReturnPosition()
    {
        return target.position;
    }

    public void PositionChange(PlayerMove player)
    {
        Debug.Log("플레이어 이동");
        player.gameObject.transform.position = target.position;
    }
}
