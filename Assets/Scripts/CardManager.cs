using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject Cards;
    public List<GameObject> allCards = new List<GameObject>();
    public GameObject[] cardPrefabs;

    public GameObject CurrentCard = null;
    void Start()
    {
        // kaarten instantiaten
        for (int i = 0; i < 12; i++)
        {
            GameObject randomCard = cardPrefabs[Random.Range(0, cardPrefabs.Length)];
            var addCard = Instantiate(randomCard, Cards.transform.position, Quaternion.identity);
            addCard.transform.SetParent(Cards.transform);
            allCards.Add(addCard);
        }

        int j = 0;
        foreach (var item in allCards)
        {
            if (j <= 4)
                item.SetActive(true);

            else
                item.SetActive(false);

            j++;
        }

    }

    public GameObject ActivateNext()
    {
        allCards = new List<GameObject>();
        foreach (Transform child in Cards.transform)
        {
            allCards.Add(child.gameObject);
        }
        foreach (var item in allCards)
        {
            if (!item.activeSelf && !item.GetComponent<CardView>().used)
            {
                item.SetActive(true);
                break;
            }

        }

        return CurrentCard;
    }
}
