namespace TwitchCoreAPI.Core.Attribute
{
    public class Command : System.Attribute
    {
        public string NameCommand = "";
        public Command(string name)
        {
            NameCommand = name;
        }
    }
}
