using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseReward
{
    public abstract bool TryToAddReward(BaseReward baseReward);
    public abstract Sprite GetSprite();
    public abstract int GetQuantity();
    public abstract void GiveReward();
    public abstract void PlayAnimation(Transform itemTransform, Action endAnimationAction = null);
    public abstract Dictionary<Sprite, int> GetRewardData();
}