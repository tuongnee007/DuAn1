using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
public class GameIntegrationTest 
{
    //Kiểm thử tích hợp
    private GameManagerTwo gameManagerTwo;
    private UIManager uiManager;
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
}
