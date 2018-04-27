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
            BasicUnitConstructor director = new BasicUnitConstructor();

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
    class BasicUnitConstructor
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
        protected readonly Unit _unit = new Unit();

        public virtual void InitUnitType() { }
        public virtual void InitCharacteristics() { }
        public virtual void InitAttack() { }
        public virtual void InitAbilities() { }
        public abstract Unit GetUnit();
    }

    // "ConcreteBuilder1"
    class FootmanBuilder : Builder
    {
        public override void InitUnitType()
        {
            _unit.Name = "footman";
        }

        public override void InitCharacteristics()
        {
            _unit.InitCharacteristics(
                health: 400,
                mana: 0,
                armor: 5,
                cost: 200,
                food: 3);
        }

        public override void InitAttack()
        {
            _unit.AddAbility("attack");
            _unit.Damage = 15;
        }

        public override void InitAbilities()
        {
            _unit.AddAbility("shield bash");
        }

        public override Unit GetUnit()
        {
            return _unit;
        }
       
    }

    // "ConcreteBuilder2"
    class MageBuilder : Builder
    {
        public override void InitUnitType()
        {
            _unit.Name = "mage";
        }

        public override void InitCharacteristics()
        {
            _unit.InitCharacteristics(
                health: 200,
                mana: 200,
                armor: 2,
                cost: 250,
                food: 3);
        }

        public override void InitAttack()
        {
            _unit.AddAbility("attack");
            _unit.Damage = 10;
        }

        public override void InitAbilities()
        {
            _unit.AddAbility("fireball");
            _unit.AddAbility("firestorm");
        }

        public override Unit GetUnit()
        {
            return _unit;
        }

    }

    // "Product"
    class Unit
    {
        private readonly List<string> _abilities = new List<string>();

        public int MaxHealth { get; private set; }
        public int MaxMana { get; private set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Cost { get; set; }
        public int Food { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }

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
            _abilities.Add(ability);
        }

        public void Show()
        {
            Console.WriteLine("\nUnit type: " + Name);
            Console.WriteLine("Health: {0}/{1}", Health, MaxHealth);
            Console.WriteLine("Mana: {0}/{1}", Mana, MaxMana);
            Console.WriteLine("Abilities:");
            foreach (string ability in _abilities)
                Console.WriteLine(ability);
        }

        public Unit()
        {
        }
    }
}