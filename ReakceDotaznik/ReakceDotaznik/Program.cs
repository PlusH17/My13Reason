using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReakceDotaznik
{
    internal class Program
    {
        static void Main(string[] args)
        {


            int[] Moznosti = { 20, 20, 20, 20 }; // Možnosti pro každou odpověď
            string[] MoznostiNazvy = { "monster", "had", "když ne twillight", "měla byste být vděčná za nás" };


            Console.WriteLine("Drahá paní profesorko (nebo kdokoliv koho jsem otravovala z odzkoušením tohoto kódu)");
            Console.WriteLine("Zde je naše skvělé řešení naší neschopné komunikace: ✨DOTAZNÍK✨ ");
            Console.WriteLine("Odpovídejte co nejvíce upřímně a za výsledek se zlobte POUZE sama na sebe");
            Console.WriteLine("Pokud uvidíte gramatickou chybu, ne, neviděla");
            Console.WriteLine("A prosím nekoukejte na ten kód... ale věřte mi, že 20 bodů si zaslouží...");
            Console.WriteLine("Anyaway, užívejte");

            Console.ReadLine();
            Console.Clear();


            // 1. Otázka - Poměr humoru a slz
            Console.WriteLine("Správný poměr humoru a slz?");
            Console.WriteLine("a) Potřebuju slzy, pokud na konci nemám rozmazanou řasenku, nebyl to dobrý film.");
            Console.WriteLine("b) Pláč, pláč, smích a zase pláč. Perfektní kombinace.");
            Console.WriteLine("c) Rovnováha je klíč.");
            Console.WriteLine("d) Humor je základ, slza může ukápnout, ale radši slzy smíchu.");
            Console.WriteLine("e) Potřebuju se popadat za břicho a při vzpomínce na ten film se usměju i při programování řetízku přátelství.");
            Console.WriteLine("Zadejte odpovědi (např. a, b, c). Pro více odpovědí je oddělte čárkami:");
            string odpoved = Console.ReadLine();


            string[] odpovedi = odpoved.Split(',');
            foreach (string pismeno in odpovedi)
            {
                switch (pismeno.Trim().ToLower())
                {
                    case "a": Moznosti[0] += 1; break;
                    case "b": Moznosti[0] += 1; break;
                    case "c": Moznosti[2] += 1; break;
                    case "d": Moznosti[3] += 1; break;
                    case "e": Moznosti[1] += 1; break;
                    default: Console.WriteLine($"Neplatná odpověď: {pismeno}. Zadejte pouze a, b, c, d nebo e."); break;
                }
            }

            Console.Clear();
            // 2. Otázka - Postava "on top"
            Console.WriteLine("Jaká postava je ta „on top“?");
            Console.WriteLine("a) Jakákoliv, doslova i Pavel se zajímá víc o jeho oblečení než já o hlavní postavu.");
            Console.WriteLine("b) Zvládnu hodně, ale aspoň trochu, abych s ní dokázala existovat.");
            Console.WriteLine("c) Potřebuju perfektní, bezchybnou hlavní postavu, ať se od ní při reakci aspoň něco naučím.");
            Console.WriteLine("d) Budu fanoušek number one, jen ať dělá chyby, aspoň se do ní vcítím.");
            Console.WriteLine("Zadejte odpovědi (např. a, b, c). Pro více odpovědí je oddělte čárkami:");
            string odpovedpostava = Console.ReadLine();

            string[] odpovedpostavas = odpovedpostava.Split(',');
            foreach (string pismeno in odpovedpostavas)
            {
                switch (pismeno.Trim().ToLower())
                {
                    case "a": break;
                    case "b": Moznosti[3] -= 1; break;
                    case "c": Moznosti[2] += 1; break;
                    case "d": Moznosti[3] += 1; Moznosti[0] += 1; break;
                    default: Console.WriteLine($"Neplatná odpověď: {pismeno}. Zadejte pouze a, b, c, d nebo e."); break;
                }
            }

            Console.Clear();
            // 3. Otázka - Brain rot humor
            Console.WriteLine("Brain rot humor aka udržení pozornosti");
            Console.WriteLine("a) Jsem narozený po 2010, potřebuju brain rot.");
            Console.WriteLine("b) Prosím, ne.");
            Console.WriteLine("c) Mám mladší sourozence, kteří mě nakazili…");
            Console.WriteLine("Zadejte odpovědi (např. a, b, c). Pro více odpovědí je oddělte čárkami:");
            string odpovedbrainrot = Console.ReadLine();

            string[] odpovedbrainrot01 = odpovedbrainrot.Split(',');
            foreach (string pismeno in odpovedbrainrot01)
            {
                switch (pismeno.Trim().ToLower())
                {
                    case "a": Moznosti[3] += 1; break;
                    case "b": Moznosti[1] += 1; break;
                    case "c": Moznosti[1] -= 1; break;
                    default: Console.WriteLine($"Neplatná odpověď: {pismeno}. Zadejte pouze a, b, c, d nebo e."); break;
                }
            }

            Console.Clear();
            // 4. Otázka - Muzikály
            Console.WriteLine("Nzr na muzikály?");
            Console.WriteLine("a) Love it");
            Console.WriteLine("b) Nevadí, nebaví");
            Console.WriteLine("c) Nesnáším");
            Console.WriteLine("Zadejte odpovědi (např. a, b, c). Pro více odpovědí je oddělte čárkami:");
            string odpovedmuzi = Console.ReadLine();

            string[] odpovedmuzi01 = odpovedmuzi.Split(',');
            foreach (string pismeno in odpovedmuzi01)
            {
                switch (pismeno.Trim().ToLower())
                {
                    case "a": Moznosti[0] += 2; break;
                    case "b": break;
                    case "c": Moznosti[0] -= 1; break;
                    default: Console.WriteLine($"Neplatná odpověď: {pismeno}. Zadejte pouze a, b, c, d nebo e."); break;
                }
            }

            Console.Clear();
            // 5. Otázka - Romantika
            Console.WriteLine("Romantika?");
            Console.WriteLine("a) Bez romantické linky to není ono.");
            Console.WriteLine("b) Mám dostatečný romantický život, nemám potřebu si ho nahrazovat filmem.");
            Console.WriteLine("c) POTŘEBUJU ROMANTIKU, FILMY JSOU JEDINÉ, PROČ JEŠTĚ NEMÁM SRDCE Z KAMENE.");
            Console.WriteLine("d) Potřebuju si zvýšit standardy.");
            Console.WriteLine("e) Ještě jedna romantika a jdu se zabít.");
            Console.WriteLine("Zadejte odpovědi (např. a, b, c). Pro více odpovědí je oddělte čárkami:");
            string odpovedrom = Console.ReadLine();

            string[] odpovedrom01 = odpovedrom.Split(',');
            foreach (string pismeno in odpovedrom01)
            {
                switch (pismeno.Trim().ToLower())
                {
                    case "a": Moznosti[1] -= 1; break;
                    case "b": Moznosti[1] += 1; break;
                    case "c": Moznosti[0] += 1; Moznosti[2] += 1; Moznosti[3] += 1; break;
                    case "d": Moznosti[0] += 1; Moznosti[2] += 1; break;
                    case "e": Moznosti[1] += 1; break;
                    default: Console.WriteLine($"Neplatná odpověď: {pismeno}. Zadejte pouze a, b, c, d nebo e."); break;
                }
            }

            Console.Clear();
            // 6. Otázka - Krize
            Console.WriteLine("Existenciální krize");
            Console.WriteLine("a) Nemám stabilitu v životě a mám to tak rád/a, potřebuju film, co mě nechá speechless.");
            Console.WriteLine("b) Nemám stabilitu v životě a nesnáším to, čím víc předvídatelný děj, tím lépe.");
            Console.WriteLine("c) Trocha plot twistu je dobrá.");
            Console.WriteLine("Zadejte odpovědi (např. a, b, c). Pro více odpovědí je oddělte čárkami:");
            string odpovedexis = Console.ReadLine();

            string[] odpovedexis01 = odpovedexis.Split(',');
            foreach (string pismeno in odpovedexis01)
            {
                switch (pismeno.Trim().ToLower())
                {
                    case "a": Moznosti[0] += 1; break;
                    case "b": Moznosti[1] += 1; Moznosti[2] += 1; break;
                    case "c": Moznosti[1] -= 1; break;
                    default: Console.WriteLine($"Neplatná odpověď: {pismeno}. Zadejte pouze a, b, c, d nebo e."); break;
                }
            }

            Console.Clear();
            // 7. Otázka - Cringe
            Console.WriteLine("Cringe");
            Console.WriteLine("a) Cringe nedávám, umřu");
            Console.WriteLine("b) Pokud užívání cringe je forma sebepoškozování, tak mi říkejte pravý dopplerak (velmi si užívám cringe)");
            Console.WriteLine("c) Trochu cringe přežiju (přeci jen zvládám konverzace na programování)");
            Console.WriteLine("Zadejte odpovědi (např. a, b, c, d). Pro více odpovědí je oddělte čárkami:");
            string odpovedecringe = Console.ReadLine();

            string[] odpovedecringe01 = odpovedecringe.Split(',');
            foreach (string pismeno in odpovedecringe01)
            {
                switch (pismeno.Trim().ToLower())
                {
                    case "a": Moznosti[1] += 1; break;
                    case "b": Moznosti[2] += 1; Moznosti[3] += 1; break;
                    case "c": Moznosti[0] += 1; break;
                    default: Console.WriteLine($"Neplatná odpověď: {pismeno}. Zadejte pouze a, b, c, d nebo e."); break;
                }
            }

            Console.Clear();

            Console.WriteLine("Otázky od Pavla:");
            Console.WriteLine("(Ano, skutečně potřeboval kredit)");
            Console.WriteLine("(Ano, obě kategorie jsou na P, jaká to náhoda...)");
            Console.ReadLine();

            Console.Clear();
            // 8. Otázka - Preference
            Console.WriteLine("Co preferujete:");
            Console.WriteLine("a) Film se spletitým dějem, jehož závěr nebudete znát do poslední chvíle.");
            Console.WriteLine("b) Nepředvídatelný děj je fajn, ale pokud se vám ten film bude opravdu líbit, měla by tam být možnost si ho užít znovu.");
            Console.WriteLine("c) Na složitém ději si zas tak nepotrpíte, hlavně že se bavíte.");
            Console.WriteLine("d) S každým novým zhlédnutím si film užíváte stále víc.");
            Console.WriteLine("Zadejte odpovědi (např. a, b, c). Pro více odpovědí je oddělte čárkami:");
            string odpovedpreference = Console.ReadLine();

            string[] odpovedpreference01 = odpovedpreference.Split(',');
            foreach (string pismeno in odpovedpreference01)
            {
                switch (pismeno.Trim().ToLower())
                {
                    case "a": Moznosti[0] += 1; break;
                    case "b": Moznosti[0] += 1; break;
                    case "c": Moznosti[1] += 1; Moznosti[3] += 1; Moznosti[2] += 1; break;
                    case "d": break;
                    default: Console.WriteLine($"Neplatná odpověď: {pismeno}. Zadejte pouze a, b, c, d nebo e."); break;
                }
            }

            Console.Clear();
            // 9. Otázka - Popularita
            Console.WriteLine("Co takhle popularita platí spíš:");
            Console.WriteLine("a) Je pro vás absolutně nutné, aby byl film populární, mohla jste dělat cosplaye hlavních postav, lidé vás díky tomu na ulici poznávali a mohla jste se s kýmkoliv v tramvaji o tomhle filmu bavit.");
            Console.WriteLine("b) Když některý z vašich kamarádů film zná, je to fajn, ale když ne, tak jim ho alespoň můžete ukázat.");
            Console.WriteLine("c) Stačí vám úzká fanbase, která má tenhle film, ale zas o to víc ráda.");
            Console.WriteLine("d) Chcete být jediná, kdo tenhle film zná. Kdo by také koukal na \"If Night of the Day of the Dawn of the Son of the Bride of the Return of the Revenge of the Terror of the Attack of the Evil, Mutant, Hellbound, Flesh-Eating Creatures of the Damned\"?");
            Console.WriteLine("Zadejte odpovědi (např. a, b, c). Pro více odpovědí je oddělte čárkami:");
            string odpovedpop = Console.ReadLine();

            string[] odpovedpop01 = odpovedpop.Split(',');
            foreach (string pismeno in odpovedpop01)
            {
                switch (pismeno.Trim().ToLower())
                {
                    case "a": Moznosti[2] += 1; break;
                    case "b": Moznosti[0] += 1; break;
                    case "c": Moznosti[1] += 1; Moznosti[3] += 1; break;
                    case "d": break;
                    default: Console.WriteLine($"Neplatná odpověď: {pismeno}. Zadejte pouze a, b, c, d nebo e."); break;
                }
            }

            Console.Clear();

            int maxIndex = 0;
            int maxValue = Moznosti[0];
            for (int i = 1; i < Moznosti.Length; i++)
            {
                if (Moznosti[i] > maxValue)
                {
                    maxValue = Moznosti[i];
                    maxIndex = i;
                }
            }

            // Vypiš odpovídající název
            Console.WriteLine($"Nejvíce odpovědí odpovídá (a tím pádem film na reakci na reakci) : {MoznostiNazvy[maxIndex]}.");
            Console.WriteLine("Ano, vy nevíte co to je, ale my ano, F");
            Console.ReadLine();
        }
    }
}
