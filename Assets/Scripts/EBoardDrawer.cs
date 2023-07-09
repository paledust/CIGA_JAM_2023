using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBoardDrawer : MonoBehaviour
{
    [SerializeField] private Animation m_anime;
    [SerializeField] private EBoardDrawPoint[] segmentPoints;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private ParticleSystem boom_particle;
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
    }
    public void BreakLine(){
        this.enabled = false;
        currentLine.positionCount --;
        IsDrawing = false;
    }
    void FinishLine(){
        this.enabled = false;
        IsDrawing = false;
        EventHandler.Call_OnFinishDrawLine();
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
}
