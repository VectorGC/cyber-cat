﻿using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private static Scene AuthorizationScene;
    private static Scene CodeEditorScene;

    public static bool CanInput
    {
        get
        {
            var scene = SceneManager.GetActiveScene();
            if (!AuthorizationScene.IsValid() && scene.name == "AuthorizationScene")
            {
                AuthorizationScene = scene;
            }

            if (!CodeEditorScene.IsValid() && scene.name == "CodeEditor")
            {
                CodeEditorScene = scene;
            }

            return scene != AuthorizationScene && scene != CodeEditorScene;
        }
    }

    public PlayerInteractHandler Interact { get; private set; }

    [Inject]
    public void Construct(PlayerInteractHandler interactHandler)
    {
        Interact = interactHandler;
    }

    private void Update()
    {
        if (!CanInput)
            return;

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var directionVector = new Vector3(horizontal, 0, vertical);
        _navMeshAgent.velocity = Vector3.ClampMagnitude(directionVector, 1);

        Interact.OnUpdate();
    }
}