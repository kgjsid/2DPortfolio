using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask transitionLayer;

    Vector2 moveDir;
    Vector2 currentPoint;
    Vector2 nextPoint;

    bool isMove;
    bool isTrigger;
    private void Start()
    {
        //tileSizeX = (int)tileMap.cellSize.x;
        //tileSizeY = (int)tileMap.cellSize.y;
        currentPoint = new Vector2((int)transform.position.x, (int)transform.position.y);
        currentPoint.x += 0.5f;
        currentPoint.y += 0.5f;
        transform.position = currentPoint;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // 한 타일만 이동하게 수정하기
        // 도트 움직임 구현..
        if (!isMove)
        {
            if (moveDir.Equals(Vector2.right))
            {
                animator.SetTrigger("MoveLeft");
                spriteRenderer.flipX = true;
                FindNextTile(Vector2.right);
            }
            if (moveDir.Equals(Vector2.left))
            {
                animator.SetTrigger("MoveLeft");
                spriteRenderer.flipX = false;
                FindNextTile(Vector2.left);
            }
            if (moveDir.Equals(Vector2.up))
            {
                animator.SetTrigger("MoveBack");
                FindNextTile(Vector2.up);
            }
            if (moveDir.Equals(Vector2.down))
            {
                animator.SetTrigger("MoveFront");
                FindNextTile(Vector2.down);
            }
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();

        moveDir = inputDir;
    }

    private void FindNextTile(Vector2 nextPos)
    {   // 충돌 타겟 검사
        if(Physics2D.Raycast(transform.position, nextPos, 1f, obstacleLayer))
        {
            return;
        }

        if (gridManager.points.ContainsKey(currentPoint + nextPos))
            nextPoint = gridManager.points[currentPoint + nextPos];
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        float rate = 0f;
        isMove = true;
        while (rate < 1f)
        {
            rate += 0.05f;
            transform.position = Vector2.Lerp(currentPoint, nextPoint, rate);

            yield return new WaitForSeconds(0.01f);
        }
        currentPoint = nextPoint;
        isMove = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & transitionLayer) != 0 && !isTrigger)
        {
            isTrigger = true;
            StartCoroutine(TransitionRoutine(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & transitionLayer) != 0 && isTrigger)
        {
            isTrigger = false;
        }
    }

    private void FindNearestTile()
    {
        float minDistance = Mathf.Infinity;
        Vector2 nearestPos = new Vector2(0, 0);
        foreach(Vector2 tile in gridManager.points.Keys)
        {
            float distance = Vector2.Distance(transform.position, tile);
            if(distance < minDistance)
            {
                minDistance = distance;
                nearestPos = tile;
            }
        }

        currentPoint = nearestPos;
    }

    IEnumerator TransitionRoutine(Collider2D collision)
    {
        // 트리거 진입해서 이동 -> 트리거 탈출하고 -> 이동 후 다시 트리거에 들어감 -> 다시 이동됨..
        // 조금 더 조절하기(이동하고 한칸 앞으로 갈 수 있도록 설정)

        ScreenTransition point = collision.gameObject.GetComponent<ScreenTransition>();
        nextPoint = point.ReturnPosition();
        currentPoint = point.ReturnPosition();
        FindNearestTile();

        yield return new WaitUntil(() => (isTrigger == false));
    }
}
