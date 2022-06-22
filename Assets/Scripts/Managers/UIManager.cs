using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] Image toppingIcon;
    [SerializeField] Sprite[] toppingImage;
    [SerializeField] TextMeshProUGUI finishedPizzaText;
    [SerializeField] TextMeshProUGUI fireballText;
    [SerializeField] TextMeshProUGUI doughCountText;

    private BulletType bulletType = null;
    public int finishedPizza = 0;
    public int fireballCount = 10;

    private GameManager gameManager = null;

    private void Start()
    {
        bulletType = FindObjectOfType<BulletType>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        ChangeImage();

        finishedPizzaText.text = "완성된 피자 : " + finishedPizza.ToString();
        fireballText.text = "남은 화염구 개수 : " + fireballCount.ToString();
        doughCountText.text = "현재 적 수 : " + gameManager.curDoughCount.ToString();
    }

    void ChangeImage()
    {
        switch (bulletType.curToppingType)
        {
            case ToppingType.Cheese:
                toppingIcon.sprite = toppingImage[0];
                break;
            case ToppingType.Onion:
                toppingIcon.sprite = toppingImage[1];
                break;
            case ToppingType.Tomato:
                toppingIcon.sprite = toppingImage[2];
                break;
        }
    }
}
