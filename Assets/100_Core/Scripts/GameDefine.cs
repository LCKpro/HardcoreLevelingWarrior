public static class GameDefine
{
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
