using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailButtonScript : MonoBehaviour
{
    public bool isPressed;
    public Button MailButton;
    public Image Loadimg;
    public Image Backloadimg;
    public Image Doneimg;
    public void ButtonPress()
    {
        if(!isPressed)
            StartCoroutine(CoroutineExmple());
    }

    IEnumerator CoroutineExmple()
    {
        isPressed = true;

        Doneimg.fillAmount = 0;
        Backloadimg.fillAmount = 1;

        float waitTime = 0.5f;
        for(int i = 0; i < 10; i++ )
        {
            Loadimg.fillAmount += waitTime/5f;
            yield return new WaitForSeconds(waitTime);
        }

        Loadimg.fillAmount = 0;
        Backloadimg.fillAmount = 0;
        Doneimg.fillAmount = 1;


        Debug.Log("Ok!");
        isPressed = false;
        //isgone = false;
        //float progress = 0;
        //StartCoroutine(FiveSecTimer());
        //while (!isgone)
        //{    
        //    progress += 0.0005f;
        //    img.fillAmount = progress;
        //    yield return null;
        //}
        //img.fillAmount = 0;
        ////yield return new WaitForSeconds(5);

    }

    //IEnumerator FiveSecTimer()
    //{
    //    yield return new WaitForSeconds(5);      
    //    isgone = true;
    //}
}
