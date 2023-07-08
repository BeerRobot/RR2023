using UnityEngine;


// Automatic wall arrow trap
// ToDo: Trigger wall arrows with pressure pads
public class WallArrow : MonoBehaviour
{
    // Private Variables
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject arrowParent;

    [SerializeField] private float speed = 2;

    private GameObject arrow;


    // Start
    private void Start()
    {
        InvokeRepeating("SpawnArrow", 5f, 5f);
    }


    // Update is called once per frame
    void Update()
    {
        if (arrow != null)
        {
            arrow.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Destroy(arrow, 5);
        }
    }


    // Spawn arrow
    private void SpawnArrow()
    {
        arrow = Instantiate(arrowPrefab, arrowParent.transform);
    }
}
