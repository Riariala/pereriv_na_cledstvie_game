using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public GameObject currentTab;
    public GameObject currentMenu;

    public void OpenTab(GameObject tab)
    {
        if (!(currentTab is null)){CloseTab();}
        currentTab = tab;
        currentTab.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void CloseTab()
    {
        currentTab.transform.localScale = new Vector3(1f, 0.75f, 1f);
    }

    public void OpenMenu(GameObject menu)
    {
        if (!(currentMenu is null)) { currentMenu.SetActive(false); }
        currentMenu = menu;
        currentMenu.SetActive(true);

    }

    public void close_inv(GameObject inv)
    {
        inv.SetActive(false);
    }
}
