namespace ianco99.ToolBox.Pool
{
    public interface IResetteable
    {
        public void Assign(params object[] parameters);
        public void Reset();
    }
}