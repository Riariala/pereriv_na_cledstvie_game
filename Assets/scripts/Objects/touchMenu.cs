using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class touchMenu : MonoBehaviour
{

    [SerializeField] public GameObject menu_obj_touch;
    [SerializeField] public Transform parent;

    private GameObject menu;
    private GameObject touched_item;

    void Update()
    {
        //обработка нажатия мышки, тестовое для работы на ноуте, для обработки нажатия пальцем раскомментить строки и закомментить ону строку под каждой
    // if (Input.touchCount == 1)                   //НЕ УДАЛЯТЬ
       if (Input.GetMouseButtonDown(0))
        {
            if (menu != null)
            {
                Destroy(menu);
            }

        //  Touch touch = Input.GetTouch(0);        //НЕ УДАЛЯТЬ
        //  Vector3 pos = touch.position;           //НЕ УДАЛЯТЬ
            Vector3 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Item")
                {   
                    touched_item = hit.collider.gameObject;
                    menu = Instantiate(menu_obj_touch, parent);
                    menu.transform.position = pos;
                    List<bool> enable_actionsl = new List<bool>(touched_item.GetComponent<ObjectManager>().enable_actions);
                    int count_actions = enable_actionsl.Count(x => x == true);

                    float koef =  Mathf.PI * 2 / count_actions;
                    int indActiveAction = 0;
                    for (int i = 0; i < enable_actionsl.Count; i++)
                    {
                        if (enable_actionsl[i])
                        {
                            GameObject btn = menu.transform.GetChild(i).gameObject;
                            btn.SetActive(true); 
                            btn.transform.localPosition  = new Vector3(Mathf.Cos(indActiveAction * koef)*50, Mathf.Sin(indActiveAction * koef)*50, 0);
                            indActiveAction++;
                        }
                    }
                    
                }
            }
         }

        // if (Input.touchCount == 1)
        // {
        //     if (menu != null)
        //     {
        //         Debug.Log(menu.tag);
        //         Destroy(menu);
        //     }
        //     Touch touch = Input.GetTouch(0);
        //     Vector3 pos = touch.position;
        //     Ray ray = Camera.main.ScreenPointToRay(pos);

        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         if (hit.collider.tag == "Item")
        //         {
        //             menu = Instantiate(menu_obj_touch, parent);
        //             menu.transform.position = pos;
        //         }
        //     }
        // } 
    }
}
