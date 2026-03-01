using UnityEngine;
using System.Collections;

public class CoffeeMachine : MonoBehaviour
{
    [Header("Main Objects")]
    public GameObject frontMachine;
    public GameObject BackMachine;
    public GameObject resultCup;

    [Header("Animators")]
    public Animator coffeeAnimator;
    public Animator mayDanhAnimator;

    [Header("Cup Move Setting")]
    public Transform cupHolder;
    public Transform startPostition;
    public Transform coffeePostition;
    public Transform mixPosition;

    [Header("Mix Device Move Setting")]
    public Transform mixDevice;
    public Transform mixStartPosition;
    public Transform mixDownPosition;



    private float MOVE_SPEED = 5f;

    private float DELAY_TIME = 1f;
    bool isProcessing = false;

    public void MakeBlackCoffee()
    {
        if (isProcessing) return;
        StartCoroutine(ProcessBlackCoffee());
    }

    IEnumerator ProcessBlackCoffee()
    {
        isProcessing = true;
        frontMachine.SetActive(false);
        resultCup.SetActive(false);
        BackMachine.SetActive(true);
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(MoveTo(coffeePostition));
        yield return new WaitForSeconds(DELAY_TIME);

        coffeeAnimator.SetTrigger("Pour_Coffee");
        yield return new WaitForSeconds(6f);

        yield return StartCoroutine(MoveTo(mixPosition));
        yield return new WaitForSeconds(DELAY_TIME);

        yield return StartCoroutine(MixDeviceMove(mixDownPosition));
        yield return new WaitForSeconds(DELAY_TIME);

        mayDanhAnimator.SetTrigger("Mix");
        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(MixDeviceMove(mixStartPosition));
        yield return new WaitForSeconds(DELAY_TIME);

        yield return StartCoroutine(MoveTo(startPostition));
        yield return new WaitForSeconds(DELAY_TIME);

        BackMachine.SetActive(false);
        frontMachine.SetActive(true);
        resultCup.SetActive(true);

        yield return new WaitForSeconds(2f);
        resultCup.SetActive(false);
        isProcessing = false;
    }

    IEnumerator MoveTo(Transform target)
    {
        while (Vector3.Distance(cupHolder.position, target.position) > 0.1f)
        {
            cupHolder.position = Vector3.MoveTowards(
                cupHolder.position,
                target.position,
                MOVE_SPEED * Time.deltaTime
            );

            yield return null;
        }
    }
    IEnumerator MixDeviceMove(Transform target)
    {
        while (Vector3.Distance(mixDevice.position, target.position) > 0.1f)
        {
            mixDevice.position = Vector3.MoveTowards(
                mixDevice.position,
                target.position,
                MOVE_SPEED * Time.deltaTime
            );

            yield return null;
        }
    }
}