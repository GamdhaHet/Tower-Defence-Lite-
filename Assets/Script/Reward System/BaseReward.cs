using UnityEngine;

public abstract class BaseReward
{
    public abstract Sprite GetSprite();
    public abstract int GetQuantity();
    public abstract void GiveReward();
}