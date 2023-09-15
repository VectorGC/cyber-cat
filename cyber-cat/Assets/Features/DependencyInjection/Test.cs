using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Models;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject]
    public async void Construct(AsyncInject<IPlayer> playerAsync)
    {
        var player = await playerAsync;
        var t = 10;
    }
}
