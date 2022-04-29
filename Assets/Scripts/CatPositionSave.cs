using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CatPositionSaver 
{  
    public static void Save() //safe x,y,z coords
    {
        GameObject cat;
        cat = GameObject.FindGameObjectWithTag("Player");
        PlayerPrefs.SetFloat("x-catPos", cat.transform.position.x);
        PlayerPrefs.SetFloat("y-catPos", cat.transform.position.y);
        PlayerPrefs.SetFloat("z-catPos", cat.transform.position.z);
        PlayerPrefs.Save();
        Debug.Log("CatPosSave");
    }
    public static void LoadAndSync() // Tp the cat to the last saved space
    {
        GameObject cat;
        cat = GameObject.FindGameObjectWithTag("Player");
        if(PlayerPrefs.HasKey("x-catPos"))
        {
            cat.transform.position = new Vector3(PlayerPrefs.GetFloat("x-catPos"),
                                                 PlayerPrefs.GetFloat("y-catPos"),
                                                 PlayerPrefs.GetFloat("z-catPos"));
        }
        CatPositionSaver.Clear();
        Debug.Log("CatPosLoad");
    }
    public static void Clear() // removal coords after use 
    {
        PlayerPrefs.DeleteKey("x-catPos");
        PlayerPrefs.DeleteKey("y-catPos");
        PlayerPrefs.DeleteKey("z-catPos");
    }
}
