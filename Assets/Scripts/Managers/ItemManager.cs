using UnityEngine;
using Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public int fairies;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        fairies = 0;
        UpdateUI();
    }

    public void AddFairies(int amount = 1)
    {
        fairies += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        UIInGameManager.UpdateTextFairies(fairies.ToString());
    }
}
