using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerVFXController : MonoBehaviour
{
    private Player _player;
    [SerializeField] private float _skidThreshold = .5f;
    [SerializeField] private TrailRenderer[] _skidMarkTrails;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (TrailRenderer renderer in _skidMarkTrails)
        {
            renderer.emitting = Mathf.Abs(_player.GetLateralSpeed()) > _skidThreshold;
        }
    }
}
