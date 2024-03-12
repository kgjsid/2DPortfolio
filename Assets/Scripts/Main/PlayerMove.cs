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
        // �� Ÿ�ϸ� �̵��ϰ� �����ϱ�
        // ��Ʈ ������ ����..
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
    {   // �浹 Ÿ�� �˻�
        Debug.Log(transform.position);
        if (Physics2D.Raycast(transform.position, nextPos, 1f, obstacleLayer))
        {
            return;
        }

        if (Physics2D.Raycast(transform.position, nextPos, 1f, jumpLayer))
        {
            // �װ��� �� ���⿡���� ������ �����ϵ��� ����?
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
        // Ʈ���� �����ؼ� �̵� -> Ʈ���� Ż���ϰ� -> �̵� �� �ٽ� Ʈ���ſ� �� -> �ٽ� �̵���..
        // ���� �� �����ϱ�(�̵��ϰ� ��ĭ ������ �� �� �ֵ��� ����)
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
        // �ִϸ��̼� ����
        // ������ ��ĭ ����
        isJump = true;
        Debug.Log("���� ��ƾ");
        Debug.Log($"�÷��̾� : {gameObject.GetComponent<Collider2D>()}");
        Debug.Log($"����Ÿ�� : {Physics2D.Raycast(transform.position, nextPos, 1f, jumpLayer).collider}");
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
    // ������ƾ ���� �ʿ� -> �ݶ��̴� ���ÿ��� ���� �߻�
}
