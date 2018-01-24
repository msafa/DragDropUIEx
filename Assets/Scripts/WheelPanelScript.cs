using UnityEngine;

public class WheelPanelScript : MonoBehaviour {

    //WheelPanelScript wheelPanelScript;

    public int speedOfRotation = 3;
    float delta;

    void Update()
    {

        if(MainPanelUI.surviveTime < 10)
            delta += Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, delta * speedOfRotation);

    }

}
