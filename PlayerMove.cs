using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent PlayerAgent;
    private Animator _animator;
    private bool isMoving = false; // 新增移动状态标志
    // Start is called before the first frame update
    void Start()
    {
        PlayerAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        PlayerAgent.stoppingDistance = 0.1f; // 设置停止阈值
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
           Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            bool isCollide = Physics.Raycast(ray, out hit);
            if(isCollide)
            {
                PlayerAgent.SetDestination(hit.point);

                _animator.SetBool("isRun", true);

                isMoving = true;
                // 启动协程检测到达
                StartCoroutine(CheckMovementCompletion());
            }

        }

    }
    IEnumerator CheckMovementCompletion()
    {
        // 等待路径计算完成
        while (PlayerAgent.pathPending)
            yield return null;

        // 等待到达目的地
        while (PlayerAgent.remainingDistance > PlayerAgent.stoppingDistance)
            yield return null;

        // 等待速度降为零（确保完全停止）
        while (PlayerAgent.velocity.sqrMagnitude > 0.01f)
            yield return null;

        // 已经到达并且停止
        _animator.SetBool("isRun", false);
        isMoving = false;
    }
}
