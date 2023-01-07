using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Fisher-Yates Shuffle
public static class ArrayExtensions
{
    public static void Shuffle<T>(this T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject[] gameObjectsToSpawn; // array of game objects to spawn
    public Transform[] spawnPoints; // array of spawn points

    GameObject c;

    void Start()
    {
        // Shuffle the array of game objects
        gameObjectsToSpawn.Shuffle();

        // Iterate over the array of game objects
        for (int i = 0; i < gameObjectsToSpawn.Length; i++)
        {
            // Select a spawn point
            Transform spawnPoint = spawnPoints[i];

            // Spawn the game object at the selected spawn point
            c = Instantiate(gameObjectsToSpawn[i], spawnPoint.position, spawnPoint.rotation);
        }
    }
}

