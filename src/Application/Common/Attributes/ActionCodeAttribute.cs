namespace Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ActionCodeAttribute : Attribute
    {
        public string? ActionCode { get; }

        public ActionCodeAttribute(string? actionCode)
        {
            ActionCode = actionCode;
        }
    }
}
