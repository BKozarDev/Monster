using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using Panda;

[RequireComponent(typeof(NavMeshAgent),typeof(FieldOfView))]
public class PenguinBehaviour : MonoBehaviour
{
    private static string playerTag = "Player";

    [SerializeField]
    private float playerNearnessTreshold = 1f;

    [SerializeField]
    private Transform guardPositionTransform;
    private Transform penguinTransform;
    private Transform playerTransform;
    private Player_Controller playerController;

    private NavMeshAgent agent;
    private FieldOfView fov;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();

        penguinTransform = transform;
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        playerController = playerTransform.GetComponent<Player_Controller>();
    }

    [Task]
    public bool IsOnGuardPosition { get
        {
            var m = (guardPositionTransform.position - penguinTransform.position).magnitude;
            var b = ( m< 1f
 //               && (guardPositionTransform.eulerAngles - penguinTransform.eulerAngles).magnitude < 1f
    );
            return b;
        }
    }

        [Task]
    public bool IsPlayerVisible
    {
        get
        {
            return fov.visibleTargets.Contains(playerTransform);
        }
    }

    [Task]
    public bool IsPlayerHasEgg
    {
        get
        {
            return playerController.isHunted;
        }
    }

    [Task]
    public bool IsPlayerNear
    {
        get
        {
            var m = (playerTransform.position - penguinTransform.position).magnitude;
            Debug.Log(m);
            return m < playerNearnessTreshold;
        }
    }

    [Task]
    public void MoveToNextPoint()
    {

    }

    [Task]
    public void GoToLastPlayersPosition()
    {
        agent.SetDestination(playerTransform.position);
    }

    [Task]
    public void Attack()
    {
        Debug.Log("attacked");
    }


    [Task]
    public void GoToPosition()
    {
        agent.SetDestination(guardPositionTransform.position);
        StartCoroutine(RotateIfStoped());
        Task.current.Fail();
    }

    IEnumerator RotateIfStoped()
    {
        if (agent.velocity.magnitude <= 0.1f)
        {
            if ((transform.eulerAngles - guardPositionTransform.eulerAngles).magnitude > 0.1f)
            {
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, guardPositionTransform.eulerAngles, Time.deltaTime * 10f);
                yield return null;
            }
        } else
        {
            yield return null;
        }

    }
}
