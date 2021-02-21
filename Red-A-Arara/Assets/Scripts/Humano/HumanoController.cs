using UnityEngine;

public class HumanoController : BaseEnemyController
{
    //Player
    [SerializeField]
    private GameObject player;
    private Player playerScript;

    //Humano
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private GameObject pointA;
    [SerializeField]
    private GameObject pointB;
    [SerializeField]
    private bool reverseMove = false;
    [SerializeField]
    private Transform objectToUse;
    [SerializeField]
    private bool moveThisObject = false;

    private readonly float distanciaHumanoAndPointMax = 0.1f;
    private Animator animator;
    private float startTime;
    private float journeyLength;
    private float distCovered;
    private float fracJourney;

    void Start()
    {
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();

        startTime = Time.time;

        if (moveThisObject)
            objectToUse = transform;

        journeyLength = Vector3.Distance(pointA.transform.position, pointB.transform.position);
    }

    void Update()
    {
        distCovered = (Time.time - startTime) * moveSpeed;
        fracJourney = distCovered / journeyLength;

        if (pointA.Equals(null) || pointB.Equals(null))
            return;

        if (!playerScript.isAlive) return;

        float disA = gameObject.transform.position.x - pointA.transform.position.x;
        float disB = gameObject.transform.position.x - pointB.transform.position.x;
        disA = disA > 0 ? (disA * 1) : disA * -1;
        disB = disB > 0 ? (disB * 1) : disB * -1;

        if (player.gameObject.transform.position.x >= pointA.transform.position.x &&
            player.gameObject.transform.position.x <= pointB.transform.position.x &&
            disA >= distanciaHumanoAndPointMax && disB >= distanciaHumanoAndPointMax)
            MoveHumano(isCaptura: true);
        else
            MoveHumano(isCaptura: false);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        //if (collision2D.gameObject.CompareTag(TagsConstants.CocoPartido))
        //{
        //    
        //}
    }

    private void MoveHumano(bool isCaptura)
    {
        if (reverseMove)
            objectToUse.position = Vector3.Lerp(pointB.transform.position, pointA.transform.position, fracJourney);
        else
            objectToUse.position = Vector3.Lerp(pointA.transform.position, pointB.transform.position, fracJourney);

        if (Vector3.Distance(objectToUse.position, pointB.transform.position) == 0.0f ||
            Vector3.Distance(objectToUse.position, pointA.transform.position) == 0.0f) //Checks if the object has travelled to one of the points
        {
            if (reverseMove)
                Flip();
            else
                Flip();
            startTime = Time.time;
        }

        if (isCaptura)
            animator.Play(AnimationTagsConstants.CapturaHumano);
        else
            animator.Play(AnimationTagsConstants.IdleHumano);
    }

    private void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1;
        gameObject.transform.localScale = newScale;
        reverseMove = !reverseMove;
    }
}