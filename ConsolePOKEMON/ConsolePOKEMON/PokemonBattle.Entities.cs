namespace PokemonBattle.Entities
{
    // Class to represent a Pokemon
    public class Pokemon
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public List<Move> Moves { get; set; }

        public Pokemon(string name, int health, List<Move> moves)
        {
            Name = name;
            Health = health;
            Moves = moves;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }
    }
}