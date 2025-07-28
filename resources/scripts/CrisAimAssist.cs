using UnityEngine;

public class CrisAimAssist : MonoBehaviour
{
    public float assistRadius = 10f;
    public LayerMask enemyLayer;
    public Transform aimTarget;

    void Update()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, assistRadius, enemyLayer);
        Transform closestHead = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            Transform head = enemy.transform.Find("head");
            if (head == null) continue;

            float distance = Vector3.Distance(aimTarget.position, head.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestHead = head;
            }
        }

        if (closestHead != null)
        {
            aimTarget.position = Vector3.Lerp(aimTarget.position, closestHead.position, 1f);
            Vector3 dir = (closestHead.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
