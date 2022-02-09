using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchMenu : MonoBehaviour
{

    [SerializeField] public GameObject menu_obj_touch;
    [SerializeField] public Transform parent;

    private GameObject menu;


    void Update()
    {
        //обработка нажатия мышки, тестовое для работы на ноуте
    //    if (Input.GetMouseButtonDown(0))
    //      {
    //         if (menu != null)
    //         {
    //             Debug.Log(menu.tag);
    //             Destroy(menu);
    //         }
    //         Vector3 pos = Input.mousePosition; 
    //         Ray ray = Camera.main.ScreenPointToRay(pos);
    //         RaycastHit hit;

    //         if (Physics.Raycast(ray, out hit))
    //         {
    //             if (hit.collider.tag == "Item")
    //             {
    //                 menu = Instantiate(menu_obj_touch, parent);
    //                 menu.transform.position  = pos;
    //             }
    //         }
    //      }

        //обработка нажатия пальцем
        if (Input.touchCount == 1)
        {
            if (menu != null)
            {
                Debug.Log(menu.tag);
                Destroy(menu);
            }
            Touch touch = Input.GetTouch(0);
            Vector3 pos = touch.position;
            Ray ray = Camera.main.ScreenPointToRay(pos);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Item")
                {
                    menu = Instantiate(menu_obj_touch, parent);
                    menu.transform.position = pos;
                }
            }

        } 
    }
}
