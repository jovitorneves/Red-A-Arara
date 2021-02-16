using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoController : MonoBehaviour
{
    [SerializeField]
    private Animator portaAnimator;

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

        portaAnimator.enabled = true;
        portaAnimator.Play(AnimationTagsConstants.OpenClosePorta);
    }
}