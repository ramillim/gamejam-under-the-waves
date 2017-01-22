using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
    public CanvasGroup winScreen;
    public CanvasGroup loseScreen;

    void Start()
    {
        if (GameManager.Instance.IsWon)
        {
            winScreen.alpha = 1;
            loseScreen.alpha = 0;
        }
        else
        {
            loseScreen.alpha = 1;
            winScreen.alpha = 0;
        }
    }
}
