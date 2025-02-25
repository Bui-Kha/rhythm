using Unity.Mathematics;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private bool canBePressed;
    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();

                if (math.abs(transform.position.y) > 0.25)
                {
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                    GameManager.instance.NormalHit();
                }
                else if (math.abs(transform.position.y) > 0.05f)
                {
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                    GameManager.instance.GoodHit();
                }
                else
                {
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                    GameManager.instance.PerfectHit();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeInHierarchy)
        {
            if (other.tag == "Activator")
            {
                canBePressed = false;

                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }
}
