using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private CapsuleCollider col;
    public InfernAds infernAds;
    private Animator anim;
    private Score score;
    private Vector3 dir;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private int coins;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private Text coinsText;
    [SerializeField] private Score scoreScript;

    private bool dive;
    private bool isImmortal;

    private int lineToMove = 1;
    public float lineDistance = 4;
    private int tryCount;
    private float maxSpeed = 110;

    void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        controller = GetComponentInChildren<CharacterController>();
        col = GetComponentInChildren<CapsuleCollider>();
        score = scoreText.GetComponent<Score>();
        score.scoreMultiplier = 1;
        Time.timeScale = 1;
        coins = PlayerPrefs.GetInt("coins");
        coinsText.text = coins.ToString();
        StartCoroutine(SpeedIncrease());
        isImmortal = false;

        tryCount = PlayerPrefs.GetInt("tryCount");
    }

    void Update()
    {
        if (SwapController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }

        if (SwapController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if (SwapController.swipeUp)
        {
            if (controller.isGrounded)
                Jump();
        }

        if (SwapController.swipeDown)
        {
            StartCoroutine(Slide());
        }

        if (controller.isGrounded && !dive)
            anim.SetBool("run", true);
        else
            anim.SetBool("run", false);

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void Jump()
    {
        dir.y = jumpForce;
        anim.SetTrigger("jump");
    }
    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {

            

            if (isImmortal)
                Destroy(hit.gameObject);
            else
            {
                tryCount++;
                PlayerPrefs.SetInt("tryCount", tryCount);
                if (tryCount % 2 == 0)
                    infernAds.ShowAd();
                losePanel.SetActive(true);
                int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
                PlayerPrefs.SetInt("lastRunScore", lastRunScore);
                Time.timeScale = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            coins++;
            PlayerPrefs.SetInt("coins", coins);
            coinsText.text = coins.ToString(); //coins.ToString
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "BonusStar")
        {
            StartCoroutine(StarBonus());
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "BonusShield")
        {
            StartCoroutine(ShieldBonus());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(1);

        if (speed < maxSpeed)
        {
            speed += 1;
            StartCoroutine(SpeedIncrease());
        }
    }

    private IEnumerator Slide()
    {
        col.center = new Vector3(0.43f, -1.198786f, -0.02000001f);
        col.height = 1;
        dive = true;
        anim.SetTrigger("dive");

        yield return new WaitForSeconds(1);

        col.center = new Vector3(0f, 0.8779559f, 0.1141407f);
        col.height = 1.895343f;
        dive = false;
    }

    private IEnumerator StarBonus()
    {
        score.scoreMultiplier = 2;

        yield return new WaitForSeconds(5);

        score.scoreMultiplier = 1;
    }

    private IEnumerator ShieldBonus()
    {
        isImmortal = true;

        yield return new WaitForSeconds(5);

        isImmortal = false;
    }
}
