using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedBoost")]
public class SpeedPowerUp : PowerUpBase
{
    public float speedMultiplier;
    private GameObject itemObject;
    private float originalSpeed;
    private PlayerMovement player;
    private bool isActiveted=false;
    private float durationTimer;

    private void OnEnable()
    {
        isActiveted=false;
    } 
    
    public override void Apply(GameObject target, GameObject sendObject)
    {
        if (isActiveted)
        {
            durationTimer = 0f;
            return;
        }
        player = target.GetComponent<PlayerMovement>();
        itemObject = sendObject;
        
        
        if (player != null)
        {
            originalSpeed = 3f;
            player.StartCoroutine(PowerUpWithDuration());
        } 
    }
    protected override IEnumerator PowerUpWithDuration()
    {
        player.moveSpeed *= speedMultiplier;
        isActiveted = true;

        durationTimer = 0f;
        while (durationTimer < duration)
        {
            durationTimer += Time.deltaTime;
            yield return null;
        }

        isActiveted = false;
        player.moveSpeed = originalSpeed;
        itemObject.SetActive(false);
    }

   
}
