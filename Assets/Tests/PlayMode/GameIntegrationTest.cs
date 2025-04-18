using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
public class GameIntegrationTest 
{
    //Kiểm thử tích hợp
    private GameManagerTwo gameManagerTwo;
    private UIManager uiManager;
    private EnemyHealth enemyHealth;
    [SetUp]
    public void SetUp()
    {
        gameManagerTwo = new GameObject().AddComponent<GameManagerTwo>();
        uiManager = new GameObject().AddComponent<UIManager>();

        var scoreText = new GameObject().AddComponent<Text>();
        var livesText = new GameObject().AddComponent<Text>();

        uiManager.scoreText = scoreText;
        uiManager.livesText = livesText;

        gameManagerTwo.uiManager = uiManager;
        gameManagerTwo.UpdateUI();
    }

    [UnityTest]
    public IEnumerator TestScoreAndLivesUI()
    {
        Assert.AreEqual("Score: 0",uiManager.scoreText.text);
        Assert.AreEqual("Lives: 3", uiManager.livesText.text);

        gameManagerTwo.CollectItem(10);
        Assert.AreEqual("Score: 10", uiManager.scoreText.text);

        gameManagerTwo.PlayerDided();
        Assert.AreEqual("Lives: 2", uiManager.livesText.text);
        yield return null;
    }
    // Test Animator die and sound
    //[SetUp]
    //public void Setup()
    //{
    //    enemyHealth = new GameObject().AddComponent<EnemyHealth>();
    //    var animator = new GameObject().AddComponent<Animator>();
    //    enemyHealth.anim = animator;

    //}
    //[UnityTest]
    //public IEnumerator TestAnimatorAndSound()
    //{
    //    //Assert.AreEqual("");
    //    yield return null;
    //}
    //Parrel Testing    

    [UnityTest]
    [Category("PC")]
    public IEnumerator TestCollectItemOnPC()
    {
        gameManagerTwo.CollectItem(10);
        Assert.AreEqual("Score: 10", uiManager.scoreText.text);
        yield return null;
    }   
    [UnityTest]
    [Category("Mobile")]
    public IEnumerator TestCollectItemOnMobile()
    {
        gameManagerTwo.CollectItem(10);
        Assert.AreEqual("Score: 10", uiManager.scoreText.text);
        yield return null;
    }

    [UnityTest]
    [Category("PC")]
    public IEnumerator TestPlayerDidedOnPC()
    {
        gameManagerTwo.PlayerDided();
        Assert.AreEqual("Lives: 2", uiManager.livesText.text);
        yield return null;
    }   
    [UnityTest]
    [Category("Mobile")]
    public IEnumerator TestPlayerDidedOnMobile()
    {
        gameManagerTwo.PlayerDided();
        Assert.AreEqual("Lives: 2", uiManager.livesText.text);
        yield return null;
    } 

    [UnityTest]
    [Category("PC")]
    public IEnumerator TestPlayerAddDidedOnPC()
    {
        gameManagerTwo.Lives = 3;
        gameManagerTwo.PlayerAddDided();
        Assert.AreEqual("Lives: 4", uiManager.livesText.text);
        yield return null;
    }   
    [UnityTest]
    [Category("Mobile")]
    public IEnumerator TestPlayerAddDidedOnMobile()
    {
        gameManagerTwo.Lives = 3;
        gameManagerTwo.PlayerAddDided();
        Assert.AreEqual("Lives: 4", uiManager.livesText.text);
        yield return null;
    }
    //Line coverage
    [UnityTest]
    public IEnumerator TestAddPoint()
    {
        gameManagerTwo.CollectItem(50);
        Assert.AreEqual(50, gameManagerTwo.Score);
        gameManagerTwo.CollectItem(60);
        Assert.AreEqual(100, gameManagerTwo.Score);
        yield return null;
    }   
    //Branch coverage
    [UnityTest]
    public IEnumerator TestCheckGameState()
    {
        gameManagerTwo.Score = 100;
        Assert.AreEqual("Win", gameManagerTwo.CheckGameState());
        gameManagerTwo.Score = 50;
        Assert.AreEqual("Lose", gameManagerTwo.CheckGameState());
        yield return null;
    }  
    //Method coverage
    [UnityTest]
    public IEnumerator TestAddAndSubtractPoints()
    {
        gameManagerTwo.CollectItem(10);
        Assert.AreEqual(10, gameManagerTwo.Score);
        gameManagerTwo.SubtractPoints(5);
        Assert.AreEqual(5, gameManagerTwo.Score);
        yield return null;
    }

