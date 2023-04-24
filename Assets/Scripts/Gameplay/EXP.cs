using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : MonoBehaviour
{
    public float expValue;
    public bool hasBeenCollected = false;

    public Sprite smallEXP;
    public float smallEXPThreshold = 5;

    public Sprite mediumEXP;
    public float mediumEXPThreshold = 25;    

    public Sprite bigEXP;

    private SpriteRenderer renderer;

    public void Awake() {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(Vector3 pos, float expValue) {
        hasBeenCollected = false;
        this.expValue = expValue;
        renderer.enabled = true;
        gameObject.SetActive(true);
        gameObject.transform.position = pos;
        SetupSprite();
    }

    public void Pool() {
        hasBeenCollected = true;
        renderer.enabled = false;
        gameObject.SetActive(false);
        EXPPoolManager.instance.AddToPool(this);
    }





    private void SetupSprite() {
        //print("Setting up sprite with exp val " + expValue);
        if(expValue < smallEXPThreshold)
        {
             renderer.sprite = smallEXP;
        }
        else if (expValue < mediumEXPThreshold)
        {
            renderer.sprite = mediumEXP;
        }
        else { renderer.sprite = bigEXP; }
    }

}
