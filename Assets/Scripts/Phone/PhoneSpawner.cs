using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PhoneSpawner : MonoBehaviour
{
    public GameObject phonePrefab;

    public bool useVariants = false;
    public PhoneVariant[] variants;
    
    public float averageSpawnTime = 5;
    public float spawnTimeDeviation = 0.5f;

    public int maxPhones = 10;

    public Vector2 minSpawnDistance = new Vector2(2, 3);
    public float minSpawnDistanceBorder = 3;
    public int triesForRandomPoint = 20;
    public bool randomZ;

    public List<Phone> phones = new List<Phone>();

    // Start is called before the first frame update
    void Start()
    {
        Hub.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        var camBounds = GetCameraBounds();
        var min = camBounds.min + new Vector2(minSpawnDistanceBorder, minSpawnDistanceBorder);
        var max = camBounds.max - new Vector2(minSpawnDistanceBorder, minSpawnDistanceBorder);

        Gizmos.DrawLine(min, new Vector2(min.x, max.y)); // Left
        Gizmos.DrawLine(min, new Vector2(max.x, min.y)); // Bottom
        Gizmos.DrawLine(max, new Vector2(max.x, min.y)); // Right
        Gizmos.DrawLine(max, new Vector2(min.x, max.y)); // Top

        Gizmos.DrawWireSphere(Vector2.zero, 1);
        Gizmos.DrawWireSphere(minSpawnDistance, 1);
        Gizmos.color = Color.white;
    }

    Rect GetCameraBounds()
    {
        var camHeight = 2 * Camera.main.orthographicSize;
        var camWidth = camHeight * Camera.main.aspect;

        var camPos = Camera.main.transform.position;
        return new Rect(
            camPos.x - camWidth / 2,
            camPos.y - camHeight / 2,
            camPos.x + camWidth,
            camPos.y + camHeight
        );
    }

    Vector3 GetSpawnPoint()
    {
        var camBounds = GetCameraBounds();
        var min = camBounds.min + new Vector2(minSpawnDistanceBorder, minSpawnDistanceBorder);
        var max = camBounds.max - new Vector2(minSpawnDistanceBorder, minSpawnDistanceBorder);

        Vector3 point = Vector3.zero;
        for (int tries = 0; tries < triesForRandomPoint; tries++)
        {
            point = RandomPointInBounds(min, max);

            if (phones.All(phone =>
            {
                var xComp = phone.isHorizontal ? minSpawnDistance.y : minSpawnDistance.x;
                var yComp = phone.isHorizontal ? minSpawnDistance.x : minSpawnDistance.y;

                return Math.Abs(point.x - phone.transform.position.x) >= xComp ||
                       Math.Abs(point.y - phone.transform.position.y) >= yComp;
            }))
            {
                //Spawn!
                return point;
            }
        }

        Debug.Log("Could find valid spawn point. Falling back to " + point);
        return point;
    }

    public static Vector3 RandomPointInBounds(Vector2 min, Vector2 max)
    {
        return new Vector3(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y),
            0
        );
    }

    public void SpawnPhone()
    {
        var point = GetSpawnPoint();
        point.z = randomZ ? Random.Range(-0.05f, 0) : 0;
        var phone = Instantiate(phonePrefab, point, Quaternion.identity, transform);

        var phoneScript = phone.GetComponent<Phone>();
        phones.Add(phoneScript);

        phoneScript.targetPosition = point;

        if (useVariants)
        {
            var variant = variants[Random.Range(0, variants.Length)];
            phoneScript.SetVariant(variant);
        }


        Debug.Log("Spawned phone at position " + point);
        //Other stuff to do with phones
    }

    public void RemoveAllPhones()
    {
        phones.ForEach(phone => phone.Despawn());
        phones.Clear();
    }

    public void RemovePhone(Phone phone)
    {
        phones.Remove(phone);
        phone.Despawn();
    }
}