    //Test case hud chế độ play mode
    [UnityTest]
    public IEnumerator SliderPosition()
    {
        SceneManager.LoadScene("Map 1");
        yield return null;
        yield return new WaitForSeconds(1);

        var sliderGameObejct = GameObject.Find("Canvas/Slider all/SliderHealthPlayer");
        Assert.IsNotNull(sliderGameObejct, "Không tìm thấy Scene Map 1");

        var rt = sliderGameObejct.GetComponent<RectTransform>();
        Assert.IsNotNull(rt, "Không tìm thấy rect transform");

        Vector2 expectedAnchor = new Vector2(0.5f, 0.5f);
        Vector2 expectedAnchoeddPos = new Vector2(-778f, 504f);

        Assert.AreEqual(expectedAnchor, rt.anchorMin);
        Assert.AreEqual(expectedAnchor, rt.anchorMax);
        Assert.AreEqual(expectedAnchoeddPos, rt.anchoredPosition, "Slider nằm không đúng vị trí");
    }   
    //Test vị trí FlashPosition chế độ play mode
    [UnityTest]
    public IEnumerator FlashPosition()
    {
        SceneManager.LoadScene("Map 1");
        yield return null ;
        yield return new WaitForSeconds(1);

        var flashGo = GameObject.Find("Canvas/Skill all/Flash");
        Assert.IsNotNull(flashGo, "Không tìm thấy Scene Map 1");


        var rt = flashGo.GetComponent<RectTransform>();
        Assert.IsNotNull(rt, "Flash không có rect transform");

        Vector2 expectedAnchor = new Vector2(0.5f, 0.5f);
        Vector2 expectedAnchoeddPos = new Vector2(-490f, 508f);

        Assert.AreEqual(expectedAnchor, rt.anchorMin);
        Assert.AreEqual(expectedAnchor, rt.anchorMax);
        Assert.AreEqual(expectedAnchoeddPos, rt.anchoredPosition, "Flash nằm không đúng vị trí");
    }

    //Test TextSliderHealthPlayer chế độ play mode
    [UnityTest]
    public IEnumerator TextSliderHealthPlayer()
    {
        SceneManager.LoadScene("Map 1");
        yield return null ;
        yield return new WaitForSeconds(1);

        var textHealth = GameObject.Find("Canvas/Slider all/SliderHealthPlayer/HealthText");
        Assert.IsNotNull(textHealth, "Không tìm thấy healthText");


        var tx = textHealth.GetComponent<TextMeshProUGUI>();
        Assert.IsNotNull(tx, "Text health không có text");
        
        Assert.AreEqual("Health: 100%",tx.text, "% máu chưa hiển thị đúng");
    }

    //Test PlaySound chế độ play mode
    [UnityTest]
    public IEnumerator TestPlaySound()
    {
        SceneManager.LoadScene("Map 1");
        yield return null ;
        yield return new WaitForSeconds(1);

        var soundManager = GameObject.Find("Audio/SoundManager");
        Assert.IsNotNull(soundManager, "Không tìm thấy soundManger");

        var audio = soundManager.GetComponent<AudioSource>();
        Assert.IsNotNull(audio, "SoundManager không có audioSource");
        
        Assert.AreEqual(true,audio.playOnAwake, "audio chưa phát khi bắt đầu game");
        Assert.AreEqual(true, audio.loop, "audio chưa phát khi bắt đầu game");
    }
    //Test animation run chế độ play mode
    [UnityTest]
    public IEnumerator TestAnimationRun()
    {
        SceneManager.LoadScene("Map 1");
        yield return null ;
        yield return new WaitForSeconds(1);

        var player = GameObject.Find("Player 1");
        Assert.IsNotNull(player, "Không tìm thấy soundManger");

        var animator = player.GetComponent<Animator>();
        Assert.IsNotNull(animator, "Player không có animator");
        var rigibody2d = player.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rigibody2d, "Player không có rigibody2d");

        rigibody2d.velocity = new Vector2(5f, 0);
        float speed = rigibody2d.velocity.magnitude;
        animator.SetBool("run", speed > 0.1f);
        bool isRunning = animator.GetBool("run");
        Assert.AreEqual(true, isRunning, "animation run không chạy khi nhân vật di chuyển");
    }

    [UnityTest]
    public IEnumerator TestOpenPanelSetting()
    {
        SceneManager.LoadScene("Map 1");
        yield return null;
        yield return new WaitForSeconds(1);

        var button = GameObject.Find("Canvas/Setting Button");
        Assert.IsNotNull(button, "Không có button trong canvas");
        Button button2 = button.GetComponent<Button>();
        Assert.IsNotNull(button2, "Không có Button trong Scene");

        var panel = GameObject.Find("Canvas/Setting Button/Setting Panel");
        Assert.IsNotNull(panel, "Không có Panel trong Scene"); 
        button2.onClick.Invoke();
        Assert.IsTrue(panel.activeSelf, "Panel không được bật");
    }
}