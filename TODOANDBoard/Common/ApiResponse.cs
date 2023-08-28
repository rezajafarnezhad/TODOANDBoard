namespace TODOANDBoard.Common;

public class ApiResponse
{
    public bool Flag { get; set; }
    public string Message { get; set; }
}


public class ApiResponse<T> : ApiResponse
{
    public T Data { get; set; }
}