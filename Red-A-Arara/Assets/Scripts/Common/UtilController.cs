using UnityEngine;

public class UtilController : Singleton<UtilController>
{
    public enum HitDirection { None, Top, Bottom, Left, Right }

    public HitDirection ReturnDirection(ContactPoint2D[] collision2D)
    {

        HitDirection hitDirection = HitDirection.None;

        foreach (ContactPoint2D missileHit in collision2D)
        {
            if (missileHit.normal.y < 0) { hitDirection = HitDirection.Top; }
            if (missileHit.normal.y > 0) { hitDirection = HitDirection.Bottom; }
            if (missileHit.normal.x < 0) { hitDirection = HitDirection.Right; }
            if (missileHit.normal.x > 0) { hitDirection = HitDirection.Left; }
        }

        return hitDirection;
    }
}
