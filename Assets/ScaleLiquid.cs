using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLiquid : MonoBehaviour
{
    public string ColorRepresentation;
    public GameObject source;

    bool input = true;
    Vector3 delta = new Vector3(0,1,0);

    // Update is called once per frame
    void Update()
    {
        Vector3 timeDelta = delta * Time.deltaTime;
        if (Input.GetMouseButtonUp(0))
        {
            input = false;
        }
        transform.localScale += timeDelta;
        transform.position -= timeDelta/2;
        if (!input) {
            transform.localScale -= timeDelta;
            transform.position -= timeDelta/2;
        }
        GetComponent<SpriteRenderer>().material.color  = source.GetComponent<SpriteRenderer>().material.color;

        if (transform.localScale.y < 0.05) {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Vector3 timeDelta = delta * Time.deltaTime;
        if (col.gameObject.name == "BeakerLiquid") {
            transform.localScale -= timeDelta;
            transform.position += timeDelta/2;
        }
    }
}
