namespace GM027
{
    public interface IInteractable
    {
        string InteractionText { get; }
        void Interact(PlayerController player);
    }
}
