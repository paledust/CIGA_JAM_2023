using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CD_Rotation : BasicPointAndClickInteractable
{
    [SerializeField, ShowOnly] private CD_STATE cd_state = CD_STATE.IDLE;
    [SerializeField] private SpriteRenderer m_sprite;
    [SerializeField] private ParticleSystem lightningParticle;
    [SerializeField] private ParticleSystemForceField particleForceField;
    [SerializeField] private float cd_radius = 4.7f;
    [SerializeField] private float state_transitionTime = 0.25f;
    [SerializeField] private float state_transitionSpeed = 5f;
    [SerializeField, ShowOnly] private bool isControlling = false;
[Header("Words")]
    [SerializeField] private string lyrics = "歌词大意：99个气球...在天空...是UFO...来自太空...将军...派出战机...";
    [SerializeField] private string feels = "光滑，反光，旋转。";
[Header("Song")]
    [SerializeField] private AudioSource music_good_source;
    [SerializeField] private AudioSource music_bad_source;
    [SerializeField] private AudioSource sfx_source;
    [SerializeField] private AudioClip sfx_dj_in;
    [SerializeField] private AudioClip sfx_dj_out;
    private float angluarSpeed = 0;
    private float transitionTimer = 0;
    private Vector3 grabPoint;
    void Update(){
        switch(cd_state){
            case CD_STATE.IDLE:
                if(isControlling){
                    transitionTimer = 0;
                    Vector3 grabPointDir = transform.TransformPoint(grabPoint) - transform.position;
                    grabPointDir.z = 0;
                    Vector3 handtipDir = PointClick_InteractableHandler.grabPos - transform.position;
                    handtipDir.z = 0;

                    float angle = Vector3.SignedAngle(grabPointDir, handtipDir, transform.forward);
                    angluarSpeed = angle/Time.deltaTime;
                    transform.rotation *= Quaternion.Euler(0,0,angle);
                }
                else{
                    transform.rotation *= Quaternion.Euler(0,0,angluarSpeed*Time.deltaTime);

                    if(Mathf.Abs(angluarSpeed)>state_transitionSpeed) {
                        transitionTimer += Time.deltaTime;
                        if(transitionTimer>state_transitionTime){
                            m_sprite.sortingLayerName = "Default";
                            m_sprite.sortingOrder = 0;
                            cd_state = CD_STATE.ROTATING;
                            
                            if(!music_good_source.isPlaying)music_good_source.Play();
                            if(!music_bad_source.isPlaying)music_bad_source.Play();


                            if(angluarSpeed>0) particleForceField.rotationSpeed = -2;
                            else particleForceField.rotationSpeed = 2;
                        }
                    }
                    else{
                        transitionTimer = 0;
                    }
                }
                break;
            case CD_STATE.ROTATING:
                if(isControlling){
                    lightningParticle.transform.position = PointClick_InteractableHandler.tipPos;
                    if(!lightningParticle.isPlaying){
                        lightningParticle.Play(true);
                    }
                    music_good_source.volume = Mathf.Lerp(music_good_source.volume, 1, Time.deltaTime * 10);
                    music_bad_source.volume = Mathf.Lerp(music_bad_source.volume, 0, Time.deltaTime * 10);
                }
                else{
                    if(lightningParticle.isPlaying){
                        lightningParticle.Stop(true);
                    }
                    music_bad_source.volume = Mathf.Lerp(music_bad_source.volume, 0.75f, Time.deltaTime * 10);
                    music_good_source.volume = Mathf.Lerp(music_good_source.volume, 0, Time.deltaTime * 10);
                }
                transform.rotation *= Quaternion.Euler(0,0,angluarSpeed*Time.deltaTime);
                break;
        }
    }
    public override void OnHover(bool isTouching, PointClick_InteractableHandler interactableHandler)
    {
        base.OnHover(isTouching, interactableHandler);
        if(isTouching){
            switch(cd_state){
                case CD_STATE.ROTATING:
                    isControlling = true;
                    sfx_source.PlayOneShot(sfx_dj_in);

                    EventHandler.Call_OnFeelWords(lyrics);
                    break;
            }
        }
    }
    public override void OnExitHover()
    {
        base.OnExitHover();
        switch(cd_state){
            case CD_STATE.ROTATING:
                if(isControlling){
                    isControlling = false;
                    sfx_source.PlayOneShot(sfx_dj_out);
                }
                break;
        }
    }
    public override void OnClick(PointClick_InteractableHandler interactableHandler)
    {
        base.OnClick(interactableHandler);
        switch(cd_state){
            case CD_STATE.IDLE:
                grabPoint = transform.InverseTransformPoint(PointClick_InteractableHandler.grabPos);

                m_sprite.sortingLayerName = "Hand";
                m_sprite.sortingOrder = 2;
                isControlling = true;
                interactableHandler.HoldTheInteractable(this);
                interactableHandler.GrabOnToPoint(transform.position, cd_radius);
                EventHandler.Call_OnFeelWords(feels);
                break;
            case CD_STATE.ROTATING:
                isControlling = true;
                sfx_source.PlayOneShot(sfx_dj_in);
                interactableHandler.HoldTheInteractable(this);
                EventHandler.Call_OnFeelWords(lyrics);
                break;
        }
    }
    public override void OnRelease(PointClick_InteractableHandler interactableHandler)
    {
        base.OnRelease(interactableHandler);
        switch(cd_state){
            case CD_STATE.IDLE:
                isControlling = false;
                m_sprite.sortingLayerName = "Default";
                m_sprite.sortingOrder = 0;
                interactableHandler.ReleaseGrab();
                break;
            case CD_STATE.ROTATING:
                lightningParticle.Stop(true);
                if(isControlling){
                    isControlling = false;
                    sfx_source.PlayOneShot(sfx_dj_out);
                }
                break;
        }

    }
}
