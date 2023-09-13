using System;
using System.Threading.Tasks;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace Loading.Operations
{
    public class PlayGamesLoginOperation : ILoadingOperation
    {
        private readonly TaskCompletionSource<bool> _tcs = new ();
        private Action<float> _onProgress;

        public Action<string> OnDescriptionChange { get; set; }
        public string Description => "Authentication";
        
        public async Task Load(Action<float> onProgress)
        {
            _onProgress = onProgress;
            _onProgress?.Invoke(0.3f);
            
            PlayGamesPlatform.Instance.Authenticate(Authenticate);
            await _tcs.Task;
            _onProgress?.Invoke(1f);
        }

        private void Authenticate(SignInStatus status)
        {
            switch (status)
            {
                case SignInStatus.Success:
                    _onProgress?.Invoke(0.5f);
                    _tcs.SetResult(true);
                    break;
                case SignInStatus.InternalError:
                case SignInStatus.Canceled:
                default:
                    OnDescriptionChange?.Invoke("Authentication failed");
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}