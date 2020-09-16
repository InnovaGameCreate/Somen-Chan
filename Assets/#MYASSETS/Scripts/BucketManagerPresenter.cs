using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Assets.Scripts.Manager;

public class BucketManagerPresenter : MonoBehaviour
{
    [SerializeField]
    private MainGameManager mainGameManager = default;
    private void Start()
    {
        mainGameManager.CurrentGameState
            .Subscribe(state =>
            {
                
            });
    }

    public void SetGameState(GameState state)
    {
        mainGameManager.SetGameState(state);
    }
}
