using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Скрипт висит на Main Camera
public class Button_OK : MonoBehaviour
{
    [SerializeField]
    InputField field;

    public void OnClick()
    {
        Debug.Log("Button was pressed. User email: " + field.text.ToString());
        //Далее передать куда надо field.text.ToString()
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Enter key was pressed. User email: " + field.text.ToString());
            //Далее передать куда надо field.text.ToString()
        }
    }
}
