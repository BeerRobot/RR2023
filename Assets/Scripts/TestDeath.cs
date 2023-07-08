using UnityEngine;


// Kill the player
public class TestDeath : MonoBehaviour
{
    // On Trigger Enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player Model"))
        {
            if (!PlayerDeathController.GetDeathState())
            {
                PlayerDeathController.Die();
            }
        }
    }

}
