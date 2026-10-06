using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 5f;
    public float turnSpeed = 2f;
    public float turnAngle = 45f;

    [Header("Health")]
    public int maxHits = 5;

    private int currentHits;

    void Start()
    {
        currentHits = maxHits;
    }

    void Update()
    {
        transform.position += transform.right * forwardSpeed * Time.deltaTime;

        float turnInput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                turnInput = -1f;
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                turnInput = 1f;
            }
        }

        // Gradual turning using Quaternion
        if (turnInput != 0f)
        {
            Quaternion targetRotation =
                Quaternion.Euler(0f, turnInput * turnAngle, 0f)
                * transform.rotation;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );
        }
    }

    public void TakeHit()
    {
        currentHits--;

        Debug.Log("Player Hit! Hits remaining: " + currentHits);

        if (currentHits <= 0)
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}