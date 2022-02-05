using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UIDialogs", menuName = "ScriptableObjects/UIDialogs", order = 1)]
public class UIDialogs : ScriptableObject
{
    [SerializeField]
    public LoadWindowView LoadWindowPrefab;
    
    private static UIDialogs _instance;
    public static UIDialogs Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.FindObjectsOfTypeAll<UIDialogs>().First();
            }
            
            return _instance;
        }
    }
}
