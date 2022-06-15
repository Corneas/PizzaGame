using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image toppingIcon;
    [SerializeField] Sprite[] toppingImage;

    private BulletType bulletType = null;

    private void Start()
    {
        bulletType = FindObjectOfType<BulletType>();
    }

    private void Update()
    {
        ChangeImage();
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
