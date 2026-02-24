namespace ianco99.ToolBox.Pool
{
    public interface IResettable
    {
        public void Assign(params object[] parameters);
        public void Reset();
    }
}