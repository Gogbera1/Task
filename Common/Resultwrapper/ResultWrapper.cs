using Common.Enums;

namespace Common.Resultwrapper
{
    public class ResultWrapper
    {
        public ResultCodeEnum Status { get; set; }

        public static ResultWrapper New(ResultCodeEnum status)
            => new() { Status = status };
    }

    public class ResultWrapper<T> : ResultWrapper
    {
        public T Value { get; set; }

        public static ResultWrapper<T> New(ResultCodeEnum status, T data) =>
            new()
            {
                Status = status,
                Value = data
            };
    }
}
