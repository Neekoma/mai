using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PointRider : MonoBehaviour
{
    public GameObject cj;
    public GameObject icecube;
    public Transform[] arr;
    public float speed;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        CVLogic.OnDetected += (y => { StartCoroutine("MoveToPerson"); });
        Button.onButtonClick += (x =>
        {
            if (x == true) StartCoroutine("Move");
            if (x == false)
            {
                StopAllCoroutines();
            }
        });
    }
    private void Update()
    {
        
    }

    private IEnumerator Move()
    {
        while (true)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                while ((arr[i].position - transform.position).magnitude >= 0.5)
                {
                    transform.position = Vector3.Lerp(transform.position, arr[i].position, speed * Time.deltaTime);
                    transform.LookAt(arr[i]);
                    yield return new WaitForEndOfFrame();
                }

            }
        }
    }
    private IEnumerator MoveToPerson()
    {
        transform.LookAt(cj.transform.position);

        while ((transform.position - cj.transform.position).magnitude >= 2)
        {
            transform.position = Vector3.Lerp(transform.position, cj.transform.position, speed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        cj.transform.SetParent(transform);
        cj.transform.localPosition = Vector3.zero;
        transform.LookAt(arr[arr.Length - 2].position);


        while ((transform.position - arr[arr.Length - 2].position).magnitude >= 0.5)
        {
            transform.position = Vector3.Lerp(transform.position, arr[arr.Length - 2].position, speed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator MoveToSecondPerson()
    {
        transform.LookAt(icecube.transform.position);

        while ((transform.position - icecube.transform.position).magnitude >= 2)
        {
            transform.position = Vector3.Lerp(transform.position, icecube.transform.position, speed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        Application.Quit(0);


    }
}

