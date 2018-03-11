using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        public static SortedDictionary<string, Ability> allAbilities = new SortedDictionary<string, Ability>();

        public static void Attack(Unit caster, Unit target)
        {
            target.Health -= (caster.Damage - target.Armor) < 0 ? 1 : caster.Damage - target.Armor;
            if (target.Health < 1)
                target.Die();
            Console.WriteLine(caster.Name + " attacks " + target.Name);
        }

        public static void Fireball(Unit caster, Unit target)
        {
            target.Health -= (caster.Damage * 5);
            caster.Mana -= 50;
            if (target.Health < 1)
                target.Die();
            Console.WriteLine("Fireball");
        }

        public static void ShieldBash(Unit caster, Unit target)
        {
            target.Health -= (caster.Damage + caster.Armor * 2 - target.Armor) < 0 ? 1 : caster.Damage + caster.Armor * 2 - target.Armor;
            if (target.Health < 1)
                target.Die();
            Console.WriteLine(caster.Name + " bashes " + target.Name + " with shield ");
        }

        static void Main(string[] args)
        {
            var units = new List<Unit>();
            var buildings = new List<Building>();

            allAbilities.Add("attack", new Ability("attack", Attack, 0));
            allAbilities.Add("shield bash", new Ability("shield bash", ShieldBash, 0));
            allAbilities.Add("fireball", new Ability("fireball", Fireball, 50));

            var footman1 = new Unit("footman", 300, 0, 15, 5, 100, 2);
            footman1.abilities.Add("attack");

            var mage1 = new Unit("mage", 200, 40, 10, 1, 150, 2);
            mage1.abilities.Add("attack");
            mage1.abilities.Add("fireball");

            var fortress1 = new Building("fortress", 1000, 0, 15, 500);

            //footman1.CastAbility("attack", mage1);
            //footman1.CastAbility("attack", fortress1);
            mage1.CastAbility("fireball", footman1);

            var mageAbils = from s in mage1.abilities
                            select s;
            int i = 1;
            foreach (var s in mageAbils)
            {
                Console.Write(i++ + " " + s + "  ");
            }
            Console.WriteLine();

            var allAbils = from a in allAbilities
                           select a;
            foreach (var a in allAbils)
            {
                Console.Write("[" + a.Value.name + "] ");
            }
            Console.WriteLine();

            var abils = from s in mage1.abilities
                        from a in allAbilities
                        where a.Value.name == s
                        select a;
            foreach (var a in abils)
            {
                Console.WriteLine();
                Console.WriteLine(a.Value.name);
                Console.WriteLine(a.Value.manaCost + " маны");
            }
        }
    }

    class Unit
    {
        protected string name;
        public List<string> abilities;
        protected int health,
            mana,
            damage,
            armor,
            cost,
            food;

        public Unit(string n, int h, int m, int d, int a, int c, int f)
        {
            abilities = new List<string>();
            name = n;
            health = h;
            mana = m;
            damage = d;
            armor = a;
            cost = c;
            food = f;
        }

        public Unit()
        {
        }

        public int Health { get => health; set => health = value; }
        public int Mana { get => mana; set => mana = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Armor { get => armor; set => armor = value; }
        public int Cost { get => cost; set => cost = value; }
        public int Food { get => food; set => food = value; }
        public string Name { get => name; set => name = value; }

        public void CastAbility(string ability, Unit target)
        {
            var currentAbility = Program.allAbilities[ability];
            currentAbility.This(this, target);
        }

        public void CastAbility(string ability)
        {

        }

        public void Die()
        {

        }
    }

    class Building : Unit
    {
        public Building(string n, int h, int d, int a, int c)
        {
            name = n;
            health = h;
            damage = d;
            armor = a;
            cost = c;
        }
    }

    class Ability
    {
        public string name;
        public int manaCost;
        public Action<Unit, Unit> This;
        public Ability(string n, Action<Unit, Unit> ab, int m)
        {
            name = n;
            This = ab;
            manaCost = m;
        }
        public void CastThis(Unit c, Unit t)
        {
            if (c.Mana < manaCost)
            {
                Console.WriteLine("Недостаточно маны");
            }
            else
            {
                This(c, t);
                c.Mana -= manaCost;
            }
        }
    }
}