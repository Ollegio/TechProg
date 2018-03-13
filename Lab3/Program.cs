using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder
{

    public class MainApp
    {
        public static void Main()
        {
            // Create director and builders
            Director director = new Director();

            Builder b1 = new FootmanBuilder();
            Builder b2 = new MageBuilder();

            // Construct two products
            director.Construct(b1);
            Unit p1 = b1.GetUnit();
            p1.Show();

            director.Construct(b2);
            Unit p2 = b2.GetUnit();
            p2.Show();

            // Wait for user
            Console.Read();
        }
    }

    // "Director"
    class Director
    {
        // Builder uses a complex series of steps
        public void Construct(Builder builder)
        {
            builder.InitUnitType();
            builder.InitCharacteristics();
            builder.InitAttack();
            builder.InitAbilities();
        }
    }

    // "Builder"
    abstract class Builder
    {
        public virtual void InitUnitType() { }
        public virtual void InitCharacteristics() { }
        public virtual void InitAttack() { }
        public virtual void InitAbilities() { }
        public abstract Unit GetUnit();
    }

    // "ConcreteBuilder1"
    class FootmanBuilder : Builder
    {
        private readonly Unit unit = new Unit();

        public override void InitUnitType()
        {
            unit.Name = "footman";
        }

        public override void InitCharacteristics()
        {
            unit.InitCharacteristics(
                health: 400,
                mana: 0,
                armor: 5,
                cost: 200,
                food: 3);
        }

        public override void InitAttack()
        {
            unit.AddAbility("attack");
            unit.Damage = 15;
        }

        public override void InitAbilities()
        {
            unit.AddAbility("shield bash");
        }

        public override Unit GetUnit()
        {
            return unit;
        }
       
    }

    // "ConcreteBuilder2"
    class MageBuilder : Builder
    {
        private readonly Unit unit = new Unit();

        public override void InitUnitType()
        {
            unit.Name = "mage";
        }

        public override void InitCharacteristics()
        {
            unit.InitCharacteristics(
                health: 200,
                mana: 200,
                armor: 2,
                cost: 250,
                food: 3);
        }

        public override void InitAttack()
        {
            unit.AddAbility("attack");
            unit.Damage = 10;
        }

        public override void InitAbilities()
        {
            unit.AddAbility("fireball");
            unit.AddAbility("firestorm");
        }

        public override Unit GetUnit()
        {
            return unit;
        }

    }

    // "Product"
    class Unit
    {
        private readonly List<string> abilities = new List<string>();
        private int maxHealth,
            maxMana,
            damage,
            armor,
            cost,
            food;
        private int currentHealth, currentMana;
        private string name;

        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int MaxMana { get => maxMana; set => maxMana = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Armor { get => armor; set => armor = value; }
        public int Cost { get => cost; set => cost = value; }
        public int Food { get => food; set => food = value; }
        public string Name { get => name; set => name = value; }
        public int Health { get => currentHealth; set => currentHealth = value; }
        public int Mana { get => currentMana; set => currentMana = value; }

        public void InitCharacteristics(int health, int mana, int armor, int cost, int food)
        {
            MaxHealth = health;
            Health = health;
            MaxMana = mana;
            Mana = mana;
            Armor = armor;
            Cost = cost;
            Food = food;
        }

        public void AddAbility(string ability)
        {
            abilities.Add(ability);
        }

        public void Show()
        {
            Console.WriteLine("\nUnit type: " + name);
            Console.WriteLine("Health: {0}/{1}", Health, MaxHealth);
            Console.WriteLine("Mana: {0}/{1}", Mana, MaxMana);
            Console.WriteLine("Abilities:");
            foreach (string ability in abilities)
                Console.WriteLine(ability);
        }

        public Unit()
        {
        }
    }
}