using UnityEngine;

public class XPDrop : Pickup
{   [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Material biggXpDropMaterial; 
    [SerializeField] private Material mediumXpDropMaterial; 
    [SerializeField] private Material smallXpDropMaterial; 
    [SerializeField] private SpriteRenderer circle;
    [SerializeField] private Color originalBlueColor;
    public int biggMaxXpValue;
    public int mediumMaxXpValue;
    public int smallMaxXpValue;
    [SerializeField] private int XP;
    private ParticleSystemRenderer particleSystemRenderer;

    private void Awake()
    {
        particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        SetXpType();
    }
    public XPDrop Initialize(int initialXP)
    {   move = false;
        XP = initialXP;

        SetXpType();
        return this;  
    }

    public new void MoveToPlayer()
    {   
        move = true;
    }
    public void AddXP(int xpToAdd)
    {
        XP += xpToAdd;
        SetXpType();
    }
    public int GetXpValue()
    {
        return XP;
    }
    

    public void SetXpType()
    {
        if (XP <= smallMaxXpValue) {
            SetXpToSmallXp();
        } else if (XP <= mediumMaxXpValue) {
            SetXpToMediumXp();
        } else if (XP <= biggMaxXpValue) {
            SetXpToBiggXp();
        } else {
            Debug.LogWarning("XPDrop was too big.");
        }
    }
    
    public void SetXpToBiggXp(){
        XPDropPool.Instance.SetAsBiggXp(this);
         particleSystemRenderer.material = biggXpDropMaterial;
        particleSystemRenderer.trailMaterial = biggXpDropMaterial;
        circle.material = biggXpDropMaterial;
        circle.color = Color.white;
    }
    public void SetXpToMediumXp(){
        XPDropPool.Instance.SetAsMediumXp(this);
        particleSystemRenderer.material = mediumXpDropMaterial;
        particleSystemRenderer.trailMaterial = mediumXpDropMaterial;
        circle.material = mediumXpDropMaterial;
        circle.color = Color.white;
    }

    public void SetXpToSmallXp(){
        XPDropPool.Instance.SetAsSmallXp(this); 
        particleSystemRenderer.material = smallXpDropMaterial;
        particleSystemRenderer.trailMaterial = smallXpDropMaterial;
        circle.material = smallXpDropMaterial;
        circle.color = originalBlueColor;
    }
    public int NeededXpForNextLevel()
    {
        //if (XP < smallMaxXpValue) return smallMaxXpValue - XP;
        if (XP < mediumMaxXpValue) return mediumMaxXpValue - XP;
        if (XP < biggMaxXpValue) return biggMaxXpValue - XP;
        return 0;  
    }

    
    
    protected override void ResetThis()
    {   XPDropPool.Instance.RemoveFromActiveList(this);
        XP = 0;
    }
    protected override void IndividualPickupAction(){
        SoundManager.Instance.PlayExpSFX(pickupSound, 1);
        player.GetComponent<Player>().AddXP(XP);

    }

    
}