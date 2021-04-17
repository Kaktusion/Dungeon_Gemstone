using System;
using Newtonsoft.Json;
using System.IO;
//SIEMA ELO NAURA Z FARTEM
namespace DungeonGemstone
{
    class Program
    {           
        
        static void Main()
        {
            int LiczbaFal = 0;
            bool Defeat = false;
            void Attack(Player player, int DmgScatter, Entity e)
                {
                   Random NextAttack = new Random();
                    int DamageDealt = e.attackDmg - player.defence + NextAttack.Next(DmgScatter)+1;
                    int CritDmgDealt = (e.attackDmg + (NextAttack.Next(DmgScatter) + 1)) * 2 - player.defence;
                    int attackOption = NextAttack.Next(10);
                    switch (attackOption)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            if (DamageDealt > 1)
                            {
                                player.healthPoints -= DamageDealt;
                                Console.WriteLine("{0} zadał ci {1} obrażeń", e.name,DamageDealt);
                                Console.ReadKey();
                            }
                            else
                            {
                                player.healthPoints--;
                                Console.WriteLine("{0} zadał ci {1} obrażeń", e.name, 1);
                                Console.ReadKey();
                            }
                            break;
                        case 7:
                        case 8:
                        Console.WriteLine("potwór zbiera siłę do ataku");
                            e.attackDmg++;
                            Console.ReadKey();
                            break;
                        case 9:
                            player.healthPoints -=CritDmgDealt;
                            Console.WriteLine("{0} zadał ci {1} obrażeń", e.name,CritDmgDealt);
                            Console.ReadKey();
                            break;
                    }
                }
            void Combat(Player p, Entity e)
            {
                                bool enemydefeated = false;
                                Random r = new Random();
                                Entity en = new Entity(e);
                                en.EnemyScaling(p.level);
                                while ((en.hp > 0 || en.Xtralives > 0) && p.healthPoints > 0)
                                {
                                    Console.WriteLine("zaatakował cię {0}, ma on {1} hp", en.name, en.hp);
                                    Console.WriteLine("masz {0} hp, {1} many", p.healthPoints, p.manaPoints);
                                    Console.WriteLine("Co zrobić?\n" +
                                        "(A)tak\n" +
                                        "(O)brona\n" +
                                        "(L)eczenie\n");
                                    string a = Console.ReadLine().ToUpper();
                                    Console.Clear();
                                    switch (a)
                                    {
                                        case "A":
                                            if (r.Next(100) + 1 > 100 - Convert.ToInt32(p.criticalChance + p.dextirity * 0.5))
                                            {
                                                en.hp -= Convert.ToInt32((p.physicalDamage + r.Next(p.strength) + 1) * (p.criticalModifier + p.dextirity / 100));
                                                Console.WriteLine("Zadałeś {0} dmg", Convert.ToInt32(p.physicalDamage * (p.criticalModifier + p.dextirity / 100)));
                                                Console.Read();
                                            }
                                            else
                                            {
                                                if (p.physicalDamage - en.PhisicalResistance < 1)
                                                {
                                                    en.hp -= 1;
                                                    Console.WriteLine("Zadałeś 1 dmg");
                                                    Console.Read();
                                                }
                                                else
                                                {
                                                    en.hp -= p.physicalDamage - en.PhisicalResistance + r.Next(p.strength) + 1;
                                                    Console.WriteLine("Zadałeś {0} dmg", p.physicalDamage - en.PhisicalResistance + r.Next(p.strength) + 1);
                                                    Console.Read();
                                                }
                                            }
                                            if (en.hp > 0)
                                            {
                                                Attack(p, Convert.ToInt32(e.hp * 0.1), en);
                                            }
                                            else
                                            {
                                                enemydefeated = true;
                                            }
                                            break;
                                        case "O":
                                            if (p.manaPoints >= 5)
                                            {
                                                p.defence = Convert.ToInt32(p.defence * (1 + p.inteligence / 10));
                                                p.manaPoints -= Convert.ToInt32(5 * p.ManaCost);
                                                Console.WriteLine("Przechodzisz do pozycji obronnej");
                                                Attack(p, Convert.ToInt32(e.hp * 0.1), en);
                                                Console.WriteLine("Wychodzisz z pozycji obronnej");
                                                Console.Read();
                                                p.defence = Convert.ToInt32(p.defence / (1 + p.inteligence / 10));
                                            }
                                            else
                                            {
                                                Console.WriteLine("Za mało many");
                                                Console.Read();
                                                continue;
                                            }
                                            break;
                                        case "L":
                                            if (p.manaPoints >= 10)
                                            {
                                                p.healthPoints += 2 * p.inteligence;
                                                p.manaPoints -= Convert.ToInt32(10 * p.ManaCost);
                                                Console.WriteLine("Leczysz się {0} hp", 2 * p.inteligence);
                                                Attack(p, Convert.ToInt32(e.hp * 0.1), en);
                                                Console.Read();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Za mało many");
                                                Console.Read();
                                                continue;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (p.healthPoints > 0 && enemydefeated == true)
                                {
                    p.healthPoints += 25 * en.Xtralives + 1;
                                    p.manaPoints += 25;
                                    Console.WriteLine("Gratulacje, zdobywasz {0} monet i {1}xp!", Convert.ToInt32(e.hp * 0.25 + r.Next(Convert.ToInt32(e.hp * 0.25))) * en.Xtralives + 1, Convert.ToInt32(e.expGiven * (
                                        e.hp / 10)) * en.Xtralives + 1);
                                    p.levelup(Convert.ToInt32(e.expGiven * (e.hp / 10)) * en.Xtralives + 1);
                                    p.money += Convert.ToInt32(e.hp * 0.25 + r.Next(Convert.ToInt32(e.hp * 0.25)) * en.Xtralives + 1);
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Niestety przegrałeś");
                                    Defeat = true;
                                }
            }
            {
                Directory.CreateDirectory(@".\PlayerSaves");
                if (!File.Exists(@".\PlayerSaves\PlayerData1.json"))
                {
                    File.Create(@".\PlayerSaves\PlayerData1.json");
                    Reset(1);
                }
                if (!File.Exists(@".\PlayerSaves\PlayerData2.json"))
                {
                    File.Create(@".\PlayerSaves\PlayerData2.json");
                    Reset(2);
                }
                if (!File.Exists(@".\PlayerSaves\PlayerData3.json"))
                {
                    File.Create(@".\PlayerSaves\PlayerData3.json");
                    Reset(3);
                }
            }
            void reset()
            {
                Console.Clear();
                Console.WriteLine("Czy napewno chcerz zresetować? T/N");
                char[] s = Console.ReadLine().ToUpper().ToCharArray();
                if (s[0] == 'T')
                {
                    Player reset = new Player();
                    reset.Save(reset, 1);
                    reset.Save(reset, 2);
                    reset.Save(reset, 3);
                }
                Console.Clear();
            }
            void Reset(int slotnumber)
            {
                Player reset = new Player();
                reset.Save(reset, slotnumber);
            }

            bool game = true;
            while (game)
            {

                
                

                Random r = new Random();
                Shop sh = new Shop();
                Player player = new Player()
                {
                    playerName = "",
                    healthPoints = 100,
                    manaPoints = 100,
                    strength = 10,
                    dextirity = 10,
                    inteligence = 10,
                    charisma = 10,
                    defence = 10,
                    level = 0,
                    actualXp = 0,
                    expModifier = 1,
                    ExpRequiredToLevelUp = 100,
                    livesleft = 0,
                    physicalDamage = 10,
                    Protection = 0,
                    criticalChance = 10,
                    criticalModifier = 1.1f,
                    money = 0,
                    ManaCost = 1f
                };

                Entity[] EnemyTable = {
            new Entity("Goblin",50,4,5,0),
            new Entity("Wilk",70,14,5,0),
            new Entity("Szczur",30,3,2,0),
            new Entity("Ork",150,12,7,0),
            new Entity("Wąż",30,15,7,0),
            new Entity("Ryś",100,14,8,0),
            new Entity("Hobgoblin",125,13,6,0),
            new Entity("Ghul",50,11,8,0),
            new Entity("Ogr",125,12,7,0),
            new Entity("Wielki Admin",100,10,10,0),
            new Entity("Kubi",75,12,14,0),
            new Entity("Barbarzyńca",200,7,10,0)
            };

                bool i = true;
                while (i)
                {
                    Console.WriteLine("Witaj w lochu przybyszu!\n" +
                        "co byś chciał zrobić?\n"+
                        "(N)owa gra - zaczynasz od zera\n"+
                        "(K)ontynuuj - wczytujesz postać, jeśli nie posiadasz postaci do wczytania zaczynasz od zera!\n" +
                        "(R)eset - resetuje zapisane dane\n" +
                        "(O)puść - opuszcza grę");

                    string choice = Console.ReadLine().ToUpper();
                    switch (choice)
                    {
                        case "N":
                            Console.WriteLine("Nazwij swoją postać: ");
                            player.playerName = Console.ReadLine();
                            player.SaveMenu(player);
                            i = false;
                            Console.Clear();
                            break;
                        case "K":
                            i = false;
                            player.LoadMenu(player);
                            Console.Clear();
                            break;
                        case "O":
                            goto End;
                        case "R":
                            reset();
                            break;
                        default:
                            Console.Clear();
                            break;

                    }

                }
                for (; !Defeat;)
                {
                    for (int p = 0; p < 5 && !Defeat; p++)
                    {
                        Combat(player, EnemyTable[r.Next(EnemyTable.Length)]);
                        LiczbaFal++;
                    }
                    player.SaveMenu(player);
                }



                Console.Clear();
                Console.WriteLine("Gratulacje, Pokonałeś {0} fal!          ps. dotarłeś do easter egga igiego napisaned by igi z fartem mordo", LiczbaFal);
                Console.WriteLine("Powodzenia następnym razem!");
                Console.ReadLine();
                Console.Clear();
            }
        End:
            Console.WriteLine("Powodzenia następnym razem!");
            Console.ReadKey();
        }
    }
}



