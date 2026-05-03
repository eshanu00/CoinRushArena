using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float rotateSpeed = 150f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}
