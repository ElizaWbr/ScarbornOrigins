using UnityEngine;
using Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public int fairies;
    public int potions;
    public int papers;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        fairies = 0;
        potions = 0;
        papers = 0;
        ResetUI();
    }
    public void AddFairies(int amount = 1)
    {
        fairies += amount;
        UpdateUIFairies();
    }
    public void AddPotions(int amount = 1)
    {
        potions += amount;
        UpdateUIPotions();
    }
    public void AddPapers(int amount = 1)
    {
        papers += amount;
        UpdateUIPapers();
    }
    private void UpdateUIFairies()
    {
        UIInGameManager.UpdateTextFairies(fairies.ToString());
    }
    private void UpdateUIPotions()
    {
        UIInGameManager.UpdateTextPotions(potions.ToString());
    }
    private void UpdateUIPapers()
    {
        UIInGameManager.UpdateTextPapers(papers.ToString());
    }

    private void ResetUI()
    {
        UpdateUIFairies();
        UpdateUIPotions();
        UpdateUIPapers();
    }
}
