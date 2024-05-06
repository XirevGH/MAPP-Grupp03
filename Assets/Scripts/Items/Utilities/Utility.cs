public abstract class Utility : Item
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override string GetItemType()
    {
        return "Utility";
    }
}
