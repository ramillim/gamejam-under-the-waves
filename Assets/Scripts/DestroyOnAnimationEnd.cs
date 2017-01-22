using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour
{
    void Start()
    {
        float animationLength = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animationLength + 0.1f);
    }
}
