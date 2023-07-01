/*  Rotate an object over time, relative to its parent.
 */

using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private Vector3 eulerAngles;

    [SerializeField]
    private Transform rotationTransform;

    private void Update()
    {
        rotationTransform.Rotate(eulerAngles * Time.deltaTime, Space.Self);
    }
}
