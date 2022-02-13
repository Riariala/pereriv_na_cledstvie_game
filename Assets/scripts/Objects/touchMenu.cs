using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
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
     //if (Input.touchCount == 1)                   //НЕ УДАЛЯТЬ
       if (Input.GetMouseButtonDown(0))
        {
            //Touch touch = Input.GetTouch(0);        //НЕ УДАЛЯТЬ


            //if ((menu != null ) && (!EventSystem.current.IsPointerOverGameObject(touch.fingerId)))
            if ((menu != null ) && (!EventSystem.current.IsPointerOverGameObject()))
            {
                Destroy(menu);
            }

          //Vector3 pos = touch.position;           //НЕ УДАЛЯТЬ
            Vector3 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if ((menu == null) && (Physics.Raycast(ray, out hit)))
            {
                Debug.Log(hit.collider.tag);
                
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
                            btn.transform.localPosition  = new Vector3(Mathf.Cos(indActiveAction * koef)*60, Mathf.Sin(indActiveAction * koef)*60, 0);
                            indActiveAction++;
                        }
                    }
                }
            }
         }
    }

    //кнопки менюшки объектов, позже приделаю сюда нормальную реализацию, а не просто подписи
    public void watchAtAction()
    {
        Debug.Log("Когда-нибудь тут будут описываться итоги осмотра. И он будет привязан к объекту, на который смотрят.");
    }

    public void TakeItAction()
    {
        Debug.Log("Когда-нибудь тут возьмут предмет. И действие будет привязано к объекту, который берут.");
    }

    public void doItAction()
    {
        Debug.Log("Когда-нибудь тут будут описываться итоги взаимодействия. И оно будет привязано к объекту, с которым взаимодействуют.");
    }
}
