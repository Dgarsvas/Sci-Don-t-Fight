using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// How to use: add script to game object in scene
// add game objects to scene and make them children of this object for convenience.
// add them to the list below in order the npc is supposed traverse the points.


[DefaultExecutionOrder(-1)]
public class GlobalNavigationController : Singleton<GlobalNavigationController>
{ 
    // TODO: provide more convenient way to supply points
    // ordered in way you're supposed to go
    [SerializeField] private List<GameObject> OrderedListOfPointsForNavigation;

    public bool hasPoints => OrderedListOfPointsForNavigation != null && OrderedListOfPointsForNavigation.Count > 0;


    public Vector3 GetClosestPoint(Vector3 pos)
    {
        float length = 99999999999999.0f;
        int idx = -1;

        for(int i = 0; i < OrderedListOfPointsForNavigation.Count; i++)
        {
            var position = OrderedListOfPointsForNavigation[i].transform.position;
            float mag = (pos - position).magnitude;
            if ( mag < length)
            {
                idx = i;
                length = mag;
            }
        }
        return OrderedListOfPointsForNavigation[idx].transform.position;
    }

    public Vector3 GetNextPoint(Vector3 pos)
    {
        // probably should use custom data structure that has a vec3 and an index so we don't have to cycle through so many times
        // no time fo dat now
        int idx = -1;

        for (int i = 0; i < OrderedListOfPointsForNavigation.Count; i++)
        {
            var position = OrderedListOfPointsForNavigation[i].transform.position;
            if((pos - position).magnitude <= 1.0f) // navmesh probably corrects destination so this doesn't work very well, must make the epsilon big
                                                    // that means that positions CANT be near each other
            {
                idx = (i + 1) % OrderedListOfPointsForNavigation.Count;
                return OrderedListOfPointsForNavigation[idx].transform.position;
            }
        }

        Debug.LogError("Damn");
        return default;
    }
}
