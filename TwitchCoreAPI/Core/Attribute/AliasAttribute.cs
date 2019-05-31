using System;

namespace TwitchCoreAPI.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AliasAttribute : System.Attribute
    {
        public string NameAlias = "";

        public AliasAttribute(string name)
        {
            NameAlias = name;
        }
    }
}
