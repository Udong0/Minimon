namespace PokemonBattle.GameLogic
{
    using PokemonBattle.Entities;

    // Class to handle the battle logic
    public class Battle
    {
        private List<Pokemon> PlayerTeam;
        private Pokemon OpponentPokemon;
        private int PlayerPokemonIndex; // Index for the current active Pokémon

        public Battle(List<Pokemon> playerTeam, Pokemon opponent)
        {
            PlayerTeam = playerTeam;
            OpponentPokemon = opponent;
            PlayerPokemonIndex = 0; // Start with the first Pokémon in the player's team
        }

        public void Start()
        {
            Console.WriteLine($"A wild {OpponentPokemon.Name} appeared!");
            Console.WriteLine($"Go {PlayerTeam[PlayerPokemonIndex].Name}!");
            Console.ReadKey(); // Wait for user input to proceed
            Console.Clear();

            while (PlayerTeam[PlayerPokemonIndex].Health > 0 && OpponentPokemon.Health > 0)
            {
                ShowHP(); // Display HP before each turn
                PlayerTurn();
                if (OpponentPokemon.Health <= 0) break;

                OpponentTurn();
                if (PlayerTeam[PlayerPokemonIndex].Health <= 0) break;
            }

            ShowHP(); // Final HP display after the battle
            if (PlayerTeam[PlayerPokemonIndex].Health > 0)
            {
                Console.WriteLine($"You defeated {OpponentPokemon.Name}!");
            }
            else
            {
                Console.WriteLine($"Your {PlayerTeam[PlayerPokemonIndex].Name} fainted! You lost the battle.");
            }

            Console.WriteLine("Press Enter to end...");
            Console.ReadKey();
        }

        private void ShowHP()
        {
            Console.Clear(); // Clear screen before displaying current HP
            Console.WriteLine("Current HP:");
            Console.WriteLine($"{PlayerTeam[PlayerPokemonIndex].Name}: {PlayerTeam[PlayerPokemonIndex].Health} HP");
            Console.WriteLine($"{OpponentPokemon.Name}: {OpponentPokemon.Health} HP");
        }

        private void PlayerTurn()
        {
            Console.WriteLine("\nYour Turn! Choose a move or swap Pokémon:");

            // If the player has more than one Pokémon, show the swap option
            if (PlayerTeam.Count > 1)
            {
                Console.WriteLine("1. Fight");
                Console.WriteLine("2. Swap Pokémon");
            }
            else
            {
                Console.WriteLine("1. Fight");
                Console.WriteLine("There are no other Pokémon to swap.");
            }

            int choice = 0;
            while (choice < 1 || choice > (PlayerTeam.Count > 1 ? 2 : 1))  // Limit options based on team size
            {
                Console.Write("Enter your choice: ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            if (choice == 1)
            {
                // Player chooses a move
                SelectMove();
            }
            else
            {
                // Player chooses to swap Pokémon
                SwapPokemon();
            }
        }

        private void SelectMove()
        {
            Console.Clear(); // Clear the screen before showing the available moves
            Console.WriteLine("Choose a move:");
            for (int i = 0; i < PlayerTeam[PlayerPokemonIndex].Moves.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {PlayerTeam[PlayerPokemonIndex].Moves[i].Name} (Power: {PlayerTeam[PlayerPokemonIndex].Moves[i].Power})");
            }

            int choice = 0;
            while (choice < 1 || choice > PlayerTeam[PlayerPokemonIndex].Moves.Count)
            {
                Console.Write("Enter the number of your move: ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            Move selectedMove = PlayerTeam[PlayerPokemonIndex].Moves[choice - 1];
            Console.Clear(); // Clear the screen before showing the result
            Console.WriteLine($"{PlayerTeam[PlayerPokemonIndex].Name} used {selectedMove.Name}!");
            OpponentPokemon.TakeDamage(selectedMove.Power);
            Console.WriteLine($"{OpponentPokemon.Name} has {OpponentPokemon.Health} HP remaining.");
            Console.WriteLine("Press Enter to next...");
            Console.ReadKey(); // Wait for Enter key to continue to next action
        }

        private void SwapPokemon()
        {
            if (PlayerTeam.Count == 1)
            {
                Console.Clear(); // Clear the screen before showing the message
                Console.WriteLine("You have no other Pokémon to swap to!");
                Console.WriteLine("Press Enter to next...");
                Console.ReadKey();
                return;
            }

            Console.Clear(); // Clear the screen before showing the available Pokémon
            Console.WriteLine("Choose a Pokémon to swap in:");

            for (int i = 0; i < PlayerTeam.Count; i++)
            {
                if (i != PlayerPokemonIndex) // Don't show the currently active Pokémon
                {
                    Console.WriteLine($"{i + 1}. {PlayerTeam[i].Name} (Health: {PlayerTeam[i].Health} HP)");
                }
            }

            int choice = 0;
            while (choice < 1 || choice > PlayerTeam.Count || choice == PlayerPokemonIndex + 1)
            {
                Console.Write("Enter the number of the Pokémon you want to swap in: ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            PlayerPokemonIndex = choice - 1; // Update the active Pokémon index
            Console.Clear();
            Console.WriteLine($"You swapped in {PlayerTeam[PlayerPokemonIndex].Name}!");
            Console.WriteLine("Press Enter to next...");
            Console.ReadKey(); // Wait for Enter key to continue to next action
        }

        private void OpponentTurn()
        {
            Random random = new Random();
            Move selectedMove = OpponentPokemon.Moves[random.Next(OpponentPokemon.Moves.Count)];

            Console.Clear(); // Clear the screen before showing the result
            Console.WriteLine($"{OpponentPokemon.Name} used {selectedMove.Name}!");
            PlayerTeam[PlayerPokemonIndex].TakeDamage(selectedMove.Power);
            Console.WriteLine($"{PlayerTeam[PlayerPokemonIndex].Name} has {PlayerTeam[PlayerPokemonIndex].Health} HP remaining.");
            Console.WriteLine("Press Enter to next...");
            Console.ReadKey(); // Wait for Enter key to continue to next action
        }
    }
}
