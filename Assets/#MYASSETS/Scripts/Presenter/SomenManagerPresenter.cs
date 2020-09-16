using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Assets.Scripts.Somen;
using Assets.Scripts.Manager;

public class SomenManagerPresenter : MonoBehaviour
{
    [SerializeField]
    private GameObject somen = default;
    [SerializeField]
    private MainGameManager Main = default;

    private void Start()
    {
        var core = somen.GetComponent<SomenCore>();
        var somenMove = somen.GetComponent<SomenMove>();
        var somenSound = somen.GetComponent<SomenSound>();

        core.IsAlive
            .Where(isAlive => isAlive)
            .Subscribe(_ =>
            {
                Main.SetGameState(GameState.Main);
            }).AddTo(gameObject);

        // ゲームオーバー処理
        core.IsAlive
            .Where(isAlive => !isAlive && somenMove.IsGrabbed)
            .Subscribe(_ =>
            {
                Main.SetGameState(GameState.GameOver);
            }).AddTo(gameObject);

        Main.CurrentGameState
            .Subscribe(state =>
            {
                core.SetGameState(state);
                switch (state)
                {
                    case GameState.Initialize:
                        core.SetIsAlive(false);
                        somenMove.Stop();
                        core.ResetSomenPosition();
                        break;
                }
            }).AddTo(gameObject);
        
        Main.IsPause
            .Subscribe(isPause =>
            {
                somenMove.SetIsPause(isPause);
            }).AddTo(gameObject);
    }
}
