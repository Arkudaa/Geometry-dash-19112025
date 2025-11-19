using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rig;
    public float speed;
    public float jumpspeed;
    private bool isJumping=false;
    public Transform GroundDetector;
    public LayerMask groundMask;
    public float groundRadius;
    public AudioClip jumpSound;
    public AudioClip collectSound;
    public AudioClip dangerSound;
    public AudioSource sounds;
    public int wallet;
    public Text walletText;
    public GameObject coinSparkles;
    public TextMeshProUGUI winText;
    public float winTextDuration;
    public float delayBeforeNextLevel;




 


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }



    void Start()
    {
        walletText.text = "My Score: " + wallet;
        winText.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            
            Jump();
           
        }

    }

    private void FixedUpdate()
    {
        //rig.velocity = new Vector2(speed,rig.velocity.y);
        Vector2 velocity = rig.velocity;
        velocity.x = speed;
        if (isJumping)
        {
            sounds.PlayOneShot(jumpSound);
            velocity.y = jumpspeed;
            isJumping = false;
        }
       
        rig.velocity = velocity;

        

    }
     
    public void Jump()
    {   if(isGrounded())
        isJumping = true;
    }


    


    public bool isGrounded()
    {
         Collider2D ground=Physics2D.OverlapCircle(GroundDetector.position, groundRadius, groundMask);
        return ground != null;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "danger")
        {
            GetComponent<TrailRenderer>().enabled = false;
           StartCoroutine(RestartLevel());
           
        }

        else if (collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
            Instantiate(coinSparkles, collision.transform.position, Quaternion.identity);
            wallet++;
            walletText.text = "My Score: " + wallet;
            sounds.PlayOneShot(collectSound);


        }

        else if (collision.gameObject.tag == "end")
        {
            //SceneManager.LoadScene(0);
            StartCoroutine(LevelComplete());
        }

    }


    IEnumerator LevelComplete()
    {
        winText.gameObject.SetActive(true);
        winText.alpha = 0;
        for(float i=0;i<3; i += Time.deltaTime * 2f)
        {
            winText.alpha +=0.03f;
           yield return null;

        }


      
    }


   IEnumerator RestartLevel()

    {
       
        Time.timeScale = 0;
        sounds.ignoreListenerPause = true;
        //sounds.PlayOneShot(dangerSound);
      
        yield return new WaitForSecondsRealtime(2f);
       
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }



}
