using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TBGameCustomConsole {

    public partial class MainWindow : Window {
        // 2D List for X and Y Co-Ords
        private List<List<int>> w_map;
        // Player Object
        private Player player;

        /// <summary>
        /// Basic Constructor that initialises the Required Components
        /// </summary>
        public MainWindow() {
            // Called By Default
            InitializeComponent();

            // Create the dev room map
            this.w_map = [
                new() { 1, 2, 3 }, // City = 1, POI = 2, Lake = 3
                new(){ 4, 4, 4 }, // path = 4
                new() { 4, 4, 5 }  // Forest = 5
            ];

            // Create new Instance of Player and Initialising It
            this.player = new() {
                WorldMap = this.w_map,
                MaxHp = 50,
                CurrentHp = 50
            };

            // Start the Screen Rendering
            this.SpawnInDevRoom(); 
            this.RenderHp();
        }

        /// <summary>
        /// Spawns the player in the dev room
        /// </summary>
        private void SpawnInDevRoom() {
            // Spawn the Player at these location
            this.player.Y = 2;
            this.player.X = 0;
        }

        /// <summary>
        /// This method is used to get the perspective Object of the players current perspective
        /// </summary>
        /// <returns>Perspective object with its necessary details</returns>
        private Perspective GetPerspective() {
            // Get the users Tile ID from the map Position
            int current_point = this.w_map[this.player.Y][this.player.X];

            // Create the object and Set the current Scene
            Perspective @return = new() {
                Current = this.DescribePoint(current_point)
            };

            // Define the west Point
            int west_point;
            // Check if the west point is a wall or a scene that needs describing
            if (this.player.X == 0) {
                west_point = 0;
            } else {
                west_point = this.w_map[this.player.Y][this.player.X - 1];
            }
            // Set the west point description
            @return.West = this.DescribePoint(west_point);

            // Define the north point
            int north_point;
            // Check if the north point is a wall or a scene that needs describing
            if (this.player.Y == 0) {
                north_point = 0;
            } else {
                north_point = this.w_map[this.player.Y - 1][this.player.X];
            }
            // Set the north point description
            @return.North = this.DescribePoint(north_point);

            // Define the east point
            int east_point;
            // Check if the east point is a wall or a scene that needs describing
            if (this.player.X == 2) {
                east_point = 0;
            } else {
                east_point = this.w_map[this.player.Y][this.player.X + 1];
            }
            // Set the east point description
            @return.East = this.DescribePoint(east_point);

            // Define the south point
            int south_point;
            // Check if the south point is a wall or a point that needs describing
            if (this.player.Y == 2) {
                south_point = 0;
            } else {
                south_point = this.w_map[this.player.Y + 1][this.player.X];
            }
            // Set the south point description
            @return.South = this.DescribePoint(south_point);

            // Return the perspective object
            return @return;
        }
        /// <summary>
        /// This method is used to define what the given tile is and return the description for the tile (Will be refined and refactored)
        /// </summary>
        /// <param name="map_point">The Tile ID</param>
        /// <returns>String of the description of the tile ID</returns>
        private string DescribePoint(int map_point) {
            // Initialise the return value to an empty string so .NET doesnt shit the bed
            string @return = "";
            // Create a switch on the map point and set the return value appropriately depending on the Tile ID
            switch (map_point) {
                case 1:
                    @return = "a city";
                    break;

                case 2:
                    @return = "a point of interest";
                    break;

                case 3:
                    @return = "a Lake";
                    break;

                case 4:
                    @return = " a path";
                    break;

                case 5:
                    @return = "a forest";
                    break;

                case 0:
                    @return = "a wall";
                    break;

            }
            // Return the return string
            return @return;
        }
        /// <summary>
        /// This method creates a string to be used in the UpdateSurroundings(string) method 
        /// </summary>
        /// <param name="p">The Perspective object that was generated</param>
        /// <returns>A String of the current surroundings perspective</returns>
        private string ReturnPerspectiveString(Perspective p) {
            string @return = $"{p.Current}\r";
            @return += $"{p.North}\r";
            @return += $"{p.East}\r";
            @return += $"{p.West}\r";
            @return += $"{p.South}\r";

            return @return;
        }

        /// <summary>
        /// This method is made just to update the surroundings of the player
        /// </summary>
        private void UpdateSurroundings() {
            // Get the perspective object of the users position
            Perspective p = this.GetPerspective();
            // Generate the perspective string
            string currentPerspectiveString = this.ReturnPerspectiveString(p);
            // Add the text to the current text box
            this.RTB_Out.AppendText(currentPerspectiveString);
        }

        /// <summary>
        /// This method is used to parse the current command sent by the user
        /// </summary>
        /// <param name="command">The users current perspective</param>
        private void ParseCommad(string command) {
            // split the command into each individual words by splitting it by the space ' '
            string[] words = command.Split(' ');
            // Check the first word for an action word
            if (words[0].ToUpper() == "GO") {
                // Create a boolean variable to check if the user has moved
                bool playerMoved = false;
                // Create a switch case for the current avaliable positions
                switch (words[1].ToUpper()) {
                    // Try move north
                    case "NORTH":
                        playerMoved = this.player.MoveNorth();
                        break;
                    // Try move east
                    case "EAST":
                        playerMoved = this.player.MoveEast();
                        break;
                    // Try move south
                    case "SOUTH":
                        playerMoved = this.player.MoveSouth();
                        break;
                    // Try move west
                    case "WEST":
                        playerMoved = this.player.MoveWest();
                        break;
                }
                // Check if the player has moved
                if (!playerMoved) {
                    // Output a text to say the user hasnt moved
                    this.RTB_Out.AppendText("You could not move there!\r");
                } else {
                    // Update the surroundings of the player
                    this.UpdateSurroundings();
                }
                // Sample command to test the scene description
            } else if (command.ToUpper() == "DESCRIBE SCENE") {
                // Update the surroundings of the player
                this.UpdateSurroundings();
            // Check the dev command "RM" -> Remove
            } else if (words[0].ToUpper() == "RM") {
                // Check if we are removing Health Points
                if (words[1].ToUpper() == "HP") {
                    // Damage the player
                    this.DamagePlayer();
                }
            // Check the dev command "ADD"-> add
            } else if (words[0].ToUpper() == "ADD") {
                // Check if we are adding hit points
                if (words[1].ToUpper() == "HP") {
                    // Heal the player
                    this.HealPlayer();
                }
            } else {
                // Tell the player the command is unknown
                this.RTB_Out.AppendText("Command Unknown\r");
            }
        }
        /// <summary>
        /// This method handles the key press commands from the textbox
        /// </summary>
        /// <param name="sender">The Object Sending the command</param>
        /// <param name="e">The args for each key event</param>
        private void TBCommand_KeyDown(object sender, KeyEventArgs e) {
            // Check if the key pressed is enter
            if (e.Key == Key.Enter) {
                // Get the command text
                string cmd = this.TBCommand.Text;
                // Update the output to display the command
                this.SendUpdate($"\r--{cmd}--\r");
                // Parse the command
                this.ParseCommad(cmd);
                // Clear the command box
                this.TBCommand.Clear();

                // Check to see if the player died, if they did disable the command box
                if (!this.player.IsAlive) {
                    this.TBCommand.IsEnabled = false;
                }
            }
        }
        /// <summary>
        /// Display the health of the player
        /// </summary>
        private void RenderHp() {
            // Set the content of each label to the current hp
            this.CurrentHp.Content = $"{this.player.CurrentHp}";
            this.MaxHp.Content = $"{this.player.MaxHp}";
        }

        /// <summary>
        /// This method is used to damage the player
        /// </summary>
        private void DamagePlayer() {
            // Tell the player to take 10 (Magic Number) damage
            this.player.TakeDamage(10);
            // Render the players current HP
            this.RenderHp();
            // Check if the player is still alive and render the appropriate message
            if (this.player.IsAlive) {
                this.SendUpdate("You took 10 Damage");
            } else {
                this.SendUpdate("You Died!");
            }
            
        }

        /// <summary>
        /// This method is used just to heal the player
        /// </summary>
        private void HealPlayer() {
            // Heal the player 15 (Magic Number) health
            this.player.Heal(15);
            // Update the players HP
            this.RenderHp();
            // Tell the user they healed HP
            this.SendUpdate("You healed 15 hp");
        }

        /// <summary>
        /// This method just sends the text to the screen with the return character
        /// </summary>
        /// <param name="msg">The message to be output</param>
        private void SendUpdate(string msg) {
            // Add the message
            this.RTB_Out.AppendText($"{msg}\r");
        }
    }
}

// Mana regen after n amount of turns
// n is determined by class