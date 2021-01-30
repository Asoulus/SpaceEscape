using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesinfectionRoom : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> _steams = new List<ParticleSystem>();

    [SerializeField]
    private AudioSource _sound = null;

    [SerializeField]
    private LevelLoader _loader = null;

    [SerializeField]
    private bool _canPlay = false;

    [SerializeField]
    private Device _door = null;

    [SerializeField]
    private string _lvlName = string.Empty;

    private void Start()
    {
        ParticleSystem[] tmp = GetComponentsInChildren<ParticleSystem>();

        for (int i = 0; i < tmp.Length; i++)
        {
            _steams.Add(tmp[i]);
        }

        if (!_canPlay)
        {
            if (_door != null)
            {
                StartCoroutine(Doors());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_canPlay)
            return;

        PlayerReference.instance.GetComponent<FirstPersonMovement>().CanMove = false;
        PlayerReference.instance.GetComponent<PlayerJump>().enabled = false;

        //TODO animation + load new lvl
        foreach (var steam in _steams)
        {
            steam.Play();
        }

        PlayerReference.instance.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        _sound.Play();

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.25f);
        _loader.LoadLevel(_lvlName);
    }
    private IEnumerator Doors()
    {
        yield return new WaitForSeconds(1f);
        string[] tmp = new string[1];
        _door.TypeFunctions("open", tmp);
    }
}
