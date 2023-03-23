using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    private float _speed = 3f;
    [SerializeField] private NetworkVariable<ushort> hitPoints = new NetworkVariable<ushort>();

    private void Initialise()
    {
        // Do any specific set up things here,
        // such as GetComponent, resetting/randomising values, etc.
        hitPoints.Value = (ushort)Random.Range(50, 100);
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Initialise();
    }

    void Update()
    {
        // A guard clause that prevents player movement from being executed if we're using another window or not the owner.
        if (!IsOwner || !Application.isFocused) return;

        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * _speed;
    }

    private void OnGUI()
    {
        if (!IsOwner) return;

        GUILayout.BeginArea(new Rect(30, 30, 200, 40));
        GUILayout.Label("Hit points: " + hitPoints.Value.ToString());
        GUILayout.EndArea();
    }
}
