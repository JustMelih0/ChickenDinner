
using System.Collections;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField]protected float followDuration;
    [SerializeField]protected float randomPointDistance;

    [SerializeField]protected ItemType itemType;
    [SerializeField]protected SpriteRenderer spriteRenderer;

    public PowerUpBase powerUp;

    protected bool canClaimable=false;
    protected Vector2 startPosition;
    protected Transform playerRef;
    protected virtual void Start() 
    {
        playerRef=GameManager.Instance.playerInstance.transform;
    }

    public virtual void SpawnDropped()
    {
        spriteRenderer.sprite = powerUp.icon; 
        Vector2 upPoint=transform.position;
        upPoint.y = upPoint.y+randomPointDistance;
        upPoint.x = upPoint.x+Random.Range(-0.2f,+0.2f);
        StopAllCoroutines();
        StartCoroutine(FollowtheTarget(upPoint));
    }
    public virtual void FollowPlayer()
    {
      canClaimable = true;
      StartCoroutine(FollowtheTarget(playerRef.position));
    }
     IEnumerator FollowtheTarget(Vector2 target)
    {
        float time=0f;
        startPosition=transform.position;
        while(time<followDuration)
        {
          time += Time.deltaTime;
          float t = time / followDuration;
          transform.position = Vector2.Lerp(startPosition, target, t);
          yield return null;
        }
        FollowPlayer();

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag=="Player"&&canClaimable)
        {     
           EnterPlayer(other.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag=="Player"&&canClaimable)
        {     
            EnterPlayer(other.gameObject);
        }
    }
    protected virtual void EnterPlayer(GameObject target)
    {
        OnPickedUp();
        powerUp.Apply(target,transform.gameObject);

    }
    protected virtual void OnPickedUp()
    {
       StopAllCoroutines();
       canClaimable=false;
       gameObject.SetActive(false);
    }
    

    [System.Serializable]
    public enum ItemType
    {
        Half,
        Entire

    }
}
