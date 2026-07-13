using Cloth;

public class ClothItemJump : ClothItemBase
{
    public float jumpIncreaseAmount = 50f;

    public override void Collect()
    {
        base.Collect();
        Player.Instance.SetJump(jumpIncreaseAmount, duration);
    }
}