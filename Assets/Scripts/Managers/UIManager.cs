using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] Image toppingIcon;
    [SerializeField] Sprite[] toppingImage;
    [SerializeField] TextMeshProUGUI finishedPizzaText;
    [SerializeField] TextMeshProUGUI fireballText;
    [SerializeField] TextMeshProUGUI doughCountText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] Slider playerHP;
    public Image skillCoolDownImage;

    [SerializeField]
    private TextMeshProUGUI countDownText;
    public float countDownTimer { private set; get; } = 120f;

    private BulletType bulletType = null;
    public int finishedPizza = 0;
    public int fireballCount = 10;

    private GameManager gameManager = null;
    private PlayerAttack playerAttack = null;
    private PlayerController playerController = null;

    private bool isPause = false;

    private void Start()
    {
        bulletType = FindObjectOfType<BulletType>();
        gameManager = FindObjectOfType<GameManager>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        ChangeImage();

        playerHP.value = playerController.hp * 0.05f;

        finishedPizzaText.text = "완성된 피자 : " + finishedPizza.ToString();
        fireballText.text = "남은 화염구 개수 : " + fireballCount.ToString();
        doughCountText.text = "현재 적 수 : " + gameManager.curDoughCount.ToString();

        skillCoolDownImage.fillAmount = (playerAttack.skillDelay - playerAttack.curDelay) / playerAttack.skillDelay;
        skillCoolDownImage.fillAmount = (playerAttack.skillDelay - playerAttack.curDelay) / playerAttack.skillDelay;
        skillCoolDownImage.fillAmount = (playerAttack.skillDelay - playerAttack.curDelay) / playerAttack.skillDelay;

        if (gameManager.isBossSpawn)
        {
            if (countDownTimer <= 0)
            {
                ActiveGameOverPanel();
            }
            countDownText.gameObject.SetActive(true);

            countDownTimer -= Time.deltaTime;
            countDownText.text = $"{countDownTimer:F2}";
        }
        else
        {
            countDownTimer = 120f;
            countDownText.gameObject.SetActive(false);
        }


        if(playerController.hp <= 0)
        {
            ActiveGameOverPanel();
        }
        TogglePausePanel();
    }

    void TogglePausePanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            pausePanel.SetActive(!pausePanel.activeSelf);
            if (isPause)
            {
                Time.timeScale = 0f;
            }
            else if(!isPause)
            {
                Time.timeScale = 1f;
            }

            MouseManager.Show(isPause);
            MouseManager.Lock(!isPause);
        }
    }

    void ChangeImage()
    {
        switch (bulletType.curToppingType)
        {
            case ToppingType.Cheese:
                toppingIcon.sprite = toppingImage[0];
                break;
            case ToppingType.Pepperoni:
                toppingIcon.sprite = toppingImage[1];
                break;
            case ToppingType.Tomato:
                toppingIcon.sprite = toppingImage[2];
                break;
        }
    }

    void ActiveGameOverPanel()
    {
        gameManager.SendMessage("GameOver");
        gameOverPanel.SetActive(true);
    }

    public void Resume()
    {
        isPause = false;
        Time.timeScale = 1f;
        MouseManager.Show(false);
        MouseManager.Lock(true);
        pausePanel.SetActive(false);
    }

    public void Home()
    {
        Time.timeScale = 1f;
        MouseManager.Show(true);
        MouseManager.Lock(false);
        SceneManager.LoadScene("TitleScene");
    }

}
