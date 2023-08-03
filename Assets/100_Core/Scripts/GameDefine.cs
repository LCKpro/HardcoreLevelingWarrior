public static class GameDefine
{
    public enum SelectHeroSortType
    {
        None = 0,
        Tanker = 1,         // 탱커
        Dealer_Melee = 2,   // 근거리 딜러
        Dealer_Range = 3,   // 원거리 딜러
        Supporter = 4,  // 지원가
        Healer = 5,     // 힐러
    }

    public enum ItemType
    {
        Weapon = 0,
        Armor = 1,
    }

    // 전용 무기인지 체크.
    // ex. 소라 전용 무기면 Sora, 비전용 무기면 None
    public enum ItemOwner
    {
        None = 0,
        Sora = 1,

    }

    public enum ItemAbility
    {
        Sora_0 = 0,

        None = 99,
    }

}
