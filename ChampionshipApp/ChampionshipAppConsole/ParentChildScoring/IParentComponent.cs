namespace ChampionshipAppConsole.ParentChildScoring
{
    interface IParentComponent<T> where T : Point
    {
        void AddChild(Score<T> componentScore);
    }
}
