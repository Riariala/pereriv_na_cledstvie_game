using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //порядок: осмотреть, взять, взаимодействовать;       соответствует порядку расположения кнопок в префабе menu_obj_touch 
    public int ID;
    public bool[] enable_actions = new bool[] {false, false, false};
}
