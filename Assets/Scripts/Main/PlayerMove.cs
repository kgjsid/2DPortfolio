using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask transitionLayer;
    [SerializeField] LayerMask jumpLayer;

    Vector2 moveDir;
    Vector2 currentPoint;
    Vector2 nextPoint;

    bool isMove;
    bool isJump;
    bool isTrigger;

    public UnityEvent transitionEvent;
    public UnityEvent moveEvent;

    public void SetCurrentPos()
    {
        currentPoint = new Vector2((int)transform.position.x, (int)transform.position.y);
        currentPoint.x += 0.5f;
        currentPoint.y += 0.5f;
        transform.position = currentPoint;
    }

    public void SetNextPos()
    {
        nextPoint = currentPoint;
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
            animator.SetFloat("xSpeed", moveDir.x);
            animator.SetFloat("ySpeed", moveDir.y);
            if (moveDir.Equals(Vector2.right))
            {
                spriteRenderer.flipX = true;
                FindNextTile(Vector2.right);
            }
            if (moveDir.Equals(Vector2.left))
            {
                spriteRenderer.flipX = false;
                FindNextTile(Vector2.left);
            }
            if (moveDir.Equals(Vector2.up))
            {
                FindNextTile(Vector2.up);
            }
            if (moveDir.Equals(Vector2.down))
            {
                FindNextTile(Vector2.down);
            }

            animator.SetBool("isMove", isMove);
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();

        moveDir = inputDir;
    }

    private void FindNextTile(Vector2 nextPos)
    {   // 충돌 타겟 검사
        Debug.Log(transform.position);
        if (Physics2D.Raycast(transform.position, nextPos, 1f, obstacleLayer))
        {
            return;
        }

        if (Physics2D.Raycast(transform.position, nextPos, 1f, jumpLayer))
        {
            // 그거의 뒷 방향에서만 점프가 가능하도록 설정?
            if (isJump) return;

            //StartCoroutine(JumpRoutine(currentPoint, nextPos));
            return;
        }

        if (!gridManager.points.ContainsKey(currentPoint + nextPos))
            FindNearestTile();

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
        moveEvent?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & transitionLayer) != 0 && !isTrigger)
        {
            StartCoroutine(TransitionRoutine(collision, moveDir));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & transitionLayer) != 0 && isTrigger)
        {
            // isTrigger = false;
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
        transform.position = currentPoint;
    }

    IEnumerator TransitionRoutine(Collider2D collision, Vector2 moveDir)
    {
        // 트리거 진입해서 이동 -> 트리거 탈출하고 -> 이동 후 다시 트리거에 들어감 -> 다시 이동됨..
        // 조금 더 조절하기(이동하고 한칸 앞으로 갈 수 있도록 설정)
        transitionEvent?.Invoke();
        isTrigger = true;
        ScreenTransition point = collision.gameObject.GetComponent<ScreenTransition>();
        nextPoint = point.ReturnPosition();
        currentPoint = point.ReturnPosition();
        FindNearestTile();

        yield return new WaitForSeconds(0.1f);
        isTrigger = false;
        
    }

    IEnumerator JumpRoutine(Vector2 currentPoint, Vector2 nextPos)
    {
        // 애니메이션 설정
        // 앞으로 두칸 가기
        isJump = true;
        Debug.Log("점프 루틴");
        Debug.Log($"플레이어 : {gameObject.GetComponent<Collider2D>()}");
        Debug.Log($"점프타일 : {Physics2D.Raycast(transform.position, nextPos, 1f, jumpLayer).collider}");
        nextPos = currentPoint + nextPos * 2;
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), Physics2D.Raycast(transform.position, nextPos, 1f, jumpLayer).collider, true);
        float rate = 0f;
        isMove = true;
        while (rate < 1f)
        {
            rate += 0.05f;
            transform.position = Vector2.Lerp(currentPoint, nextPoint, rate);

            yield return new WaitForSeconds(0.01f);
        }
        currentPoint = nextPoint;

        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), Physics2D.Raycast(transform.position, nextPos, 1f, jumpLayer).collider, false);
        isMove = false;
        isJump = false;
        moveEvent?.Invoke();
    }
    // 점프루틴 수정 필요 -> 콜라이더 무시에서 에러 발생
}
