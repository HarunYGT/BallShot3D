using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager gameManager;
    Rigidbody rb;
    Renderer ballColor;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballColor = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bucket"))
        {
            TriggerSettings();
            gameManager.isBallEnter(true);
        }
        else if (other.CompareTag("Hit"))
        {
            TriggerSettings();
            gameManager.isBallEnter(false);
        }
    }
    public void TriggerSettings()
    {
        gameManager.ParcEffect(gameObject.transform.position, ballColor.material.color);

        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
