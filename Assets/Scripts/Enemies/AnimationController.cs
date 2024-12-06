using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Die()
    {
        _animator.SetBool("Die", true);
    }
    public void Attack()
    {
        _animator.SetBool("Attack", true);
    }
    public void Chase()
    {
        _animator.SetBool("Fly", true);
    }
    public void Fall()
    {
        _animator.SetBool("Fall", true);
    }
}
