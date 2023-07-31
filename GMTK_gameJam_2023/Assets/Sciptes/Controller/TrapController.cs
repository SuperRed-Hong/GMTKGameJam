
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private float traptime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Trapped:"+other.gameObject.name);
            PlayerController playerCaught = other.gameObject.GetComponent<PlayerController>();
            playerCaught.onStunned();
            //playerCaught.enabled=false;
            yield return new WaitForSeconds(traptime);
            //other.gameObject.GetComponent<PlayerController>().enabled=true;
            playerCaught.offStunned();
            Destroy(this.gameObject);
        }
    }
}
