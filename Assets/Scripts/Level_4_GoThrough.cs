using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_4_GoThrough : MonoBehaviour
{
    [SerializeField] private Hand_Project_Text project_Text;
    [SerializeField] private Hand_Shaking handShake;
    [SerializeField] private ButtonPress button;
    [SerializeField] private GameObject blScr;
    private void Awake(){
        EventHandler.E_OnHandShake += StartProjectText;
        EventHandler.E_OnSelectAnswer += FadeInButton;
        EventHandler.E_OnPressButton += FlyAway;
    }
    private void OnDestroy(){
        EventHandler.E_OnHandShake -= StartProjectText;
        EventHandler.E_OnSelectAnswer -= FadeInButton;
        EventHandler.E_OnPressButton -= FlyAway;
    }
    void StartProjectText(){
        project_Text.enabled = true;
        handShake.StopShake();
    }
    void FadeInButton(AnswerSelect selectAnswer){
        StartCoroutine(coroutineFadeInButton());
    }
    void FlyAway(){
        foreach(var moves in FindObjectsOfType<MovingRight>()){
            moves.enabled = true;
        }
        StartCoroutine(coroutineEndGame());
    }
    IEnumerator coroutineEndGame(){
        yield return new WaitForSeconds(2f);
        blScr.SetActive(true);
        yield return new WaitForSeconds(3f);
        GameManager.Instance.SwitchingScene("Level_4", "EndLevel");
    }
    IEnumerator coroutineFadeInButton(){
        yield return new WaitForSeconds(2f);
        button.gameObject.SetActive(true);
        button.becomeVisible();
    }
}
