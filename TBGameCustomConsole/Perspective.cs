using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBGameCustomConsole {
/*
    North = -1
    East = +1
    West = -1
    South = -1
*/
    /// <summary>
    /// Basic Perspective Class NESW
    /// </summary>
    class Perspective {

        private string _north;
        /// <summary>
        /// The North Descripton
        /// </summary>
        public string North {
            get => $"To your north you see {this._north}"; 
            set => _north = value;
        }

        private string _east;
        /// <summary>
        /// The East Description
        /// </summary>
        public string East {
            get => $"To your east you see {this._east}" ;
            set => _east = value;
        }

        private string _south;
        /// <summary>
        /// The South Description
        /// </summary>
        public string South {
            get => $"To your north you see {this._south}"; 
            set => _south = value;
        }

        private string _west;
        /// <summary>
        /// The West Description
        /// </summary>
        public string West {
            get => $"To your west you see {this._west}";
            set => _west = value;
        }

        private string _current;
        /// <summary>
        /// The Current Description
        /// </summary>
        public string Current {
            get => $"You are at {this._current}";
            set => this._current = value;
        }
    }
}
