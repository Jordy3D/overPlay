using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class ClickerManager : MonoBehaviour
{
  public TextMeshProUGUI scoreText;

  public int score;

  public List<Button> upgradeButtons;
  public List<int> upgradeCosts;

  public List<bool> upgradeAvailable;

  public int searchforPointsValue, peckforPointsValue;

  void Start()
  {
    score = 0;
    scoreText.SetText($"Score: {score}");

    ResetGame();
  }

  void Update()
  {

  }

  public void IncreaseScore(int _val)
  {
    print("Invreasing score!");
    score += _val;
    scoreText.SetText($"Score: {score}");

    UpgradeAvailabilityHandler();
  }

  void ResetGame()
  {
    for (int i = 0; i < upgradeButtons.Count; i++)
    {
      upgradeButtons[i].gameObject.SetActive(false);
      upgradeAvailable[i] = false;
    }
  }

  public void UpgradeAvailabilityHandler()
  {
    for (int i = 0; i < upgradeAvailable.Count; i++)
    {
      if (score >= (upgradeCosts[i] / 2))
      {
        upgradeAvailable[i] = true;
        upgradeButtons[i].gameObject.SetActive(true);
      }

      upgradeButtons[i].interactable = score >= upgradeCosts[i];
    }
  }
}
