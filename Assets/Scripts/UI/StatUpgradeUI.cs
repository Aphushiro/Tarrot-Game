using UnityEngine;

public class StatUpgradeUI : MonoBehaviour
{
    public void PickStatUpgrade(int toUpgrade)
    {
        // 0 - cup
        // 1 - sword
        // 2 - pentacle
        // 3 - wand
        OnSelection(toUpgrade);
    }

    void OnSelection(int selection)
    {
        if (PlayerStats.Instance.UpgradeStat(selection) == false) { return; }
        CloseStatSelectionPanel();
    }

    public GameObject statUpgradeUiObj;
    public void OpenStatSelectionPanel ()
    {
        statUpgradeUiObj.SetActive(true);
    }

    public void CloseStatSelectionPanel ()
    {
        statUpgradeUiObj.SetActive(false);
    }

}
