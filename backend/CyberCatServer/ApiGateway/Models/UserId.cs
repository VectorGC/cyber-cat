using MongoDB.Bson;

namespace ApiGateway.Models;

public readonly struct UserId : IEquatable<UserId>
{
    private readonly ObjectId _id;

    public UserId(ObjectId id)
    {
        _id = id;
    }

    public static implicit operator ObjectId(UserId userId)
    {
        return userId._id;
    }

    public override string ToString()
    {
        return _id.ToString();
    }

    public bool Equals(UserId other)
    {
        return _id.Equals(other._id);
    }

    public override bool Equals(object? obj)
    {
        return obj is UserId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }

    public static bool operator ==(UserId left, UserId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(UserId left, UserId right)
    {
        return !(left == right);
    }
}