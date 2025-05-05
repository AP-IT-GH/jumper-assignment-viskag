using UnityEngine;

public class SuccessZone : MonoBehaviour
{
    public JumpAgent jumpAgent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            jumpAgent.GiveReward();
        }
        else if (other.CompareTag("Reward"))
        {
            jumpAgent.TakeReward();
        }
    }
}
