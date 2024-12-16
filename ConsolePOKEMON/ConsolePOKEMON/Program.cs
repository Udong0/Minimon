namespace PokemonBattle
{
    using PokemonBattle.Entities;
    using PokemonBattle.GameLogic;

    class Program
    {
        static void Main(string[] args)
        {
            // Display the game title
            Console.WriteLine("╚═╝     ╚═╝╚═╝╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚═╝  ╚═══╝");
            Console.WriteLine("███╗   ███╗██╗███╗   ██╗██╗███╗   ███╗██████╗  ███╗   ██╗");
            Console.WriteLine("████╗ ████║██║████╗  ██║██║████╗ ████║██╔═══██╗████╗  ██║");
            Console.WriteLine("██╔████╔██║██║██╔██╗ ██║██║██╔████╔██║██║   ██║██╔██╗ ██║");
            Console.WriteLine("██║╚██╔╝██║██║██║╚██╗██║██║██║╚██╔╝██║██║   ██║██║╚██╗██║");
            Console.WriteLine("██║ ╚═╝ ██║██║██║ ╚████║██║██║ ╚═╝ ██║╚██████╔╝██║ ╚████║");
            Console.WriteLine("╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚═╝╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝");

            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            // Clear the screen after starting
            Console.Clear();

            // Create moves
            Move tackle = new Move("Tackle", 10);
            Move ember = new Move("Ember", 15);
            Move waterGun = new Move("Water Gun", 15);
            Move vineWhip = new Move("Vine Whip", 12);
            Move thunderShock = new Move("Thunder Shock", 18);

            // Create a list of Pokémon for the player to choose from
            List<Pokemon> availablePokemon = new List<Pokemon>
            {
                new Pokemon("Charmander", 50, new List<Move> { tackle, ember }),
                new Pokemon("Squirtle", 50, new List<Move> { tackle, waterGun }),
                new Pokemon("Bulbasaur", 50, new List<Move> { tackle, vineWhip }),
                new Pokemon("Pikachu", 50, new List<Move> { tackle, thunderShock })
            };

            // Ask the player how many Pokémon they want to pick
            int teamSize = 0;
            while (teamSize < 1 || teamSize > 5)
            {
                Console.Write("How many Pokémon do you want in your team? (Choose 1-5): ");
                int.TryParse(Console.ReadLine(), out teamSize);
            }

            // Create a list to store the player's team
            List<Pokemon> playerTeam = new List<Pokemon>();

            // Let the player choose their Pokémon for the team
            for (int i = 0; i < teamSize; i++)
            {
                Console.WriteLine($"Choose Pokémon {i + 1}:");
                for (int j = 0; j < availablePokemon.Count; j++)
                {
                    Console.WriteLine($"{j + 1}. {availablePokemon[j].Name}");
                }

                int playerChoice = 0;
                while (playerChoice < 1 || playerChoice > availablePokemon.Count || playerTeam.Contains(availablePokemon[playerChoice - 1]))
                {
                    Console.Write("Enter the number of your choice: ");
                    int.TryParse(Console.ReadLine(), out playerChoice);
                }

                playerTeam.Add(availablePokemon[playerChoice - 1]);
                Console.WriteLine($"{availablePokemon[playerChoice - 1].Name} added to your team!");

                // Prevent choosing the same Pokémon again
                availablePokemon.RemoveAt(playerChoice - 1);
            }

            // Create opponent's Pokémon
            Pokemon opponentPokemon = availablePokemon[new Random().Next(availablePokemon.Count)];

            // Start the battle
            Battle battle = new Battle(playerTeam, opponentPokemon);
            battle.Start();

            Console.WriteLine("Game Over.");
        }
    }
}
