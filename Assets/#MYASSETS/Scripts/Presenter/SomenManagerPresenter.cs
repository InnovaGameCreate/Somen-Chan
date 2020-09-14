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

        core.IsAlive
            .Where(isAlive => isAlive)
            .Subscribe(_ =>
            {
                Main.SetGameState(GameState.Main);
            }).AddTo(gameObject);

        Main.CurrentGameState
            .Subscribe(state =>
            {
                core.SetGameState(state);
                if (state == GameState.Initialize)
                {
                    core.SetIsAlive(false);
                    somenMove.Stop();
                    core.ResetSomenPosition();
                }
            }).AddTo(gameObject);
    }
}
