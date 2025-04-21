using Invector.vCharacterController;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VehicleEnterExit : MonoBehaviour
{
    public Transform seatPosition; // Assign a seat position in inspector
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public string playerTag = "Player";

    private GameObject player;
    private bool isPlayerNearby = false;
    private bool isInVehicle = false;
    private Button enterButton;
    private AudioSource audioSource;

    private Animator playerAnimator;
    private vThirdPersonController playerController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        enterButton = GameObject.Find("EnterButton")?.GetComponent<Button>();
        if (enterButton != null)
        {
            enterButton.gameObject.SetActive(false);
            enterButton.onClick.AddListener(ToggleVehicle);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            player = other.gameObject;
            isPlayerNearby = true;
            playerAnimator = player.GetComponent<Animator>();
            
           playerController = player.GetComponent<vThirdPersonController>();

            if (enterButton != null)
                enterButton.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNearby = false;
            if (!isInVehicle && enterButton != null)
                enterButton.gameObject.SetActive(false);
        }
    }

    void ToggleVehicle()
    {
        if (!isInVehicle)
            StartCoroutine(EnterVehicle());
        else
            StartCoroutine(ExitVehicle());
    }

    IEnumerator EnterVehicle()
    {
        if (player == null || seatPosition == null) yield break;

        playerController.lockMovement = true;
        playerController.lockRotation = true;
        playerController.GetComponent<CapsuleCollider>().enabled = false;
        playerController.GetComponent<Rigidbody>().isKinematic = true;
        playerController.gameObject.SetActive(false);
        playerController.transform.localPosition = seatPosition.position;
        playerController.transform.rotation = seatPosition.rotation;
        playerController.gameObject.SetActive(true);
        playerAnimator.Play("EnterVehicle");
        // Step 2: Play sit animation
        //  playerAnimator.SetTrigger("Sit");

        yield return new WaitForSeconds(0.5f); // Wait for animation sync

        // Step 3: Parent the player to vehicle
        player.transform.SetParent(seatPosition);

        // Play door sound
        if (doorOpenSound != null)
            audioSource.PlayOneShot(doorOpenSound);

        isInVehicle = true;
        if (enterButton != null)
            enterButton.GetComponentInChildren<Text>().text = "Exit";
    }

    IEnumerator ExitVehicle()
    {
        if (player == null) yield break;

        // Play stand animation
        playerAnimator.SetTrigger("Stand");

        yield return new WaitForSeconds(0.5f); // Wait for stand animation

        // Unparent and reposition player just outside
        player.transform.SetParent(null);
        player.transform.position = transform.position + transform.right * 2f;
        playerController.enabled = true;

        // Play door sound
        if (doorCloseSound != null)
            audioSource.PlayOneShot(doorCloseSound);

        isInVehicle = false;
        if (enterButton != null)
            enterButton.GetComponentInChildren<Text>().text = "Enter";
    }
}
