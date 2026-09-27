using UnityEngine;
public class ItemCollectablePaper : ItemCollectableBase
{
    protected override void OnCollect()
    {
        ItemManager.Instance.AddPapers();
    }
}
