using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRider : MonoBehaviour
{
    public LinkedList<Transform> points = new LinkedList<Transform>();
    public Transform[] arr;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Button.onButtonClick += (x =>
        {
            if (x == true) StartCoroutine("RideToPoint1");
            if (x == false) {
                StopAllCoroutines();
                StartCoroutine("RideToPoint2"); }
        });

        foreach (var ar in arr)
        {
            points.AddLast(ar);
        }
        //StartCoroutine("RideToPoint1");
        //StopAllCoroutines();
        //StartCoroutine("RideToPoint2");
    }
    private void Update()
    {
        if (CVLogic.status == true)
        {
            StartCoroutine("RideToPoint1");
        }
        
    }
    public IEnumerator RideToPoint1()
    {
        
            var point = points.First;
            if (point != null)
            {
                while (arr[0].transform.position != transform.position)
                {
                    //transform.position = Vector3.MoveTowards(transform.position, point.Value.position, Time.deltaTime * speed);
                    transform.position = Vector3.Lerp(transform.position, point.Value.position, Time.deltaTime * speed);
                    yield return new WaitForEndOfFrame();
                }
                
            
            yield return null;
        }
    }
    /*
    public IEnumerator RideToPoint2()
    {
        
            var point = points.Last;
            if (point != null)
            {
                while (arr[1].transform.position != transform.position)
                {
                    //transform.position = Vector3.MoveTowards(transform.position, point.Value.position, Time.deltaTime * speed);
                    transform.position = Vector3.Lerp(point.Value.position, transform.position, Time.deltaTime * speed);
                    yield return new WaitForEndOfFrame();
                }
                
            
            yield return null;
        }
    
    }
    */
}

