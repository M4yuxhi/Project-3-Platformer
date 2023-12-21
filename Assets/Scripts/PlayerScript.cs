using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotationAngle;
    private Transform cameraTransform;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    private float longjumpForce;
    [SerializeField] private Collider checkGroundCollider;

    [Header("UI")]
    [SerializeField] private PlayerUIScript playerUI;
    private Color damageCDColor = Color.red;
    private Color initialColor = Color.white;
    private MeshRenderer meshRenderer;

    [Header("ViewCamera")]
    [SerializeField] private ViewCameraScript viewCamera;
    [SerializeField] private float rotationAngleVC;

    [Header("SoundController")]
    [SerializeField] private SoundController collectablesSC;
    public SoundController actionsSC;
    public SoundController musicSC;

    private int hp = 3;
    private bool DamageCooldown;
    private const float DamageCooldownTime = 5;
    private float DamageCooldownCounter;

    private Rigidbody rb;
    private List<Collider> groundColliders = new List<Collider>();

    private int goldCoinCount = 0;
    private int greenCoinCount = 0;

    private float coyoteTime = 2f;
    private float coyoteTimeCounter;

    private bool initialized = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = viewCamera.gameObject.transform;
        longjumpForce = (jumpForce * 75);

        meshRenderer = GetComponent<MeshRenderer>();
        initialColor = meshRenderer.material.color;
        musicSC.PlayAudioClip("normalMusic", true, 0.2f * Globals.Sound.VolMusic, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized) InitializeValuesUI();

        ProcessInput();
        
        if (DamageCooldown)
        {
            DamageCooldownCounter += Time.deltaTime;
            meshRenderer.material.color = damageCDColor;

            if(DamageCooldownCounter > DamageCooldownTime) 
            {
                DamageCooldown = false;
                DamageCooldownCounter = 0;
                meshRenderer.material.color = initialColor;
            }
        }
        
        if(hp <= 0)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private void InitializeValuesUI()
    {
        playerUI.UpdateGoldCoinCounter(goldCoinCount);
        playerUI.UpdateGreenCoinCounter(greenCoinCount);
        playerUI.UpdateHPCounter(hp);
        initialized = true;
    }

    public void Move(Vector3 deltaVec)
    {
        transform.position += deltaVec;
    }

    private void ProcessInput()
    {
        bool pause = Input.GetKeyDown(KeyCode.P);

        if (pause)
        {
            if (Globals.Pause) actionsSC.PlayAudioClip("pause", false, 1f, 3);
            else actionsSC.PlayAudioClip("unpause", false, 1f, 3);

            Globals.ChangeConditionPause();
            playerUI.pauseController.gameObject.SetActive(Globals.Pause);

            if (!Globals.Pause)
            {
                playerUI.pauseController.SetActiveHowToPlayParent(Globals.Pause);
            }
        }

        Vector3 movement = Vector3.zero;
        float asignedSpeed = 0;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            asignedSpeed = runSpeed;
        }
        else if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            asignedSpeed = speed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement += cameraTransform.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement -= cameraTransform.forward;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement -= cameraTransform.right;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement += cameraTransform.right;
        }
        if (!Globals.Pause)
        {
            if (Input.GetKey(KeyCode.I))
            {
                viewCamera.Rotate(0, rotationAngleVC, transform.position);
            }
            if (Input.GetKey(KeyCode.K))
            {
                viewCamera.Rotate(0, -rotationAngleVC, transform.position);
            }
            if (Input.GetKey(KeyCode.J))
            {
                viewCamera.Rotate(1, rotationAngleVC, transform.position);
            }
            if (Input.GetKey(KeyCode.L))
            {
                viewCamera.Rotate(1, -rotationAngleVC, transform.position);
            }
        }

        movement.y = 0;
        movement.Normalize();
        movement *= asignedSpeed * Time.deltaTime;
        Quaternion PrevRot = transform.rotation;
        transform.LookAt(transform.position + movement);
        Quaternion ObjRot = transform.rotation;
        transform.rotation = Quaternion.RotateTowards(PrevRot, ObjRot, Time.deltaTime * 1000);

        transform.position += movement;

        //Jump
        bool canJump = false;
        foreach (Collider groundCollider in groundColliders)
        {
            if (groundCollider != null && checkGroundCollider.bounds.Intersects(groundCollider.bounds))
            {
                canJump = true;
                break;
            }
        }
        if (canJump)
        {
            coyoteTimeCounter = coyoteTime;
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Vector3 currentVelocity = rb.velocity;
                currentVelocity.x = movement.x * longjumpForce;
                currentVelocity.y = jumpForce / 2;
                currentVelocity.z = longjumpForce * movement.z;

                rb.velocity = currentVelocity;
                coyoteTimeCounter = 0;
                actionsSC.PlayAudioClip("jump", false, 0.1f, 1);
            }
        }
        else
        {
            if (coyoteTimeCounter > 0) coyoteTimeCounter -= Time.deltaTime;
        }

        if (canJump || coyoteTimeCounter > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print(canJump);
                coyoteTimeCounter = 0;
                print(coyoteTimeCounter);
                Vector3 currentVelocity = rb.velocity;
                currentVelocity.y = jumpForce;
                rb.velocity = currentVelocity;
                actionsSC.PlayAudioClip("jump", false, 0.1f, 1);
            }
        }
        else
        {
            if (rb.velocity.y < 0)
            {
                rb.AddForce(100f * rb.mass * Time.deltaTime * Physics.gravity);
            }
        }
    }

    float dy = 0;

    private void OnCollisionEnter(Collision other)
    {
        if (!groundColliders.Contains(other.collider))
        {
            groundColliders.Add(other.collider);
        }
        if(other.gameObject.CompareTag("Enemigo") && !DamageCooldown)
        {
            foreach (ContactPoint contactPoint in other.contacts)
            {
                if (contactPoint.normal.y > dy)
                {
                    hp--;
                    DamageCooldown = true;
                    playerUI.UpdateHPCounter(hp);
                    break;
                }
            }
        }

    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Enemigo") && !DamageCooldown)
        {
            foreach (ContactPoint contactPoint in other.contacts)
            {
                if (contactPoint.normal.y > dy)
                {
                    hp--;
                    DamageCooldown = true;
                    actionsSC.PlayAudioClip("falling", false, 0.2f * Globals.Sound.VolFX, 2);
                    playerUI.UpdateHPCounter(hp);
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;

        if (otherGO.CompareTag("GoldCoin")) 
        {
            Destroy(otherGO);

            collectablesSC.PlayAudioClip("goldCoin", false, 0.6f, 1);
            goldCoinCount = LevelControllerScript.goldCoinsToCollect - LevelControllerScript.currentGoldCoins + 1;
            playerUI.UpdateGoldCoinCounter(goldCoinCount);
        }
        if (otherGO.CompareTag("GreenCoin")) 
        {
            Destroy(otherGO);

            greenCoinCount = LevelControllerScript.greenCoinsToCollect - LevelControllerScript.currentGreenCoins + 1;
            collectablesSC.PlayAudioClip("greenCoin" + greenCoinCount, false, 0.6f, 2);
            playerUI.UpdateGreenCoinCounter(greenCoinCount);
        }
        if (otherGO.CompareTag("MinigameBarrel"))
        {
            Destroy(otherGO);
        }
    }
}
