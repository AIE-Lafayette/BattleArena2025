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
        struct Monster
        {
            public string Name;
            public float Health;
            public float Damage;
            public float Defense;
            public float Stamina;
        }

        void PrintStats(Monster monster)
        {
            Console.WriteLine("Name: " + monster.Name);
            Console.WriteLine("Health: " + monster.Health);
            Console.WriteLine("Damage: " + monster.Damage);
            Console.WriteLine("Defense: " + monster.Defense);
            Console.WriteLine("Stamina: " + monster.Stamina);
        }

        float Attack(Monster attacker, Monster defender)
        {
            float totalDamage = attacker.Damage - defender.Defense;

            return defender.Health - totalDamage;
        }

        void Fight(ref Monster monster1, ref Monster monster2)
        {
            PrintStats(monster1);
            PrintStats(monster2);

            Console.WriteLine(monster1.Name + " punches " + monster2.Name + "!");
            monster2.Health = Attack(monster1, monster2);
            Console.ReadKey(true);

            PrintStats(monster1);
            PrintStats(monster2);

            Console.WriteLine(monster2.Name + " punches " + monster1.Name + "!");
            monster1.Health = Attack(monster2, monster1);
            Console.ReadKey(true);

            PrintStats(monster1);
            PrintStats(monster2);
        }

        float Heal(Monster monster, float healAmount)
        {
            float newHealth = monster.Health + healAmount;

            return newHealth;
        }

        public void Run()
        {
            ///Make a function that can heal a monster. The function should take in the monster
            ///that needs to be healed and the amount to heal it by. Return the new health of
            ///the monster.

            Monster JoePable;
            Monster JohnCena;

            JoePable.Name = "JoePable";
            JoePable.Health = 2119f;
            JoePable.Damage = 246.90f;
            JoePable.Defense = .9f;
            JoePable.Stamina = 3;

            JohnCena.Name = "JOHN.....cena";
            JohnCena.Health = 2120f;
            JohnCena.Damage = 246.91f;
            JohnCena.Defense = 1f;
            JohnCena.Stamina = 4f;

            Monster LucyJill;
            LucyJill.Name = "Lucy Jill Dirtbag Biden";
            LucyJill.Health = 2118f;
            LucyJill.Damage = 246.89f;
            LucyJill.Defense = .8f;
            LucyJill.Stamina = 0f;


            Fight(ref JoePable,ref LucyJill);

            Console.Clear();

            Fight(ref JoePable, ref LucyJill);

            Console.Clear();

            Fight(ref JoePable, ref LucyJill);
        }
    }
}
