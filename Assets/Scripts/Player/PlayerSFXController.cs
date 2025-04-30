using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSFXController : MonoBehaviour
{
    private Player _player;
    private float _enginePitch = .5f;
    private float _tirePitch = .5f;
    [SerializeField] private float _tireSquealThreshold = 1.5f;
    [SerializeField] private AudioSource _engineAudioSource;
    [SerializeField] private AudioSource _tireAudioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSfx();
        UpdateTireScreechSfx();
    }

    private void UpdateEngineSfx()
    {
        float velocity = _player.Velocity / _player.MaxSpeed;
        float volume = Mathf.Clamp(velocity, .2f, 1);
        _enginePitch = Mathf.Clamp(velocity * 2.5f, .5f, 2.5f);
        _engineAudioSource.volume = Mathf.Lerp(_engineAudioSource.volume, volume, Time.deltaTime * 5);
        _engineAudioSource.pitch = Mathf.Lerp(_engineAudioSource.pitch, _enginePitch, Time.deltaTime * 1);
    }

    void UpdateTireScreechSfx()
    {
        if (Mathf.Abs(_player.GetLateralSpeed()) > _tireSquealThreshold)
        {
            _tirePitch = Mathf.Clamp(Mathf.Abs(_player.GetLateralSpeed()) * .2f, .2f, 1);
            _tireAudioSource.volume = Mathf.Lerp(_tireAudioSource.volume, 1.0f, Time.deltaTime * 10);
            _tireAudioSource.pitch = Mathf.Lerp( _tireAudioSource.pitch, _tirePitch, Time.deltaTime * 1.5f);
            return;
        }

        _tireAudioSource.volume = Mathf.Lerp(_tireAudioSource.volume, 0, Time.deltaTime * 10);
    }
}
