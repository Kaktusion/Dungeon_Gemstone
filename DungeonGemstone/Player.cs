using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace DungeonGemstone
{
    class Player
    {
        //Zmienne
        public string playerName { get; set; }
        public int healthPoints { get; set; }
        public int manaPoints { get; set; }
        public int strength { get; set; }
        public int dextirity { get; set; }
        public int inteligence { get; set; }
        public int defence { get; set; }
        public int charisma { get; set; }
        public int level { get; set; }
        public int actualXp { get; set; }
        public int expModifier { get; set; }
        public int ExpRequiredToLevelUp { get; set; }
        public int money { get; set;}
        public int livesleft { get; set; }
        public int physicalDamage { get; set; }
        public int Protection { get; set; }
        public int criticalChance { get; set; }
        public float criticalModifier { get; set; }
        public float ManaCost { get; set; }
        //metody
        public void objectOverwrite(Player p1)
        {
            playerName = p1.playerName;
            healthPoints = p1.healthPoints;
            manaPoints = p1.manaPoints;
            strength = p1.strength;
            dextirity = p1.dextirity;
            inteligence = p1.inteligence;
            defence = p1.defence;
            charisma = p1.charisma;
            level = p1.level;
            actualXp = p1.actualXp;
            expModifier = p1.expModifier;
            ExpRequiredToLevelUp = p1.ExpRequiredToLevelUp;
            money = p1.money;
            livesleft = p1.livesleft;
            physicalDamage = p1.physicalDamage;
            Protection = p1.Protection;
            criticalChance = p1.criticalChance;
            criticalModifier = p1.criticalModifier;
        }
        public void AddItemStats(Item item)
        {
            physicalDamage += item.physicalDamage;
            Protection += item.Protection;
            criticalChance += item.criticalChance;
            criticalModifier += item.criticalModifier;
            livesleft += item.moreLives;
        }
        public void levelup(Entity enemyDefeated)
        {
            int expNeededToLevelUp = level * 100+100;
            actualXp += Convert.ToInt32(enemyDefeated.expGiven * (Convert.ToDouble(expModifier)/100));
            if (actualXp > expNeededToLevelUp)
            {
                level++;
                int xpup = actualXp - expNeededToLevelUp;
                actualXp = 0 + xpup;
                LevelUpMenu();
            }
        }
        public void levelup(int expgiven)
        {
            int expNeededToLevelUp = level * 100+100;
            actualXp += Convert.ToInt32(expgiven * expModifier);
            if (actualXp > expNeededToLevelUp)
            {
                level++;
                int xpup = actualXp - expNeededToLevelUp;
                actualXp = 0 + xpup;
                LevelUpMenu();
            }
        }
        public void getLevel()
        {
            Console.Clear();
            Console.WriteLine("Your level: {0}\nYour xp: {1}\nXp required to level up: {2}",level,actualXp,level*ExpRequiredToLevelUp);
        }
        public void save(Player p,int lp)
        {
            string path = @".\PlayerSaves\PlayerData";
            path += lp+".json";
            Console.WriteLine("Zapis...");
            File.Create(path).Close();
            string PlayerToJson = JsonConvert.SerializeObject(p);
            using (StreamWriter sw = new StreamWriter(path))
            {
                try
                {
                    sw.Write(PlayerToJson);
                    Console.WriteLine("Zapisano!");
                    Console.Read();
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
        public void Save(Player p, int lp)
        {
            string path = @".\PlayerSaves\PlayerData";
            path += lp + ".json";
            File.Create(path).Close();
            string PlayerToJson = JsonConvert.SerializeObject(p);
            using (StreamWriter sw = new StreamWriter(path))
            {
                try
                {
                    sw.Write(PlayerToJson);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
        public void load(int lp)
        {
            string data;
            string path = @".\PlayerSaves\PlayerData";
            path += lp + ".json";
            Console.WriteLine("Wczytywanie...");
            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    data  = sr.ReadLine();
                    Player p2 = JsonConvert.DeserializeObject<Player>(data);
                    objectOverwrite(p2);
                    Console.WriteLine("Postac wczytana!");
                    Console.Read();
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
        public void Load(int lp)
        {
            string data;
            string path = @".\PlayerSaves\PlayerData";
            path += lp + ".json";
            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    data = sr.ReadLine();
                    Player p2 = JsonConvert.DeserializeObject<Player>(data);
                    objectOverwrite(p2);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
        public void SaveMenu(Player p)
        {
            bool IsSaved = false;
            while (!IsSaved)
            {
                Player p1 = new Player();
                p1.Load(1);
                Player p2 = new Player();
                p2.Load(2);
                Player p3 = new Player();
                p3.Load(3);
                Console.WriteLine("Wybierz slot zapisu\n1.{0}\n2.{1}\n3.{2}", p1.playerName, p2.playerName, p3.playerName);
                string o = Console.ReadLine();
                switch (o)
                {
                    case "1":
                        p.save(p, 1);
                        IsSaved = true;
                        break;
                    case "2":
                        p.save(p, 2);
                        IsSaved = true;
                        break;
                    case "3":
                        p.save(p, 3);
                        IsSaved = true;
                        break;
                    default:
                        Console.Clear();
                        break;

                }
            }
        }
        public void LoadMenu(Player p)
        {
            bool IsSaved = false;
            while (!IsSaved)
            {
                Player p1 = new Player();
                p1.Load(1);
                Player p2 = new Player();
                p2.Load(2);
                Player p3 = new Player();
                p3.Load(3);
                Console.WriteLine("Wybierz slot z którego chcesz wczytać:\n1.{0}\n2.{1}\n3.{2}", p1.playerName, p2.playerName, p3.playerName);
                string o = Console.ReadLine();
                switch (o)
                {
                    case "1":
                        p.objectOverwrite(p1);
                        IsSaved = true;
                        break;
                    case "2":
                        p.objectOverwrite(p2);
                        IsSaved = true;
                        break;
                    case "3":
                        p.objectOverwrite(p3);
                        IsSaved = true;
                        break;
                    default:
                        Console.Clear();
                        break;

                }
            }
        }
        public void LevelUpMenu()
        {
            bool choiceDone = false;
            while (!choiceDone) {
                getLevel();
                Console.WriteLine("Gratulacje, Dostałeś nowy poziom!\n" +
                    "Teraz Wybierz profesję:\n" +
                    "1. Wojownik (+5 ataku, +5 do rozrzutu)\n" +
                    "2. Łotr (+5 szansy na kryta, +0.1 do mnożnika krytycznego)\n" +
                    "3. Czarodziej (-5% kosztu zaklęć[max. 50% kosztru], +5 mocy zaklęć)\n" +
                    "4. Krzyżowiec (+5 do obrony, +2 do rozrzutu )\n" +
                    "5. Walet (+0.1 do mnożnika krytycznego, +5 do mocy zaklęć, +5 do rozrzutu)");
                string o = Console.ReadLine();
                switch (o) {
                    case "1":
                        physicalDamage += 5;
                        strength += 5;
                        choiceDone = true;
                        break;
                    case "2":
                        criticalChance += 5;
                        criticalModifier += 0.1f;
                        choiceDone = true;
                        break;
                    case "3":
                        ManaCost -= 0.05f;
                        inteligence += 5;
                        choiceDone = true;
                        break;
                    case "4":
                        defence += 5;
                        strength += 2;
                        choiceDone = true;
                        break;
                    case "5":
                        criticalModifier += 0.1f;
                        strength += 5;
                        inteligence += 5;
                        choiceDone = true;
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }

        }
    }
}
