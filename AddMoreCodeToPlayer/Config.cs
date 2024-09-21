using Exiled.API.Interfaces;
using System.ComponentModel;

namespace AddMoreCodeToPlayer
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Damage per seconds")]
        public int Damage { get; set; } = 10;
        [Description("attraction to the center")]
        public float Atraction { get; set; } = 0.1f;
    }
}
