using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    PokemonData data;
    TextMeshProUGUI text;
    [SerializeField] bool player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(player);
        if(player) data = GameObject.Find("PlayerPokemon").GetComponent<PokemonData>();
        else data = GameObject.Find("EnemyPokemon").GetComponent<PokemonData>();
        text = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = data.pokemonName;
        text.text += "\n";
        text.text += "HP: ";
        text.text += data.currentHP.Value.ToString();
    }
}
