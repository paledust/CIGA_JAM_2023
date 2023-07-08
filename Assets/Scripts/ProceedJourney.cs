using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceedJourney : MonoBehaviour
{
    [SerializeField] private LineRenderer journey_line;
    [SerializeField] private float drawTime = 2f;
    [SerializeField] private float endingDelay = 3f;
    private string progressName = "_Progress";
    void OnEnable(){
        StartCoroutine(coroutineToNextLandMark());
    }
    IEnumerator coroutineToNextLandMark(){
        float currentProgress = GameManager.Instance.GetCurrentJourneyProgress();
        journey_line.material.SetFloat(progressName, currentProgress);
        GameManager.Instance.ToNextJourney();
        float targetProgress = GameManager.Instance.GetCurrentJourneyProgress();
        string targetSceneName = GameManager.Instance.GetCurrentJourneyName();

        for(float t=0; t<1; t+=Time.deltaTime/drawTime){
            journey_line.material.SetFloat(progressName, Mathf.Lerp(currentProgress, targetProgress, EasingFunc.Easing.QuartEaseIn(t)));
            yield return null;
        }
        journey_line.material.SetFloat(progressName, targetProgress);
        yield return new WaitForSeconds(endingDelay);
        GameManager.Instance.SwitchingScene(GameManager.loadingScreenScene, targetSceneName);
    }
}
