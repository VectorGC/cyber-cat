using Bonsai;
using Bonsai.Core;
using UnityEngine;

[BonsaiNode("Getters/", "PlayerPosition")]
public class GetPlayerPosition : GetterNode<Vector3>
{
    public override Vector3 Get()
    {
        var player = FindObjectOfType<Player>();
        return player.transform.position;
    }
}