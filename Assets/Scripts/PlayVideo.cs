using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string videoName;
    void Start()
    {
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoName);
        videoPlayer.Play();
    }
}
