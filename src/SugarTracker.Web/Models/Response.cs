﻿namespace SugarTracker.Web.Models
{
    public class Response
    {
      public Response()
      {
        
      }

      public Response(string message)
      {
        Message = message;
      }
      public string Message { get; set; }
    }
}
