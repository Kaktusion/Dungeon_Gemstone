using System;
using System.Collections.Generic;
using System.Text.Json;

namespace DungeonGemstone
{
    class Entity
    {
        public string name { get; set; }
        public int hp{ get; set; }
        public int attackDmg{ get; set; }
        public int PhisicalResistance{ get; set; }
        public int Xtralives{ get; set; }
        public float expGiven = 10f;
        public Entity(string name,int hp, int dmg, int resistance, int lives)
        {
            this.name = name;
            this.hp = hp;
            this.attackDmg = dmg;
            this.PhisicalResistance = resistance;
            this.Xtralives = lives;
        }
        public Entity(Entity x)
        {
            this.name = x.name;
            this.hp = x.hp;
            this.attackDmg = x.attackDmg;
            this.PhisicalResistance = x.PhisicalResistance;
            this.Xtralives = x.Xtralives;
            this.expGiven = x.expGiven;
        }
        public Entity(string name, int hp, int dmg, int resistance, int lives, float exp)
        {
            this.name = name;
            this.hp = hp;
            this.attackDmg = dmg;
            this.PhisicalResistance = resistance;
            this.Xtralives = lives;
            this.expGiven = exp;
        }
        //Methods
        public void EnemyScaling(int level)
        {
            level++;
            int IncreasedPackSize = 10;
            name = name + " "+ level+"lvl";
            hp += Convert.ToInt32(hp*level*0.2);
            attackDmg += Convert.ToInt32(attackDmg * level * 0.1);
            PhisicalResistance += Convert.ToInt32(PhisicalResistance * level * 0.07);
            if(IncreasedPackSize == level)
            {
                Xtralives++;
                IncreasedPackSize += 10;
            }
               

        }
        
    }
}

