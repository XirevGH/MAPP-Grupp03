using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UppgradeControler : MonoBehaviour
{
    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI MonyText1;
    [SerializeField] private TextMeshProUGUI MonyText2;
    
    [SerializeField] private TextMeshProUGUI DamegCost;
    [SerializeField] private TextMeshProUGUI DamegStats;

    [SerializeField] private TextMeshProUGUI BPMCost;
    [SerializeField] private TextMeshProUGUI BPMStats;

    [SerializeField] private TextMeshProUGUI SizeCost;
    [SerializeField] private TextMeshProUGUI SizeStats;

    [SerializeField] private TextMeshProUGUI PirceCost;
    [SerializeField] private TextMeshProUGUI PirceStats;

    [SerializeField] private TextMeshProUGUI XpMultCost;
    [SerializeField] private TextMeshProUGUI XpMultStats;

    [SerializeField] private TextMeshProUGUI HPCost;
    [SerializeField] private TextMeshProUGUI HPStats;

    [SerializeField] private TextMeshProUGUI DefenceCost;
    [SerializeField] private TextMeshProUGUI DefenceStats;

    [SerializeField] private TextMeshProUGUI MovSpeedCost;
    [SerializeField] private TextMeshProUGUI MovSpeedStats;

    [SerializeField] private TextMeshProUGUI InvinsebliletyCost;
    [SerializeField] private TextMeshProUGUI InvinsebliletyStats;

    [SerializeField] private TextMeshProUGUI MoneyMultCost;
    [SerializeField] private TextMeshProUGUI MoneyMultStats;

    private List<int> upgrades = new List<int>();

    public int perLevelPriceIncrease;
    private int levelDamage;
    private int levelBPM;
    private int levelSize;
    private int levelPrice;
     private int levelXpMultiplier;
    private int levelHP;
    private int levelDefence;
    private int levelMoveSpeed;
    private int levelInvincibilityFrames;
    private int levelMoneyMult;
    

     private void Start()
    {
        InitalisingPannel();
    }

    public void InitalisingPannel(){
    
    
    levelDamage = 0;
    levelBPM = 0;
    levelSize = 0;
    levelPrice = 0;
    levelXpMultiplier = 0;
    levelHP = 0;
    levelDefence = 0;
    levelMoveSpeed = 0;
    levelInvincibilityFrames = 0;
    levelMoneyMult = 0;
    


        MonyText1.SetText(money.ToString());
        MonyText2.SetText(money.ToString());

        DamegCost.SetText(((levelDamage + 1) * perLevelPriceIncrease).ToString());
        DamegStats.SetText(levelDamage * 5 + "%");

        BPMCost.SetText(((levelBPM + 1) * perLevelPriceIncrease).ToString());
        BPMStats.SetText(levelBPM * 5 + "%");

        SizeCost.SetText(((levelSize + 1) * perLevelPriceIncrease).ToString());
        SizeStats.SetText(levelSize * 10 + "%");

        
        PirceCost.SetText(((levelPrice + 1) * perLevelPriceIncrease).ToString());
        PirceStats.SetText(levelPrice + "");

        XpMultCost.SetText(((levelXpMultiplier + 1) * perLevelPriceIncrease).ToString());
        XpMultStats.SetText(levelXpMultiplier * 2.5 + "%");

        HPCost.SetText(((levelHP + 1) * perLevelPriceIncrease).ToString());
        HPStats.SetText(levelHP * 10 + "%");

        DefenceCost.SetText(((levelDefence + 1) * perLevelPriceIncrease).ToString());
        DefenceStats.SetText(levelDefence * 5 + "");

        MovSpeedCost.SetText(((levelMoveSpeed + 1) * perLevelPriceIncrease).ToString());
        MovSpeedStats.SetText(levelMoveSpeed * 0.1 + "");

        InvinsebliletyCost.SetText(((levelInvincibilityFrames + 1) * perLevelPriceIncrease).ToString());
        InvinsebliletyStats.SetText(levelInvincibilityFrames * 0.1 + "s");

        MoneyMultCost.SetText(((levelMoneyMult + 1) * perLevelPriceIncrease).ToString());
        MoneyMultStats.SetText(levelMoneyMult * 2.5 + "%");

        
    }

    public void ResetUppgrades(){
        upgrades.AddRange(new int[] { levelDamage, levelBPM, levelSize, levelPrice, levelXpMultiplier, levelHP, levelDefence, levelMoveSpeed, levelInvincibilityFrames, levelMoneyMult });
        foreach (int level in upgrades){

            for (int i = 1; i <= level; ++i)
            {
                money += i * perLevelPriceIncrease; // Add the cost of each level to 'mony'
            }
        }
        upgrades.Clear();
        
        InitalisingPannel();
    }
    public void ByDameg()
    {
        if(EnothMoney((levelDamage + 1) * perLevelPriceIncrease)){
            levelDamage += 1;
            DamegCost.SetText(((levelDamage + 1) * perLevelPriceIncrease).ToString());
            DamegStats.SetText(levelDamage * 5 + "%");
        }
    }

    public void ByBPM()
    {
        if(EnothMoney((levelBPM + 1) * perLevelPriceIncrease)){
            levelBPM += 1;
            BPMCost.SetText(((levelBPM + 1) * perLevelPriceIncrease).ToString());
            BPMStats.SetText(levelBPM * 5 + "%");
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
            PirceCost.SetText(((levelPrice + 1) * perLevelPriceIncrease).ToString());
            PirceStats.SetText(levelPrice + "");
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
            HPCost.SetText(((levelHP + 1) * perLevelPriceIncrease).ToString());
            HPStats.SetText(levelHP * 10 + "%");
            
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
            MovSpeedCost.SetText(((levelMoveSpeed + 1) * perLevelPriceIncrease).ToString());
            MovSpeedStats.SetText(levelMoveSpeed * 0.1 + "");
        }
    }

    public void ByInvinsebiletyFrames()
    {
       if(EnothMoney((levelInvincibilityFrames + 1) * perLevelPriceIncrease)){
            levelInvincibilityFrames += 1;
            InvinsebliletyCost.SetText(((levelInvincibilityFrames + 1) * perLevelPriceIncrease).ToString());
            InvinsebliletyStats.SetText(levelInvincibilityFrames * 0.1 + "s");
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
            MonyText1.SetText(money.ToString());
            MonyText2.SetText(money.ToString());
            return true;
        }
        return false;
    }

    

    

     
    



    

    

}
