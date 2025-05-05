using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Linq;

public class JumpAgent : Agent
{
    public float jumpForce = 5f;
    public Rigidbody agentRb;
    private bool isGrounded;
    public ObstacleSpawner obstacleSpawner;
    private int obstaclesJumped = 0;
    public GameObject obstacle;
    public Vector3 relativePosition;

    public override void Initialize()
    {
        agentRb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        obstaclesJumped = 0;
        //this.transform.localPosition = new Vector3(0, 0.5f, 0); this.transform.localRotation = Quaternion.identity;
        //agentRb.linearVelocity = Vector3.zero;

        obstacleSpawner.ResetObstacle();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(isGrounded);
        sensor.AddObservation(obstacle.GetComponent<Obstacle>().relativePosition);
        sensor.AddObservation(obstacle.GetComponent<Obstacle>().GetSpeed());
        sensor.AddObservation(obstacle.CompareTag("Reward"));
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];
        if (action == 1 && isGrounded)
        {
            agentRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            AddReward(-0.01f); // Slight penalty for jumping to discourage unnecessary jumps
        }
        if (isGrounded)
        {
            AddReward(0.005f); // Reward for being grounded
        }
        AddReward(0.01f); // Survival bonus
    }

    public void GiveReward() // called when successfully jumped over obstacle
    {
        AddReward(1f);
        obstacleSpawner.ResetObstacle();
        obstaclesJumped++;
        if (obstaclesJumped >= 3)
        {
            AddReward(0.5f);
            EndEpisode();
        }
    }

    public void TakeReward() // called when successfully jumped over reward
    {
        AddReward(-0.5f);
        obstacleSpawner.ResetObstacle();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            AddReward(-1.0f); // Negative reward for hitting an obstacle
            EndEpisode();
        }
        else if (other.CompareTag("Reward"))
        {
            AddReward(0.5f);
            obstacleSpawner.ResetObstacle();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        
        var discreteActions = actionsOut.DiscreteActions;

        discreteActions[0] = Input.GetAxis("Jump") == 0 ? 0 : 1;
    }
}
