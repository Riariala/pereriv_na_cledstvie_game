using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class touchMenu : MonoBehaviour
{

    [SerializeField] public GameObject menu_obj_touch;
    [SerializeField] public Transform parent;
    [SerializeField] public GameObject inventory_panel;

    private GameObject menu;
    private GameObject touched_item;
    private Vector2 start_pos;
    private Vector2 direction;
    private float screen_height;

    void Start()
    {
        screen_height = Screen.height;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    start_pos = touch.position;
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    direction = touch.position - start_pos;
                    if ((direction.y <= -50) && (start_pos.y <= 75))
                    {
                        inventory_panel.SetActive(true);
                    }

                    if ((direction.y >= 50) && (start_pos.y >= screen_height - 75))
                    {
                        inventory_panel.SetActive(false);
                    }

                    if (menu != null){Destroy(menu);}

                    Vector3 pos = touch.position;
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

                            float koef = Mathf.PI * 2 / count_actions;
                            int indActiveAction = 0;
                            for (int i = 0; i < enable_actionsl.Count; i++)
                            {
                                if (enable_actionsl[i])
                                {
                                    GameObject btn = menu.transform.GetChild(i).gameObject;
                                    btn.SetActive(true);
                                    btn.transform.localPosition = new Vector3(Mathf.Cos(indActiveAction * koef + (Mathf.PI / 2)) * 60, Mathf.Sin(indActiveAction * koef + (Mathf.PI / 2)) * 60, 0);
                                    indActiveAction++;
                                }
                            }
                        }
                    }

                    break;
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
