using Bonsai;
using Bonsai.Core;
using UnityEngine;

[BonsaiNode("Getter/", "Object")]
public class GetObjectPosition : GetterNode<Vector3>
{
    public override Vector3 Get()
    {
        var player = GameObject.Find("locker_4");
        return player.transform.position;
    }
}