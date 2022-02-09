using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMenuBtns : MonoBehaviour
{
    private touchMenu touch_menu;

    public void watchAtActionInside()
    {
        if (!touch_menu)
        {
            touch_menu = this.transform.root.gameObject.GetComponent<touchMenu>();
        }
        touch_menu.watchAtAction();
    }
    
    public void TakeItActionInside()
    {
        if (!touch_menu)
        {
            touch_menu = this.transform.root.gameObject.GetComponent<touchMenu>();
        }
        touch_menu.TakeItAction();
    }

    public void doItActionInside()
    {
        if (!touch_menu)
        {
            touch_menu = this.transform.root.gameObject.GetComponent<touchMenu>();
        }
        touch_menu.doItAction();
    }
}
