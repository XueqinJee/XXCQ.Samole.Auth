namespace AuthWebServer.Config {
    public class Result {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public Result() { }
        public Result(int code, string message, object data) {
            Code = code;
            Message = message;
            Data = data;
        }

        public static Result Success(object data) {
            return new Result(200, "Success", data);
        }

        public static Result Success() {
            return new Result(200, "成功", null);
        }

        public static Result Success(string message) {
            return new Result(200, message, null);
        }

        public static Result Error(string message) {
            return new Result(500, message, null);
        }

        public static Result Error() {
            return new Result(500, "", null);
        }
    }
}
