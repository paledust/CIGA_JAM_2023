using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : BasicPointAndClickInteractable
{
    [SerializeField] private SpriteRenderer m_signal_renderer;
    [SerializeField] private SpriteRenderer m_rad_renderer;
    [SerializeField] private Transform buttonPressGroup;
    private IEnumerator coroutineSwipeSprite;
    void Awake(){
        Color whiteClear = Color.white;
        whiteClear.a = 0;
        foreach(var sprite in GetComponentsInChildren<SpriteRenderer>()){
            sprite.color = whiteClear;
        }
    }
    public void becomeVisible(){
        GetComponent<Animation>().Play();       
    }
    public override void OnHover(bool isTouching, PointClick_InteractableHandler pointclick_handler)
    {
        base.OnHover(isTouching, pointclick_handler);
        
        if(coroutineSwipeSprite!=null) StopCoroutine(coroutineSwipeSprite);
        coroutineSwipeSprite = coroutineSwitchSprite(m_signal_renderer, m_rad_renderer);
        StartCoroutine(coroutineSwipeSprite);
    }
    public override void OnExitHover()
    {
        base.OnExitHover();

        if(coroutineSwipeSprite!=null) StopCoroutine(coroutineSwipeSprite);
        coroutineSwipeSprite = coroutineSwitchSprite(m_rad_renderer, m_signal_renderer);
        StartCoroutine(coroutineSwipeSprite);
    }
    public override void OnClick(PointClick_InteractableHandler interactableHandler)
    {
        base.OnClick(interactableHandler);
        EventHandler.Call_OnPressButton();
        interactableHandler.HoldTheInteractable(this);
        buttonPressGroup.transform.localScale *= 0.8f;
        DisableHitbox();
    }
    public override void OnRelease(PointClick_InteractableHandler interactableHandler)
    {
        base.OnRelease(interactableHandler);
        buttonPressGroup.transform.localScale /= 0.8f;
    }
    IEnumerator coroutineSwitchSprite(SpriteRenderer startRenderer, SpriteRenderer targetRenderer){
        for(float t=0; t<1; t+=Time.deltaTime*2){
            startRenderer.color = Color.Lerp(Color.white, Color.clear, EasingFunc.Easing.SmoothInOut(t));
            targetRenderer.color = Color.Lerp(Color.clear, Color.white, EasingFunc.Easing.SmoothInOut(t));
            yield return null;
        }
        startRenderer.color = Color.clear;
        targetRenderer.color = Color.white;
    }
}
