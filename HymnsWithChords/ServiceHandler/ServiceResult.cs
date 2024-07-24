namespace HymnsWithChords.ServiceHandler
{
    public class ServiceResult<T>
    {
        public T Data { get; set; }
        public Exception Error { get; set; }

        public int StatusCode => GetStatusCode();

        private int GetStatusCode()
        {
			if (Error is NotFoundException) return 404;
			if (Error is BadRequestException) return 400;
			if (Error is ConflictException) return 409;
			return 500;
		}

        public bool IsSuccess => Error == null;

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T> { Data = data };           
        }

        public static ServiceResult<T> Failure(Exception error)
        {
            return new ServiceResult<T> { Error = error };
        }
        
    }
}
