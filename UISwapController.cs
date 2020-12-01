using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwapController : MonoBehaviour
{

    public Sprite swapFilled, swapEmpty;
    private Image swapImage;
    private DimensionSwap dimensionSwapHandler;
    private PlayerControl player;
    private int otherDimension;


    // Start is called before the first frame update
    void Start()
    {
        swapImage = GameObject.Find("Cursor").GetComponent<Image>();
        dimensionSwapHandler = GameObject.Find("Dimension Swap Handler").GetComponent<DimensionSwap>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dimensionSwapHandler.playerIsBlocked)
        {
            swapImage.sprite = swapEmpty;
        } else
        {
            swapImage.sprite = swapFilled;
        }
    }
}
