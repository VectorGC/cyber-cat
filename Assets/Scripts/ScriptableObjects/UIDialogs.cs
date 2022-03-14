using System;
using System.Linq;
using MonoBehaviours.PropertyFields;
using TMPro;
using UnityEngine;

[Serializable]
public struct TMPSettings
{
    public TMP_FontAsset FontAsset;
    public float CharacterSpacing;
}

[CreateAssetMenu(fileName = "UIDialogs", menuName = "ScriptableObjects/UIDialogs", order = 1)]
public class UIDialogs : ScriptableObject
{
    [SerializeField] private LoadingScreen _loadingScreenPrefab;
    private LoadingScreen _loadingScreen;
    public LoadingScreen LoadingScreen
    {
        get
        {
            if (_loadingScreen)
            {
                return _loadingScreen;
            }
            
            _loadingScreen = Instantiate(_loadingScreenPrefab);
            _loadingScreen.gameObject.SetActive(false);
            
            DontDestroyOnLoad(_loadingScreen);
            
            return _loadingScreen;
        }
    }

    public TMPSettings TMPSettings;

    public SceneField StartScene;

    private static UIDialogs _instance;

    public static UIDialogs Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.Load<UIDialogs>("UIDialogs");
            }

            return _instance;
        }
    }
}