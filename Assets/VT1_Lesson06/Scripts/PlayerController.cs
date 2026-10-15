using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int PlayerMoveSpped = 5;
    public int PlayerRotateSpeed = 90;
    void Start()
    {
        
    }

    void Update()
    {
        if(Keyboard.current.wKey.isPressed == true)
        {
            transform.Translate(Vector3.forward * PlayerMoveSpped * Time.deltaTime);
        }

        if (Keyboard.current.sKey.isPressed == true)
        {
            transform.Translate(Vector3.forward * -PlayerMoveSpped * Time.deltaTime);
        }

        if (Keyboard.current.dKey.isPressed == true)
        {
            transform.Rotate(Vector3.up * PlayerRotateSpeed * Time.deltaTime);
        }

        if (Keyboard.current.aKey.isPressed == true)
        {
            transform.Rotate(Vector3.up * -PlayerRotateSpeed * Time.deltaTime);
        }
    }
}
