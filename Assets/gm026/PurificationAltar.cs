using UnityEngine;

public class PurificationAltar : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemType requiredCrystal;
    [SerializeField] private bool consumeCrystal = true;

    private SpriteRenderer spriteRenderer;
    private bool purified;

    public string InteractionText => purified ? "정화된 제단" : "제단 정화";

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Configure(ItemType crystal, bool consume)
    {
        requiredCrystal = crystal;
        consumeCrystal = consume;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Interact(PlayerInteractor player)
    {
        if (purified)
        {
            Debug.Log("이미 정화된 제단입니다.");
            return;
        }

        if (player.Inventory.Count(requiredCrystal) <= 0)
        {
            Debug.Log(ItemTypeName.Get(requiredCrystal) + "이 필요합니다!");
            return;
        }

        if (consumeCrystal)
            player.Inventory.TryUse(requiredCrystal);

        purified = true;
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(1f, 0.9f, 0.35f);

        Debug.Log("제단 정화 성공! 필요한 결정: " + ItemTypeName.Get(requiredCrystal));
    }
}
