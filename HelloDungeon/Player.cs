using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace HelloDungeon
{
    internal class Player : Character
    {
        private string _playerChoice;
        private int _lives = 3;

        public Player()
        {
            _playerChoice = "";
        }

        public Player(string name, float health, float damage, float defense, Weapon weapon) : base(name, health, damage, defense, weapon)
        {
            _playerChoice = "";
        }

        public override void PrintStats()
        {
            Console.WriteLine("Name: " + GetName());
            Console.WriteLine("Health: " + GetHealth());
            Console.WriteLine("Damage: " + GetDamage());
            Console.WriteLine("Defense: " + GetDefense());
            Console.WriteLine("Lives: " + _lives);
        }

        public string GetInput(string prompt, string option1, string option2, string option3)
        {
            _playerChoice = "";

            Console.WriteLine(prompt);
            Console.WriteLine("1." + option1);
            Console.WriteLine("2." + option2);
            Console.WriteLine("3." + option3);
            Console.Write(">");

            _playerChoice = Console.ReadLine();

            return _playerChoice;
        }

        public string GetInput(string prompt, string option1, string option2, string option3, string option4)
        {
            _playerChoice = "";

            Console.WriteLine(prompt);
            Console.WriteLine("1." + option1);
            Console.WriteLine("2." + option2);
            Console.WriteLine("3." + option3);
            Console.WriteLine("4." + option4);
            Console.Write(">");

            _playerChoice = Console.ReadLine();

            return _playerChoice;
        }
    }
}
