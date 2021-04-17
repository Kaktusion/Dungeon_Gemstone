using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGemstone
{
    class Options
    {
        enum DifficulityLevel
        {
            easy = 0,
            normal,
            hard,
            madness
        }
        enum AutoSaveSlotNumber { 
            First = 1,
            Second,
            Thrid
        }
        enum DuelType { 
            Regular=0,
            GroupFight
        }
    }
}
