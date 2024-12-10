using UnityEngine;
using UnityEngine.AI;

public class Raygun : MonoBehaviour
{
    public LayerMask layerMask;
    public OVRInput.RawButton shootingButton;
    public LineRenderer linePrefab;
    public GameObject rayImpactPrefab;
    public Transform shootingPoint;
    public float maxLineDistance = 5;
    public float lineShowTimer = 0.3f;
    public AudioSource source;
    public AudioClip shootingAudioClip;

    private void Start()
    {
    }

    private void Update()
    {
        if (OVRInput.GetDown(shootingButton)) Shoot();
    }

    public void Shoot()
    {
        source.PlayOneShot(shootingAudioClip);

        Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layerMask);

        Vector3 endPoint = Vector3.zero;
        
        if (hasHit)
        {
            endPoint = hit.point;
            
            Ghost ghost = hit.transform.GetComponentInParent<Ghost>();

            if (ghost)
            {
                hit.collider.enabled = false;
                ghost.Kill();
            }
            else
            {
            Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);
                
            GameObject rayImpact = Instantiate(rayImpactPrefab, hit.point, rayImpactRotation);
            Destroy(rayImpact, 1);
            }
        }
        else
        {
            endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;
        }

        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2;
        line.SetPosition(0, shootingPoint.position);
        line.SetPosition(1, endPoint);

        Destroy(line.gameObject, lineShowTimer);
    }



}