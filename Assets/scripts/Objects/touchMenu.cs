using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;

public class touchMenu : Photon.Bolt.EntityBehaviour<ICustomPlayer>//MonoBehaviour
{

    [SerializeField] public GameObject menu_obj_touch;
    [SerializeField] public Transform parent;
    public DialogPlayer dialogPlayer;

    private GameObject menu;
    private GameObject touched_item;
    private Vector2 start_pos;
    private Vector2 direction;
    private float screen_height;
    private bool isInitiator;
    public NetworkCallbacks callbacks;
    private bool isBusy;

    void Start()
    {
        screen_height = Screen.height;
    }

    void Update()
    {
        if (callbacks.click)
        {
            if (isInitiator = false)
            {
                callbacks.click = false;
                var busy = IsBusy.Create();
                busy.Busy = callbacks.data.isBusy;
                busy.Send();
                //isBusy = busy.Busy;
                if (!busy.Busy) //!!!!!!!!!!!!!!!!КРИСТИНА СЮДА ПРОВЕРКУ ЗАНЯТ ЛИ ВТОРОЙ ИГРОК!!!!!!!!!!!!!!!!!!
                {
                    dialogPlayer.beginPlayersDialog(dialogPlayer.dialogSaver.playerData.dialogId);
                }
                Debug.Log("Я ответил второму игроку.Я занят? - " + busy.Busy.ToString());
            }
            else
            {
                isInitiator = false;
                callbacks.click = false;
                //var click = ClickOnPlayer.Create();
                //click.Click = false;
                //click.Send();
                if (!callbacks.isBusy) //!!!!!!!!!!!!!!!!КРИСТИНА СЮДА ПРОВЕРКУ ЗАНЯТ ЛИ ВТОРОЙ ИГРОК!!!!!!!!!!!!!!!!!!
                {
                   Debug.Log("Я не занят. Могу говорить.");
                   dialogPlayer.beginPlayersDialog(dialogPlayer.dialogSaver.playerData.dialogId);
                }

            }
        }
        

#if UNITY_ANDROID && !UNITY_EDITOR
        forAndroid();
#endif

#if !UNITY_ANDROID || UNITY_EDITOR
        forEditorUpdate();
#endif
    }

    public void forAndroid()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        if (menu != null) { Destroy(menu); }

                        Vector3 pos = touch.position;
                        Ray ray = Camera.main.ScreenPointToRay(pos);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("NPC"))
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
                            else if (hit.collider.CompareTag("Player"))
                            {
                                bool isfirst = hit.collider.transform.parent.gameObject.name == "Rogers";
                                if (dialogPlayer.dialogSaver.playerData.isPlayer1 != isfirst) //это чтобы не вызывал диалог сам с собой
                                {
                                    //Debug.Log("Хэээээээээй");
                                    var click = ClickOnPlayer.Create();
                                    click.Click = true;
                                    click.Send();
                                    isInitiator = true;
                                    //if (!isBusy) //!!!!!!!!!!!!!!!!КРИСТИНА СЮДА ПРОВЕРКУ ЗАНЯТ ЛИ ВТОРОЙ ИГРОК!!!!!!!!!!!!!!!!!!
                                    /*{
                                        Debug.Log("Жизньтлен");
                                        dialogPlayer.beginPlayersDialog(dialogPlayer.dialogSaver.playerData.dialogId);
                                    }
                                    isInitiator = false;
                                    click.Click = false;
                                    click.Send();*/
                                }
                            }
                        }
                    }
                    else
                    {
                        PointerEventData pointer = new PointerEventData(EventSystem.current);
                        Vector3 pos = new Vector3(touch.position.x, touch.position.y, 0f);
                        pointer.position = pos;
                        List<RaycastResult> raycastResults = new List<RaycastResult>();
                        EventSystem.current.RaycastAll(pointer, raycastResults);
                        if (raycastResults.Count != 0)
                        {
                            if (!(raycastResults[0].gameObject.CompareTag("menu_touch")))
                            {
                                if (menu != null) { Destroy(menu); }
                            }
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:

                    break;
            }
        }
    }

    public void forEditorUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (menu != null) { Destroy(menu); }

                Vector3 pos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("NPC"))
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
                    else if (hit.collider.CompareTag("Player"))
                    {
                        bool isfirst = hit.collider.transform.parent.gameObject.name == "Rogers";
                        Debug.Log(hit.collider.transform.parent.gameObject.name);
                        if (dialogPlayer.dialogSaver.playerData.isPlayer1 != isfirst) //это чтобы не вызывал диалог сам с собой
                        {
                            //Debug.Log("Хэээээээээй");
                            isInitiator = true;
                            var click = ClickOnPlayer.Create();
                            click.Click = true;
                            click.Send();
                            Debug.Log(callbacks.isBusy);
                            //if (!isBusy) //!!!!!!!!!!!!!!!!КРИСТИНА СЮДА ПРОВЕРКУ ЗАНЯТ ЛИ ВТОРОЙ ИГРОК!!!!!!!!!!!!!!!!!!
                            //{
                            //    Debug.Log("Жизньтлен");
                            //    dialogPlayer.beginPlayersDialog(dialogPlayer.dialogSaver.playerData.dialogId);
                            //}
                            //isInitiator = false;
                        }
                        else { Debug.Log("Нельзя разговаривать самим с собой!"); }
                    }
                }
            }
            else
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = Input.mousePosition;
                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, raycastResults);
                if (raycastResults.Count != 0)
                {
                    if (!raycastResults[0].gameObject.CompareTag("menu_touch"))
                    {
                        if (menu != null) { Destroy(menu); }
                    }
                }
            }
        }
    }

    //кнопки меню игрока
    public void callMenuBtns(GameObject btnHolder)
    {
        btnHolder.SetActive(!(btnHolder.activeInHierarchy));
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void callJournal(GameObject journal_panel)
    {
        journal_panel.SetActive(true);
    }

    //кнопки менюшки объектов
    public void watchAtAction() //actionKind = 0
    {
        int itemID = touched_item.GetComponent<ObjectManager>().ID;
        dialogPlayer.beginDialog(itemID, 0);
        if (menu != null) { Destroy(menu); }
    }

    public void TakeItAction() //actionKind = 1
    {
        int itemID = touched_item.GetComponent<ObjectManager>().ID;
        dialogPlayer.beginDialog(itemID, 1);
        if (menu != null) { Destroy(menu); }
    }

    public void doItAction()
    {
        Debug.Log("Когда-нибудь тут будут описываться итоги взаимодействия. И оно будет привязано к объекту, с которым взаимодействуют.");
    }
}
