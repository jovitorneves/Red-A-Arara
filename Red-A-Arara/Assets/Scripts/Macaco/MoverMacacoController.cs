using UnityEngine;

public class MoverMacacoController : ScriptableObject
{
    public GameObject macacoGameObject;
    public Transform posicaoA;
    public Transform posicaoB;

    public bool isLookLeft = false;

    private bool reverseMove = true;
    private float startTime = Time.time;
    private readonly float journeyLength;
    private readonly float speedMacaco = 1.5f;

    public MoverMacacoController(GameObject macacoGameObject, Transform posicaoA, Transform posicaoB)
    {
        this.macacoGameObject = macacoGameObject;
        this.posicaoA = posicaoA;
        this.posicaoB = posicaoB;
        this.journeyLength = Vector3.Distance(posicaoA.transform.position, posicaoB.transform.position);
    }

    public Vector3 MoverMacaco(Vector3 currentMonnkey)
    {
        float distCovered = (Time.time - startTime) * speedMacaco;
        float fracJourney = distCovered / journeyLength;

        float disA = currentMonnkey.x - posicaoA.position.x;
        float disB = currentMonnkey.x - posicaoB.position.x;
        disA = disA > 0 ? (disA * 1) : disA * -1;
        disB = disB > 0 ? (disB * 1) : disB * -1;

        if (reverseMove)
            macacoGameObject.transform.position = Vector3.Lerp(posicaoB.transform.position, posicaoA.transform.position, fracJourney);
        else
            macacoGameObject.transform.position = Vector3.Lerp(posicaoA.transform.position, posicaoB.transform.position, fracJourney);

        if (Vector3.Distance(macacoGameObject.transform.position, posicaoB.transform.position) == 0.0f ||
            Vector3.Distance(macacoGameObject.transform.position, posicaoA.transform.position) == 0.0f) //Checks if the object has travelled to one of the points
        {
            if (reverseMove)
                reverseMove = false;
            else
                reverseMove = true;

            Flip();

            startTime = Time.time;
        }

        return macacoGameObject.transform.position;
    }

    private void Flip()
    {
        float x = isLookLeft ? 1 : -1;

        macacoGameObject.transform.localScale = new Vector3(x, macacoGameObject.transform.localScale.y, macacoGameObject.transform.localScale.z);
        isLookLeft = !isLookLeft;
    }
}
