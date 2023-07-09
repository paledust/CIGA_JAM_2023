using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBoardDrawer : MonoBehaviour
{
    [SerializeField] private Animation m_anime;
    [SerializeField] private EBoardDrawPoint[] segmentPoints;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private ParticleSystem boom_particle;
[Header("Audio")]
    [SerializeField] private AudioSource m_audio;
    private float[] segment_lengths;
    private int currentLine_Index = 0;
    private LineRenderer currentLine;
    private Vector3 currentDir;
    private Camera mainCam;
    public bool IsDrawing{get; private set;} = false;
    void Awake(){
        mainCam = Camera.main;
        segment_lengths = new float[segmentPoints.Length-1];
        for(int i=0; i<segmentPoints.Length-1; i++){
            segment_lengths[i] = (segmentPoints[i+1].transform.position - segmentPoints[i].transform.position).magnitude;
        }
    }
    public void StartLine(EBoardDrawPoint drawPoint){
        int index = 0;
        for(;index<segmentPoints.Length;index++){
            if(segmentPoints[index] == drawPoint) break;
        }
        currentLine_Index = index;
        currentDir = segmentPoints[index+1].transform.position - drawPoint.transform.position;
        currentDir = currentDir.normalized;

        if(currentLine==null){
            currentLine = GameObject.Instantiate(linePrefab).GetComponent<LineRenderer>();
            m_anime.Play();
        }
        else currentLine.positionCount ++;

        currentLine.SetPosition(currentLine_Index, drawPoint.transform.position);
        currentLine.SetPosition(currentLine_Index+1, drawPoint.transform.position);

        this.enabled = true;

        EventHandler.Call_OnDrawNewLine();

        IsDrawing = true;
        if(!m_audio.isPlaying)m_audio.Play();

        StopAllCoroutines();
        StartCoroutine(CoroutinePulseVolume());
    }
    public void BreakLine(){
        this.enabled = false;
        currentLine.positionCount --;
        IsDrawing = false;

        m_audio.Stop();
    }
    void FinishLine(){
        this.enabled = false;
        IsDrawing = false;
        EventHandler.Call_OnFinishDrawLine();

        m_audio.Stop();
    }
    void Update(){
        Vector3 mousePos = PointClick_InteractableHandler.tipPos;
        Vector3 originPos = currentLine.GetPosition(currentLine_Index);
        mousePos.z = originPos.z;
        float length = Vector3.Dot(mousePos-originPos, currentDir);
        length = Mathf.Max(length, 0);
        currentLine.SetPosition(currentLine_Index+1, originPos + length*currentDir);

        if(length >= segment_lengths[currentLine_Index]){
            if(currentLine_Index>=segmentPoints.Length-2){
                segmentPoints[currentLine_Index].DisableHitbox();
                segmentPoints[currentLine_Index+1].HideSprite();
                boom_particle.Play();
                FinishLine();
            }
            else{
                segmentPoints[currentLine_Index].DisableHitbox();
                segmentPoints[currentLine_Index+1].HideSprite();
                segmentPoints[currentLine_Index+1].EnableHitbox();
                StartLine(segmentPoints[currentLine_Index+1]);
            }
        }
    }
    IEnumerator CoroutinePulseVolume(){
        m_audio.volume = 1;
        for(float t=0; t<1; t+=Time.deltaTime*4){
            m_audio.volume = Mathf.Lerp(1, 0.3f, EasingFunc.Easing.QuadEaseIn(t));
            yield return null;
        }
        m_audio.volume = 0.3f;
    }
}
