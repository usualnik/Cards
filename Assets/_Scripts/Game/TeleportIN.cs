using UnityEngine;

public class TeleportIN : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CardMovement cardMovement))
        {
            cardMovement.SetSplineContainer(Conveyor.Instance.GetTeleportSplineContainer());
        }
    }
}
