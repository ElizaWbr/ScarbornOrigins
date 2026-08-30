using UnityEngine;
public class ItemCollectableFairy : ItemCollectableBase
{
    protected override void OnCollect()
    {
        ItemManager.Instance.AddFairies();
    }
}
