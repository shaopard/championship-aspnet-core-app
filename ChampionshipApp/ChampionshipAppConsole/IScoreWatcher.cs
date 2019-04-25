using ChampionshipAppConsole.ParentChildScoring;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionshipAppConsole
{
    interface IScoreWatcher
    {
        void Notify(Score score);
    }
}
