using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionshipAppConsole.ScoreTracking
{
    public interface IEndTracker : IChangeTracker
    {
        void NotifyEnd();
    }
}
