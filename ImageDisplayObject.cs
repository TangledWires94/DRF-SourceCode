using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplayObject : MonoBehaviour
{
    public Sprite displaySprite;
    public Image displayImage;

    public void ToggleImage(bool state)
    {
        displayImage.gameObject.SetActive(state);
    }

    public void ChangeSprite()
    {
        displayImage.sprite = displaySprite;
    }
}
