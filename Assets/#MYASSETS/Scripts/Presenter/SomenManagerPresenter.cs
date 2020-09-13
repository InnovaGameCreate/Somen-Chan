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
    private GameObject somen;
    [SerializeField]
    private MainGameManager Main;

    private void Start()
    {
        var core = somen.GetComponent<SomenCore>();

        core.IsAlive
            .Where(isAlive => isAlive)
            .Subscribe(_ =>
            {
                Main.SetGamaState(GameState.Main);
            }).AddTo(gameObject);
        
        Main.CurrentGameState
            .Subscribe(state => core.SetGameState(state)).AddTo(gameObject);
    }
}
