using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonSettControler : MonoBehaviour
{
    /*
    [SerializeField] public TextMeshProUGUI price;
    [SerializeField] public TextMeshProUGUI titelName;
    [SerializeField] public TextMeshProUGUI increas;
    [SerializeField] public TextMeshProUGUI curentIncreas;

    public void AsighButton(int price,string name, int increas, int upgradeCounter, string symbol){
        price.SetText((price).ToString);
        titelName.SetText(name);
        increas.SetText(increas.ToString() + symbol);
        curentIncreas.SetText((upgradeCounter*increas).ToString()+ symbol);
    }
    */






    /*[System.Serializable]
public class UpgradButtons{
    [SerializeField]public GameObject buttonsPrefab;
    public int basePrice;
    public string upgradName;
    public string asigningSymbol;
    public int statIncreas; 
    private int upgradeCounter;

    public UpgradButtons() {
        SetButtonStats();
        
    }
    public void SetButtonStats(){
        buttonsPrefab.GetComponent<ButtonSettControler>().AsighButton(GetCurentPrice(), upgradName, statIncreas, upgradeCounter, asigningSymbol);
    }

    public int ResetUpgrdes(){
        private int mony = 0;
        for (int i = 1; i < upgradeCounter; i++)
        {
            mony += basePrice + (i * basePrice);
        }
        upgradeCounter = 0;
        SetButtonStats();
        return mony;
    }

    public void ByUpgrade(){
        if(EnothMoney(GetCurentPrice())){
            upgradeCounter++;
            SetButtonStats();
        }
    }

    public int GetCurentPrice(){

        return basePrice + (basePrice * upgradeCounter );
    }

    public float GetStats(){
        return statIncreas * upgradeCounter;
    }
    


}
public class UpgradeController : MonoBehaviour
{   
    [SerializeField] private List<UpgradButtons> upgradList = new List<UpgradButtons>();
    [SerializeField] private TextMeshProUGUI MoneyText1;
    [SerializeField] private TextMeshProUGUI MoneyText2;
    
    public int money;
    private void Start()
    {
       updateMony();
    }

    

    public void ResetUppgrades(){
        foreach ( UpgradButtons buttons in upgradList){
            money += buttons.ResetUpgrdes();
        }
        updateMony();
    }
    void updateMony(){
        MoneyText1.SetText(money.ToString());
        MoneyText2.SetText(money.ToString());
    }
    
    public bool EnothMoney(int price)
    {
        if(money >= price){
            money -= price;
        updateMony();
            return true;
        }
        return false;
    }

    
    


    

}
*/



/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[System.Serializable]
public struct UpgradStats
{
    public int baseHealth, money;
    [Range(-1, 10)] public float damage, projectileSpeed, healthMultiplier;
    [Range(-1, 5)] public float movementSpeed, areaOfEffectSize, duration, moneyMultiplier, xpMultiplier;
    [Range(-1, 10)] public int defence, regeneration;
    [Range(-1, 5)] public int pierce, burstAmount;

    public static UpgradStats operator +(UpgradStats one, UpgradStats two)
    {
        one.baseHealth += two.baseHealth;
        one.money += two.money;
        one.damage += two.damage;
        one.projectileSpeed += two.projectileSpeed;
        one.healthMultiplier += two.healthMultiplier;
        one.movementSpeed += two.movementSpeed;
        one.areaOfEffectSize += two.areaOfEffectSize;
        one.duration += two.duration;
        one.moneyMultiplier += two.moneyMultiplier;
        one.xpMultiplier += two.xpMultiplier;
        one.defence += two.defence;
        one.regeneration += two.regeneration;
        one.pierce += two.pierce;
        one.burstAmount += two.burstAmount;
        return one;
    }


}
public class UpgradeController : MonoBehaviour
{
    [SerializeField] public UpgradStats upgradStats;
    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI MoneyText1;
    [SerializeField] private TextMeshProUGUI MoneyText2;
    
    [SerializeField] private TextMeshProUGUI DamageCost;
    [SerializeField] private TextMeshProUGUI DamageStats;

    [SerializeField] private TextMeshProUGUI ProjectileSpeedCost;
    [SerializeField] private TextMeshProUGUI ProjectileSpeedStats;

    [SerializeField] private TextMeshProUGUI SizeCost;
    [SerializeField] private TextMeshProUGUI SizeStats;

    [SerializeField] private TextMeshProUGUI PierceCost;
    [SerializeField] private TextMeshProUGUI PierceStats;

    [SerializeField] private TextMeshProUGUI XpMultCost;
    [SerializeField] private TextMeshProUGUI XpMultStats;

    [SerializeField] private TextMeshProUGUI HealthMultiplierCost;
    [SerializeField] private TextMeshProUGUI HealthMultiplierStats;

    [SerializeField] private TextMeshProUGUI DefenceCost;
    [SerializeField] private TextMeshProUGUI DefenceStats;

    [SerializeField] private TextMeshProUGUI MovementSpeedCost;
    [SerializeField] private TextMeshProUGUI MovementSpeedStats;

    [SerializeField] private TextMeshProUGUI DurationCost;
    [SerializeField] private TextMeshProUGUI DurationStats;

    [SerializeField] private TextMeshProUGUI MoneyMultCost;
    [SerializeField] private TextMeshProUGUI MoneyMultStats;

    [SerializeField] private TextMeshProUGUI RegenerationCost;
    [SerializeField] private TextMeshProUGUI RegenerationStats;

    [SerializeField] private TextMeshProUGUI BurstAmountCost;
    [SerializeField] private TextMeshProUGUI BurstAmountStats;

    private List<int> upgrades = new List<int>();

    public int perLevelPriceIncrease;
    private int levelDamage;
        public float Damage;
    private int levelProjectileSpeed;
    private int levelSize;
    private int levelPrice;
    private int levelXpMultiplier;
    private int levelHP;
    private int levelDefence;
    private int levelMoveSpeed;
    private int levelInvincibilityFrames;
    private int levelMoneyMult;
        public int Regeneration;
    private int levelRegeneration;
        public int Regeneration;
    private int levelBurstAmount;
        public int BurstAmount; // how much things het increst by
    

     private void Start()
    {   upgradStats =  //the privius JsonUtility save
        InitialisingPanel();
    }

    
    public void InitialisingPanel(){
        MoneyText1.SetText(money.ToString());
        MoneyText2.SetText(money.ToString());

        DamageCost.SetText(((levelDamage + 1) * perLevelPriceIncrease).ToString());
        DamageStats.SetText(levelDamage * 5 + "%");

        ProjectileSpeedCost.SetText(((levelProjectileSpeed + 1) * perLevelPriceIncrease).ToString());
        ProjectileSpeedStats.SetText(levelProjectileSpeed * 5 + "%");

        SizeCost.SetText(((levelSize + 1) * perLevelPriceIncrease).ToString());
        SizeStats.SetText(levelSize * 10 + "%");

        
        PierceCost.SetText(((levelPrice + 1) * perLevelPriceIncrease).ToString());
        PierceStats.SetText(levelPrice + "");

        XpMultCost.SetText(((levelXpMultiplier + 1) * perLevelPriceIncrease).ToString());
        XpMultStats.SetText(levelXpMultiplier * 2.5 + "%");

        HealthMultiplierCost.SetText(((levelHP + 1) * perLevelPriceIncrease).ToString());
        HealthMultiplierStats.SetText(levelHP * 10 + "%");

        DefenceCost.SetText(((levelDefence + 1) * perLevelPriceIncrease).ToString());
        DefenceStats.SetText(levelDefence * 5 + "");

        MovementSpeedCost.SetText(((levelMoveSpeed + 1) * perLevelPriceIncrease).ToString());
        MovementSpeedStats.SetText(levelMoveSpeed * 0.1 + "");

        DurationCost.SetText(((levelInvincibilityFrames + 1) * perLevelPriceIncrease).ToString());
        DurationStats.SetText(levelInvincibilityFrames * 0.1 + "s");

        MoneyMultCost.SetText(((levelMoneyMult + 1) * perLevelPriceIncrease).ToString());
        MoneyMultStats.SetText(levelMoneyMult * 2.5 + "%");

        RegenerationCost.SetText(((levelRegeneration + 1) * perLevelPriceIncrease).ToString());
        RegenerationStats.SetText(levelRegeneration * 2.5 + "%");

        BurstAmountCost.SetText(((levelBurstAmount + 1) * perLevelPriceIncrease).ToString());
        BurstAmountStats.SetText(levelBurstAmount * BurstAmount + "%");

        
    }

    public void ResetUppgrades(){
        upgrades.AddRange(new int[] { levelDamage, levelProjectileSpeed, levelSize, levelPrice, levelXpMultiplier, levelHP, levelDefence, levelMoveSpeed, levelInvincibilityFrames, levelMoneyMult });
        foreach (int level in upgrades){

            for (int i = 1; i <= level; ++i)
            {
                money += i * perLevelPriceIncrease; 
            }
        }
        upgrades.Clear();
        upgradStats = new UpgradStats;
        InitialisingPanel();
    }
    public void ByDameg()
    {
        if(EnothMoney((levelDamage + 1) * perLevelPriceIncrease)){
            levelDamage += 1;
            upgradStats.damage += (Damage / 100);  
            DamageCost.SetText(((levelDamage + 1) * perLevelPriceIncrease).ToString());
            DamageStats.SetText(levelDamage * 5 + "%");
        }
    }

    public void ByProjectileSpeed()
    {
        if(EnothMoney((levelProjectileSpeed + 1) * perLevelPriceIncrease)){
            levelProjectileSpeed += 1;
            ProjectileSpeedCost.SetText(((levelProjectileSpeed + 1) * perLevelPriceIncrease).ToString());
            ProjectileSpeedStats.SetText(levelProjectileSpeed * 5 + "%");
        }
    }

    public void BySize()
    {
        if(EnothMoney((levelSize + 1) * perLevelPriceIncrease)){
            levelSize += 1;
            SizeCost.SetText(((levelSize + 1) * perLevelPriceIncrease).ToString());
            SizeStats.SetText(levelSize * 10 + "%");
        }
    }

    public void ByPirce()
    {
        if(EnothMoney((levelPrice + 1) * perLevelPriceIncrease)){
            levelPrice += 1;
            PierceCost.SetText(((levelPrice + 1) * perLevelPriceIncrease).ToString());
            PierceStats.SetText(levelPrice + "");
        }
    }

    public void ByXpMultiplayer()
    {
        if(EnothMoney((levelXpMultiplier + 1) * perLevelPriceIncrease)){
            levelXpMultiplier += 1;
            XpMultCost.SetText(((levelXpMultiplier + 1) * perLevelPriceIncrease).ToString());
            XpMultStats.SetText(levelXpMultiplier * 2.5 + "%");
        }
    }

    public void ByHP()
    {
        if(EnothMoney((levelHP + 1) * perLevelPriceIncrease)){
            levelHP += 1;
            HealthMultiplierCost.SetText(((levelHP + 1) * perLevelPriceIncrease).ToString());
            HealthMultiplierStats.SetText(levelHP * 10 + "%");
            
        }
    }

    public void ByDefence()
    {
        if(EnothMoney((levelDefence + 1)  * perLevelPriceIncrease)){
            levelDefence += 1;
            DefenceCost.SetText(((levelDefence + 1) * perLevelPriceIncrease).ToString());
            DefenceStats.SetText(levelDefence * 5 + "");
        }
    }

    public void ByMovSpeed()
    {
        if(EnothMoney((levelMoveSpeed + 1) * perLevelPriceIncrease)){
            levelMoveSpeed += 1;
            MovementSpeedCost.SetText(((levelMoveSpeed + 1) * perLevelPriceIncrease).ToString());
            MovementSpeedStats.SetText(levelMoveSpeed * 0.1 + "");
        }
    }

    public void ByDuration()
    {
       if(EnothMoney((levelInvincibilityFrames + 1) * perLevelPriceIncrease)){
            levelInvincibilityFrames += 1;
            DurationCost.SetText(((levelInvincibilityFrames + 1) * perLevelPriceIncrease).ToString());
            DurationStats.SetText(levelInvincibilityFrames * 0.1 + "s");
        }
    }

    public void ByMoneyMult()
    {
       if(EnothMoney((levelMoneyMult + 1) * perLevelPriceIncrease)){
            levelMoneyMult += 1;
            MoneyMultCost.SetText(((levelMoneyMult + 1) * perLevelPriceIncrease).ToString());
            MoneyMultStats.SetText(levelMoneyMult * 2.5 + "%");
        }
    }

    public bool EnothMoney(int price)
    {
        if(money >= price){
            money -= price;
            MoneyText1.SetText(money.ToString());
            MoneyText2.SetText(money.ToString());
            return true;
        }
        return false;
    }

    

    

     
    



    

    

}
*/
}
