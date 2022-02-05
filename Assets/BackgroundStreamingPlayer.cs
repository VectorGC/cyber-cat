using UnityEngine;
using UnityEngine.Video;

public class BackgroundStreamingPlayer : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Background.mp4");

        _videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}