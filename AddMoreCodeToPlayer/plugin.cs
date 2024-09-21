using Exiled.API.Features;
using UnityEngine;
using Servere = Exiled.Events.Handlers.Server;
using System.Linq;
using MapEditorReborn.API.Features;
using Exiled.API.Enums;
using Unity.Jobs.LowLevel.Unsafe;
using MapEditorReborn.Events.EventArgs;
using Org.BouncyCastle.Crypto.IO;
using MapEditorReborn.Events.Handlers;

namespace AddMoreCodeToPlayer
{
    public class plugin : Plugin<Config>
    {
        public static plugin Instance { get; private set; } = new();
        public override void OnEnabled()
        {
            MapEditorReborn.Events.Handlers.Schematic.SchematicSpawned += SchematicSpawn;
            Instance = this;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            MapEditorReborn.Events.Handlers.Schematic.SchematicSpawned -= SchematicSpawn;
            Instance = null;
            base.OnDisabled();
        }

        public void SchematicSpawn(SchematicSpawnedEventArgs ev)
        {
            if (ev == null) return;

            if (ev.Schematic.Name == "Scp012")
            {
                Log.Debug($"Schematic {ev.Schematic.Name} ha sido generado. Añadiendo componentes...");

                // Agregar componentes a los bloques del schematic
                var block = ev.Schematic.AttachedBlocks.FirstOrDefault(b => b.name == "Collaider");
                var block2 = ev.Schematic.AttachedBlocks.FirstOrDefault(b => b.name == "KillZone");

                if (block != null)
                {
                    block.AddComponent<CapsuleCollider>().isTrigger = true;
                    block.AddComponent<Scp012Firts>();
                }

                if (block2 != null)
                {
                    block2.AddComponent<CapsuleCollider>().isTrigger = true;
                    block2.AddComponent<Scp012Second>();
                }

            }

        }
    }

}

