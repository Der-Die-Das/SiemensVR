using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCardReader : MonoBehaviour {
    public enum CardReaderStatus { locked = 0, unlocked = 1};

    public GameObject statuslight;
    public Material lightMaterialUnlocked;
    public Material lightMaterialLocked;
    private CardReaderStatus status;
    public int relatedRoom;
    public GameObject relatedDoor;
    


	// Use this for initialization
	void Start () {
        status = CardReaderStatus.locked;
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Keycard>())                      // check if colliding Object is a Keycard
        {
            foreach (int permittedRoom in other.GetComponent<Keycard>().permittedRooms)
            {
                if(permittedRoom == relatedRoom)                // check if keycard has the to the cardreader related room in its permittedRooms-List
                {
                    switch (status)
                    {
                        case CardReaderStatus.locked:
                            status = CardReaderStatus.unlocked;
                            statuslight.GetComponent<MeshRenderer>().material = lightMaterialUnlocked;
                            break;
                        case CardReaderStatus.unlocked:
                            status = CardReaderStatus.locked;
                            statuslight.GetComponent<MeshRenderer>().material = lightMaterialLocked;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
