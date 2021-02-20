using UnityEngine;

public class BotaoController : MonoBehaviour
{
    [SerializeField]
    private Animator portaAnimator;
    private bool IsCoco = false;
    private bool IsPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        portaAnimator = portaAnimator.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (UtilController.Instance.ReturnDirection(other.contacts) != HitDirection.Top)
            return;

        IsCoco = other.gameObject.CompareTag(TagsConstants.CocoPartido);
        IsPlayer = other.gameObject.CompareTag(TagsConstants.Player);

        if (IsCoco || IsPlayer)
            portaAnimator.Play(AnimationTagsConstants.OpenPorta);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsCoco)
        {
            IsCoco = false;
            portaAnimator.Play(AnimationTagsConstants.ClosePorta);
        }

        if (IsPlayer)
        {
            IsPlayer = false;
            portaAnimator.Play(AnimationTagsConstants.ClosePorta);
        }
    }
}