using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Move()
    {
        while (transform.position.y >= -9f)
        {
            transform.Translate(0, -1, 0);
            yield return new WaitForSeconds(GameManager.instanse.SpeedGame);
        }

    }
}
