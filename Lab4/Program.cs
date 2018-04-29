using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicUnitConstructor director = new BasicUnitConstructor();
            Builder footmanBuilder = new FootmanBuilder();
            Builder dragonBuilder = new DragonBuilder();

            director.Construct(footmanBuilder);
            UnitImplStrategy footman = footmanBuilder.GetUnit();
            footman.Show();
           

            director.Construct(dragonBuilder);
            UnitImplStrategy dragon = dragonBuilder.GetUnit();
            dragon.Show();

            Console.WriteLine();
            footman.Move();
            dragon.Move();
            Console.WriteLine("Footman gets magic item : boots of speed");
            footman.SetMoveStrategy(new FastGroundUnit());
            footman.Move();
        }
    }

    public class BasicUnitConstructor
    {
        public void Construct(Builder builder)
        {
            builder.InitUnitType();
            builder.InitCharacteristics();
            builder.InitAttack();
            builder.InitAbilities();
            builder.InitMoveStrategy();
        }
    }

    public abstract class Builder
    {
        protected readonly UnitImplStrategy _unit = new UnitImplStrategy();

        public abstract void InitUnitType();
        public abstract void InitCharacteristics();
        public abstract void InitAttack();
        public abstract void InitAbilities();
        public abstract void InitMoveStrategy();
        public abstract UnitImplStrategy GetUnit();
    }

    public class FootmanBuilder : Builder
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

        public override void InitMoveStrategy()
        {
            _unit.MoveStrategy = new GroundUnit();
        }

        public override UnitImplStrategy GetUnit()
        {
            return _unit;
        }
    }

    public class DragonBuilder : Builder
    {
        public override void InitUnitType()
        {
            _unit.Name = "dragon";
        }

        public override void InitCharacteristics()
        {
            _unit.InitCharacteristics(
                health: 1400,
                mana: 400,
                armor: 12,
                cost: 700,
                food: 7);
        }

        public override void InitAttack()
        {
            _unit.AddAbility("attack");
            _unit.Damage = 65;
        }

        public override void InitAbilities()
        {
            _unit.AddAbility("fireball");
        }

        public override void InitMoveStrategy()
        {
            _unit.MoveStrategy = new FlyingUnit();
        }

        public override UnitImplStrategy GetUnit()
        {
            return _unit;
        }

    }

    public interface IMoveStrategy
    {
        void Move(UnitImplStrategy unit);
    }

    public class FastGroundUnit : IMoveStrategy
    {
        public void Move(UnitImplStrategy unit)
        {
            Console.WriteLine($"{unit.Name} walks faster");
        }
    }

    public class GroundUnit : IMoveStrategy
    {
        public void Move(UnitImplStrategy unit)
        {
            Console.WriteLine($"{unit.Name} walks");
        }
    }

    public class FlyingUnit : IMoveStrategy
    {
        public void Move(UnitImplStrategy unit)
        {
            Console.WriteLine($"{unit.Name} flies");
        }
    }

    public class UnitImplStrategy
    {
        public IMoveStrategy MoveStrategy { get; set; }

        public void SetMoveStrategy(IMoveStrategy moveStrategy)
        {
            MoveStrategy = moveStrategy;
        }

        public void Move()
        {
            MoveStrategy.Move(this);
        }

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

    }
}
