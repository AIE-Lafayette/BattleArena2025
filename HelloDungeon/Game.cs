using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HelloDungeon
{
    struct Weapon
    {
        public string Name;
        public float Damage;
    }
    class Game
    {

        bool gameOver;
        int currentScene = 0;

        Character JoePable;
        Character JohnCena;
        Character LucyJill;
        Character LucyLucy;
        Character[] Enemies;
        int currentEnemyIndex = 0;

        Player PlayerCharacter;

        void Fight(ref Character monster2)
        {
            PlayerCharacter.PrintStats();
            monster2.PrintStats();

            bool isDefending = false;
            string battleChoice = PlayerCharacter.GetInput("Choose an action", "Attack", "Defend", "Run");
            
            if (battleChoice == "1")
            {
                monster2.TakeDamage(PlayerCharacter.GetDamage());
                 
                Console.WriteLine("You used " + PlayerCharacter.GetWeapon().Name + " !");

                if (monster2.GetHealth() <= 0)
                {
                    return;
                }
            }
            else if (battleChoice == "2")
            {
                isDefending = true;
                PlayerCharacter.RaiseDefense();
                Console.WriteLine("You grit your teeth.");
            }
            else if (battleChoice == "3")
            {
                Console.WriteLine("You fled from the battle as fast as you could!");
                currentScene = 2;
                return;
            }

            Console.WriteLine(monster2.GetName() + " punches " + PlayerCharacter.GetName() + "!");
            PlayerCharacter.TakeDamage(monster2.GetDamage());

            Console.ReadKey(true);

            if (isDefending == true)
            {
                PlayerCharacter.ResetDefense();
            }
        }

        float Heal(Character monster, float healAmount)
        {
            float newHealth = monster.GetHealth() + healAmount;

            return newHealth;
        }

        void CharacterSelectScene()
        {
            PlayerCharacter = new Player();
            string characterChoice = PlayerCharacter.GetInput("Choose Your Character", JoePable.GetName(), JohnCena.GetName(), LucyJill.GetName(), LucyLucy.GetName());

            if (characterChoice == "1")
            {
                PlayerCharacter = new Player(JoePable.GetName(), JoePable.GetHealth(), JoePable.GetDamage(), JoePable.GetDefense(), JoePable.GetWeapon());
            }
            else if (characterChoice == "2")
            {
                PlayerCharacter = new Player(JohnCena.GetName(), JohnCena.GetHealth(), JohnCena.GetDamage(), JohnCena.GetDefense(), JohnCena.GetWeapon());
            }
            else if (characterChoice == "3")
            {
                PlayerCharacter = new Player(LucyJill.GetName(), LucyJill.GetHealth(), LucyJill.GetDamage(), LucyJill.GetDefense(), LucyJill.GetWeapon());
            }
            else if (characterChoice == "4")
            {
                PlayerCharacter = new Player(LucyLucy.GetName(), LucyLucy.GetHealth(), LucyLucy.GetDamage(), LucyLucy.GetDefense(), LucyLucy.GetWeapon());
            }
            else
            {
                Console.WriteLine("Invalid Input");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                Console.Clear();
                return;
            }

            PlayerCharacter.PrintStats();
            Console.ReadKey(true);
            Console.Clear();
            currentScene = 1;
        }

        void BattleScene()
        {
            Fight(ref Enemies[currentEnemyIndex]);

            Console.Clear();


            if (PlayerCharacter.GetHealth() <= 0 || Enemies[currentEnemyIndex].GetHealth() <= 0)
            {
                currentScene = 2;
            }
        }

        void WinResultsScene()
        {
            if (PlayerCharacter.GetHealth() > 0 && Enemies[currentEnemyIndex].GetHealth() <= 0)
            {
                Console.WriteLine("The winner is: " + PlayerCharacter.GetName());
                currentScene = 1;
                currentEnemyIndex++;

                if (currentEnemyIndex >= Enemies.Length)
                {
                    gameOver = true;
                }
            }
            else if (Enemies[currentEnemyIndex].GetHealth() > 0 && PlayerCharacter.GetHealth() <= 0)
            {
                Console.WriteLine("The winner is: " + Enemies[currentEnemyIndex].GetName());
                currentScene = 3;
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        void EndGameScene()
        {
            ///Create a new overload of the GetInput function that takes in two choices. 
            ///Replace the function below with the new overload.
            string playerChoice = PlayerCharacter.GetInput("You Died. Play Again?", "Yes", "No", "");

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

            JoePable = new Character("Joe Pable", 2119f, 9999999999, .9f, deezHandz);

            JohnCena = new Character("JOHN.....cena", 2120f, 246.91f, 1f, chairAdjustment);

            LucyJill = new Character("Lucy Jill Dirtbag Biden", 2118f, 246.89f, .8f, chairAdjustment);

            LucyLucy = new Character("LUCY Lucy", 2119f, 246.9f, .9f, chairAdjustment);

            //                           0         1         2         3
            Enemies = new Character[4] { JoePable, JohnCena, LucyJill, LucyLucy };

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
