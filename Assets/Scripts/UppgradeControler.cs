using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UppgradeControler : MonoBehaviour
{
    [SerializeField] private int mony;
    [SerializeField] private TextMeshProUGUI MonyText;
    
    [SerializeField] private TextMeshProUGUI DamegCost;
    [SerializeField] private TextMeshProUGUI DamegStats;

    [SerializeField] private TextMeshProUGUI BPMCost;
    [SerializeField] private TextMeshProUGUI BPMStats;

    [SerializeField] private TextMeshProUGUI SizeCost;
    [SerializeField] private TextMeshProUGUI SizeStats;

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

    public int perLevelPricIncreas;
    private int levelDamag;
    private int levelBPM;
    private int levelSize;
     private int levelXpMultilapyer;
    private int levelHP;
    private int levelDefece;
    private int levelMoveSpeed;
    private int levelInvinsebiletyFrames;
    

    private void Start()
    {
    
    levelDamag = 0;
    levelBPM = 0;
    levelSize = 0;
    levelXpMultilapyer = 0;
    levelHP = 0;
    levelDefece = 0;
    levelMoveSpeed = 0;
    levelInvinsebiletyFrames = 0;


        MonyText.SetText(mony.ToString());

        DamegCost.SetText(((levelDamag + 1) * perLevelPricIncreas).ToString());
        DamegStats.SetText(levelDamag * 5 + "%");

        BPMCost.SetText(((levelBPM + 1) * perLevelPricIncreas).ToString());
        BPMStats.SetText(levelBPM * 5 + "%");

        SizeCost.SetText(((levelSize + 1) * perLevelPricIncreas).ToString());
        SizeStats.SetText(levelSize * 10 + "%");

        XpMultCost.SetText(((levelXpMultilapyer + 1) * perLevelPricIncreas).ToString());
        XpMultStats.SetText(levelXpMultilapyer * 2.5 + "%");

        HPCost.SetText(((levelHP + 1) * perLevelPricIncreas).ToString());
        HPStats.SetText(levelHP * 10 + "%");

        DefenceCost.SetText(((levelDefece + 1) * perLevelPricIncreas).ToString());
        DefenceStats.SetText(levelDefece * 5 + "");

        MovSpeedCost.SetText(((levelMoveSpeed + 1) * perLevelPricIncreas).ToString());
        MovSpeedStats.SetText(levelMoveSpeed * 0.1 + "");

        InvinsebliletyCost.SetText(((levelInvinsebiletyFrames + 1) * perLevelPricIncreas).ToString());
        InvinsebliletyStats.SetText(levelInvinsebiletyFrames * 0.1 + "s");

        
    }
    public void ByDameg()
    {
        if(EnothMoney((levelDamag + 1) * perLevelPricIncreas)){
            levelDamag += 1;
            DamegCost.SetText(((levelDamag + 1) * perLevelPricIncreas).ToString());
            DamegStats.SetText(levelDamag * 5 + "%");
        }
    }

    public void ByBPM()
    {
        if(EnothMoney((levelBPM + 1) * perLevelPricIncreas)){
            levelBPM += 1;
            BPMCost.SetText(((levelBPM + 1) * perLevelPricIncreas).ToString());
            BPMStats.SetText(levelBPM * 5 + "%");
        }
    }

    public void BySize()
    {
        if(EnothMoney((levelSize + 1) * perLevelPricIncreas)){
            levelSize += 1;
            SizeCost.SetText(((levelSize + 1) * perLevelPricIncreas).ToString());
            SizeStats.SetText(levelSize * 10 + "%");
        }
    }

    public void ByXpMultiplayer()
    {
        if(EnothMoney((levelXpMultilapyer + 1) * perLevelPricIncreas)){
            levelXpMultilapyer += 1;
            XpMultCost.SetText(((levelXpMultilapyer + 1) * perLevelPricIncreas).ToString());
            XpMultStats.SetText(levelXpMultilapyer * 2.5 + "%");
        }
    }

    public void ByHP()
    {
        if(EnothMoney((levelHP + 1) * perLevelPricIncreas)){
            levelHP += 1;
            HPCost.SetText(((levelHP + 1) * perLevelPricIncreas).ToString());
            HPStats.SetText(levelHP * 10 + "%");
            
        }
    }

    public void ByDefence()
    {
        if(EnothMoney((levelDefece + 1)  * perLevelPricIncreas)){
            levelDefece += 1;
            DefenceCost.SetText(((levelDefece + 1) * perLevelPricIncreas).ToString());
            DefenceStats.SetText(levelDefece * 5 + "");
        }
    }

    public void ByMovSpeed()
    {
        if(EnothMoney((levelMoveSpeed + 1) * perLevelPricIncreas)){
            levelMoveSpeed += 1;
            MovSpeedCost.SetText(((levelMoveSpeed + 1) * perLevelPricIncreas).ToString());
            MovSpeedStats.SetText(levelMoveSpeed * 0.1 + "");
        }
    }

    public void ByInvinsebiletyFrames()
    {
       if(EnothMoney((levelInvinsebiletyFrames + 1) * perLevelPricIncreas)){
            levelInvinsebiletyFrames += 1;
            InvinsebliletyCost.SetText(((levelInvinsebiletyFrames + 1) * perLevelPricIncreas).ToString());
            InvinsebliletyStats.SetText(levelInvinsebiletyFrames * 0.1 + "s");
        }
    }

    public bool EnothMoney(int price)
    {
        if(mony >= price){
            mony -= price;
            MonyText.SetText(mony.ToString());
            return true;
        }
        return false;
    }

    

    

     
    



    

    

}
