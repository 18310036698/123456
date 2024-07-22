using UnityEngine;

public class BoxTriggerGizmo : MonoBehaviour
{
    private BoxCollider2D[] _myBoxColliders;
    private void Start()
    {
        _myBoxColliders = GetComponents<BoxCollider2D>();   
    }

    private void OnDrawGizmos()
    {
        if (_myBoxColliders != null)
        {
            foreach (BoxCollider2D _col in _myBoxColliders)
            {
                if (_col.isTrigger)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(transform.position, new Vector3(_col.size.x, _col.size.y, 1));
                }
            }
        }
    }

}
