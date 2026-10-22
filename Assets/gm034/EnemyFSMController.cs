using UnityEngine;

namespace GM034
{
    public class EnemyFSMController : MonoBehaviour
    {
        private enum EnemyState
        {
            Idle,
            Chase,
            Attack
        }

        [Header("Target")]
        [SerializeField] private Transform target;

        [Header("Distance")]
        [SerializeField] private float attackDistance = 1.5f;
        [SerializeField] private float chaseDistance = 4f;
        [SerializeField] private float moveSpeed = 2f;

        [Header("View")]
        [SerializeField] private float viewDistance = 4f;
        [SerializeField] private float viewAngle = 90f;

        private EnemyState currentState = EnemyState.Idle;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            UpdateColor();
        }

        private void Update()
        {
            if (target == null)
                return;

            float distance = Vector2.Distance(transform.position, target.position);

            switch (currentState)
            {
                case EnemyState.Idle:
                    if (distance <= chaseDistance)
                    {
                        ChangeState(EnemyState.Chase);
                    }
                    break;

                case EnemyState.Chase:
                    if (distance <= attackDistance)
                    {
                        ChangeState(EnemyState.Attack);
                    }
                    else if (distance > chaseDistance * 1.2f)
                    {
                        ChangeState(EnemyState.Idle);
                    }
                    else
                    {
                        Vector3 direction = (target.position - transform.position).normalized;
                        transform.right = direction;
                        transform.position += direction * moveSpeed * Time.deltaTime;
                    }
                    break;

                case EnemyState.Attack:
                    if (distance > attackDistance)
                        ChangeState(EnemyState.Chase);
                    break;
            }
        }

        private void ChangeState(EnemyState nextState)
        {
            if (currentState == nextState)
                return;

            currentState = nextState;
            UpdateColor();
        }

        private void UpdateColor()
        {
            if (spriteRenderer == null)
                return;

            switch (currentState)
            {
                case EnemyState.Idle:
                    spriteRenderer.color = Color.gray;
                    break;
                case EnemyState.Chase:
                    spriteRenderer.color = Color.yellow;
                    break;
                case EnemyState.Attack:
                    spriteRenderer.color = Color.red;
                    break;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseDistance * 1.2f);

            if (target != null)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(transform.position, target.position);
            }
        }

/*        bool IsTargetInsideViewCone(Vector2 enemyPosition, Vector2 forward, Vector2 targetPosition, float viewDistance, float viewAngle)
        {
            Vector2 toTarget = targetPosition - enemyPosition;
            float distance = toTarget.magnitude;

            if (distance > viewDistance)
                return false;

            if (distance == 0f)
                return true;

            forward.Normalize();
            toTarget.Normalize();

        }*/

    }
}
