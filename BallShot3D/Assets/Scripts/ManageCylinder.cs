using UnityEngine;
using UnityEngine.EventSystems;

public class ManageCylinder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool ButtonPressed;
    public GameObject Cylinder;
    [SerializeField] private float rotatePower;
    [SerializeField] private string rotateDirection;
    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonPressed = false;
    }
    void Update()
    {
        if (ButtonPressed)
        {
            if (rotateDirection == "Left")
            {
                Cylinder.transform.Rotate(0, rotatePower * Time.deltaTime, 0, Space.Self);
            }
            else
            {
                Cylinder.transform.Rotate(0, -rotatePower * Time.deltaTime, 0, Space.Self);
            }
        }
    }
}
