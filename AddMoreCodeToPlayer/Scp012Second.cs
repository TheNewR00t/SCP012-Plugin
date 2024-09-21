using Exiled.API.Features;
using MEC;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AddMoreCodeToPlayer
{
    public class Scp012Second : MonoBehaviour
    {
        private Dictionary<Player, Coroutine> playerCoroutines = new Dictionary<Player, Coroutine>();

        private void OnTriggerEnter(Collider other)
        {
            Player player = Player.Get(other.GetComponentInParent<NetworkIdentity>());
            if (player != null)
            {
                if (!player.IsScp)
                {
                    if (!playerCoroutines.ContainsKey(player))
                    {
                        Coroutine coroutine = StartCoroutine(DamagePlayer(player));
                        playerCoroutines.Add(player, coroutine);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Player player = Player.Get(other.GetComponentInParent<NetworkIdentity>());
            if (player != null)
            {
                if (playerCoroutines.TryGetValue(player, out Coroutine coroutine))
                {
                    StopCoroutine(coroutine);
                    playerCoroutines.Remove(player);
                }
            }
        }

        private IEnumerator DamagePlayer(Player player)
        {
            while (true)
            {
                if (player.IsScp)
                {
                    player.Broadcast(5, "You can't die!");
                }
                else
                {
                    Log.Debug($"Applying damage to {player.Nickname}!");
                    player.Hurt(plugin.Instance.Config.Damage, Exiled.API.Enums.DamageType.Bleeding);
                }

                yield return new WaitForSeconds(1f);
            }
        }
    }
}
