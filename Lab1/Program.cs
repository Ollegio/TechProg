using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    class Globals
    {
        public static SortedDictionary<string, Ability> AllAbilities { get; set; } = new SortedDictionary<string, Ability>();
    }

    class Program
    {

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

        private static void Main(string[] args)
        {
            Globals.AllAbilities.Add("attack", new Ability("attack", Attack, 0));
            Globals.AllAbilities.Add("shield bash", new Ability("shield bash", ShieldBash, 0));
            Globals.AllAbilities.Add("fireball", new Ability("fireball", Fireball, 50));

            var footman1 = new Unit("footman", 300, 0, 15, 5, 100, 2);
            footman1.Abilities.Add("attack");

            var mage1 = new Unit("mage", 200, 40, 10, 1, 150, 2);
            mage1.Abilities.Add("attack");
            mage1.Abilities.Add("fireball");

            mage1.CastAbility("fireball", footman1);

            var mageAbils = from s in mage1.Abilities
                            select s;
            int i = 1;
            foreach (var s in mageAbils)
            {
                Console.Write(i++ + " " + s + "  ");
            }
            Console.WriteLine();

            var allAbils = from a in Globals.AllAbilities
                           select a;
            foreach (var ability in allAbils)
            {
                Console.Write("[" + ability.Value.Name + "] ");
            }
            Console.WriteLine();

            var abils = from s in mage1.Abilities
                        from a in Globals.AllAbilities
                        where a.Value.Name == s
                        select a;
            foreach (var ability in abils)
            {
                Console.WriteLine();
                Console.WriteLine(ability.Value.Name);
                Console.WriteLine(ability.Value.ManaCost + " маны");
            }

            Console.ReadKey();
        }
    }

    class Unit
    {
        public string Name { get; protected set; }
        public List<string> Abilities { get; }

        public int Health { get; protected internal set; }
        public int Mana { get; protected internal set; }
        public int Damage { get; protected set; }
        public int Armor { get; protected set; }
        public int Cost { get; protected set; }
        public int Food { get; protected set; }

        public Unit(string n, int h, int m, int d, int a, int c, int f)
        {
            Abilities = new List<string>();
            Name = n;
            Health = h;
            Mana = m;
            Damage = d;
            Armor = a;
            Cost = c;
            Food = f;
        }

        public Unit()
        {
        }

        public void CastAbility(string ability, Unit target)
        {
            var currentAbility = Globals.AllAbilities[ability];
            currentAbility.This(this, target);
        }

        public void Die() { }

        public override string ToString()
        {
            return $"{Name}: {Health}/{Mana}| Abilities:";
        }
    }

    class Ability
    {
        public string Name { get; }
        public int ManaCost { get; }
        public Action<Unit, Unit> This;
        public Ability(string n, Action<Unit, Unit> ab, int m)
        {
            Name = n;
            This = ab;
            ManaCost = m;
        }
        public void CastThis(Unit c, Unit t)
        {
            if (c.Mana < ManaCost)
            {
                Console.WriteLine("Недостаточно маны");
            }
            else
            {
                This(c, t);
                c.Mana -= ManaCost;
            }
        }
    }
}