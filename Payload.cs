namespace ToDoCqrs
{
    public class Payload
    {
        public int StatusCode { get; private set; }

        public string StatusDescription { get; private set; }

        public object Data { get; private set; }

        public Payload(int statusCode, string statusDescription, object data = null)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            Data = data;
        }

    }
}