using System;
using System.Linq;
using System.Reflection;

namespace Reactor;

[AttributeUsage(AttributeTargets.Class)]
public class ReactorModFlagsAttribute : Attribute
{
    public ModFlags Flags { get; }

    public ReactorModFlagsAttribute(ModFlags flags)
    {
        Flags = flags;
    }

    public static ModFlags GetModFlags(Type type)
    {
        var attribute = type.GetCustomAttribute<ReactorModFlagsAttribute>();
        if (attribute != null)
        {
            return attribute.Flags;
        }
        
        var metadataAttribute = type.Assembly.GetCustomAttributes<AssemblyMetadataAttribute>().SingleOrDefault(x => x.Key == "Reactor.ModFlags");
        if (metadataAttribute != null && metadataAttribute.Value != null)
        {
            return Enum.Parse<ModFlags>(metadataAttribute.Value);
        }

        return ModFlags.None;
    }
}