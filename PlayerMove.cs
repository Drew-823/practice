using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent PlayerAgent;
    private Animator _animator;
    private bool isMoving = false; // �����ƶ�״̬��־
    // Start is called before the first frame update
    void Start()
    {
        PlayerAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        PlayerAgent.stoppingDistance = 0.1f; // ����ֹͣ��ֵ
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
                // ����Э�̼�⵽��
                StartCoroutine(CheckMovementCompletion());
            }

        }

    }
    IEnumerator CheckMovementCompletion()
    {
        // �ȴ�·���������
        while (PlayerAgent.pathPending)
            yield return null;

        // �ȴ�����Ŀ�ĵ�
        while (PlayerAgent.remainingDistance > PlayerAgent.stoppingDistance)
            yield return null;

        // �ȴ��ٶȽ�Ϊ�㣨ȷ����ȫֹͣ��
        while (PlayerAgent.velocity.sqrMagnitude > 0.01f)
            yield return null;

        // �Ѿ����ﲢ��ֹͣ
        _animator.SetBool("isRun", false);
        isMoving = false;
    }
}
