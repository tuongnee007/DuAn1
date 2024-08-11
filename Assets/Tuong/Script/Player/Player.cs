using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Tốc độ, độ cao,trái phải")]
    public float runSpeed = 8f;
    public float highJump = 8f;
    private Rigidbody2D rb;
    private Animator anim;
    public bool IsFacingRight = true;
    private bool grounded = true;
    private bool doubleJump;
    //Hiệu ứng
    public ParticleSystem dust;
    public ParticleSystem flashSmoke;
    public ParticleSystem doubleJumpEffect;
    private float horizontalInput;
    public AudioSource runEffect;
    //express
    [Header("Tốc hành")]
    public float tagertRunSpeed;
    public float timetoChangeSpeed;
    //private bool isCollingdown = false;
    public float coolDownTime;
    private float coolDownTimer;
    //text
    public TMP_Text cooldownText;
    public TMP_Text express;
    //flash
    [Header("Tốc biến")]
    public Transform teleportTagert;
    public float teleportCooldown;
    private float cooldownTimer2;
    private bool canTeleport = true;
    public AudioSource flashAudio;
    [Header("Dịch chuyển")]
    public Transform recallTagert;
    public float recallTime;
    private bool isRicalling = false;
    public TMP_Text recallText;
    private float recallTimer;
    public SpriteRenderer recallSprire;
    // NÂng cấp tốc chạy
    public float speedUpdateCost = 75f;
    public int upgradeLevel = 1;
    public float maxUpgradeLevel = 10f;
    private float initialRunSpeed;
    // Kill
    public TMP_Text enemyDeathCountText;
    //Update Text
    public TMP_Text updateSpeedText;
    private void Awake()
    {
        flashAudio.Stop();
        runEffect.Stop();
        initialRunSpeed = runSpeed;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coolDownTimer = coolDownTime;
        cooldownTimer2 = teleportCooldown;
        recallTimer = recallTime;
        recallSprire.gameObject.SetActive(false);
        UpdateSpeedText();
        LoadPlayerStats();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleSpeedBost();
        HandleTeleport();
        HandleRecall();
        anim.SetBool("run", Mathf.Abs(horizontalInput) > 0);
        anim.SetBool("grounded", grounded);
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        float targetSpeed = runSpeed * horizontalInput;
        float speedDifference = targetSpeed - rb.velocity.x;
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? 10f : 20f;
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, 0.9f) * Mathf.Sign(speedDifference);
        rb.AddForce(movement * Vector2.right);
        if (horizontalInput != 0)
        {
            flip();
        }
    }
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                Jump();
            }
            else if (!doubleJump)
            {
                Jump();
                DoubleJumpEffect();
                doubleJump = true;
            }
        }
    }
    private void HandleSpeedBost()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ChangeSpeedOverTime(tagertRunSpeed));
            StartCoroutine(CoolldownTime(coolDownTime));
        }       
    }
    private void HandleTeleport()
    {
        if (Input.GetMouseButtonDown(1) && canTeleport)
        {
            Teleport();
            FlashAudio();
            FlashSmoke();
        }
    }
    private void HandleRecall()
    {
        if (Input.GetKey(KeyCode.V) && !isRicalling)
        {
            StartCoroutine(Recall());
        }
    }
    void flip()
    {
        if ((IsFacingRight && horizontalInput < 0) || (!IsFacingRight && horizontalInput > 0))
        {
            IsFacingRight = !IsFacingRight;
            Vector3 size = transform.localScale;
            size.x *= -1;
            transform.localScale = size;
            if (grounded)
            {
                CreateDust();
            }
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, highJump);
        anim.SetTrigger("jump");
        grounded = false;
        CreateDust();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
            doubleJump = false;
        }
    }
    private void CreateDust()
    {
        dust.Play();
    }
    private void FlashSmoke()
    {
        flashSmoke.Play();
    }
    private void DoubleJumpEffect()
    {
        doubleJumpEffect.Play();
    }    
    private IEnumerator ChangeSpeedOverTime(float targetSpeed)
    {
        float intialSpeed = runSpeed;
        runEffect.Play();
        runSpeed = targetSpeed;
        yield return new WaitForSeconds(timetoChangeSpeed);
        runSpeed = intialSpeed;
    }

    private IEnumerator CoolldownTime(float coolDown)
    {

        coolDownTimer = coolDown;
        while (coolDownTimer > 0)
        {
            cooldownText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            coolDownTimer -= 1f;
            cooldownText.gameObject.SetActive(false);
            UpdateCooldownText();
        }

        coolDownTimer = 0;
        UpdateCooldownText();
    }
    private void UpdateCooldownText()
    {
        if (cooldownText != null)
        {
            cooldownText.text = Mathf.Max(0, coolDownTimer).ToString();
        }
    }
    private void Teleport()
    {
        if (teleportTagert != null)
        {
            transform.position = teleportTagert.position;
            canTeleport = false;
            StartCoroutine(TeleportCooldown());
        }
    }
    private IEnumerator TeleportCooldown()
    {
        express.gameObject.SetActive(true);
        cooldownTimer2 = teleportCooldown;
        while (cooldownTimer2 > 0)
        {
            yield return new WaitForSeconds(1);
            cooldownTimer2 -= 1f;
            UpdateTeleportCooldownText();
        }
        canTeleport = true;
        express.gameObject.SetActive(false);
    }
    private void UpdateTeleportCooldownText()
    {
        if (express != null)
        {
            express.text = Mathf.Max(0, cooldownTimer2).ToString();
        }
    }
    private IEnumerator Recall()
    {
        isRicalling = true;
        recallText.gameObject.SetActive(true);
        recallSprire.gameObject.SetActive(true);
        recallTimer = recallTime;
        while (recallTimer > 0)
        {
            yield return new WaitForSeconds(0.1f);
            recallTimer -= 0.1f;
            displacementUpdate();
        }
        transform.position = recallTagert.position;
        isRicalling = false;
        recallText.gameObject.SetActive(false);
        recallSprire.gameObject.SetActive(false);
    }
    private void displacementUpdate()
    {
        if (recallText != null)
        {
            recallText.text = Mathf.Max(0, recallTimer).ToString();
        }
    }
    public void FlashAudio()
    {
        flashAudio = GetComponent<AudioSource>();
        flashAudio.volume = 1f;
        flashAudio.Play();
    }

    public void UpgradeSpeed()
    {
        if (upgradeLevel >= maxUpgradeLevel)
        {
            return;
        }
        float cost = GetUpgradeCost();
        PlayerAttack2 playerAttack2 = FindObjectOfType<PlayerAttack2>();
        if (playerAttack2 != null && playerAttack2.score >= cost)
        {
            float speedIncrease = GetSpeedIncrease();
            runSpeed += speedIncrease;
            playerAttack2.AddScore(-cost);
            upgradeLevel++;
            UpdateSpeedText();
            SavePlayerStats();
        }
    }
    public void HackUpdateSpeed()
    {
        float cost = GetUpgradeCost();
        PlayerAttack2 playerAttack2 = FindObjectOfType<PlayerAttack2>();
        if (playerAttack2 != null && playerAttack2.score >= cost)
        {
            float healthIncrease = GetSpeedIncrease();
            runSpeed += healthIncrease;
            playerAttack2.AddScore(-cost);
            upgradeLevel++;
            UpdateSpeedText();
        }
    }
    public void BloodSpeed()
    {
        if (runSpeed > 0)
        {
                runSpeed -= GetSpeedIncrease();
                upgradeLevel--;
                UpdateSpeedText();
        }
    }
    public void hypotension()
    {
        if(runSpeed > 0)
        {
            if(upgradeLevel > 1)
            {
                runSpeed -= GetSpeedIncrease();
                upgradeLevel--;
                UpdateSpeedText();
                SavePlayerStats();
                }
            else
            {
                runSpeed = initialRunSpeed;
                UpdateSpeedText();
                SavePlayerStats();
            }
        }
    }

    private float GetUpgradeCost()
    {
        if (upgradeLevel >= 1 && upgradeLevel <= 3)
        {
            return speedUpdateCost * upgradeLevel;
        }
        else if (upgradeLevel == 4 || upgradeLevel == 5)
        {
            return speedUpdateCost * 3;
        }
        else if (upgradeLevel >= 6 && upgradeLevel <= 8)
        {
            return speedUpdateCost * 5;
        }
        else if (upgradeLevel == 9)
        {
            return speedUpdateCost * 7;
        }
        else if (upgradeLevel == 10)
        {
            return speedUpdateCost * 8;
        }
        return speedUpdateCost;
    }

    private float GetSpeedIncrease()
    {
        return 2f;
    }

    private void UpdateSpeedText()
    {
        updateSpeedText.text = $"Speed: {runSpeed}";
        SavePlayerStats();
    }

    private void SavePlayerStats()
    {
        PlayerPrefs.SetFloat("PlayerSpeed", runSpeed);
        PlayerPrefs.SetInt("UpgradeLevel", upgradeLevel);
    }

    private void LoadPlayerStats()
    {
        runSpeed = PlayerPrefs.GetFloat("PlayerSpeed", runSpeed);
        upgradeLevel = PlayerPrefs.GetInt("UpgradeLevel", 1);
    }

}
