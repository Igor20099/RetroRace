using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEnemy : MonoBehaviour
{
    private int health;
    [SerializeField] private SpriteRenderer[] spriteRenderers;

    public int Health { get => health; set => health = value; }

    // Start is called before the first frame update
    void Start()
    {
        Health = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Health ==1)
        {
            foreach(var sprite in spriteRenderers)
            {
                sprite.color = Color.white;
            }
        }
        if (Health == 0)
        {
            SoundManager.instance.PlayDestroy();
            Destroy(this.gameObject);
            GameManager.instanse.AddScore(40);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Health--;
        }
    }
}
