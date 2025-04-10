using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerVFXController : MonoBehaviour
{
    private Player player;
    [SerializeField] private float skidThreshold = .5f;
    [SerializeField] private TrailRenderer[] skidMarkTrails;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (TrailRenderer renderer in skidMarkTrails)
        {
            renderer.emitting = Mathf.Abs(player.GetLateralSpeed()) > skidThreshold;
        }
    }
}
