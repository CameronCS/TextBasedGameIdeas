using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBGameCustomConsole {
    /// <summary>
    /// Part of the player class that handles the player stats
    /// </summary>
    partial class Player {
        /// <summary>
        /// Generic Constructor Defined in this class (Will be refactored)
        /// </summary>
        public Player() {
            this._isAlive = true;
        }

        private bool _isAlive;
        /// <summary>
        /// Check for if the player is alive
        /// </summary>
        public bool IsAlive => _isAlive;

        /// <summary>
        /// The Players Max hit points
        /// </summary>
        public int MaxHp {
            get; set;
        }

        /// <summary>
        /// The Players current Hitpoints
        /// </summary>
        public int CurrentHp {
            get; set;
        }

        /// <summary>
        /// Method to deal damage to the player
        /// </summary>
        /// <param name="damage">The amount of damage to be taken</param>
        public void TakeDamage(int damage) {
            if (this.CurrentHp <= damage) {
                this.CurrentHp = 0;
                this._isAlive = false;

            } else {
                this.CurrentHp -= damage;
            }
 
        }

        /// <summary>
        /// The method to heal the players hit points
        /// </summary>
        /// <param name="healAmount">the amount to heal</param>
        public void Heal(int healAmount) {
            if (healAmount >= this.CurrentHp) {
                this.CurrentHp = this.MaxHp;
            } else {
                this.CurrentHp += healAmount;
            }
        }
    }
}
