using System;

[Serializable]
public struct CommandRequest
{
    public CommandType Type;
    public CommandStatus Status;
    public object Args;

    public bool Equals(CommandRequest other)
    {
        return Type == other.Type && Equals(Args, other.Args);
    }

    public override bool Equals(object obj)
    {
        return obj is CommandRequest other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Type, Args);
    }
}