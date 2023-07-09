using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AnswerSpawn : MonoBehaviour
{
    [SerializeField] private Hand_Project_Text project_Text;
    [SerializeField] private AnswerSelect[] answers;
    [SerializeField] private float chargeTime = 4;
    private float projectTimer = 0;
    private int answerIndex = 0;
    void Awake(){
        EventHandler.E_OnSelectAnswer += StopAnswer;
    }
    void OnDestroy(){
        EventHandler.E_OnSelectAnswer -= StopAnswer;
    }
    void OnEnable(){
        for(int i=0; i<answers.Length; i++){
            answers[i].Initiate();
        }
    }
    void Update(){
        if(project_Text.isProjecting){
            projectTimer += Time.deltaTime;
            if(projectTimer>chargeTime){
                projectTimer = 0;
                answers[answerIndex].MoveDown();
                answerIndex ++;
                if(answerIndex>=answers.Length) {
                    this.enabled = false;
                    for(int i=0; i<answers.Length; i++){
                        answers[i].EnableHitbox();
                        project_Text.StopProject();
                    }
                }
            }
        }
    }
    void StopAnswer(AnswerSelect answer){
        for(int i=0; i<answers.Length; i++){
            if(answers[i]!=answer){
                Debug.Log(answers[i].GetComponent<TextMeshPro>().text);
                Destroy(answers[i].gameObject);
            }
            else{
                answer.transform.localScale *= 1.5f;
            }
        } 
    }
}
