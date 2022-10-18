namespace ContactsHub.Extensions;

using Data.Model;
using Microsoft.EntityFrameworkCore;

public static class ShortCodeExtensions
{
    public static async Task<string> GetUnusedCode(this DbSet<Code> codes, int length)
    {
        do
        {
            var newShortCode = Base62Code.GetBase62(length);
            var count = await codes.CountAsync(x => x.Short == newShortCode);
            if (count == 0)
            {
                return newShortCode;
            }
        } while (true);
    }
}