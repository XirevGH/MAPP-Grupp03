using System.Reflection;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private UpgradeAbility upgradeAbility;
    private MethodInfo methodToBeCalled;
    private Item item;
    private string upgrade;

    public void PrepareMethodToExecute(string method, Item item, string upgrade)
    { 
        this.item = item;
        this.upgrade = upgrade;
        methodToBeCalled = typeof(UpgradeAbility).GetMethod(method);
    }
    public void PrepareMethodToExecute(string method, Item item)
    {
        this.item = item;
        methodToBeCalled = typeof(UpgradeAbility).GetMethod(method);
    }
    public void ExecuteMethod()
    {
        if (methodToBeCalled.Name == "PerformRandomizedUpgrade") 
        { 
            methodToBeCalled.Invoke(upgradeAbility, new object[] {item, upgrade});
        }
        else
        {
            methodToBeCalled.Invoke(upgradeAbility, new object[] {item});
        }
    }
}
