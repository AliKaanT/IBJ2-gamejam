using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeRewind : MonoBehaviour
{
    public float maxRewindTime = 5f;
    private List<RecordedState> recordedStates = new List<RecordedState>();
    private bool isRewinding = false;
    private bool didRewindStart = false;
    private Rigidbody2D rb;

    [SerializeField] private GameObject _tpMarker;

    [SerializeField] private float _shakeDuration = 1f;
    [SerializeField] private float _shakeStrength = 1f;
    [SerializeField] private int _shakeVibrato = 10;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRewind();
        }
        if (isRewinding)
        {
            RewindTime();
        }
        else
        {
            RecordState();
        }
    }

    private void RecordState()
    {
        if (didRewindStart == false)
        {
            if (recordedStates.Count > Mathf.Round(maxRewindTime / Time.fixedDeltaTime))
            {
                recordedStates.RemoveAt(recordedStates.Count - 1);
            }
            recordedStates.Insert(0, new RecordedState(transform.position, rb.velocity, transform.rotation, Time.time));

            if (recordedStates.Count > 0)
            {
                _tpMarker.transform.position = new Vector3(recordedStates[recordedStates.Count - 1].position.x, recordedStates[recordedStates.Count - 1].position.y, -1f);
            }
        }
    }

    private void StartRewind()
    {
        transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato).OnStart(() =>
        {
            AudioManager.instance.PlayEkkoSound();

            GetComponent<CapsuleCollider2D>().enabled = false;
            didRewindStart = true;
            _tpMarker.SetActive(false);
        }).OnComplete(() =>
        {
            isRewinding = true;
            rb.isKinematic = true;
        });
    }

    private void RewindTime()
    {


        if (recordedStates.Count > 0)
        {
            RecordedState recordedState = recordedStates[0];
            transform.position = recordedState.position;
            rb.velocity = recordedState.velocity;
            transform.rotation = recordedState.rotation;

            for (int i = 0; i < 20; i++)
            {
                if (recordedStates.Count > 0)
                    recordedStates.RemoveAt(0);
            }
        }
        else
        {
            StopRewind();
        }
    }

    private void StopRewind()
    {
        didRewindStart = false;
        isRewinding = false;
        rb.isKinematic = false;
        _tpMarker.SetActive(true);
        GetComponent<CapsuleCollider2D>().enabled = true;
    }
}

public class RecordedState
{
    public Vector3 position;
    public Vector3 velocity;
    public Quaternion rotation;
    public float timestamp;

    public RecordedState(Vector3 pos, Vector3 vel, Quaternion rot, float time)
    {
        position = pos;
        velocity = vel;
        rotation = rot;
        timestamp = time;
    }
}
