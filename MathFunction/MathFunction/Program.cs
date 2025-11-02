using System;
using System.Collections.Generic;

namespace MathFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<MatFunkce> funkce = new List<MatFunkce>();
            funkce.Add(new LinearniFce(2, 3));
            funkce.Add(new LinearniAbsolutHodnota(3, 4));
            funkce.Add(new LinearniLomena(4, 5, 1, 8));
            funkce.Add(new KvadratickaRce(1, -2, 1));

            double x = 2;

            foreach (var f in funkce)
            {
                f.VypsatInfo();
                Console.WriteLine($"f({x}) = {f.Vypocitej(x)}");
                if (f is LzeDerivovat)
                {
                    Console.WriteLine(((LzeDerivovat)f).Derivace());
                }
                if (f is LzeInverzovat)
                {
                    Console.WriteLine(((LzeInverzovat)f).Inverze());
                }
                Console.WriteLine();
            }
        }

        struct Interval
        {
            public double HorniMez { get; }
            public double DolniMez { get; }
            public char DolniZavorka { get; }
            public char HorniZavorka { get; }

            public Interval(char dz, double dm, double hm, char hz)
            {
                HorniMez = hm;
                DolniMez = dm;
                DolniZavorka = dz;
                HorniZavorka = hz;
            }

            // přidáno, aby šel použít prázdný konstruktor
            public override string ToString()
            {
                return $"{DolniZavorka}{DolniMez}; {HorniMez}{HorniZavorka}";
            }
        }

        public interface LzeDerivovat
        {
            string Derivace();
        }

        public interface LzeInverzovat
        {
            string Inverze();
        }

        abstract class MatFunkce
        {
            public string Nazev { get; }
            public string Rovnice { get; }
            public List<Interval> DefObor { get; protected set; }
            public List<Interval> OborHod { get; protected set; }

            public MatFunkce(string nazev, string rovnice)
            {
                Nazev = nazev;
                Rovnice = rovnice;
                DefObor = new List<Interval>();
                OborHod = new List<Interval>();
            }

            public abstract double Vypocitej(double x);

            public virtual void VypsatInfo()
            {
                Console.WriteLine($"{Nazev}: {Rovnice}");
            }
        }

        class LinearniFce : MatFunkce, LzeDerivovat, LzeInverzovat
        {
            private double a;
            private double b;
            

            public LinearniFce(double a, double b) : base("Lineární funkce", $"f(x) = {a}x + {b}")
            {
                this.a = a;
                this.b = b;
                Interval interval = new Interval('(', double.NegativeInfinity, double.PositiveInfinity, ')');
                DefObor.Add(interval);
                OborHod.Add(interval);
            }

            public override double Vypocitej(double x)
            {
                return a * x + b;
            }

            public string Derivace()
            {
                
                return $"f'(x) = {a}";
            }

            public string Inverze()
            {
                if (a == 0) return "Nelze inverzovat (a = 0)";
                return $"f⁻¹(x) = (x - {b}) / {a}";
            }
        }

        class LinearniAbsolutHodnota : MatFunkce, LzeDerivovat
        {
            private double a;
            private double b;

            public LinearniAbsolutHodnota(double a, double b) : base("Lineární funkce s absolutní hodnotou", $"f(x) = |{a}x + {b}|")
            {
                this.a = a;
                this.b = b;
                Interval interval = new Interval('(', double.NegativeInfinity, double.PositiveInfinity, ')');
                DefObor.Add(interval);
                Interval oborHodnot = new Interval('[', 0, double.PositiveInfinity, ')');
                OborHod.Add(oborHodnot);
            }

            public override double Vypocitej(double x)
            {
                return Math.Abs(a * x + b);
            }

            public string Derivace()
            {
                return $"f'(x) = {a} pro (ax + b) > 0, {(-a)} pro (ax + b) < 0";
            }
        }

        class LinearniLomena : MatFunkce, LzeDerivovat, LzeInverzovat
        {
            private double a;
            private double b;
            private double c;
            private double d;

            public LinearniLomena(double a, double b, double c, double d)
                : base("Lineární lomená funkce", $"f(x) = ({a}x + {b}) / ({c}x + {d})")
            {
                this.a = a;
                this.b = b;
                this.c = c;
                this.d = d;

                double podminka = (-d) / c;
                Interval prvni = new Interval('(', double.NegativeInfinity, podminka, ')');
                Interval druhy = new Interval('(', podminka, double.PositiveInfinity, ')');
                DefObor.Add(prvni);
                DefObor.Add(druhy);

                Interval interval = new Interval('(', double.NegativeInfinity, double.PositiveInfinity, ')');
                OborHod.Add(interval);
            }

            public override void VypsatInfo()
            {
                base.VypsatInfo();
                Console.WriteLine("Popis: Funkce má hyperbolický průběh.");
            }
            public override double Vypocitej(double x)
            {
                if (x == ((-d) / c) )
                {
                    Console.WriteLine("Mimo definiční obor!");
                    return double.NaN;
                }
                return (a * x + b) / (c * x + d);
            }

            public string Derivace()
            {
                return $"f'(x) = ({a}*({c}x + {d}) - ({a}x + {b})*{c}) / ({c}x + {d})²";
            }

            public string Inverze()
            {
                return $"f⁻¹(x) = (-{d}x + {b}) / ({c}x - {a})";
            }
        }

        class KvadratickaRce : MatFunkce, LzeDerivovat
        {
            private double a;
            private double b;
            private double c;

            public KvadratickaRce(double a, double b, double c)
                : base("Kvadratická funkce", $"f(x) = {a}x² + {b}x + {c}")
            {
                this.a = a;
                this.b = b;
                this.c = c;
                Interval interval = new Interval('(', double.NegativeInfinity, double.PositiveInfinity, ')');
                DefObor.Add(interval);

                double vrcholY = c - (b * b) / (4 * a);
                Interval obor = a > 0
                    ? new Interval('[', vrcholY, double.PositiveInfinity, ')')
                    : new Interval('(', double.NegativeInfinity, vrcholY, ']');
                OborHod.Add(obor);
            }

            public override void VypsatInfo()
            {
                base.VypsatInfo();
                Console.WriteLine("Popis: Funkce má parabolický průběh.");
            }

            public override double Vypocitej(double x)
            {
                return a * x * x + b * x + c;
            }

            public string Derivace()
            {
                return $"f'(x) = {2 * a}x + {b}";
            }
        }
    }
}
