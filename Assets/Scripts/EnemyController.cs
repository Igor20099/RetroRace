using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Score score;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -8.6f)
        {
            Destroy(this.gameObject);
            if (!this.gameObject.CompareTag("Bullet"))
            GameManager.instanse.AddScore(score.ScoreCount);
        }
    }

    IEnumerator Move()
    {
       while(transform.position.y >= -9f)
        {
            transform.Translate(0, -1, 0);
            yield return new WaitForSeconds(GameManager.instanse.SpeedGame * speed);
        }
       
    }
}
