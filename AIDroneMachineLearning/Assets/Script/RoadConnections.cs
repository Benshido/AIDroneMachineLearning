using System.Collections.Generic;
using UnityEngine;

public class RoadConnections : MonoBehaviour
{
    public List<GameObject> ConnectingNotes;

    private void OnDrawGizmosSelected()
    {
        // Set the color for the Gizmos
        Gizmos.color = Color.green;

        // Draw a sphere at the position of this GameObject
        Gizmos.DrawSphere(transform.position, 0.2f);

        // Iterate through the list of connecting notes
        foreach (GameObject note in ConnectingNotes)
        {
            if (note != null)
            {
                // Draw a line from this GameObject to each connecting note
                Gizmos.DrawLine(transform.position, note.transform.position);

                // Optionally, draw a sphere at the position of each connecting note
                Gizmos.DrawSphere(note.transform.position, 0.1f);
            }
        }
    }
}