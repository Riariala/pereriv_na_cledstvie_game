using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
      //порядок: осмотреть, взять, взаимодействовать;       соответствует порядку расположения кнопок в префабе menu_obj_touch 
    [SerializeField]  public bool[] enable_actions = new bool[] {false, false, false};  
    
       // // Update is called once per frame
    // void Update()
    // {

    // }
}
