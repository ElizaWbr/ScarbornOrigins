using UnityEngine;
public class ItemCollectablePotion : ItemCollectableBase
{
    protected override void OnCollect()
    {
        ItemManager.Instance.AddPotions();
    }
}
