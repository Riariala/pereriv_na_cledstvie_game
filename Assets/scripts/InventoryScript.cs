using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public GameObject currentTab;
    public GameObject currentMenu;
    private float defaultPosX;

    void Start()
    {
        defaultPosX = currentTab.transform.position.x;
        OpenTab(currentTab);
        OpenMenu(currentMenu);
    }

    public void OpenTab(GameObject tab)
    {
        if (!(currentTab is null)){CloseTab();}
        currentTab = tab;
        Transform tabTr = currentTab.transform;
        defaultPosX = tabTr.position.x;
        tabTr.position = new Vector2(tabTr.parent.position.x, tabTr.position.y);
    }

    public void CloseTab()
    {
        currentTab.transform.position = new Vector2(defaultPosX, currentTab.transform.position.y);
    }

    public void SetHelpActive(GameObject helpTab)
    {
        helpTab.SetActive(true);
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
