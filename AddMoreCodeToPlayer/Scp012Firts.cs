using Exiled.API.Features;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AddMoreCodeToPlayer
{
    public class Scp012Firts : MonoBehaviour
    {

        private Vector3 triggerCenter;
        private float attractionSpeed = 5f; // Velocidad de atracción
        private Dictionary<Player, Coroutine> playerCoroutines = new Dictionary<Player, Coroutine>();

        private void Start()
        {
            triggerCenter = GetComponent<Collider>().bounds.center;
        }

        private void OnTriggerEnter(Collider other)
        {
            Player player = Player.Get(other.GetComponentInParent<NetworkIdentity>());
            if (player != null)
            {
                if (!playerCoroutines.ContainsKey(player))
                {
                    Coroutine coroutine = StartCoroutine(AttractPlayer(player));
                    playerCoroutines[player] = coroutine;
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

        private IEnumerator AttractPlayer(Player player)
        {
            while (true)
            {
                MovePlayerToCenter(player);

                // nuevamente
                yield return new WaitForSeconds(plugin.Instance.Config.Atraction);
            }
        }

        private void MovePlayerToCenter(Player player)
        {
            Vector3 direction = (triggerCenter - player.Position).normalized;
            player.Position += direction * attractionSpeed * Time.deltaTime;
        }
    }
}
