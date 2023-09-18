using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelloDungeon
{
    class Game
    {

        struct Character
        {
            public string Name;
            public float Health;
            public float Damage;
            public float Defense;
            public float Stamina;
            public Weapon CurrentWeapon;
        }

        struct Weapon
        {
            public string Name;
            public float Damage;
        }

        bool gameOver;
        int currentScene = 0;

        Character JoePable;
        Character JohnCena;
        Character LucyJill;
        Character[] Enemies;
        int currentEnemyIndex = 0;

        Character Player;
        void PrintStats(Character character)
        {
            Console.WriteLine("Name: " + character.Name);
            Console.WriteLine("Health: " + character.Health);
            Console.WriteLine("Damage: " + character.Damage);
            Console.WriteLine("Defense: " + character.Defense);
            Console.WriteLine("Stamina: " + character.Stamina);
        }

        float Attack(Character attacker, Character defender)
        {
            float totalDamage = attacker.Damage + attacker.CurrentWeapon.Damage - defender.Defense;

            return defender.Health - totalDamage;
        }

        void Fight(ref Character monster2)
        {
            PrintStats(Player);
            PrintStats(monster2);

            bool isDefending = false;
            string battleChoice = GetInput("Choose an action", "Attack", "Defend", "Run");

            if (battleChoice == "1")
            {
                monster2.Health = 0;
                Console.WriteLine("You used " + Player.CurrentWeapon.Name + " !");

                if (monster2.Health <= 0)
                {
                    return;
                }
            }
            else if (battleChoice == "2")
            {
                isDefending = true;
                Player.Defense *= 5;
                Console.WriteLine("You grit your teeth.");
            }
            else if (battleChoice == "3")
            {
                Console.WriteLine("You fled from the battle as fast as you could!");
                currentScene = 2;
                return;
            }

            Console.WriteLine(monster2.Name + " punches " + Player.Name + "!");
            Player.Health = Attack(monster2, Player);
            Console.ReadKey(true);

            if (isDefending == true)
            {
                Player.Defense /= 5;
            }
        }

        float Heal(Character monster, float healAmount)
        {
            float newHealth = monster.Health + healAmount;

            return newHealth;
        }

        string GetInput(string prompt, string option1, string option2, string option3)
        {
            string playerChoice = "";

            Console.WriteLine(prompt);
            Console.WriteLine("1." + option1);
            Console.WriteLine("2." + option2);
            Console.WriteLine("3." + option3);
            Console.Write(">");

            playerChoice = Console.ReadLine();

            return playerChoice;
        }

        void CharacterSelectScene()
        {
            string characterChoice = GetInput("Choose Your Character", JoePable.Name, JohnCena.Name, LucyJill.Name);

            if (characterChoice == "1")
            {
                Player = JoePable;
            }
            else if (characterChoice == "2")
            {
                Player = JohnCena;
            }
            else if (characterChoice == "3")
            {
                Player = LucyJill;
            }
            else
            {
                Console.WriteLine("Invalid Input");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                Console.Clear();
                return;
            }

            PrintStats(Player);
            Console.ReadKey(true);
            Console.Clear();
            currentScene = 1;
        }

        void BattleScene()
        {
            Fight(ref Enemies[currentEnemyIndex]);

            Console.Clear();

            if (Player.Health <= 0 || Enemies[currentEnemyIndex].Health <= 0)
            {
                currentScene = 2;
            }
        }

        void WinResultsScene()
        {
            if (Player.Health > 0 && Enemies[currentEnemyIndex].Health <= 0)
            {
                Console.WriteLine("The winner is: " + Player.Name);
                currentScene = 1;
                currentEnemyIndex++;

                if (currentEnemyIndex >= Enemies.Length)
                {
                    gameOver = true;
                }
            }
            else if (Enemies[currentEnemyIndex].Health > 0 && Player.Health <= 0)
            {
                Console.WriteLine("The winner is: " + Enemies[currentEnemyIndex].Name);
                currentScene = 3;
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        void EndGameScene()
        {
            string playerChoice = GetInput("You Died. Play Again?", "Yes", "No", "");

            if (playerChoice == "1")
            {
                currentScene = 0;
            }
            else if (playerChoice == "2")
            {
                gameOver = true;
            }
        }

        void Start()
        {
            Weapon deezHandz;
            deezHandz.Name = "Deez Handz";
            deezHandz.Damage = .5f;

            Weapon chairAdjustment;
            chairAdjustment.Name = "Chair Adjustment";
            chairAdjustment.Damage = 500.5f;

            Weapon bidenBlast;

            bidenBlast.Name = "You've done well to make it this far but I've only been using " +
                ".1% of my power. \n Now get ready to face my BIDEN...BLAST!!!!";
            bidenBlast.Damage = 9000.01f;


            JoePable.Name = "JoePable";
            JoePable.Health = 2119f;
            JoePable.Damage = 246.90f;
            JoePable.Defense = .9f;
            JoePable.Stamina = 3;
            JoePable.CurrentWeapon = deezHandz;

            JohnCena.Name = "JOHN.....cena";
            JohnCena.Health = 2120f;
            JohnCena.Damage = 246.91f;
            JohnCena.Defense = 1f;
            JohnCena.Stamina = 4f;
            JohnCena.CurrentWeapon = chairAdjustment;

            LucyJill.Name = "Lucy Jill Dirtbag Biden";
            LucyJill.Health = 2118f;
            LucyJill.Damage = 246.89f;
            LucyJill.Defense = .8f;
            LucyJill.Stamina = 0f;
            LucyJill.CurrentWeapon = chairAdjustment;

            //                           0         1         2  
            Enemies = new Character[3] { JoePable, JohnCena, LucyJill };

        }

        void Update()
        {
            if (currentScene == 0)
            {
                CharacterSelectScene();
            }
            else if (currentScene == 1)
            {
                BattleScene();
            }
            else if (currentScene == 2)
            {
                WinResultsScene();
            }
            else if(currentScene == 3)
            {
                EndGameScene();
            }
        }

        void End()
        {
            Console.WriteLine("Thanks for playing");
        }

        public void Run()
        {
            //start - called before the first loop
            //Used to initialize variables at the beginning of the game.
            Start();

            while (gameOver == false)
            {
                //update - called every time the game loops
                //Used to update game logic like player input, character positions, game timers, etc.
                Update();
            }

            //end - called after the main game loop exits
            //Used to clean up memory or display end game messages.
            End();
        }
    }
}
