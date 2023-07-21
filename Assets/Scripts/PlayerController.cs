using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput = 0;
    float speed = 5.0f;

    //Stairs Prama
    [SerializeField] string itemLayerName = "Item";
    int itemLayer;

    Vector2 pickedUpItemStorageLocation = new Vector2(1000.0f, 1000.0f);
    List<GameObject> snatchableItems = new List<GameObject>();
    List<GameObject> pickedUpItems = new List<GameObject>();

    //Stairs Params
    [SerializeField] string stairLayerName = "Stair";
    int stairLayer;

    [SerializeField] StairsBehavior closeStair = null;
    bool switchingStairs = false;
    bool stairCooldownOnGoing = false;
    int stairCooldownFrameTime = 0;
    int stairCooldownTotalTime = 2;

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

            PickUpItem(snatchableItems[closestItemIndex]);
        }

        if(closeStair != null && !switchingStairs) {
            if(Input.GetKeyDown(KeyCode.W) && closeStair.IsFloorAvailable(StairsBehavior.Direction.UP)) {
                transform.position = closeStair.TakeStair(StairsBehavior.Direction.UP);
                switchingStairs = true;
                stairCooldownOnGoing = true;
                stairCooldownFrameTime = stairCooldownTotalTime;
            }
            else if(Input.GetKeyDown(KeyCode.S) && closeStair.IsFloorAvailable(StairsBehavior.Direction.DOWN)) {
                    transform.position = closeStair.TakeStair(StairsBehavior.Direction.DOWN);
                    switchingStairs = true;
                    stairCooldownOnGoing = true;
                    stairCooldownFrameTime = stairCooldownTotalTime;
            }
        }
    }

    private void FixedUpdate() {
        transform.position = transform.position + new Vector3(horizontalInput, 0, 0) * speed * Time.fixedDeltaTime;

        if(stairCooldownOnGoing) {
            if(stairCooldownFrameTime > 0) {
                stairCooldownFrameTime--;
            }
            else {
                switchingStairs = false;
                stairCooldownOnGoing = false;
            }
        }
    }

    void PickUpItem(GameObject item) {
        pickedUpItems.Add(item);
        item.transform.position = pickedUpItemStorageLocation;
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
        }

        if(!switchingStairs && collision.gameObject.layer == stairLayer) {
            closeStair = null;
        }
    }
}
