using System.Collections.Generic;
using Loading.Operations;
using UnityEngine;

namespace Loading
{
    public class AppStartup : MonoBehaviour
    {
        [SerializeField] private string _scene;
        
        private async void Awake()
        {
            var loadingOperations = new Queue<ILoadingOperation>();
            loadingOperations.Enqueue(new PlayGamesLoginOperation());
            // loadingOperations.Enqueue(new TestOperation());
            loadingOperations.Enqueue(new LoadingSceneOperation(_scene));
            await new LoadingScreenProvider().LoadAndDestroy(loadingOperations);
        }
    }
}