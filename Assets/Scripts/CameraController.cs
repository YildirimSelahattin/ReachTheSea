
using UnityEngine;
using DG.Tweening;
public class CameraController : MonoBehaviour
{
    private bool doMovement = true;
        
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float minY =10f;
    public float maxY =90f;

    public float scrollSpeed = 5f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
        {
            return;
        }
        
        
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.DOLocalMove( transform.position + (Vector3.forward*panSpeed) ,panSpeed).SetSpeedBased(true);
        }
        if (Input.GetKey("s")|| Input.mousePosition.y <= panBorderThickness)
        {
            transform.DOLocalMove(transform.position + (Vector3.back*panSpeed),panSpeed ).SetSpeedBased(true);
        }
        if (Input.GetKey("d")|| Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.DOLocalMove(transform.position + (Vector3.right*panSpeed),panSpeed ).SetSpeedBased(true);
        }
        if (Input.GetKey("a")|| Input.mousePosition.x <= panBorderThickness)
        {
            transform.DOLocalMove(transform.position + (Vector3.left*panSpeed),panSpeed ).SetSpeedBased(true);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
