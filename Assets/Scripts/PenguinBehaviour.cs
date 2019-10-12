using UnityEngine.AI;
using UnityEngine;
using Panda;

[RequireComponent(typeof(NavMeshAgent))]
public class PenguinBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform guardPositionTransform;
    private Transform penguinTransform;
    private Transform playerTransform;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        penguinTransform = transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    [Task]
    public bool IsOnGuardPosition { get
        { return ((guardPositionTransform.position - penguinTransform.position).magnitude < 1f && 
                (guardPositionTransform.eulerAngles - penguinTransform.eulerAngles).magnitude < 1f); }
    }

    [Task]
    public bool IsPlayerVisible
    {
        get
        {
            return true;
        }
    }

    [Task]
    public bool IsPlayerHasEgg
    {
        get
        {
            return true;
        }
    }

    [Task]
    public bool IsPlayerNear
    {
        get
        {
            return false;
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
        Debug.Log("attacked");
    }
}
