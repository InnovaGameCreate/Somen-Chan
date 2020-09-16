using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Assets.Scripts.Manager;

public class ModalManagerPresenter : MonoBehaviour
{
    [SerializeField]
    private MainGameManager mainGameManager = default;
    [SerializeField]
    private ModalView modalView = default;
    
    private void Start()
    {
        mainGameManager.CurrentGameState
            .Subscribe(state => 
            {
                switch (state)
                {
                    case GameState.GameOver:
                        
                        break;
                    default:
                        break;
                }
            });

        mainGameManager.IsPause
            .Subscribe(isPause =>
            {
                if (isPause)
                {
                    modalView.ShowModal();
                }
                else
                {
                    modalView.CloseModal();
                }
            });
    }

    public void SetIsPause(bool isPause)
    {
        mainGameManager.SetIsPause(isPause);
    }

    public void SetCurrentGameState(GameState state)
    {
        mainGameManager.SetGameState(state);
    }
}
