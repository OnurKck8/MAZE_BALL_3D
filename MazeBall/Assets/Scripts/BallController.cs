using UnityEngine;
using TMPro;
public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 2f;
    
    bool isGameOver = false;
    bool isWin = false;


    float healthcounter = 20;
    float timecounter = 300;

    [Header("UI Text")]
    public TextMeshProUGUI time, health, state;


    public Button restartBtn;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        time.text = timecounter.ToString();
        health.text = healthcounter.ToString();
       
    }

    // Update is called once per frame

    void Update()
    {
        if (timecounter <= 0 || healthcounter <= 0)
        {
            isGameOver = true;
        }

        if (!isGameOver && !isWin)
        {
            timecounter -= Time.deltaTime;
            time.text = (int)timecounter + "";
        }

        else if (!isWin)
        {
            state.text = "Game Over";
            restartBtn.gameObject.SetActive(true);
        }

    }

    void FixedUpdate()
    {
        if(!isGameOver && !isWin)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 force = new Vector3(vertical, 0, -horizontal);

            rb.AddForce(force * speed);
        }

        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
       

    }


    public void OnCollisionEnter(Collision other)
    {
        string objName = other.gameObject.name;

        if(objName.Equals("Finish"))
        {
            isWin = true;
            state.text = "Congratulations";
            restartBtn.gameObject.SetActive(true);
        }

        if(other.gameObject.CompareTag("Wall"))
        {
            healthcounter--;
            health.text = healthcounter + "";
        }
    }
}
