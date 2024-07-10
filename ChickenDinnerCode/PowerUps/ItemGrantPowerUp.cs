
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;


[CreateAssetMenu(menuName = "PowerUps/ItemGrant")]
public class ItemGrantPowerUp : PowerUpBase
{
    public ItemBase.ItemType itemType;
    public float itemCount;
    private GameObject itemObject;

    public override void Apply(GameObject target,GameObject sendObject)
    {
        itemObject=sendObject;
        IA_Claimable claim = target.GetComponent<IA_Claimable>();
        claim.Claim(itemCount,itemType);
        itemObject.SetActive(false);
       
        
    }

    protected override IEnumerator PowerUpWithDuration()
    {
        return null;
    }

}
