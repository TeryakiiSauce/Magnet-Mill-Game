using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Sprite onHighlighted;
    public Sprite offHighlighted;
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
        if(ScenesLoader.instance.IsTransitioning() && thisBtn.IsInteractable())
        {
            thisBtn.interactable = false;
        }
        else if(!ScenesLoader.instance.IsTransitioning() && !thisBtn.IsInteractable())
        {
            thisBtn.interactable = true;
        }
    }

    public void MuteClicked()
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
    }

    private void SetSprite()
    {
        SpriteState spState = thisBtn.spriteState;
        if (AudioManager.instance.IsMuted())
        {
            spState.highlightedSprite = offHighlighted;
            spState.pressedSprite = offHighlighted;
            thisImg.sprite = offSprite;
            thisBtn.spriteState = spState;
        }
        else
        {
            spState.highlightedSprite = onHighlighted;
            spState.pressedSprite = onHighlighted;
            thisImg.sprite = onSprite;
            thisBtn.spriteState = spState;
        }
    }
}
