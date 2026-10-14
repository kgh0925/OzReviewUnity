using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float interactionRange = 1.5f;

    public PlayerInventory Inventory { get; private set; }

    private void Awake()
    {
        Inventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E))
            return;

        IInteractable nearest = FindNearest();
        if (nearest != null)
            nearest.Interact(this);
        else
            Debug.Log("상호작용할 제단이 가까이에 없습니다.");
    }

    private IInteractable FindNearest()
    {
        IInteractable nearest = null;
        float nearestDistance = float.MaxValue;

        MonoBehaviour[] behaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (!(behaviour is IInteractable candidate))
                continue;

            float distance = ((Vector2)behaviour.transform.position - (Vector2)transform.position).sqrMagnitude;
            if (distance <= interactionRange * interactionRange && distance < nearestDistance)
            {
                nearest = candidate;
                nearestDistance = distance;
            }
        }

        return nearest;
    }
}
