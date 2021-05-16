using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    [SerializeField] int id;
    List<Spawner> _spawners = new List<Spawner>();
    [SerializeField] GameObject[] doors;
    [SerializeField] private Sprite openDoorSprite;
    [SerializeField] bool roomGiveKey = false;


    private void Start()
    {
        foreach (var spawner in GetComponentsInChildren<Spawner>())
        {
            _spawners.Add(spawner);
        }

        foreach (var d in doors)
        {
            d.SetActive(true);
        }

        foreach (var s in _spawners)
        {
            s.OnEnemiesListEmpty += OpenDoors;
        }
    }


    void OpenDoors(int id, Spawner spawner)
    {
        if (id == this.id)
        {
            _spawners.Remove(spawner);
            if (_spawners.Count != 0)
            {
                return;
            }

            foreach (var d in doors)
            {
                d.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
                d.GetComponent<BoxCollider2D>().enabled = false;
            }

            if (roomGiveKey)
            {
                GameEvents.Current.CallEventCleared();
                roomGiveKey = false;
            }
                
        }
    }
}