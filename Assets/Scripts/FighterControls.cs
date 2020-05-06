using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterControls : MonoBehaviour
{
    [SerializeField]
    private float XYRotationSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    [Range(0f, 10f)]
    private float rotationZSpeed = 1f;

    private Transform fighterChild;

    Vector3 vectorWrite = Vector3.zero;

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
                var lerpRotation = Mathf.LerpAngle(fighterChild.localEulerAngles.z, 60f, Time.deltaTime * rotationZSpeed);

                fighterChild.localEulerAngles = new Vector3(0f, fighterChild.localEulerAngles.y, lerpRotation);

                vectorWrite.y -= Time.deltaTime * XYRotationSpeed;
            }
            else if(x > 0f)
            {
                var lerpRotation = Mathf.LerpAngle(fighterChild.localEulerAngles.z, -60f, Time.deltaTime * rotationZSpeed);

                fighterChild.localEulerAngles = new Vector3(0f, fighterChild.localEulerAngles.y, lerpRotation);

                vectorWrite.y += Time.deltaTime * XYRotationSpeed;
            }

            vectorWrite.x -= y * Time.deltaTime * XYRotationSpeed;

            transform.eulerAngles = vectorWrite;
        }

        if (Mathf.Approximately(x, 0f))
        {
            var lerpRotation = Mathf.LerpAngle(fighterChild.localEulerAngles.z, 0f, Time.deltaTime * (rotationZSpeed - 2f));

            fighterChild.localEulerAngles = new Vector3(0f, fighterChild.localEulerAngles.y, lerpRotation);
        }
    }
}
