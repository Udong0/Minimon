namespace PokemonBattle
{
    // Class to represent a move
    public class Move
    {
        public string Name { get; set; }
        public int Power { get; set; }

        public Move(string name, int power)
        {
            Name = name;
            Power = power;
        }
    }
}