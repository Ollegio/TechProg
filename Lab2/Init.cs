using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public static class Init
    {
        public static SortedDictionary<string, Ability> AllAbilities = new SortedDictionary<string, Ability>();
        public static SortedDictionary<string, Unit> Units = new SortedDictionary<string, Unit>();

        static Init()
        {
            AllAbilities.Add("attack", new Ability("attack", Attack, 0));
            AllAbilities.Add("shield bash", new Ability("shield bash", ShieldBash, 0));
            AllAbilities.Add("fireball", new Ability("fireball", Fireball, 50));
            var footman1 = new Unit("footman", 300, 0);
            footman1.abilities.Add("attack");

            var mage1 = new Unit("mage", 200, 40);
            mage1.abilities.Add("attack");
            mage1.abilities.Add("fireball");

            var fortress1 = new Building("fortress", 1000, 0, 15, 500);
            Units.Add(footman1.Name, footman1);
            Units.Add(mage1.Name, mage1);
            Units.Add(fortress1.Name, fortress1);
        }

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
    }

    public class Unit
    {
        protected string name;
        public List<string> abilities;

        public int Health { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Cost { get; set; }
        public int Food { get; set; }
        public string Name => name;

        public Unit(string n, int h, int m)
        {
            abilities = new List<string>();
            name = n;
            Health = h;
            Mana = m;
        }

        public Unit()
        {

        }

        public void CastAbility(string ability, Unit target)
        {
            var currentAbility = Init.AllAbilities[ability];
            currentAbility.This(this, target);
        }

        public void Die()
        {

        }

        public override string ToString()
        {
            var str = $"{Name} : {Health}/{Mana} | Abilities:";
            if (abilities == null)
                return str;
            foreach (var a in abilities)
            {
                str = str + a + ";";
            }

            return str;
        }
    }

    public class Building : Unit
    {
        public Building(string n, int h, int d, int a, int c)
        {
            name = n;
            Health = h;
            Damage = d;
            Armor = a;
            Cost = c;
        }
    }

    public class Ability
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

        public override string ToString()
        {
            return name;
        }
    }
}