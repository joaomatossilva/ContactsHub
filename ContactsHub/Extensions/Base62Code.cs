namespace ContactsHub.Extensions;

using System.Text;

public class Base62Code
{
    private static readonly char[] Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
            .ToCharArray();

    private static readonly Random Random = new();

    public static string GetBase62(int length)
    {
        var sb = new StringBuilder(length);

        for (int i=0; i<length; i++) 
            sb.Append(Base62Chars[Random.Next(62)]);

        return sb.ToString();
    }       

    public static string GetBase36(int length) 
    {
        var sb = new StringBuilder(length);

        for (int i=0; i<length; i++) 
            sb.Append(Base62Chars[Random.Next(36)]);

        return sb.ToString();
    }
}