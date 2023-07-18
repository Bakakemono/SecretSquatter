using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput = 0;
    float speed = 5.0f;

    List<GameObject> snatchableItems = new List<GameObject>();

    [SerializeField] string itemLayerName = "Item";
    int itemLayer;

    [SerializeField] string stairLayerName = "Stair";
    int stairLayer;

    StairsBehavior closeStair = null;

    private void Start() {
        itemLayer = LayerMask.NameToLayer(itemLayerName);
        stairLayer = LayerMask.NameToLayer(stairLayerName);
    }

    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");

        if(snatchableItems.Count > 0 && Input.GetKeyDown(KeyCode.Q)) {
            float closestItemDistance = float.MaxValue;
            int closestItemIndex = 0;

            for(int i = 0; i < snatchableItems.Count; i++) {
                if(Mathf.Abs(snatchableItems[i].transform.position.x - transform.position.x) < closestItemDistance) {
                    closestItemIndex = i;
                    closestItemDistance = Mathf.Abs(snatchableItems[i].transform.position.x - transform.position.x);
                }
            }

            FindObjectOfType<GameManager>().PickUpItem(snatchableItems[closestItemIndex]);
        }

        //if(closeStair != null && (Input.GetKeyDown()) {
            
        //}
    }

    private void FixedUpdate() {
        transform.position = transform.position + new Vector3(horizontalInput, 0, 0) * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Detect close item to sntach
        if(collision.gameObject.layer == itemLayer) {
            collision.GetComponent<SpriteRenderer>().color = Color.red;

            if(!snatchableItems.Contains(collision.gameObject)) {
                snatchableItems.Add(collision.gameObject);
            }
        }

        if(collision.gameObject.layer == stairLayer) {
            closeStair = collision.GetComponent<StairsBehavior>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.layer == itemLayer) {
            collision.GetComponent<SpriteRenderer>().color = Color.white;

            if(snatchableItems.Contains(collision.gameObject)) {
                snatchableItems.Remove(collision.gameObject);
            }

            if(collision.gameObject.layer == stairLayer) {
                closeStair = null;
            }
        }
    }
}
