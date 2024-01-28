using UnityEngine;

public class SpotlightRaycast : MonoBehaviour
{
    public float raycastDistance = 10f;
    public LayerMask playerLayer;
    public int rayCount = 12; // Number of rays to cast for the cone pattern
    [SerializeField] private Vector3[] initialRayDirections;
    //public ActivateText activateText;
    public TextBoxManager theTextBox;
    public string[] textLines;
    public int startLine;
    public int endLine;
    public TextAsset text;

    void Start()
    {
        theTextBox = FindObjectOfType<TextBoxManager>();

        if (text != null)
        {
            textLines = (text.text.Split('\n'));
        }

        if (endLine == 0)
        {
            endLine = textLines.Length - 1;
        }
        // Calculate initial ray directions based on the spotlight's cone angle
        initialRayDirections = CalculateRayDirections();
    }

    void Update()
    {
        // Loop through the ray directions
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 initialRayDirection = initialRayDirections[i];

            // Rotate the initial direction to match the light's current orientation
            Vector3 rayDirection = transform.rotation * initialRayDirection;

            // Shoot a ray in the current direction
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDirection, out hit, raycastDistance, playerLayer))
            {
                // Check if the raycast hit a player
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player detected!");
                    // You can perform actions here when a player is detected.
                    theTextBox.ReloadScript(text);
                    theTextBox.currentLine = startLine;
                    theTextBox.endAtLine = endLine;
                    theTextBox.EnableTextBox();
                }
            }

            // Debug draw a line to visualize the direction and length of the ray
            Debug.DrawRay(transform.position, rayDirection * raycastDistance, Color.green);
        }
    }

    Vector3[] CalculateRayDirections()
    {
        Vector3[] directions = new Vector3[rayCount];
        float halfConeAngle = transform.GetComponent<Light>().spotAngle / 2f;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = (2 * halfConeAngle / (rayCount - 1)) * i - halfConeAngle;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            directions[i] = rotation * Vector3.forward;
        }

        return directions;
    }
}