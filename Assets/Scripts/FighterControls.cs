using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterControls : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float speed;

    private Transform fighterChild;

    float rotationZ;

    float t = 0f;

    private void Start()
    {
        fighterChild = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        transform.position += transform.forward * speed;

        if (!Mathf.Approximately(x, 0f) || !Mathf.Approximately(y, 0f))
        {
            if (x < 0f)
            {
                var lerpRotation = Mathf.LerpAngle(fighterChild.localEulerAngles.z, 30f, Time.deltaTime);

                fighterChild.localEulerAngles = new Vector3(0f, fighterChild.localEulerAngles.y, lerpRotation);

                transform.eulerAngles -= new Vector3(0f, Time.deltaTime * rotationSpeed, 0f);
            }
            else if(x > 0f)
            {
                var lerpRotation = Mathf.LerpAngle(fighterChild.localEulerAngles.z, -30f, Time.deltaTime);

                fighterChild.localEulerAngles = new Vector3(0f, fighterChild.localEulerAngles.y, lerpRotation);

                transform.eulerAngles += new Vector3(0f, Time.deltaTime * rotationSpeed, 0f);
            }

            transform.eulerAngles -= new Vector3(y * Time.deltaTime * rotationSpeed, 0.0f, 0f);
        }

        if (Mathf.Approximately(x, 0f))
        {
            var lerpRotation = Mathf.LerpAngle(fighterChild.localEulerAngles.z, 0f, Time.deltaTime);

            fighterChild.localEulerAngles = new Vector3(0f, fighterChild.localEulerAngles.y, lerpRotation);
        }
    }
}
