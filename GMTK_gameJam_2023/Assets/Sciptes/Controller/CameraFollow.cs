using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<GameObject> followObjects = new List<GameObject>();
    public Vector2 followOffsetOut;
    public Vector2 followOffsetIn;
    public float speed = 3;
    private Vector2 thresholdOut;
    private Vector2 thresholdIn;
    private Rigidbody2D rb;
    public float size = 16f;
    // Start is called before the first frame update
    void Start()
    {
        
        //rb = followObjects[0].GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        thresholdOut = calculateThresholdOut();
        thresholdIn = calculateThresholdIn();
        Vector2 allVector2 = new Vector2(0,0);
        for (int i = 0;i < followObjects.Count; i++)
        {
            allVector2 = new Vector2(allVector2.x + followObjects[i].transform.position.x, allVector2.y + followObjects[i].transform.position.y);
        }
        Vector2 follow = allVector2/followObjects.Count;
        Vector3 newPosition = transform.position;
        for (int i = 0; i < followObjects.Count; i++)
        {
            float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * followObjects[i].transform.position.x);
            float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * followObjects[i].transform.position.y);
            //print("thresholdOut" + thresholdOut);
            //print("thresholdIn" + thresholdIn);
            print("xDifference" + thresholdIn);
            print("yDifference" + Mathf.Abs(xDifference));
            print((Mathf.Abs(xDifference) <= thresholdIn.x));
            if (Mathf.Abs(xDifference) >= thresholdOut.x)
            {
                newPosition.x = follow.x;
                if (Camera.main.orthographicSize < 9.3f)
                {
                    size += 0.1f;
                }
            }
            if (Mathf.Abs(yDifference) >= thresholdOut.y)
            {
                newPosition.y = follow.y;
                if (Camera.main.orthographicSize < 9.3f)
                {
                    size += 0.1f;
                }
            }
            if (Mathf.Abs(xDifference) <= thresholdIn.x)
            {
                newPosition.x = follow.x;
                newPosition.y = follow.y;
                if (Camera.main.orthographicSize > 6.6f)
                {
                    StartCoroutine(sizeReduce());
                }
            }
            float moveSpeed = followObjects[i].GetComponent<Rigidbody2D>().velocity.magnitude > speed ? followObjects[i].GetComponent<Rigidbody2D>().velocity.magnitude : speed;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }
        Camera.main.orthographicSize = (float)Screen.height/(float)Screen.width * 0.5f * size;
    }
    private Vector3 calculateThresholdOut()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffsetOut.x;
        t.y -= followOffsetOut.y;
        return t;
    }
    private Vector3 calculateThresholdIn()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffsetIn.x;
        t.y -= followOffsetIn.y;
        return t;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border1 = calculateThresholdOut();
        Vector2 border2 = calculateThresholdIn();
        Gizmos.DrawWireCube(transform.position, new Vector3(border1.x * 2, border1.y * 2 ,1));
        Gizmos.DrawWireCube(transform.position, new Vector3(border2.x * 2, border2.y * 2, 1));
    }
    private IEnumerator sizeReduce()
    {
        yield return new WaitForSeconds(1.0f);
        size -= 0.1f;
    }
}
