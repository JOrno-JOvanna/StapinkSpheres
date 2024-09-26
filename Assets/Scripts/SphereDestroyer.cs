using UnityEngine;

public class SphereDestroyer : MonoBehaviour
{
    public DelegateHolder delegateHolder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        delegateHolder.InvokeDestroySpheresByMistake(-15);
    }
}
