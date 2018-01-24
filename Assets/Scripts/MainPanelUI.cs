using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelUI : MonoBehaviour
{

    public static float surviveTime;
    public static float startTime;

    public Transform wheelSlotsParent;
    public Transform numberSlotsParent;
    Slot[] wheelSlots;
    Slot[] numberSlots;

    int a, b;
    bool hasHint;

    void Start()
    {

        hasHint = false;
        startTime = Time.time;

        wheelSlots = wheelSlotsParent.GetComponentsInChildren<Slot>();
        numberSlots = numberSlotsParent.GetComponentsInChildren<Slot>();

    }

    void Update()
    {

        surviveTime = Time.time - startTime;

        if (surviveTime > 5)
        {
            // for checking match
            for (int j = 0; j < numberSlotsParent.childCount; j++)
            {
                for (int i = 0; i < wheelSlotsParent.childCount; i++)
                {
                    if (wheelSlots[i].transform.GetChild(0).GetComponent<Image>().sprite.ToString().
                        Equals(numberSlots[j].transform.GetChild(0).GetComponent<Image>().sprite.ToString()))
                    {
                        //there is a case that has no hint.
                        hasHint = true;
                        a = i; b = j;
                        i = 20; j = 20;
                    }

                }

            }

            //reset the time
            startTime = Time.time;

            if(hasHint)
                AnimationFunc(2);

        }
    }

    void AnimationFunc(float sec)
    {
        StartCoroutine("AnimationCoroutine", sec);

    }

    IEnumerator AnimationCoroutine(float x)
    {

        wheelSlots[a].transform.GetComponent<Animator>().SetTrigger("isHint");
        numberSlots[b].transform.GetComponent<Animator>().SetTrigger("isHint");

        yield return new WaitForSeconds(x);

        hasHint = false;

        wheelSlots[a].transform.GetComponent<Animator>().Rebind();
        numberSlots[b].transform.GetComponent<Animator>().Rebind();

        StopCoroutine("AnimationCoroutine");

    }



}
