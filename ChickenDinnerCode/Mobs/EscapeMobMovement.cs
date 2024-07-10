using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMobMovement : MobMovementBase
{
    [SerializeField]protected float escapeRange;
    protected bool isRunning=false;
    [SerializeField]protected LayerMask escapeLayer;

    protected List<Collider2D> targets = new List<Collider2D>();

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        ControlEscape();

    }
    protected override void CharacterFlip()
    {
        base.CharacterFlip();
    }
    public override void Anime()
    {
        base.Anime();
    }
    protected override void Move()
    {
        base.Move();
    }
    protected void ControlEscape()
    {
        Collider2D escapeTarget = CheckArea();
        if (escapeTarget != null && isRunning==false)
        {
            float newDirection = escapeTarget.transform.position.x-transform.position.x;
            newDirection = newDirection < 0 ? 1 : -1;
            SetDirection((int)newDirection);
            currentSpeed*=2.5f;
            isRunning=true;
            return;
        }

        if (isRunning)
        {
            isRunning = false;
            currentSpeed=speed;
        }
        
    }

    Collider2D CheckArea()
    {
        targets.Clear();
        targets.AddRange(Physics2D.OverlapCircleAll(transform.position, escapeRange, escapeLayer));

        if (targets.Count > 0)
        {
            return targets[0];
        }

        return null;

    }
    public override void SetDirection(int direction)
    {
        base.SetDirection(direction);
    }
    private void OnDrawGizmos() {
        Gizmos.color=Color.yellow;
        Gizmos.DrawWireSphere(transform.position,escapeRange);
    }

    protected override void Start()
    {
        base.Start();
    }
}
