using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ScreenPet;

public class ClickerUpgrades : MonoBehaviour
{
  public BirdNest nest;
  public ClickerManager manager;
  public void PurchaseBird()
  {
    if (manager.score >= manager.upgradeCosts[0])
    {
      nest.SpawnBird();
      manager.IncreaseScore(-manager.upgradeCosts[0]);
    }
  }

  public void SearchForPoints()
  {
    if (manager.score >= manager.upgradeCosts[1])
    {
      manager.searchforPointsValue++;
      manager.IncreaseScore(-manager.upgradeCosts[1]);
    }
  }

  public void PeckForPoints()
  {
    if (manager.score >= manager.upgradeCosts[2])
    {
      manager.peckforPointsValue++;
      manager.IncreaseScore(-manager.upgradeCosts[2]);
    }
  }
}
