using System;
using System.Text;

namespace Lab5
{
    internal class Program
    {
        private static void Main()
        {
            Tower t1 = new GuardTower();
            Console.WriteLine(t1);
            Console.WriteLine();

            Tower t2 = new GuardTower();
            t2 = new HigherDamageTower(t2);
            Console.WriteLine(t2);
            Console.WriteLine();

            t2 = new HigherRangeTower(t2);
            Console.WriteLine(t2);
            Console.WriteLine();

            Tower t3 = new CannonTower();
            t3 = new HigherDamageTower(t3);
            t3 = new HigherRangeTower(t3);
            Console.WriteLine(t3);
            Console.ReadLine();
        }
    }

    internal abstract class Tower
    {
        protected Tower(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public abstract int Range { get; }
        public abstract int Damage { get; }

        public override string ToString()
        {
            var res = new StringBuilder();
            res.Append($"Название: {Name}\n");
            res.Append($"Урон: {Damage} | ");
            res.Append($"Радиус поражения: {Range}");
            return res.ToString();
        }
    }

    internal class GuardTower : Tower
    {
        public GuardTower() : base("Стрелковая башня") { }

        public override int Range => 150;
        public override int Damage => 15;
    }

    internal class CannonTower : Tower
    {
        public CannonTower() : base("Орудийная башня") { }

        public override int Range => 200;
        public override int Damage => 35;
    }

    internal abstract class TowerUpgrade : Tower
    {
        protected Tower Tower { get; }

        protected TowerUpgrade(string n, Tower tower) : base(n)
        {
            Tower = tower;
        }
    }

    internal class HigherDamageTower : TowerUpgrade
    {
        public HigherDamageTower(Tower p)
            : base(p.Name + ", увеличенный урон", p) { }

        public override int Range => Tower.Range;
        public override int Damage => Tower.Damage + 5;
    }

    internal class HigherRangeTower : TowerUpgrade
    {
        public HigherRangeTower(Tower p)
            : base(p.Name + ", увеличенный радиус", p) { }

        public override int Range => Tower.Range + 50;
        public override int Damage => Tower.Damage;
    }
}