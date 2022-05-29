using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Sprite currentMenuImgName;
    public GameObject currentMenu;
    public GameObject background;
    private float defaultPosX;

    void Start()
    {
        OpenTab(currentMenuImgName);
        OpenMenu(currentMenu);
    }

    public void OpenTab(Sprite tabmenu_type)
    {
        background.GetComponent<Image>().sprite = tabmenu_type;
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
