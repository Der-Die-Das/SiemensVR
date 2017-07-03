using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(VRObjectStand))]
public class KeycardReader : MonoBehaviour
{
    private VRObjectStand stand;
    private Keycard insertedCard;

    // Use this for initialization
    void Start()
    {
        stand = GetComponent<VRObjectStand>();
        stand.keycardInserted += OnCardInsert;
        stand.keycardEjected += OnCardEject;
    }

    private void OnCardInsert(Keycard card)
    {
        if (insertedCard != null)
        {
            throw new System.Exception("Fatal Error");
        }
        insertedCard = card;

    }

    private void OnCardEject(Keycard card)
    {
        insertedCard = null;
    }

    
    

   
}
