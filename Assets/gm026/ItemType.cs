public enum ItemType
{
    HealingHerb,
    RedCrystal,
    BlueCrystal,
    GreenCrystal
}

public static class ItemTypeName
{
    public static string Get(ItemType type)
    {
        switch (type)
        {
            case ItemType.HealingHerb: return "회복 허브";
            case ItemType.RedCrystal: return "붉은 결정";
            case ItemType.BlueCrystal: return "푸른 결정";
            case ItemType.GreenCrystal: return "초록 결정";
            default: return type.ToString();
        }
    }
}
