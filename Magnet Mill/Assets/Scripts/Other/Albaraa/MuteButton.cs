using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Button fakeButton;
    public Sprite offSprite;
    private Sprite onSprite;
    private Image thisImg;
    private Button thisBtn;
    void Start()
    {
        thisImg = GetComponent<Image>();
        thisBtn = GetComponent<Button>();
        onSprite = thisImg.sprite;
        SetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if(ScenesLoader.instance.IsTransitioning() && thisBtn.IsInteractable()) //disable button while scene is transitioning
        {
            thisBtn.interactable = false;
        }
        else if(!ScenesLoader.instance.IsTransitioning() && !thisBtn.IsInteractable())
        {
            thisBtn.interactable = true;
        }
    }

    public void MuteClicked() //this function will be called when the button clicked
    {
        if(AudioManager.instance.IsMuted())
        {
            AudioManager.instance.UnMuteAll();
        }
        else
        {
            AudioManager.instance.MuteAll();
        }
        SetSprite();
        fakeButton.Select();    //click on the fake button just to reset the click event of the mute button
    }

    private void SetSprite()    //this function will switch the sprites of the mute button depeding if the sounds are muted or not
    {
        if (AudioManager.instance.IsMuted())
        {
            thisImg.sprite = offSprite;
        }
        else
        {
            thisImg.sprite = onSprite;
        }
    }
}
