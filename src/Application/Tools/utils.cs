using CleanArchitecture.Application.Common.Exceptions;

namespace CleanArchitecture.Application.Tools;

public static class Utils
{
    public static void CheckIsIdsAreSame(string id1, string id2)
    {
        if (id1 != id2)
        {
            throw new BadRequestException("route 'id' and request body 'id' not same");
        }
    }
}