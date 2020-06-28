using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrulEnemy : MonoBehaviour
{
    [SerializeField] private Transform leftSpawn, middleSpawn,rightSpawn;
    bool isRight = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Patrul());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Patrul()
    {
      
        while (true)
        {
            if (transform.position.x == -3.5)
            {
               
                transform.Translate(Mathf.Floor(3f), 0, 0);
                isRight = true;
               
            }
          
                 else if (transform.position.x == -0.5f && isRight)
            {
                transform.Translate(Mathf.Floor(3f), 0, 0);
               
            }
            else if (transform.position.x == -0.5f && !isRight)
            {
                transform.Translate(Mathf.Floor(-3f), 0, 0);
            }
           else if (transform.position.x == 2.5f)
            {
               // isRight = false;
                transform.Translate(Mathf.Floor(-3f), 0, 0);
                isRight = false;

            }
            yield return new WaitForSeconds(0.7f);
        }
    }
}
