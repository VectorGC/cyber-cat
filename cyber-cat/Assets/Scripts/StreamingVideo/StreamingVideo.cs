using UnityEngine;
using UnityEngine.Video;
using Object = UnityEngine.Object;

namespace StreamingVideo
{
    [RequireComponent(typeof(VideoPlayer))]
    public class StreamingVideo : MonoBehaviour
    {
        public const string StreamingVideoAssetFieldName = nameof(streamingVideoAsset);
        public const string FilePathFieldName = nameof(filePath);

        [SerializeField] private Object streamingVideoAsset;
        [SerializeField] private string filePath;

        private VideoPlayer _videoPlayer;

        public void Awake()
        {
            TryGetComponent(out _videoPlayer);
        }

        public void OnEnable()
        {
            _videoPlayer.url = System.IO.Path.Combine(filePath);
            _videoPlayer.Play();
        }
    }
}