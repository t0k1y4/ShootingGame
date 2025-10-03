using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.0f;
    public bool isVertical;
    public float changeTime = 2.0f;

    Rigidbody2D rb;
    float timer;
    int direction = 1;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0){
            direction = -direction;
            timer = changeTime;
        }
        Vector2 pos = rb.position;

        if(isVertical){
            pos.y=pos.y + Time.deltaTime * speed * direction;

        }else{
            pos.x=pos.x + Time.deltaTime * speed * direction;
        }
        rb.MovePosition(pos);
    }
}
