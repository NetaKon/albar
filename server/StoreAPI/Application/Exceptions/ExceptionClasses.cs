namespace StoreAPI.Application.Exceptions;

public class NotFoundException(string message) : Exception(message);
public class ValidationException(string message) : Exception(message);
public class UserAlreadyExistsException(string message) : Exception(message);
public class UnauthorizedAccessException(string message) : Exception(message);
