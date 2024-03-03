namespace Portfolio.API.Authorization
{
    //Skip authorization if the action method is decorated with[AllowAnonymous]

    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}
