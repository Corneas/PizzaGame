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

        finishedPizzaText.text = "�ϼ��� ���� : " + finishedPizza.ToString();
        fireballText.text = "���� ȭ���� ���� : " + fireballCount.ToString();
        doughCountText.text = "���� �� �� : " + gameManager.curDoughCount.ToString();
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
