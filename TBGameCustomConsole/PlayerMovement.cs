using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBGameCustomConsole {
    /// <summary>
    /// A Partial Class of the played specific to movement
    /// </summary>
    partial class Player {
        /// <summary>
        /// X Position
        /// </summary>
        public int X {
            get; set;
        }
        /// <summary>
        /// Y Position
        /// </summary>
        public int Y {
            get; set;
        }
        /// <summary>
        /// The Current Map Rendered
        /// </summary>
        public List<List<int>> WorldMap {
            get; set;
        }

        /// <summary>
        /// Try move the player north
        /// </summary>
        /// <returns>If the player moved</returns>
        public bool MoveNorth() {
            // The movement variable
            bool @return;

            // Check the player bounds and if its possible to move
            if (this.Y == 0) {
                @return = false;
            } else {
                this.Y -= 1;
                @return = true;
            }
            
            // Return if the player has moved
            return @return;
        }

        /// <summary>
        /// Try Move the player south
        /// </summary>
        /// <returns>if the player moved</returns>
        public bool MoveSouth() {
            // the movement variable
            bool @return;

            // Check the player bounds and if its possible to move
            if (this.Y == this.WorldMap.Count - 1) {
                @return = false;
            } else {
                this.X += 1;
                @return = true;
            }

            // Return if the player moved
            return @return;
        }

        /// <summary>
        /// Try Move the player east
        /// </summary>
        /// <returns>if the player moved</returns>
        public bool MoveEast() {
            // The movement variable
            bool @return;

            // Check the player bounds and if its possible to move
            if (this.X == this.WorldMap[this.Y].Count) {
                @return = false;
            } else {
                this.X += 1;
                @return = true;
            }

            // Return if the player moved
            return @return;
        }

        /// <summary>
        /// Try Move the player west
        /// </summary>
        /// <returns>if the player moved</returns>
        public bool MoveWest() {
            // The movement variable
            bool @return;

            // Check the player bounds and if its possible to move
            if (this.X == 0) {
                @return = false;
            } else {
                this.X -= 1;
                @return = true;
            }

            // Return if the player moved
            return @return;
        }
    }
}
