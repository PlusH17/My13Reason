using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATTTLE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Character> armada01 = new List<Character>();
            List<Character> armada02 = new List<Character>();

            for (int i = 0; i < 11; i++)
            {
                Random rand01 = new Random();
                int countWizard = rand01.Next(1, 3);
                int countWarri = rand01.Next(2, 5);
                int countArch = 10 - countWizard - countWarri;

                for (int wiz = 0; wiz < countWizard; wiz++)
                {
                    
                }
            }
            

        }
    }

    public class Character
    {
        public string Name;
        public float Health;
        public int Power;

        public void Attack(Character target, List<Wizard> list)
        {
            BATTLESCREAM();
            target.TakeDamage(Power);
            if (list.Contains(target))
            {
                Health -= Power / 2;
            }

        }

        public virtual void BATTLESCREAM()
        {
            Console.WriteLine("GRRRRRRRRRRRRRRRRRRRRRRR");
        }

        public virtual void TakeDamage(int amount)
        {
            Health -= amount;

        }

        public bool IsAlive()
        {
            if (Health <= 0)
                return false;
            return true;
        }

        public string GetInfo()
        {
            string info = null;
            var className = GetType().Name;

            info += $"{className} {Name} ({Health}/{Power}";
            return className;
        }
    }

    public class Wizard : Character
    {
        public float Health = 6;
        public int Power = 6;
        public override void BATTLESCREAM()
        {
            Console.WriteLine("YOU SHALL NOT PASS");
        }


    }

    public class Warrior : Character
    {
        public float Health = 4;
        public int Power = 8;
        public override void BATTLESCREAM()
        {
            Console.WriteLine("Hello, my name is Inigo Montoya. You killed my father. Prepare to die.");
        }


    }

    public class Archer : Character
    {
        public float Health = 8;
        public int Power = 2;
        public override void BATTLESCREAM()
        {
            Console.WriteLine("Fire is catching! And if we burn, you burn with us!");
        }


    }
}
