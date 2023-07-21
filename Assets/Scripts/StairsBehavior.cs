using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class StairsBehavior : MonoBehaviour
{
    public enum Direction {
        UP,
        DOWN
    }
    Vector3 stairUp;
    Vector2 stairDown;

    [SerializeField] Transform stairEntranceUp;
    [SerializeField] Transform stairEntranceDown;

    private void Awake() {
        if(stairEntranceUp != null) {
            stairUp = stairEntranceUp.position;
        }
        if(stairEntranceDown != null) {
            stairUp = stairEntranceDown.position;
        }
    }

    public Vector3 TakeStair(Direction direction) {
        switch(direction) {
            case Direction.UP:
                if(stairEntranceUp != null)
                    return stairUp;
                else
                    return transform.position;

            case Direction.DOWN:
                if(stairEntranceDown != null)
                    return stairDown;
                else
                    return transform.position;

            default:
                return transform.position;
        }
    }

    public bool IsFloorAvailable(Direction direction) {
        switch(direction) {
            case Direction.UP:
                if(stairEntranceUp != null)
                    return true;
                else
                    return false;

            case Direction.DOWN:
                if(stairEntranceDown != null)
                    return true;
                else
                    return false;

            default:
                return false;
        }
    }
}
