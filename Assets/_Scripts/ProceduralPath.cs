using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralPath : MonoBehaviour, IObserver<Dimension>
{
    public float scale;
    public float detail;
    public Vector3 startPosition;
    public List<GameObject> prefabs;
    public float size;

    private GameObject prevRoom;
    private GameObject currentRoom;
    private GameObject nextRoom;
    private Vector2 currentPosition;


    // Start is called before the first frame update
    public void Initialize()
    {
        prevRoom = Generate(startPosition - new Vector3(size, 0, 0));
        currentRoom = Generate(startPosition);
        nextRoom = Generate(startPosition + new Vector3(size, 0, 0));
        nextRoom.transform.position = new Vector3(size, 0, 0f);
        currentRoom.transform.position = new Vector3(0, 0, 0f);
        prevRoom.transform.position = new Vector3(-size, 0, 0f);
    }

    public bool UpdatePosition(Vector3 position, out Vector3 shouldGoTo)
    {
        currentPosition = position;
        float distance = position.x;
        if (distance > size/2f)
        {
            Destroy(prevRoom);
            prevRoom = currentRoom;
            currentRoom = nextRoom;
            nextRoom = Generate(position);
            nextRoom.transform.position = new Vector3(size, 0, 0f);
            currentRoom.transform.position = new Vector3(0, 0, 0f);
            prevRoom.transform.position = new Vector3(-size, 0, 0f);
            shouldGoTo = position - new Vector3(size, 0, 0f);
            return true;
        } else if (distance < -size/2f)
        {
            Destroy(nextRoom);
            nextRoom = currentRoom;
            currentRoom = prevRoom;
            prevRoom = Generate(position);
            nextRoom.transform.position = new Vector3(size, 0, 0f);
            currentRoom.transform.position = new Vector3(0, 0, 0f);
            prevRoom.transform.position = new Vector3(-size, 0, 0f);
            shouldGoTo = position - new Vector3(-size, 0, 0f);
            return true;
        }
        shouldGoTo = Vector3.zero;
        return false;
    }

    GameObject Generate(Vector3 position)
    {
        //Genero el noise
        float noise = Mathf.PerlinNoise(position.x, position.y);
        currentPosition = position;

        //Muevo todo uno para atras
        return Instantiate(prefabs[Mathf.FloorToInt((float)prefabs.Count * noise)]);

        //Translado al jugador atras
    }

    public void OnCompleted()
    {
        
    }

    public void OnError(Exception error)
    {
        
    }

    public void OnNext(Dimension value)
    {
       if(value is ConsciousDimension) Initialize();
    }
}
