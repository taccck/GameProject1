using UnityEngine;

public class DishBackSpin : MonoBehaviour
{
    [SerializeField] private float speed = .25f;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,0,speed), Space.Self);
    }
}