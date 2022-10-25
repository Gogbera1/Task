namespace Common.Enums
{
    public enum ResultCodeEnum
    {
        Code200Success = 0,
        Code500InternalServerError = 1,
        Code204NoContent = 2,
        Code400BadRequest = 3,
        Code404NotFound = 4,
        Code404UserNotFound = 5,
        Code401Unauthorized = 6,
        Code400UserAlreadyExist = 7,
        Code400RoleAlreadyExist = 8,
    }
}
