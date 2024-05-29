using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoneyAmount : MonoBehaviour
{

    [SerializeField] private TMP_Text moneyEarnedDuringGameText;
    public int moneyEarnedInGame;

    private void Update()
    {
        /*
        Player player = GetComponent<Player>();

        if (player != null)
        {
            moneyEarnedInGame += player.currency;

            moneyEarnedDuringGameText.text = moneyEarnedInGame.ToString();
        }*/


        //   Player.Instance.AddCurrency(GetComponent<Beat>().currencyToAdd);
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        if (Player.Instance.currency > moneyEarnedInGame)
        {
            moneyEarnedInGame = Player.Instance.currency;//GetComponent<Player>().currency;
        }

        if (currentScene.name == "Main")
        {
            moneyEarnedDuringGameText.text = "" + moneyEarnedInGame;
        }

    }
  
  
}
