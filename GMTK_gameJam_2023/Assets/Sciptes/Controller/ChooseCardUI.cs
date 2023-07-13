using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCardUI : MonoBehaviour
{
    public Image image;

    private List<GameObject> doctorCards;
    private List<GameObject> patientCards;
    private List<Sprite> images;

    private PlayerManager playerManager;
    private int lastCard;
    private int currentCard;

    public Transform left;
    public Transform right;

    private void Awake()
    {
        playerManager = GameObject.Find("UIManager").GetComponent<PlayerManager>();
        doctorCards = new List<GameObject>();
        doctorCards.Add(Resources.Load<GameObject>("Prefabs/金钟罩d"));
        doctorCards.Add(Resources.Load<GameObject>("Prefabs/减速d"));
        doctorCards.Add(Resources.Load<GameObject>("Prefabs/闪现d"));
        doctorCards.Add(Resources.Load<GameObject>("Prefabs/障碍d"));
        doctorCards.Add(Resources.Load<GameObject>("Prefabs/伸手d"));

        patientCards = new List<GameObject>();
        patientCards.Add(Resources.Load<GameObject>("Prefabs/金钟罩p"));
        patientCards.Add(Resources.Load<GameObject>("Prefabs/减速p"));
        patientCards.Add(Resources.Load<GameObject>("Prefabs/闪现p"));
        patientCards.Add(Resources.Load<GameObject>("Prefabs/障碍p"));
        patientCards.Add(Resources.Load<GameObject>("Prefabs/伸手p"));

        images = new List<Sprite>();
        images.Add(Resources.Load<Sprite>("Art/UI/doctorShaking"));
        images.Add(Resources.Load<Sprite>("Art/UI/patientShaking"));
    }

    private void Start()
    {
        UpdateImage();
    }

    private void UpdateImage()
    {
        if (!playerManager.returnWinner())
        {
            if (playerManager.whoIsCacher())
            {
                lastCard = Random.Range(0, doctorCards.Count - 1);
                currentCard = Random.Range(0, doctorCards.Count - 1);
                while (lastCard == currentCard)
                {
                    currentCard = Random.Range(0, doctorCards.Count - 1);
                }
                image.sprite = images[0];
                Instantiate(doctorCards[lastCard], left);
                Instantiate(doctorCards[currentCard], right);
            }
            else
            {
                lastCard = Random.Range(1, doctorCards.Count);
                currentCard = Random.Range(1, doctorCards.Count);
                while (lastCard == currentCard)
                {
                    currentCard = Random.Range(1, doctorCards.Count);
                }
                image.sprite = images[0];
                Instantiate(doctorCards[lastCard], left);
                Instantiate(doctorCards[currentCard], right);
            }

        }
        else
        {
            if (playerManager.whoIsCacher())
            {
                lastCard = Random.Range(1, patientCards.Count);
                currentCard = Random.Range(1, patientCards.Count);
                while (lastCard == currentCard)
                {
                    currentCard = Random.Range(1, patientCards.Count);
                }
                image.sprite = images[1];
                Instantiate(patientCards[lastCard], left);
                Instantiate(patientCards[currentCard], right);
            }
            else
            {
                lastCard = Random.Range(0, patientCards.Count - 1);
                currentCard = Random.Range(0, patientCards.Count - 1);
                while (lastCard == currentCard)
                {
                    currentCard = Random.Range(0, patientCards.Count - 1);
                }
                image.sprite = images[1];
                Instantiate(patientCards[lastCard], left);
                Instantiate(patientCards[currentCard], right);
            }

        }

    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        if(mousePos.x >= image.transform.position.x)
        {
            image.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            image.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
