using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    protected readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    [SerializeField] private Transform[] _transforms;
    [SerializeField] private Character[] _characters;
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private WhiteFlameInterectable _whiteFlame;
    [SerializeField] private TutorialGate _tutorialGate;
    [SerializeField] private CinemachineVirtualCamera _vcam;

    private bool _moveCharacters;
    private bool _canChannel;
    private float _channelingTime = 5f;
    private bool _isChanneling;
    private bool _hasChanneled;

    private void Start()
    {
        _tutorialGate.OnCharactersPassed += EndTutorial;

        foreach (Character character in _characters)
        {
            character.Animator.CrossFadeInFixedTime(character.FreeLookBlendTreeHash, .1f);
        }

        StartCoroutine(TutorialRoutine());
    }

    private void Update()
    {
        if (_moveCharacters)
        {
            for (int i = 0; i < _characters.Length; i++)
            {
                Character character = _characters[i];
                if (!_hasChanneled)
                {
                    if (i == 0)
                        character.NavMeshAgent.destination = _transforms[0].position;
                    else if (i == 1)
                        character.NavMeshAgent.destination = _transforms[1].position;
                }
                else
                {
                    character.NavMeshAgent.destination = _transforms[2].position;
                }

                float animationBlend = character.NavMeshAgent.velocity.normalized.magnitude > .1 ? 1f : 0f;
                character.Animator.SetFloat(FreeLookSpeedHash, animationBlend, .075f, Time.deltaTime);

                if (!_hasChanneled && character.NavMeshAgent.remainingDistance != 0 && character.NavMeshAgent.remainingDistance <= character.NavMeshAgent.stoppingDistance)
                {
                    character.NavMeshAgent.ResetPath();
                    _moveCharacters = false;
                    _characters[0].Animator.SetFloat(FreeLookSpeedHash, 0f);
                    _characters[1].Animator.CrossFadeInFixedTime("StartChanneling", .1f);
                    _isChanneling = true;
                }
            }
        }
        else if (_isChanneling)
        {
            _channelingTime -= Time.deltaTime;

            if (_channelingTime <= 0)
            {
                _isChanneling = false;

                _whiteFlame.LightWhiteFlame();

                _hasChanneled = true;
                _moveCharacters = true;

                _characters[0].Animator.CrossFadeInFixedTime(_characters[0].FreeLookBlendTreeHash, .1f);
                _characters[1].Animator.CrossFadeInFixedTime(_characters[1].FreeLookBlendTreeHash, .1f);
            }
        }
    }

    private IEnumerator TutorialRoutine()
    {
        _dialogue.ShowDialogue();
        yield return new WaitUntil(() => _dialogue.DialogueFinished);

        _moveCharacters = true;
    }

    private void EndTutorial()
    {
        _vcam.m_Follow = null;
        Debug.Log("Tutorial ended");

        Invoke(nameof(EndTutorialRoutine), 1f);
    }

    private void EndTutorialRoutine()
    {
        SceneManager.LoadScene(2);
    }
}
