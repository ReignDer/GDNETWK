using Unity.Netcode;
using UnityEngine;

public class PokemonData : NetworkBehaviour
{
    public string pokemonName;
    public int maxHP = 100;

    public NetworkVariable<int> currentHP = new NetworkVariable<int>(100);

    public void TakeDamage(int amount)
    {
        currentHP.Value = Mathf.Max(currentHP.Value - amount, 0);
    }
}
