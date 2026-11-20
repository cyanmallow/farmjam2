using UnityEngine;

public class IBed : MonoBehaviour, IInteractable
{
    public void OnClickAction()
    {
        PlayerMovement.Instance.WalkTo(transform);
        // slowly change lighting, play animation
        StatEvent.Instance.Sleep(8); // Example: Sleep for 8 hours
    }
}
