using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    /// <summary>
    /// Clase Partida
    /// </summary>
    public class Partida
    {
        /// <summary>
        /// Id de la partida
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la partida
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Escena actual de la partida
        /// </summary>
        public string Escena { get; set; }

        /// <summary>
        /// Puntos de la partida
        /// </summary>
        public int Puntos { get; set; }

        /// <summary>
        /// DeadEnds actuales de la partida
        /// </summary>
        public int DeadEnds { get; set; }
    }
}
