using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Image selection;
    [SerializeField] TextMeshProUGUI gemTextHud;
    [SerializeField] Slider hpHud;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        UpdateGem();
    }

    public void SetSelectionPos(int yPos)
    {
        selection.rectTransform.anchoredPosition = new Vector2(selection.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGem()
    {
        gemTextHud.text = GameManager.instance.gem.ToString() + " G";
    }

    public void UpdateHealth()
    {
        hpHud.value = GameManager.instance.health * 1f / GameManager.instance.healthMax;
    }
}
