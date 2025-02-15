using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float speed;
    public GameObject effectDestroy;
    // Start is called before the first frame update
    void Start()
    {
        speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stone"))
        {
            GameObject gm = Instantiate(effectDestroy, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            Destroy(gm, 2f);

        }
    }
}
